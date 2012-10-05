using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grunk.Domain.Repositories
{
    public interface IGrunkerRepository : IBaseRepository
    {
        void Give(int id, int amount, bool clear);

        void Delete(int grunkId);
    }
}
