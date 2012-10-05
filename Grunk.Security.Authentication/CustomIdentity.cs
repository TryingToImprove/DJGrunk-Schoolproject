using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace Grunk.Security.Authentication
{
    public class CustomIdentity : GenericIdentity, ICustomIdentity
    {
        public int AdminId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public bool IsSystemUser { get; private set; }

        public CustomIdentity(int adminId, string userName, string firstName, string lastName, bool isSystemUser)
            : base(userName, "Forms")
        {
            AdminId = adminId;
            FirstName = firstName;
            LastName = lastName;
            IsSystemUser = isSystemUser;
        }
    }
}
