using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grunk.Domain;
using Grunk.Domain.Repositories;

namespace Grunk.DAL
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(Entities context)
            : base(context)
        {
        }

        public IEnumerable<Product> GetAll()
        {
            return entities.ToList();
        }


        public void Create(Product product)
        {
            entities.AddObject(product);
            SaveChanges();
        }
    }
}
