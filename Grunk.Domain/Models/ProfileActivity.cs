using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grunk.Domain
{
    public partial class ProfileActivity
    {
        public ProfileActivity()
        {
        }

        public ProfileActivity(int activityTypeId)
        {
            this.CreationDateTime = DateTime.Now;
            this.ActivityTypeId = activityTypeId;
        }
    }
}
