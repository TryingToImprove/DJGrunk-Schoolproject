using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Grunk.Domain.Services
{
    public interface ICredentialService
    {
        bool Validate(string userName, string password, out Credential credential);

        void SignIn(Credential credential, HttpResponseBase httpResponseBase);

        void AuthenticateRequest(System.Web.HttpContext Context);

        void SignOut();

        IEnumerable<Credential> GetAll();

        void DeleteById(int id);

        Credential GetById(int id);

        bool UserNameInUse(string username);

        Credential Create(string userName, string passWord);

        Credential Create(string userName, string passWord, string firstName, string lastName);
    }
}
