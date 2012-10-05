using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Grunk.Security
{
    public interface ICustomFormsAuthentication
    {
        void SignIn(int adminId, string userName, string firstName, string lastName, bool isSystemUser, HttpResponseBase httpResponseBase);
        void SignOut();
        void AuthenticateRequestDecryptCustomFormsAuthenticationTicket(HttpContext Context);
    }
}
