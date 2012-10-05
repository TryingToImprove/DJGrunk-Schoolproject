using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grunk.Domain;
using Grunk.Domain.Repositories;

namespace Grunk.DAL
{
    public class ProfileActivityRepository : BaseRepository<ProfileActivity>, IProfileActivityRepository
    {
        public ProfileActivityRepository(Entities entities)
            : base(entities)
        {
        }

        public void Delete(int activityId)
        {
            entities.DeleteObject(entities.Single(x => x.ActivityId == activityId));
            SaveChanges();
        }
    }
}
