using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Grunk.Web.Controllers.Admin;
using Grunk.Domain.Services;
using Grunk.Web.UI.Areas.Administration.Models;
using Grunk.Domain;
using Grunk.Web.Services;

namespace Grunk.Web.UI.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProfilesController : BaseController
    {
        private IProfileService ProfileService;

        public ProfilesController(IProfileService profileService)
        {
            this.ProfileService = profileService;
        }

        //
        // GET: /Administration/Profiles/

        public ActionResult Index()
        {
            var profiles = ProfileService.GetAll();

            return View(new IndexProfilesViewModel
            {
                Profiles = profiles
            });
        }

        //
        // POST: /Administration/Profiles/Grunks
        [HttpPost]
        public ActionResult Grunks(int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    ProfileService.GiveGrunks(id.Value, 500);
                    var profile = ProfileService.GetByProfileId(id.Value);

                    Messages.Add("Grunker givet", profile.Credential.UserName + " har fået lagt 500 grunk til sine grunker", MessageType.Success);
                }
                else
                {
                    ProfileService.ResetGrunks(1500);

                    Messages.Add("Grunker givet til alle", "Alle brugere har nu 1.500 grunker", MessageType.Success);
                }
            }
            catch
            {
                Messages.AddStandardSavingError();
            }

            return RedirectToAction("Index");
        }

        //
        // POST: /Administration/Profiles/Delete/1
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var profile = ProfileService.GetById(User.Identity.AdminId);
                if (profile.ProfileId == id)
                {
                    Messages.Add("Brugeren blev ikke slettet", "Brugeren kunne ikke slettes, da du er logget ind på den", MessageType.Error);
                }
                else
                {
                    try
                    {
                        ProfileService.Delete(id);
                        Messages.Add("Brugeren er slettet", "Brugeren er blevet fjernet fra databasen", MessageType.Success);
                    }
                    catch
                    {
                        Messages.AddStandardSavingError();
                    }
                }
            }
            catch
            {
                Messages.AddStandardRetriveError();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            try
            {
                var profile = ProfileService.GetByProfileId(id);

                return View(new UpdateProfilesViewModel
                {
                    Description = profile.Description
                });
            }
            catch
            {
                Messages.AddStandardRetriveError();
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Update(int id, UpdateProfilesViewModel requestedViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var profile = ProfileService.GetByProfileId(id);
                    ProfileService.UpdateDescription(id, requestedViewModel.Description);

                    Messages.Add("Brugeren er gemt", "Brugeren " + profile.Credential.UserName + " har fået sin beskrivelse ændret", MessageType.Success);

                    return RedirectToAction("Index");
                }
                catch
                {
                    Messages.AddStandardRetriveError();
                }
            }

            return View(requestedViewModel);
        }
    }
}
