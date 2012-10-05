using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grunk.Domain.Repositories
{
    public interface IActivityTypeRepository : IBaseRepository
    {
        ActivityType GetByName(string name);
        ActivityType Create(string name);
    }
}
