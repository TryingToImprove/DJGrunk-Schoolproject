using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grunk.Domain;
using Grunk.Domain.Repositories;

namespace Grunk.DAL
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(Entities context)
            : base(context)
        {
        }

        public IEnumerable<Genre> GetAll()
        {
            return entities.ToList();
        }

        public bool GenreInUse(string name)
        {
            return entities.Any(x => x.Name == name);
        }

        public void Create(string name)
        {
            if (!GenreInUse(name))
            {
                entities.AddObject(new Genre() { Name = name });
                SaveChanges();
            }
        }

        public void Update(int genreId, string name)
        {
            entities.Single(x => x.GenreId == genreId).Name = name;
            SaveChanges();
        }


        public Genre GetById(int id)
        {
            return entities.Single(x => x.GenreId == id);
        }

        public Genre GetByName(string name)
        {
            return entities.Single(x => x.Name == name);
        }


        public void DeleteById(int id)
        {
            entities.DeleteObject(GetById(id));
            SaveChanges();
        }
    }
}
