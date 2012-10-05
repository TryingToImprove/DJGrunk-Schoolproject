using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grunk.Domain.Repositories;
using Grunk.Domain;

namespace Grunk.DAL
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(Entities entities)
            : base(entities)
        {
        }

        public Review Create(int profileId, string description, string title, string url, DateTime creationDateTime)
        {
            Review review = new Review
            {
                ProfileId = profileId,
                Description = description,
                CreationDateTime = creationDateTime,
                Title = title,
                State = (short)ReviewState.WaitingForApproval,
                ReviewLink = (!string.IsNullOrWhiteSpace(url)) ? new ReviewLink { Url = url } : null
            };

            entities.AddObject(review);

            SaveChanges();

            return review;
        }

        public void Update(int id, string title, string description)
        {
            Review review = GetById(id);
            review.Title = title;
            review.Description = description;

            SaveChanges();
        }

        public void AddPicture(int id, string path, string fileName, int width, int height)
        {
            GetById(id).Pictures.Add(new Picture
            {
                FileName = fileName,
                Path = path,
                Width = width,
                Height = height,
                UploadedDateTime = DateTime.Now
            });

            SaveChanges();
        }

        public void ChangeState(int id, ReviewState reviewState)
        {
            GetById(id).State = (short)reviewState;
            SaveChanges();
        }

        public void Delete(int id)
        {
            var review = GetById(id);

            entities.DeleteObject(review);
            SaveChanges();
        }

        public IEnumerable<Review> GetAllReviews()
        {
            return entities.ToList();
        }

        public IEnumerable<Review> GetByProfileId(int profileId)
        {
            return entities.Where(x => x.ProfileId == profileId).ToList();
        }

        public IEnumerable<Review> GetWaitingReviews()
        {
            return entities.AsQueryable()
                .WhereStateReviewIs(ReviewState.WaitingForApproval)
                .ToList();
        }

        public IEnumerable<Review> GetApprovedReviews()
        {
            return entities.AsQueryable()
                .WhereStateReviewIs(ReviewState.Approved)
                .ToList();
        }

        public IEnumerable<Review> GetRejectedReviews()
        {
            return entities.AsQueryable()
                .WhereStateReviewIs(ReviewState.NotApproved)
                .ToList();
        }

        public Review GetLastCreatedAndActivatedReview()
        {
            return entities.AsQueryable()
                .WhereStateReviewIs(ReviewState.Approved)
                .OrderByDescending(x => x.CreationDateTime)
                .FirstOrDefault();
        }

        public Review GetById(int id)
        {
            return entities.Single(x => x.ReviewId == id);
        }

        public IEnumerable<Review> GetPagedApprovedReviews(Func<IQueryable<Review>, IOrderedQueryable<Review>> orderBy, int skip, int take)
        {
            return orderBy(entities.AsQueryable()
                    .WhereStateReviewIs(ReviewState.Approved))
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public int Count(ReviewState reviewState)
        {
            return entities.AsQueryable().WhereStateReviewIs(reviewState).Count();
        }
    }

    public static class ReviewExtensions
    {
        public static IQueryable<Review> WhereStateReviewIs(this IQueryable<Review> query, ReviewState reviewState)
        {
            return query.Where(x => x.State == (short)reviewState);
        }
    }
}
