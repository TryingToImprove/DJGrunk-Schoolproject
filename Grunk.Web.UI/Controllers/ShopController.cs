using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Grunk.Web.Controllers;
using Grunk.Domain.Services;
using Grunk.Web.UI.Models;
using Grunk.Domain;
using Grunk.Domain.Exceptions;

namespace Grunk.Web.UI.Controllers
{
    public class ShopController : BaseController
    {
        private const int PageSize = 5;
        private IProductService ProductService;
        private IProfileService ProfileService;
        private IStaticTextService StaticTextService;

        public ShopController(IProductService productService, IProfileService profileService, IStaticTextService staticTextService)
        {
            this.ProductService = productService;
            this.ProfileService = profileService;
            this.StaticTextService = staticTextService;
        }

        //
        // GET: /Shop/

        public ActionResult Index(int? page, string sortBy)
        {
            int currentPage = page.HasValue ? page.Value : 0;
            int numberOfAlbums = ProductService.Count();
            int numberOfPages = Convert.ToInt32(Math.Ceiling((double)numberOfAlbums / (double)PageSize));

            if (page > numberOfPages)
            {
                throw new ArgumentOutOfRangeException("page");
            }

            #region Ordering
            Func<IQueryable<Album>, IOrderedQueryable<Album>> orderBy;

            switch (sortBy)
            {
                case "title":
                    orderBy = x => x.OrderBy(y => y.Name);
                    break;
                case "genre":
                    orderBy = x => x.OrderBy(y => y.Genre.Name);
                    break;
                case "newst":
                    orderBy = x => x.OrderByDescending(y => y.CreationDateTime);
                    break;
                default:  //Standard på artist navn 
                    orderBy = x => x.OrderBy(y => y.Artist.Name);
                    break;
            }
            #endregion

            var albums = ProductService.GetPaged(
                skip: PageSize * currentPage,
                take: PageSize,
                orderBy: orderBy
            );

            return View(new IndexShopViewModel
            {
                MenuKey = "shop",
                Albums = albums,
                Paged = new PagedViewModel
                {
                    Pages = numberOfPages,
                    CurrentPage = currentPage,
                    Action = "Index",
                    Controller = "Shop",
                    RouteData = new
                    {
                        sortBy = sortBy
                    }
                }
            });
        }

        //
        // GET: /Shop/Details/1

        public ActionResult Details(int id)
        {
            var album = ProductService.GetById(id);

            return View(new DetailsShopViewModel
            {
                MenuKey = "shop",
                Album = album
            });
        }

        //
        // GET: /Shop/Purchase/1231
        [Authorize]
        public ActionResult VerifyPurchase(int id)
        {
            var album = ProductService.GetById(id);

            return View(new VerifyPurchaseShopViewModel
            {
                MenuKey = "shop",
                Album = album
            });
        }

        //
        // POST: /Shop/Purchase/1231
        [Authorize]
        [HttpPost]
        public ActionResult Purchase(int id, VerifyPurchaseShopViewModel requestedViewModel)
        {
            var album = ProductService.GetById(id);

            try
            {
                ProfileService.BuyAlbum(User.Identity.AdminId, album.AlbumId, album.Price);

                TempData["Success"] = true;
                TempData["Message"] = album.Name + " er blevet købt";

                ProfileService.AddActivity(ProfileService.GetById(User.Identity.AdminId).ProfileId, "BoughtAAlbum");

                return RedirectToAction("Details", new { id = id });
            }
            catch (NotEnoughFundsException e)
            {
                TempData["Error"] = true;
                TempData["Message"] = string.Format("Du har ikke nok grunker på din konto. Du har {0}, men du skal bruge {1}", e.Funds, e.NeededFunds);

                return RedirectToAction("VerifyPurchase", new { id = id });
            }
            catch (AlreadyPurchasedException)
            {
                TempData["Error"] = true;
                TempData["Message"] = string.Format("Du har allerede købt {0} af {1}, så du kan ikke købe albummet igen", album.Name, album.Artist.Name);

                return RedirectToAction("VerifyPurchase", new { id = id });
            }
        }

        //
        // POST: /Shop/AjaxPurchase/1231
        [HttpPost]
        public ActionResult AjaxPurchase(int id)
        {
            if (User == null || User.Identity == null || !User.Identity.IsAuthenticated)
            {
                return Json(new
                {
                    Type = "Error",
                    Message = "Du skal være logget ind for at købe albummet. <br /> <a href=\"/Profile/Create\">Opret bruger</a> eller <a href=\"/Profile/Create\">log ind</a>"
                });
            }

            var album = ProductService.GetById(id);

            try
            {
                ProfileService.BuyAlbum(User.Identity.AdminId, album.AlbumId, album.Price);
                ProfileService.AddActivity(ProfileService.GetById(User.Identity.AdminId).ProfileId, "BoughtAAlbum");

                return Json(new
                {
                    Type = "Success",
                    Message = album.Name + " er blevet købt"
                });
            }
            catch (NotEnoughFundsException e)
            {
                return Json(new
                {
                    Type = "Error",
                    Message = string.Format("Du har ikke nok grunker på din konto. Du har {0}, men du skal bruge {1}", e.Funds, e.NeededFunds)
                });
            }
            catch (AlreadyPurchasedException)
            {
                return Json(new
                {
                    Type = "Error",
                    Message = string.Format("Du har allerede købt {0} af {1}, så du kan ikke købe albummet igen", album.Name, album.Artist.Name)
                });
            }
        }
    }
}
