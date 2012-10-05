using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grunk.Domain.Repositories
{
    public interface IArtistRepository : IBaseRepository
    {
        void Create(string name);
        bool ArtistInUse(string name);
        void Update(int genreId, string name);

        Artist CreateOrGet(string name);

        Artist GetByName(string name);
        Artist GetById(int id);

        void DeleteById(int id);


        void DeleteIfNoAlbums(int p);
    }
}
