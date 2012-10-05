using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grunk.Domain.Repositories
{
    public interface IReviewRepository : IBaseRepository
    {
        void AddPicture(int id, string path, string fileName, int width, int height);
        void ChangeState(int id, ReviewState reviewState);
        void Delete(int id);

        IEnumerable<Review> GetAllReviews();
        IEnumerable<Review> GetApprovedReviews();
        IEnumerable<Review> GetByProfileId(int profileId);
        IEnumerable<Review> GetWaitingReviews();
        IEnumerable<Review> GetPagedApprovedReviews(Func<IQueryable<Review>, IOrderedQueryable<Review>> orderBy, int skip, int take);

        Review Create(int profileId, string description, string title, string url, DateTime creationDateTime);

        Review GetById(int id);
        Review GetLastCreatedAndActivatedReview();

        int Count(ReviewState reviewState);

        IEnumerable<Review> GetRejectedReviews();

        void Update(int id, string title, string description);
    }
}
