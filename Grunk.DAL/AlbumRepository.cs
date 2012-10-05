using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grunk.Domain;
using Grunk.Domain.Repositories;

namespace Grunk.DAL
{
    public class AlbumRepository : BaseRepository<Album>, IAlbumRepository
    {
        public AlbumRepository(Entities context)
            : base(context)
        {
        }

        public Album GetById(int id)
        {
            return entities.Single(x => x.AlbumId == id);
        }

        public void AddPicture(int id, string path, string fileName, int width, int height)
        {
            GetById(id).Pictures.Add(new Picture
            {
                FileName = fileName,
                Path = path,
                Width = width,
                Height = height,
                UploadedDateTime = DateTime.Now
            });

            SaveChanges();
        }

        public void Create(Album album)
        {
            entities.AddObject(album);
            SaveChanges();
        }

        public void Delete(int id)
        {
            entities.DeleteObject(GetById(id));
            SaveChanges();
        }

        public IEnumerable<Album> GetAll()
        {
            return entities.ToList();
        }

        public Album Update(int id, short price, int genreId, string albumName, string artistName)
        {
            Album album = GetById(id);
            album.Price = price;
            album.GenreId = genreId;
            album.Name = albumName;
            album.Artist.Name = artistName;

            SaveChanges();

            return album;
        }

        public int Count()
        {
            return entities.Count();
        }


        public IEnumerable<Album> GetPaged(Func<IQueryable<Album>, IOrderedQueryable<Album>> orderBy, int skip, int take)
        {
            return orderBy(entities).Skip(skip).Take(take);
        }
    }
}
