using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grunk.Domain
{
    public partial class ActivityType
    {
        public ActivityType()
        {
        }

        public ActivityType(string name)
        {
            this.Name = name;
        }
    }
}
