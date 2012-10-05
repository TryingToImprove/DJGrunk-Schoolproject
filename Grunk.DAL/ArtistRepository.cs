using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grunk.Domain.Repositories;
using Grunk.Domain;

namespace Grunk.DAL
{
    public class ArtistRepository : BaseRepository<Artist>, IArtistRepository
    {
        public ArtistRepository(Entities entities)
            : base(entities)
        {
        }

        public bool ArtistInUse(string name)
        {
            return entities.Any(x => x.Name == name);
        }

        public void Create(string name)
        {
            if (!ArtistInUse(name))
            {
                entities.AddObject(new Artist() { Name = name });
                SaveChanges();
            }
        }

        public void Update(int id, string name)
        {
            GetById(id).Name = name;
            SaveChanges();
        }


        public Artist GetById(int id)
        {
            return entities.Single(x => x.ArtistId == id);
        }

        public Artist GetByName(string name)
        {
            return entities.Single(x => x.Name == name);
        }

        public void DeleteById(int id)
        {
            entities.DeleteObject(GetById(id));
            SaveChanges();
        }


        public Artist CreateOrGet(string name)
        {
            if (!ArtistInUse(name))
            {
                Create(name);
            }

            return GetByName(name);
        }


        public void DeleteIfNoAlbums(int id)
        {
            Artist artist = GetById(id);
            if (!artist.Albums.Any())
            {
                DeleteById(id);
            }
        }
    }
}
