using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Grunk.Domain.Services;
using Grunk.Web.Controllers;
using Grunk.Web.UI.Models;

namespace Grunk.Web.UI.Controllers
{
    public class UtilityController : BaseController
    {
        private IProductService ProductService;
        private IProfileService ProfileService;
        private IReviewService ReviewService;

        public UtilityController(IProductService productService, IProfileService profileService, IReviewService reviewService)
        {
            this.ProductService = productService;
            this.ProfileService = profileService;
            this.ReviewService = reviewService;
        }

        public ActionResult LoginRedirecter(string returnUrl)
        {
            string[] urlParts = returnUrl.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            if (urlParts.Length > 0 && urlParts[0].Equals("administration", StringComparison.InvariantCultureIgnoreCase))
            {
                return RedirectToAction("Login", "Home", new { area = "Administration", returnUrl = returnUrl });
            }
            else
            {
                return RedirectToAction("Create", "Profile", new { returnUrl = returnUrl });
            }
        }

        [ChildActionOnly]
        public ActionResult TopList()
        {
            var albums = ProductService.GetTopList(DateTime.Now.AddMonths(-1));

            return PartialView(albums);
        }

        [ChildActionOnly]
        public ActionResult LoginContainer()
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                var profile = ProfileService.GetById(User.Identity.AdminId);

                return PartialView("_OnlineLoginContainer", new OnlineLoginContainerViewModel()
                {
                    Profile = profile
                });
            }
            else
            {
                return PartialView("_OfflineLoginContainer", new LoginFormViewModel());
            }
        }

        [ChildActionOnly]
        public ActionResult NewestReview()
        {
            return PartialView(ReviewService.GetLastCreatedAndActivatedReview());
        }
    }
}
