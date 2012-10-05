using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Grunk.Web.Controllers.Admin;
using Grunk.Domain.Services;
using Grunk.Web.UI.Areas.Administration.Models;
using Grunk.Domain;

namespace Grunk.Web.UI.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReviewsController : BaseController
    {
        IReviewService ReviewService;

        public ReviewsController(IReviewService reviewService)
        {
            this.ReviewService = reviewService;
        }

        //
        // GET: /Administration/Reviews/

        public ActionResult Index()
        {
            var reviews = ReviewService.GetAllReviews()
                .OrderByDescending(x => x.CreationDateTime);

            return View(new IndexReviewsViewModel
            {
                Reviews = reviews
            });
        }

        //
        // GET: /Administration/Reviews/Waiting

        public ActionResult Waiting()
        {
            var reviews = ReviewService.GetWaitingReviews()
                .OrderByDescending(x => x.CreationDateTime);

            if (reviews.Count() == 0)
            {
                Messages.Add("Ingen anmeldelser", "Der er ikke nogle anmeldelser der venter på at blive godkendt", Services.MessageType.Info);
            }

            return View(new WaitingReviewsViewModel
            {
                Reviews = reviews
            });
        }

        //
        // GET: /Administration/Reviews/Rejected

        public ActionResult Rejected()
        {
            var reviews = ReviewService.GetRejectedReviews()
                .OrderByDescending(x => x.CreationDateTime);

            if (reviews.Count() == 0)
            {
                Messages.Add("Ingen anmeldelser", "Der er ikke nogle anmeldelser der er blevet afvist", Services.MessageType.Info);
            }
            
            return View(new RejectedReviewsViewModel
            {
                Reviews = reviews
            });
        }

        //
        // POST: /Administration/Reviews/ChangeState

        [HttpPost]
        public ActionResult ChangeState(int id, short state)
        {
            try
            {
                ReviewState reviewState;
                if (!Enum.TryParse(state.ToString(), out reviewState) && reviewState != ReviewState.WaitingForApproval)
                {
                    throw new ArgumentOutOfRangeException("ReviewState " + state + " does is not supported");
                }

                switch (reviewState)
                {
                    case ReviewState.Approved:
                        ReviewService.Approve(id);
                        break;
                    case ReviewState.NotApproved:
                        ReviewService.Reject(id);
                        break;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                Messages.AddStandardSavingError();
            }

            return RedirectToAction("Index");
        }

        //
        // POST: /Administration/Reviews/Delete

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                ReviewService.Delete(id);

                Messages.Add("Anmeldelse er slettet", "Anmeldelsen er blevet slettet!", Services.MessageType.Success);

                return RedirectToAction("Index");
            }
            catch
            {
                Messages.AddStandardDeleteError();

                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Administration/Reviews/Update/12

        public ActionResult Update(int id)
        {
            try
            {
                var review = ReviewService.GetById(id);

                return View(new UpdateReviewsViewModel
                {
                    Description = review.Description,
                    Title = review.Title,
                    Url = (review.ReviewLink != null) ? review.ReviewLink.Url : string.Empty
                });
            }
            catch
            {
                Messages.AddStandardRetriveError();
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Administration/Reviews/Update/12

        [HttpPost]
        public ActionResult Update(int id, UpdateReviewsViewModel requestedViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ReviewService.Update(id, requestedViewModel.Title, requestedViewModel.Description, requestedViewModel.Url);

                    if (requestedViewModel.Picture != null)
                    {
                        ReviewService.ChangePicture(id, requestedViewModel.Picture);
                    }

                    Messages.Add("Gemt!", "Anmeldelsen er blvet gemt", Services.MessageType.Success);

                    return RedirectToAction("Update", new { id = id });
                }
                catch
                {
                    Messages.AddStandardUpdateError();

                    return RedirectToAction("Update", new { id = id });
                }
            }

            return View(requestedViewModel);
        }
    }
}
