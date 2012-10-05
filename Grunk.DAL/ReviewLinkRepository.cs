using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grunk.Domain.Repositories;
using Grunk.Domain;

namespace Grunk.DAL
{
    public class ReviewLinkRepository : BaseRepository<ReviewLink>, IReviewLinkRepository
    {
        public ReviewLinkRepository(Entities entities)
            : base(entities)
        {
        }

        public ReviewLink GetByReviewId(int reviewId)
        {
            return entities.Single(x => x.ReviewId == reviewId);
        }

        public void Delete(int reviewId)
        {
            try
            {
                entities.DeleteObject(GetByReviewId(reviewId));
                SaveChanges();
            }
            catch
            {
            }
        }

        public void Create(int reviewId, string url)
        {
            ReviewLink reviewLink = new ReviewLink { ReviewId = reviewId, Url = url };
            entities.AddObject(reviewLink);
            SaveChanges();
        }

        public void CreateOrUpdate(int reviewId, string url)
        {
            try
            {
                ReviewLink reviewLink = GetByReviewId(reviewId);
                reviewLink.Url = url;
                SaveChanges();
            }
            catch
            {
                Create(reviewId, url);
            }
        }
    }
}
