using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Grunk.Web.Controllers;
using Grunk.Domain.Services;
using Grunk.Web.UI.Models;
using Grunk.Domain;

namespace Grunk.Web.UI.Controllers
{
    public class ProfileController : BaseController
    {
        private IProfileService ProfileService;
        private ICredentialService CredentialService;

        public ProfileController(IProfileService profileService, ICredentialService credentialService)
            : base()
        {
            this.ProfileService = profileService;
            this.CredentialService = credentialService;
        }

        //
        // GET: /Profile/

        public ActionResult Index()
        {
            if (this.User != null)
            {
                return MyProfile();
            }
            else
            {
                return RedirectToAction("Create");
            }
        }

        public ActionResult Create(string returnUrl)
        {
            ViewData["IsReturnUrlSet"] = !string.IsNullOrWhiteSpace(returnUrl);

            return View("Create", new CreateProfileViewModel
            {
                MenuKey = "profile",
                LoginForm = new LoginFormViewModel
                {
                    UserName = (string)TempData["UserName"]
                },
                CreateProfileForm = new CreateProfileFormViewModel()
            });
        }

        [HttpPost]
        public ActionResult Create(CreateProfileViewModel requestedViewModel)
        {
            if (ModelState.IsValid)
            {
                if (ProfileService.UserNameInUse(requestedViewModel.CreateProfileForm.Username))
                {
                    ModelState.AddModelError("CreateProfileForm.Username", "Brugernavnet er i brug");
                }

                if (ModelState.IsValid)
                {
                    var profile = ProfileService.Create(requestedViewModel.CreateProfileForm.Username, requestedViewModel.CreateProfileForm.Password, requestedViewModel.CreateProfileForm.Description, requestedViewModel.CreateProfileForm.Picture);
                }
            }

            return View("Create", new CreateProfileViewModel()
            {
                MenuKey = "profile",
                LoginForm = new LoginFormViewModel(),
                CreateProfileForm = new CreateProfileFormViewModel()
            });
        }

        [Authorize]
        private ActionResult MyProfile()
        {
            return View("MyProfile", new MyProfileViewModel
            {
                MenuKey = "profile",
                Profile = ProfileService.GetById(this.User.Identity.AdminId)
            });
        }

        [Authorize]
        [ChildActionOnly]
        public ActionResult UpdateDescription(int id)
        {
            var profile = ProfileService.GetByProfileId(id);

            return PartialView("_UpdateDescription", new UpdateDescriptionProfileViewModel() { Description = profile.Description });
        }

        [Authorize]
        [HttpPost]
        public ActionResult UpdateDescription(int id, UpdateDescriptionProfileViewModel requestedViewModel)
        {
            if (ModelState.IsValid)
            {
                ProfileService.UpdateDescription(id, requestedViewModel.Description);

                ProfileService.AddActivity(id, "UpdatedDescription");
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        [ChildActionOnly]
        public ActionResult UpdatePicture(int id)
        {
            return PartialView("_UpdatePicture", new UpdatePictureProfileViewModel());
        }

        [Authorize]
        [HttpPost]
        public ActionResult UpdatePicture(int id, UpdatePictureProfileViewModel requestedViewModel)
        {
            if (ModelState.IsValid)
            {
                ProfileService.UpdatePicture(id, requestedViewModel.Picture);

                ProfileService.AddActivity(id, "UpdatedProfileImage");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SignIn(LoginFormViewModel requestedViewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Credential credential;
                if (CredentialService.Validate(requestedViewModel.UserName, requestedViewModel.Password, out credential))
                {
                    CredentialService.SignIn(credential, Response);

                    if (!String.IsNullOrWhiteSpace(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    var profile = ProfileService.GetById(credential.CredentialsId);
                    ProfileService.AddActivity(profile.ProfileId, "LoggedIn");

                    return RedirectToAction("Index");
                }
            }

            TempData["LoginHeader"] = "Der skete en fejl da du prøvede at logge ind";
            TempData["LoginMessage"] = "Brugeren og kodeordet passer ikke sammen,";
            TempData["UserName"] = requestedViewModel.UserName;

            return RedirectToAction("Create");
        }

        public ActionResult ForgotPassword()
        {
            return View(new ForgotPasswordProfileViewModel()
            {
                MenuKey = "profile"
            });
        }

        [Authorize]
        public ActionResult SignOut()
        {
            CredentialService.SignOut();

            TempData["LoginHeader"] = "Du er offline..";
            TempData["LoginMessage"] = "Du blev logget ud, kom tilbage snart.. ;-)";

            return RedirectToAction("Index");
        }
    }
}
