using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Grunk.Domain;

namespace Grunk.DAL
{
    public abstract class BaseRepository<T> where T : class
    {
        private readonly ObjectContext context;
        protected readonly ObjectSet<T> entities;

        public BaseRepository(Entities context)
        {
            this.context = context;
            this.entities = context.CreateObjectSet<T>();
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}
