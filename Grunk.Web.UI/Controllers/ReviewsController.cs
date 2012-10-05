using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Grunk.Web.UI.Models;
using Grunk.Domain.Services;
using Grunk.Web.Controllers;

namespace Grunk.Web.UI.Controllers
{
    public class ReviewsController : BaseController
    {
        IReviewService ReviewService;
        IProfileService ProfileService;
        IStaticTextService StaticTextService;

        private const int PageSize = 5;

        public ReviewsController(IReviewService reviewService, IProfileService profileService, IStaticTextService staticTextService)
        {
            this.ReviewService = reviewService;
            this.ProfileService = profileService;
            this.StaticTextService = staticTextService;
        }

        //
        // GET: /Reviews/

        public ActionResult Index(int? page)
        {
            int currentPage = page.HasValue ? page.Value : 0;
            int numberOfReviews = ReviewService.CountApproved();
            int numberOfPages = Convert.ToInt32(Math.Ceiling((double)numberOfReviews / (double)PageSize));

            if (page > numberOfPages)
            {
                throw new ArgumentOutOfRangeException("page");
            }

            var reviews = ReviewService.GetPagedApprovedReviews(
                skip: PageSize * currentPage,
                take: PageSize,
                orderBy: x => x.OrderByDescending(y => y.CreationDateTime)
            );

            return View(new IndexReviewsViewModel
            {
                MenuKey = "reviews",
                Reviews = reviews,
                Paged = new PagedViewModel
                {
                    Pages = numberOfPages,
                    CurrentPage = currentPage,
                    Action = "Index",
                    Controller = "Reviews"
                }
            });
        }

        //
        // GET: /Reviews/Create

        [Authorize]
        public ActionResult Create()
        {
            return View(new CreateReviewsViewModel
            {
                MenuKey = "reviews"
            });
        }

        //
        // POST: /Reviews/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create(CreateReviewsViewModel requestedViewModel)
        {
            if (ModelState.IsValid)
            {
                var profile = ProfileService.GetById(User.Identity.AdminId);

                ReviewService.Create(profile.ProfileId, requestedViewModel.Description, requestedViewModel.Title, requestedViewModel.Url, requestedViewModel.Picture);
                ProfileService.AddActivity(profile.ProfileId, "CreatedAReview");
                return View("Created", new CreatedReviewsViewModel { MenuKey = "reviews" });
            }

            requestedViewModel.MenuKey = "reviews";
            return View(requestedViewModel);
        }

    }
}
