using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grunk.Domain.Repositories
{
    public interface IGenreRepository : IBaseRepository
    {
        IEnumerable<Genre> GetAll();

        void Create(string name);
        bool GenreInUse(string name);
        void Update(int genreId, string name);

        Genre GetByName(string name);
        Genre GetById(int id);

        void DeleteById(int id);
    }
}
