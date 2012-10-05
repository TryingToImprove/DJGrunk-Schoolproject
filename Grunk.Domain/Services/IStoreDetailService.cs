using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grunk.Domain.Services
{
    public interface IStoreDetailService
    {
        StoreDetails GetStoreDetails();

        void Update(StoreDetails storeDetails);
    }
}
