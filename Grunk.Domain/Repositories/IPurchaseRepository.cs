using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grunk.Domain.Repositories
{
    public interface IPurchaseRepository : IBaseRepository
    {
        void Delete(int albumId, int profileId);
        IEnumerable<PurchaseTopListItem> GetTopList(DateTime from);
    }
}
