using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grunk.Domain.Services;
using Grunk.Domain;
using Grunk.Security;
using System.Web;
using System.Security.Cryptography;
using Grunk.DAL;
using Grunk.Domain.Repositories;

namespace Grunk.Services
{
    public class CredentialService : ICredentialService
    {
        private ICustomFormsAuthentication CustomFormsAuthentication;
        private ICredentialRepository CredentialRepository;
        private IHashService HashService;

        public CredentialService(ICustomFormsAuthentication customFormsAuthentication, IHashService hashService, ICredentialRepository credentialRepository)
        {
            this.CustomFormsAuthentication = customFormsAuthentication;
            this.CredentialRepository = credentialRepository;
            this.HashService = hashService;
        }

        public bool Validate(string userName, string password, out Credential credential)
        {
            password = this.HashService.CreateHash(password);

            credential = this.CredentialRepository.GetByUserNameAndPassword(userName, password);

            return (credential != null);
        }

        public void SignIn(Credential credential, HttpResponseBase httpResponseBase)
        {
            this.CustomFormsAuthentication.SignIn(credential.CredentialsId, credential.UserName, credential.FirstName, credential.LastName, credential.IsSystemUser, httpResponseBase);
        }


        public void AuthenticateRequest(System.Web.HttpContext Context)
        {
            this.CustomFormsAuthentication.AuthenticateRequestDecryptCustomFormsAuthenticationTicket(Context);
        }

        public void SignOut()
        {
            this.CustomFormsAuthentication.SignOut();
        }

        public Credential GetById(int id)
        {
            return this.CredentialRepository.GetById(id);
        }


        public IEnumerable<Credential> GetAll()
        {
            return this.CredentialRepository.GetAll();
        }


        public void DeleteById(int id)
        {
            this.CredentialRepository.Delete(GetById(id));
        }


        public bool UserNameInUse(string username)
        {
            return this.CredentialRepository.UserNameInUse(username);
        }


        public Credential Create(Credential credential)
        {
            //Set the new password
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA-256");

            byte[] hash = algorithm.ComputeHash(Encoding.ASCII.GetBytes(credential.Password));

            credential.Password = Convert.ToBase64String(hash);

            this.CredentialRepository.Create(credential);

            return credential;
        }


        public Credential Create(string userName, string passWord)
        {
            Credential credential = new Credential
            {
                UserName = userName,
                Password = passWord,
                IsSystemUser = false,
                FirstName = null,
                LastName = null
            };

            return Create(credential);
        }

        public Credential Create(string userName, string passWord, string firstName, string lastName)
        {
            Credential credential = new Credential
            {
                UserName = userName,
                Password = passWord,
                IsSystemUser = true,
                FirstName = firstName,
                LastName = lastName
            };

            return Create(credential);
        }
    }
}
