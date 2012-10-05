using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grunk.Domain;
using Grunk.Domain.Repositories;

namespace Grunk.DAL
{
    public class GrunkerRepository : BaseRepository<Grunker>, IGrunkerRepository
    {
        public GrunkerRepository(Entities entities)
            : base(entities)
        {
        }

        public void Give(int id, int amount, bool clear)
        {
            if (clear)
            {
                var temp = entities.Where(x => x.ProfileId == id).ToList();
                temp.ForEach(x => entities.DeleteObject(x));
            }

            entities.AddObject(new Grunker
            {
                Sum = amount,
                ProfileId = id
            });

            SaveChanges();
        }


        public void Delete(int id)
        {
            entities.DeleteObject(entities.Single(x => x.GrunkId == id));
            SaveChanges();
        }
    }
}
