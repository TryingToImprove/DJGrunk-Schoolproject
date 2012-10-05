using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Grunk.Domain.Services;
using Grunk.Domain;
using Grunk.Web.UI.Models;

namespace Grunk.Web.UI.Controllers
{
    public class ContactController : Controller
    {
        public static bool isEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        IStaticTextService StaticTextService;
        IStoreDetailService StoreDetailService;

        public ContactController(IStoreDetailService storeDetailsService, IStaticTextService staticTextService)
        {
            StoreDetailService = storeDetailsService;
            StaticTextService = staticTextService;
        }

        //
        // GET: /Contact/

        public ActionResult Index()
        {
            StoreDetails storeDetails = StoreDetailService.GetStoreDetails();

            return View(new IndexContactViewModel()
            {
                Text = StaticTextService.GetByPosition("Contact"),
                MenuKey = "contact",
                MailForm = new SendMailFormViewModel(),
                StoreDetails = storeDetails
            });
        }
        //
        // POST: /Contact/

        [HttpPost]
        public ActionResult Index(SendMailFormViewModel requestedViewModel)
        {
            StoreDetails storeDetails = StoreDetailService.GetStoreDetails();

            if (!isEmail(requestedViewModel.Email))
            {
                ModelState.AddModelError("Email", "Emailen er ikke gyldig");
            }

            if (ModelState.IsValid)
            {
                MailMessage message = new MailMessage();
                message.To.Add(storeDetails.Contact.Email);
                message.Subject = "Besked til DJ Grunk - Kontakt";
                message.From = new MailAddress(requestedViewModel.Email);
                message.Body = requestedViewModel.Text;
                //SmtpClient smtp = new SmtpClient("yoursmtphost");
                //smtp.Send(message);

                TempData.Add("Send", true);

                return RedirectToAction("Index");
            }
            else
            {
                return View("Index", new IndexContactViewModel()
                {
                    Text = StaticTextService.GetByPosition("Contact"),
                    MenuKey = "contact",
                    MailForm = requestedViewModel,
                    StoreDetails = storeDetails
                });
            }
        }

        //
        // GET: PartialOnly

        [ChildActionOnly]
        [OutputCache(Duration = 60 * 60)]
        public ActionResult Information()
        {
            StoreDetails storeDetails = StoreDetailService.GetStoreDetails();

            return PartialView(new ContactInformationViewModel
            {
                Name = storeDetails.Name,
                Road = storeDetails.Address.Road + " " + storeDetails.Address.Number,
                Town = storeDetails.Address.Postal + " " + storeDetails.Address.Town,
                Telephone = storeDetails.Contact.Telephone,
                Fax = storeDetails.Contact.Fax,
                Email = storeDetails.Contact.Email,
                StoreDetails = storeDetails
            });
        }

    }
}
