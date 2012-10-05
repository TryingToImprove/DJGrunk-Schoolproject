using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Grunk.Domain.Services;
using Grunk.Web.Controllers.Admin;
using Grunk.Domain;
using Grunk.Web.UI.Areas.Administration.Models;

namespace Grunk.Web.UI.Areas.Administration.Controllers
{
    public class HomeController : BaseController
    {
        private ICredentialService CredentialService;
        public HomeController(ICredentialService credentialService)
        {
            this.CredentialService = credentialService;
        }

        //
        // GET: /Admin/Home/
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Admin/Home/

        public ActionResult Login()
        {
            return View(new LoginFormViewModel
            {
            });
        }

        //
        // POST: /Admin/Home/

        [HttpPost]
        public ActionResult Login(LoginFormViewModel requestedViewModel)
        {
            if (ModelState.IsValid)
            {
                Credential credential;
                if (CredentialService.Validate(requestedViewModel.UserName, requestedViewModel.Password, out credential))
                {
                    CredentialService.SignIn(credential, Response);

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Brugeren blev ikke fundet");
                }
            }

            return View(requestedViewModel);
        }

        public ActionResult LogOut()
        {
            CredentialService.SignOut();

            return RedirectToAction("Login");
        }

        [ChildActionOnly]
        public ActionResult LoginDetails()
        {
            return PartialView("_LoginDetails", new LoginDetailsViewModel
            {
                Name = User.Identity.FirstName + " " + User.Identity.LastName
            });
        }

    }
}
