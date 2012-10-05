using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grunk.Domain;

namespace Grunk.Domain.Repositories
{
    public interface IStoreDetailsRepository
    {
        StoreDetails GetStoreDetails();

        void Update(StoreDetails storeDetails);
    }
}
