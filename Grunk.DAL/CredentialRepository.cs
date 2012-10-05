using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grunk.Domain;
using Grunk.Domain.Repositories;

namespace Grunk.DAL
{
    public class CredentialRepository : BaseRepository<Credential>, ICredentialRepository
    {
        public CredentialRepository(Entities entities)
            : base(entities)
        {
        }

        public Credential GetByUserNameAndPassword(string userName, string password)
        {
            return entities.FirstOrDefault(x => x.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase) && x.Password.Equals(password, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Credential> GetAll()
        {
            return entities.Where(x => !x.IsSystemUser);
        }

        public void Delete(Credential admin)
        {
            entities.DeleteObject(admin);
            SaveChanges();
        }

        public Credential GetById(int id)
        {
            return entities.FirstOrDefault(x => x.CredentialsId == id);
        }

        public bool UserNameInUse(string username)
        {
            return entities.Any(x => x.UserName.Equals(username, StringComparison.InvariantCultureIgnoreCase));
        }

        public void Create(Credential admin)
        {
            entities.AddObject(admin);
            SaveChanges();
        }

    }
}
