using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grunk.Domain;
using Grunk.Domain.Repositories;

namespace Grunk.DAL
{
    public class ProfileRepository : BaseRepository<Profile>, IProfileRepository
    {
        public ProfileRepository(Entities context)
            : base(context)
        {
        }

        public void Create(Profile profile)
        {
            entities.AddObject(profile);
            SaveChanges();
        }


        public IEnumerable<Profile> GetAll()
        {
            return entities.ToList();
        }


        public Profile GetById(int id)
        {
            return entities.Single(x => x.ProfileId == id);
        }


        public void Delete(int id)
        {
            entities.DeleteObject(GetById(id));
            SaveChanges();
        }

        public void UpdateDescription(int id, string description)
        {
            GetById(id).Description = description;
            SaveChanges();
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


        public void AddAlbumPurchase(int id, int albumId, short price, DateTime purchasedDateTime)
        {
            GetById(id).Purchases.Add(new Purchase
            {
                AlbumId = albumId,
                Price = price,
                PurchasedDateTime = purchasedDateTime
            });

            SaveChanges();
        }


        public void AddActivity(int profileId, DateTime dateTime, ActivityType activity)
        {
            GetById(profileId).ProfileActivities.Add(new ProfileActivity()
            {
                ActivityTypeId = activity.ActivityTypeId,
                CreationDateTime = dateTime
            });
            SaveChanges();
        }
    }
}
