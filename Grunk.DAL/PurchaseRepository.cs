using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grunk.Domain;
using Grunk.Domain.Repositories;

namespace Grunk.DAL
{
    public class PurchaseRepository : BaseRepository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(Entities context)
            : base(context)
        {
        }

        public void Delete(int albumId, int profileId)
        {
            entities.DeleteObject(entities.Single(x => x.AlbumId == albumId && x.ProfileId == profileId));
            SaveChanges();
        }

        public IEnumerable<PurchaseTopListItem> GetTopList(DateTime fromDate)
        {
            return (from purchase in entities
                    where fromDate <= purchase.PurchasedDateTime
                    group purchase by purchase.AlbumId into g
                    select new PurchaseTopListItem
                    {
                        AlbumId = g.Key,
                        Count = g.Count()
                    }).OrderByDescending(x => x.Count).ToList();
        }
    }
}
