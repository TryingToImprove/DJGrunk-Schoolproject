using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grunk.Domain.Repositories
{
    public interface IReviewLinkRepository : IBaseRepository
    {
        ReviewLink GetByReviewId(int reviewId);

        void Delete(int reviewId);

        void CreateOrUpdate(int reviewId, string url);
    }
}
