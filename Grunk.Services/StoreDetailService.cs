using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grunk.Domain.Services;
using Grunk.Domain;
using Grunk.Domain.Repositories;

namespace Grunk.Services
{
    public class StoreDetailService : IStoreDetailService
    {
        IStoreDetailsRepository StoreDetailsRepository;

        public StoreDetailService(IStoreDetailsRepository storeDetailsRepository)
        {
            this.StoreDetailsRepository = storeDetailsRepository;
        }

        public StoreDetails GetStoreDetails()
        {
            var storeDetails = StoreDetailsRepository.GetStoreDetails();
            return storeDetails;
        }


        public void Update(StoreDetails storeDetails)
        {
            //Condition.Requires(storeDetails).IsNotNull();
            //Condition.Requires(storeDetails.Name).IsNotNullOrWhiteSpace();
            //Condition.Requires(storeDetails.OpeningHours).IsNotNull().HasLength(7);
            //Condition.Requires(storeDetails.Contact).IsNotNull();
            //Condition.Requires(storeDetails.Contact.Email).IsNotNullOrWhiteSpace();
            //Condition.Requires(storeDetails.Contact.Telephone).IsNotNullOrWhiteSpace();
            //Condition.Requires(storeDetails.Address).IsNotNull();
            //Condition.Requires(storeDetails.Address.Town).IsNotNullOrWhiteSpace();
            //Condition.Requires(storeDetails.Address.Postal).IsInRange(0, 9999);
            //Condition.Requires(storeDetails.Address.Number).IsGreaterThan(0);

            StoreDetailsRepository.Update(storeDetails);
        }
    }
}
