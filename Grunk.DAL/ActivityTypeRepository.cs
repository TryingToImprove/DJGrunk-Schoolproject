using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grunk.Domain;
using Grunk.Domain.Repositories;

namespace Grunk.DAL
{
    public class ActivityTypeRepository : BaseRepository<ActivityType>, IActivityTypeRepository
    {
        public ActivityTypeRepository(Entities entities)
            : base(entities)
        {
        }

        public ActivityType Create(string name)
        {
            if (entities.Any(x => x.Name == name))
            {
                throw new ArgumentException("A activity type with name '" + name + "' already exisits");
            }

            ActivityType activityType = new ActivityType(name);
            entities.AddObject(activityType);

            SaveChanges();

            return activityType;
        }

        public ActivityType GetByName(string name)
        {
            return entities.Single(x => x.Name == name);
        }
    }
}
