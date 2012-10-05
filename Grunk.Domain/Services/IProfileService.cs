using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Grunk.Domain.Services
{
    public interface IProfileService
    {
        Profile GetById(int credentialId);
        Profile Create(string userName, string passWord, string description, HttpPostedFileBase image);
        IEnumerable<Profile> GetAll();

        Profile GetByProfileId(int id);

        bool UserNameInUse(string userName);

        void GiveGrunks(int id, int amount, bool clear = false);

        void ResetGrunks(int amount);

        void Delete(int id);

        void UpdateDescription(int id, string description);

        void BuyAlbum(int credentialId, int albumId, short price);

        void UpdatePicture(int id, HttpPostedFileBase httpPostedFileBase);

        void AddActivity(int profileId, string activityName);
    }
}
