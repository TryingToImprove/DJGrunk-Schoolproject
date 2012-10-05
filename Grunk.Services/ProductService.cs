using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grunk.Domain.Services;
using Grunk.Domain.Repositories;
using Grunk.Domain;
using System.Web;
using System.IO;

namespace Grunk.Services
{
    public class ProductService : IProductService
    {
        IGenreRepository GenreRepository;
        IPictureService PictureService;
        IPictureRepository PictureRepository;
        IArtistRepository ArtistRepository;
        IAlbumRepository AlbumRepository;
        IPurchaseRepository PurchaseRepository;
        public ProductService(IGenreRepository genreRepository, IPictureService pictureService, IPictureRepository pictureRepository, IArtistRepository artistRepository, IAlbumRepository albumRepository, IPurchaseRepository purchaseRepository)
        {
            this.GenreRepository = genreRepository;
            this.PictureRepository = pictureRepository;
            this.PictureService = pictureService;
            this.ArtistRepository = artistRepository;
            this.AlbumRepository = albumRepository;
            this.PurchaseRepository = purchaseRepository;
        }

        public IEnumerable<Album> GetAll()
        {
            return AlbumRepository.GetAll();
        }

        public IEnumerable<Genre> GetGenres()
        {
            return GenreRepository.GetAll();
        }

        public void UpdateGenre(int genreId, string name)
        {
            GenreRepository.Update(genreId, name);
        }

        public void CreateGenre(string name)
        {
            GenreRepository.Create(name);
        }

        public Genre GetGenreById(int genreId)
        {
            return GenreRepository.GetById(genreId);
        }

        public void DeleteGenreById(int id)
        {
            GenreRepository.DeleteById(id);
        }

        public Album Create(string albumName, string artistName, int genreId, Int16 price, System.Web.HttpPostedFileBase cover)
        {
            Album album = new Album
            {
                Name = albumName,
                Artist = ArtistRepository.CreateOrGet(artistName),
                GenreId = genreId,
                CreationDateTime = DateTime.Now,
                Price = price
            };

            AlbumRepository.Create(album);

            UploadResizeAndSave(ref album, cover);

            return album;
        }

        public Album Update(int id, string albumName, string artistName, int genreId, Int16 price, System.Web.HttpPostedFileBase cover)
        {

            Album album = AlbumRepository.Update(id, price, genreId, albumName, artistName);

            if (cover != null)
            {

                var tempPictures = album.Pictures.Select(x => new { Id = x.PictureId, Path = x.Path, FileName = x.FileName }).ToList();
                foreach (var pic in tempPictures)
                {
                    PictureService.Delete(pic.Path, pic.FileName);
                    PictureRepository.Delete(pic.Id);
                }

                UploadResizeAndSave(ref album, cover);
            }



            return album;
        }

        public void UploadResizeAndSave(ref Album album, HttpPostedFileBase image = null)
        {
            string fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);

            if (image != null)
            {
                image.SaveAs(HttpContext.Current.Server.MapPath("~/Content/Uploads/Temp/" + fileName));
            }

            try
            {
                foreach (var size in PictureSizes.Products)
                {
                    string nPath, nFileName;
                    PictureService.ResizeAndSave("~/Content/Uploads/Temp/", fileName, size.Width, size.Height, out nPath, out nFileName);

                    AlbumRepository.AddPicture(album.AlbumId, nPath, nFileName, size.Width, size.Height);
                }
            }
            finally
            {
                if (image != null)
                {
                    File.Delete(HttpContext.Current.Server.MapPath("~/Content/Uploads/Temp/" + fileName));
                }
            }
        }

        public Album GetById(int id)
        {
            return AlbumRepository.GetById(id);
        }

        public void DeleteById(int id)
        {
            var album = AlbumRepository.GetById(id);

            ArtistRepository.DeleteIfNoAlbums(album.ArtistId);

            var tempPictures = album.Pictures.Select(x => new { Id = x.PictureId, Path = x.Path, FileName = x.FileName }).ToList();
            foreach (var pic in tempPictures)
            {
                PictureService.Delete(pic.Path, pic.FileName);
                PictureRepository.Delete(pic.Id);
            }

            var tempPurchases = album.Purchases.ToList();
            foreach (var purchase in tempPurchases)
            {
                PurchaseRepository.Delete(purchase.AlbumId, purchase.ProfileId);
            }

            AlbumRepository.Delete(album.AlbumId);
        }

        public int Count()
        {
            return AlbumRepository.Count();
        }

        public IEnumerable<Album> GetPaged(Func<IQueryable<Album>, IOrderedQueryable<Album>> orderBy, int skip, int take)
        {
            return AlbumRepository.GetPaged(orderBy, skip, take);
        }

        public IEnumerable<Album> GetTopList(DateTime from)
        {
            List<Album> albums = new List<Album>();

            foreach (var purchase in PurchaseRepository.GetTopList(from))
            {
                albums.Add(GetById(purchase.AlbumId));
            }

            return albums;
        }


        public void DeletePurchase(int albumId, int profileId)
        {
            PurchaseRepository.Delete(albumId, profileId);
        }
    }
}
