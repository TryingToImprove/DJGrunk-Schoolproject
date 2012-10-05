using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web;
using System.Threading;

namespace Grunk.Security.Authentication
{
    public class CustomFormsAuthentication : ICustomFormsAuthentication
    {
        public void SignIn(int adminId, string userName, string firstName, string lastName, bool isSystemUser, System.Web.HttpResponseBase httpResponseBase)
        {
            UserData adminData = new UserData
            {
                CredentialsId = adminId,
                UserName = userName,
                LastName = lastName,
                FirstName = firstName,
                IsSystemUser = isSystemUser
            };

            string encodedTicket = FormsAuthentication.Encrypt(
                new FormsAuthenticationTicket(
                    version: 1,
                    name: userName,
                    issueDate: DateTime.UtcNow,
                    expiration: DateTime.UtcNow.Add(FormsAuthentication.Timeout),
                    isPersistent: true,
                    userData: adminData.ToString())
            );

            HttpCookie httpCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encodedTicket);
            httpResponseBase.Cookies.Add(httpCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        public void AuthenticateRequestDecryptCustomFormsAuthenticationTicket(HttpContext httpContext)
        {
            UserData adminData;

            string formsCookieName = FormsAuthentication.FormsCookieName;
            HttpCookie httpCookie = httpContext.Request.Cookies[(String.IsNullOrWhiteSpace(formsCookieName)) ? Guid.NewGuid().ToString() : formsCookieName];

            if (httpCookie == null)
            {
                adminData = new UserData();
            }
            else
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(httpCookie.Value);

                if (!UserData.TryParse(ticket.UserData, out adminData))
                {
                    adminData = new UserData();
                }

                string[] roles = null;
                if (adminData.IsSystemUser)
                {
                    roles = new string[] { "Admin" };
                }

               
                CustomPrincipal principal = new CustomPrincipal(new CustomIdentity(adminData.CredentialsId, adminData.UserName, adminData.FirstName, adminData.LastName, adminData.IsSystemUser), roles);
                httpContext.User = principal;
                Thread.CurrentPrincipal = principal;
            }
        }
    }
}
