using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Grunk.Web.Controllers.Admin;
using Grunk.Domain.Services;
using Grunk.Web.UI.Areas.Administration.Models;
using Grunk.Web.Services;

namespace Grunk.Web.UI.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ShopController : BaseController
    {
        IProductService ProductService;
        public ShopController(IProductService productService)
        {
            ProductService = productService;
        }

        //
        // GET: /Administration/Shop/

        public ActionResult Index()
        {
            return View(new IndexShopViewModel
            {
                Albums = ProductService.GetAll()
            });
        }

        //
        // GET: /Administration/Shop/Create

        public ActionResult Create()
        {
            return View(new CreateShopViewModel
            {
                ProductForm = new ProductFormViewModel()
                {
                    PossibleGenres = ProductService.GetGenres().Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.GenreId.ToString()
                    })
                }
            });
        }

        //
        // POST: /Administration/Shop/Create
        [HttpPost]
        public ActionResult Create(CreateShopViewModel requestedViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var form = requestedViewModel.ProductForm;
                    ProductService.Create(form.AlbumName, form.ArtistName, form.GenreId, form.Price, requestedViewModel.Cover);

                    Messages.Add("Albummet er oprettet", "Albummet er blevet oprettet", MessageType.Success);
                }
                catch
                {
                    Messages.AddStandardCreateError();
                }
            }

            requestedViewModel.ProductForm.PossibleGenres = ProductService.GetGenres().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.GenreId.ToString()
            });

            return View(requestedViewModel);
        }

        //
        // GET: /Administration/Shop/Update

        public ActionResult Update(int id)
        {
            try
            {
                var album = ProductService.GetById(id);

                return View(new UpdateShopViewModel
                {
                    AlbumId = id,
                    ProductForm = new ProductFormViewModel()
                    {
                        AlbumName = album.Name,
                        ArtistName = album.Artist.Name,
                        GenreId = album.GenreId,
                        Price = album.Price,
                        PossibleGenres = ProductService.GetGenres().Select(x => new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.GenreId.ToString()
                        })
                    }
                });
            }
            catch
            {
                Messages.AddStandardRetriveError();
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Administration/Shop/Create
        [HttpPost]
        public ActionResult Update(int id, UpdateShopViewModel requestedViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var form = requestedViewModel.ProductForm;
                    ProductService.Update(id, form.AlbumName, form.ArtistName, form.GenreId, form.Price, requestedViewModel.Cover);

                    Messages.Add("Gemt!", "Albummet er blevet gemt", MessageType.Success);
                }
                catch
                {
                    Messages.AddStandardUpdateError();
                }
            }

            requestedViewModel.ProductForm.PossibleGenres = ProductService.GetGenres().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.GenreId.ToString()
            });

            requestedViewModel.AlbumId = id;

            return View(requestedViewModel);
        }

        //
        // GET: /Administration/Shop/Delete

        public ActionResult Delete(int id)
        {
            try
            {
                ProductService.DeleteById(id);
                Messages.Add("Album slettet", "Albummet er blevet slettet", MessageType.Success);
            }
            catch
            {
                Messages.AddStandardDeleteError();
            }
            return RedirectToAction("Index");
        }
    }
}
