using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Grunk.Domain;
using Grunk.Web.Controllers.Admin;
using Grunk.Domain.Services;
using Grunk.Web.UI.Areas.Administration.Models;
using Grunk.Web.Services;

namespace Grunk.Web.UI.Areas.Administration.Controllers
{
    public class StoreDetailsController : BaseController
    {
        IStoreDetailService StoreDetailService;
        public StoreDetailsController(IStoreDetailService storeDetailService)
        {
            this.StoreDetailService = storeDetailService;
        }

        //
        // GET: /Admin/StoreDetails/

        public ActionResult Index()
        {
            StoreDetails storeDetails = StoreDetailService.GetStoreDetails();

            return View(new IndexStoreDetailsViewModel
            {
                StoreDetails = storeDetails,
                Name = storeDetails.Name,
                Address = new IndexStoreDetailsViewModel.AddressInformation
                {
                    Number = storeDetails.Address.Number,
                    Postal = storeDetails.Address.Postal,
                    Road = storeDetails.Address.Road,
                    Town = storeDetails.Address.Town
                },
                Contact = new IndexStoreDetailsViewModel.ContactInformation
                {
                    Email = storeDetails.Contact.Email,
                    Fax = storeDetails.Contact.Fax,
                    Telephone = storeDetails.Contact.Telephone
                }
            });
        }


        //
        // POST: /Admin/StoreDetails/

        [HttpPost]
        public ActionResult Index(IndexStoreDetailsViewModel requestedViewModel)
        {
            List<int> openingHours = new List<int>();
            ValidateOpeningHours(requestedViewModel.OpeningHour, "OpeningHour", ref openingHours);

            List<int> closingHours = new List<int>();
            ValidateOpeningHours(requestedViewModel.CloseingHour, "CloseingHour", ref closingHours);

            if (ModelState.IsValid)
            {
                StoreDetails storeDetails = new StoreDetails();

                storeDetails.Name = requestedViewModel.Name;

                storeDetails.Address = new StoreDetails.AddressInformation
               {
                   Number = requestedViewModel.Address.Number,
                   Postal = requestedViewModel.Address.Postal,
                   Road = requestedViewModel.Address.Road,
                   Town = requestedViewModel.Address.Town
               };

                storeDetails.Contact = new StoreDetails.ContactInformation
                {
                    Email = requestedViewModel.Contact.Email,
                    Fax = requestedViewModel.Contact.Fax,
                    Telephone = requestedViewModel.Contact.Telephone
                };

                for (int i = 0; i < 7; i++)
                {
                    storeDetails.OpeningHours.Add(new StoreDetails.OpeningHour
                    {
                        Day = i,
                        Closeing = closingHours.ElementAt(i),
                        Opening = openingHours.ElementAt(i)
                    });
                }

                StoreDetailService.Update(storeDetails);

                Messages.Add("Gemt", "Informationerne er blevet gemt! Bemærk at der kan gå op til 1 time før du kan se dem på siden pga. caching.", MessageType.Success);
            }
            else
            {
                Messages.Add("Ikke korrekt udfyldt", "Alle informationer er ikke udfyldt korrekt, så data'erne blev ikke gemt.. ", MessageType.Error);

                requestedViewModel.StoreDetails = StoreDetailService.GetStoreDetails();

                return View(requestedViewModel);
            }

            return RedirectToAction("Index");
        }

        private void ValidateOpeningHours(IEnumerable<string> hours, string name, ref List<int> list)
        {
            for (var i = 0; i < hours.Count(); i++)
            {
                string modelName = name + "[" + i + "]", obj = hours.ElementAt(i);
                int openingHour;

                if (string.IsNullOrWhiteSpace(obj))
                {
                    ModelState.AddModelError(modelName, "Må ikke være tom");
                }

                if (!obj.Equals("Lukket", StringComparison.InvariantCultureIgnoreCase))
                {

                    if (!int.TryParse(obj, out openingHour))
                    {
                        ModelState.AddModelError(modelName, "Skal være et heltal");
                    }
                    else
                    {
                        if ((openingHour < 0 || openingHour > 2359) && (openingHour != -1))
                        {
                            ModelState.AddModelError(modelName, "Tallet skal være imellem 0 og 2359");
                        }
                    }
                }
                else
                {
                    openingHour = -1;
                }


                if (ModelState.IsValid)
                {
                    list.Add(openingHour);
                }
            }
        }

    }
}
