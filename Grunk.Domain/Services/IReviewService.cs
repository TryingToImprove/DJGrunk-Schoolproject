using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Grunk.Domain.Services
{
    public interface IReviewService
    {
        void Create(int profileId, string description, string title, string url, HttpPostedFileBase picture);

        void Approve(int reviewId);
        void Reject(int reviewId);
        void Delete(int reviewId);

        void DeleteByProfileId(int profileId);

        IEnumerable<Review> GetAllReviews();
        IEnumerable<Review> GetRejectedReviews();
        IEnumerable<Review> GetWaitingReviews();
        IEnumerable<Review> GetApprovedReviews();
        IEnumerable<Review> GetPagedApprovedReviews(Func<IQueryable<Review>, IOrderedQueryable<Review>> orderBy, int skip, int take);

        Review GetById(int reviewId);
        Review GetLastCreatedAndActivatedReview();

        int CountApproved();

        void Update(int id, string title, string description, string url);

        void ChangePicture(int id, HttpPostedFileBase picture);
    }
}
