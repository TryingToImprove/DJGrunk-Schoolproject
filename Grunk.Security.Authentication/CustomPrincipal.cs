using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace Grunk.Security.Authentication
{
    public class CustomPrincipal : GenericPrincipal, ICustomPrincipal
    {
        public new virtual ICustomIdentity Identity { get; private set; }

        public CustomPrincipal(ICustomIdentity customIdentity, string[] roles)
            : base(customIdentity, roles)
        {
            Identity = customIdentity;
        }
    }
}
