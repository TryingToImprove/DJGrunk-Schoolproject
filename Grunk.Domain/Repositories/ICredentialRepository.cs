using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grunk.Domain.Repositories
{
    public interface ICredentialRepository : IBaseRepository
    {
        Credential GetByUserNameAndPassword(string userName, string password);

        IEnumerable<Credential> GetAll();

        void Delete(Credential credential);

        Credential GetById(int id);

        bool UserNameInUse(string username);

        void Create(Credential credential);
    }
}
