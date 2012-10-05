using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grunk.Domain.Repositories
{
    public interface IProfileRepository : IBaseRepository
    {
        void Create(Profile profile);

        IEnumerable<Profile> GetAll();

        Profile GetById(int id);

        void Delete(int id);

        void UpdateDescription(int id, string description);

        void AddPicture(int id, string path, string fileName, int width, int height);

        void AddAlbumPurchase(int profileId, int albumId, short price, DateTime purchasedDateTime);

        void AddActivity(int profileId, DateTime dateTime, ActivityType activity);
    }
}
