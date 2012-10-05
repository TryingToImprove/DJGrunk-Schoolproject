using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grunk.Domain.Repositories
{
    public interface IAlbumRepository : IBaseRepository
    {
        Album GetById(int id);
        void AddPicture(int id, string path, string fileName, int width, int height);

        void Create(Album album);

        IEnumerable<Album> GetPaged(Func<IQueryable<Album>, IOrderedQueryable<Album>> orderBy, int skip, int take);

        int Count();

        IEnumerable<Album> GetAll();

        void Delete(int id);

        Album Update(int id, short price, int genreId, string albumName, string artistName);
    }
}
