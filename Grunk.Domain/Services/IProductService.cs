using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Grunk.Domain.Services
{
    public interface IProductService
    {
        IEnumerable<Album> GetAll();

        IEnumerable<Genre> GetGenres();

        int Count();

        IEnumerable<Album> GetPaged(Func<IQueryable<Album>, IOrderedQueryable<Album>> orderBy, int skip, int take);

        void UpdateGenre(int genreId, string name);

        void CreateGenre(string name);

        Album GetById(int id);
        Genre GetGenreById(int id);

        void DeleteGenreById(int id);

        Album Create(string albumName, string artistName, int genreId, Int16 price, HttpPostedFileBase cover);
        Album Update(int id, string albumName, string artistName, int genreId, Int16 price, HttpPostedFileBase cover);

        IEnumerable<Album> GetTopList(DateTime from);

        void DeleteById(int id);

        void DeletePurchase(int albumId, int profileId);
    }
}
