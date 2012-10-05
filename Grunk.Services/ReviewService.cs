using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grunk.Domain.Repositories;
using Grunk.Domain.Services;
using Grunk.Domain;
using System.Web;
using System.IO;
using Grunk.Domain.Exceptions;

namespace Grunk.Services
{
    public class ReviewService : IReviewService
    {
        private IReviewRepository ReviewRepository;
        private IReviewLinkRepository ReviewLinkRepository;
        private IPictureService PictureService;
        private IPictureRepository PictureRepository;

        public ReviewService(IReviewRepository reviewRepository, IPictureService pictureService, IPictureRepository pictureRepository, IReviewLinkRepository reviewLinkRepository)
        {
            this.ReviewRepository = reviewRepository;
            this.PictureService = pictureService;
            this.PictureRepository = pictureRepository;
            this.ReviewLinkRepository = reviewLinkRepository;
        }

        public void Create(int profileId, string description, string title, string url, HttpPostedFileBase picture)
        {
            Review review = ReviewRepository.Create(profileId, description, title, url, DateTime.Now);

            UploadResizeAndSave(review.ReviewId, picture);
        }

        public void Update(int id, string title, string description, string url)
        {
            ReviewRepository.Update(id, title, description);

            if (string.IsNullOrWhiteSpace(url))
            {
                ReviewLinkRepository.Delete(id);
            }
            else
            {
                ReviewLinkRepository.CreateOrUpdate(id, url);
            }
        }

        public void ChangePicture(int id, HttpPostedFileBase picture)
        {
            var review = GetById(id);

            DeletePictures(review);

            UploadResizeAndSave(id, picture);
        }

        public void Approve(int reviewId)
        {
            ReviewRepository.ChangeState(reviewId, ReviewState.Approved);
        }

        public void Reject(int reviewId)
        {
            ReviewRepository.ChangeState(reviewId, ReviewState.NotApproved);
        }

        public void Delete(int reviewId)
        {
            var review = GetById(reviewId);

            DeletePictures(review);

            ReviewLinkRepository.Delete(reviewId);
            ReviewRepository.Delete(reviewId);
        }

        public void DeleteByProfileId(int profileId)
        {
            var tempReviews = ReviewRepository.GetByProfileId(profileId);

            foreach (var review in tempReviews)
            {
                Delete(review.ReviewId);
            }
        }

        public IEnumerable<Review> GetAllReviews()
        {
            return ReviewRepository.GetAllReviews();
        }

        public IEnumerable<Review> GetWaitingReviews()
        {
            return ReviewRepository.GetWaitingReviews();
        }

        public IEnumerable<Review> GetRejectedReviews()
        {
            return ReviewRepository.GetRejectedReviews();
        }

        public IEnumerable<Review> GetApprovedReviews()
        {
            return ReviewRepository.GetApprovedReviews();
        }

        public IEnumerable<Review> GetPagedApprovedReviews(Func<IQueryable<Review>, IOrderedQueryable<Review>> orderBy, int skip, int take)
        {
            return ReviewRepository.GetPagedApprovedReviews(orderBy, skip, take);
        }

        public Review GetById(int reviewId)
        {
            return ReviewRepository.GetById(reviewId);
        }

        public Review GetLastCreatedAndActivatedReview()
        {
            return ReviewRepository.GetLastCreatedAndActivatedReview();
        }

        public int CountApproved()
        {
            return ReviewRepository.Count(ReviewState.Approved);
        }

        #region Helpers
        private void UploadResizeAndSave(int reviewId, HttpPostedFileBase image)
        {
            string fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);

            image.SaveAs(HttpContext.Current.Server.MapPath("~/Content/Uploads/Temp/" + fileName));

            try
            {
                foreach (var size in PictureSizes.Reviews)
                {
                    string nPath, nFileName;
                    PictureService.ResizeAndSave("~/Content/Uploads/Temp/", fileName, size.Width, size.Height, out nPath, out nFileName);

                    ReviewRepository.AddPicture(reviewId, nPath, nFileName, size.Width, size.Height);
                }
            }
            finally
            {
                File.Delete(HttpContext.Current.Server.MapPath("~/Content/Uploads/Temp/" + fileName));
            }
        }

        private void DeletePictures(Review review)
        {
            var tempPictures = review.Pictures.Select(x => new { Id = x.PictureId, Path = x.Path, FileName = x.FileName }).ToList();
            foreach (var pic in tempPictures)
            {
                PictureService.Delete(pic.Path, pic.FileName);
                PictureRepository.Delete(pic.Id);
            }
        }
        #endregion
    }
}
