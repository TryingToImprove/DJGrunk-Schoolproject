using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grunk.Domain.Repositories
{
    public interface IProductRepository : IBaseRepository
    {
        IEnumerable<Product> GetAll();
        void Create(Product product);
    }
}
