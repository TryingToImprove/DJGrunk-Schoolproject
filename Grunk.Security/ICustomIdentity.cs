using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace Grunk.Security
{
    public interface ICustomIdentity : IIdentity
    {
        int AdminId { get; }
        string FirstName { get; }
        string LastName { get; }
        bool IsSystemUser { get; }
    }
}
