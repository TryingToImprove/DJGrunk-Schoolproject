using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grunk.Domain;
using Grunk.Domain.Repositories;

namespace Grunk.DAL
{
    public class PictureRepository : BaseRepository<Picture>, IPictureRepository
    {
        public PictureRepository(Entities entities)
            : base(entities)
        {
        }


        public void Delete(int id)
        {
            entities.DeleteObject(entities.Single(x => x.PictureId == id));
            SaveChanges();
        }
    }
}
