using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grunk.Domain
{
    public partial class Profile
    {
        public Profile()
        {
        }

        public int GetSum()
        {
            var totalGrunks = this.Grunkers.Sum(x => x.Sum);
            var totalPurchase = this.Purchases.Sum(x => x.Price);

            return totalGrunks - totalPurchase;
        }

        public ProfileActivity LastActivity
        {
            get
            {
                try
                {
                    return this.ProfileActivities.OrderByDescending(x => x.CreationDateTime).First();
                }
                catch
                {
                    return new ProfileActivity { ActivityType = new ActivityType { Name = "Nothing", PrettyName = "???" }, CreationDateTime = DateTime.MinValue, Profile = this };
                }
            }
        }

        public Picture GetPicture(int width, int height)
        {
            try
            {
                return this.Pictures.OrderByDescending(x => x.UploadedDateTime).First(x => x.Width == width && x.Height == height);
            }
            catch
            {
                return new Picture();
            }
        }
    }
}
