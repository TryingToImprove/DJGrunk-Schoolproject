using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grunk.Domain.Repositories;
using Grunk.Domain.Services;
using Grunk.Domain;
using System.Web;
using System.IO;
using Grunk.Domain.Exceptions;

namespace Grunk.Services
{
    public class ProfileService : IProfileService
    {
        private IProfileRepository ProfileRepository;
        private ICredentialService CredentialService;
        private IProfileActivityRepository ProfileActivityRepository;
        private IActivityTypeRepository ActivityTypeRepository;
        private IGrunkerRepository GrunkRepository;
        private IPictureService PictureService;
        private IPictureRepository PictureRepository;
        private IReviewService ReviewService;
        private IProductService ProductService;

        public ProfileService(IProfileRepository profileRepository, IProfileActivityRepository profileActivityRepository, ICredentialService credentialService, IActivityTypeRepository activityTypeRepository, IGrunkerRepository grunkRepository, IPictureService pictureService, IPictureRepository pictureRepository, IReviewService reviewService, IProductService productService)
        {
            this.ProfileRepository = profileRepository;
            this.CredentialService = credentialService;
            this.ProfileActivityRepository = profileActivityRepository;
            this.ActivityTypeRepository = activityTypeRepository;
            this.GrunkRepository = grunkRepository;
            this.PictureService = pictureService;
            this.PictureRepository = pictureRepository;
            this.ReviewService = reviewService;
            this.ProductService = productService;
        }

        public ActivityType GetActivityByName(string name)
        {
            try
            {
                return this.ActivityTypeRepository.GetByName(name);
            }
            catch
            {
                return this.ActivityTypeRepository.Create(name);
            }
        }

        public Profile GetById(int credentialId)
        {
            Credential credential = CredentialService.GetById(credentialId);

            if (credential.Profiles.Any())
            {
                return credential.Profiles.First();
            }

            throw new ArgumentNullException("Could not find profile");
        }

        private Profile CreateProfile(string userName, string passWord, string description, string firstName, string lastName)
        {
            //Opret login information
            Credential credential = (string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName)) ? CredentialService.Create(userName, passWord) : CredentialService.Create(userName, passWord, firstName, lastName);

            //Opret profil
            Profile profile = new Profile()
            {
                CredentialsId = credential.CredentialsId,
                Description = description
            };

            //Tilføj grunkers
            profile.Grunkers.Add(new Grunker
            {
                Sum = 1500
            });

            //Tikføj aktivitet til profilen
            profile.ProfileActivities.Add(new ProfileActivity(GetActivityByName("Created").ActivityTypeId));

            //Gem i databasen
            ProfileRepository.Create(profile);

            return profile;
        }

        /// <summary>
        /// Creates a simple profile
        /// </summary>
        public Profile Create(string userName, string passWord, string description, HttpPostedFileBase image)
        {
            if (UserNameInUse(userName))
            {
                throw new ArgumentException("Username is in use");
            }

            var profile = CreateProfile(userName, passWord, description, null, null);

            UploadResizeAndSave(ref profile, image);

            return profile;
        }

        public bool UserNameInUse(string userName)
        {
            return CredentialService.UserNameInUse(userName);
        }

        public void UploadResizeAndSave(ref Profile profile, HttpPostedFileBase image = null)
        {
            string fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);

            if (image != null)
            {
                image.SaveAs(HttpContext.Current.Server.MapPath("~/Content/Uploads/Temp/" + fileName));
            }

            try
            {
                foreach (var size in PictureSizes.Profiles)
                {
                    string nPath, nFileName;
                    PictureService.ResizeAndSave("~/Content/Uploads/Temp/", fileName, size.Width, size.Height, out nPath, out nFileName);

                    ProfileRepository.AddPicture(profile.ProfileId, nPath, nFileName, size.Width, size.Height);
                }
            }
            finally
            {
                if (image != null)
                {
                    File.Delete(HttpContext.Current.Server.MapPath("~/Content/Uploads/Temp/" + fileName));
                }
            }
        }

        public IEnumerable<Profile> GetAll()
        {
            return ProfileRepository.GetAll();
        }

        public Profile GetByProfileId(int id)
        {
            return ProfileRepository.GetById(id);
        }

        public void GiveGrunks(int id, int amount, bool clear = false)
        {
            this.GrunkRepository.Give(id, amount, clear);
        }

        public void ResetGrunks(int amount)
        {
            foreach (Profile profile in GetAll())
            {
                int alreadySpent = 0;

                if (profile.Purchases.Any())
                {
                    alreadySpent = profile.Purchases.Sum(x => x.Price);
                }

                GiveGrunks(profile.ProfileId, amount + alreadySpent, true);
            }
        }


        public void Delete(int id)
        {
            var profile = GetByProfileId(id);

            if (profile.Pictures.Any())
            {
                var tempPictures = profile.Pictures.Select(x => new { Id = x.PictureId, Path = x.Path, FileName = x.FileName }).ToList();
                foreach (var pic in tempPictures)
                {
                    PictureService.Delete(pic.Path, pic.FileName);
                    PictureRepository.Delete(pic.Id);
                }
            }

            if (profile.Grunkers.Any())
            {
                var tempGrunks = profile.Grunkers.Select(x => x.GrunkId).ToList();
                foreach (var grunkId in tempGrunks)
                {
                    GrunkRepository.Delete(grunkId);
                }
            }

            if (profile.Purchases.Any())
            {
                var tempPurchases = profile.Purchases.Select(x => new { x.AlbumId, x.ProfileId }).ToList();
                foreach (var purchase in tempPurchases)
                {
                    ProductService.DeletePurchase(purchase.AlbumId, purchase.ProfileId);
                }
            }

            if (profile.ProfileActivities.Any())
            {
                var tempActivities = profile.ProfileActivities.ToList();
                foreach (var activityId in tempActivities)
                {
                    ProfileActivityRepository.Delete(activityId.ActivityId);
                }
            }

            ReviewService.DeleteByProfileId(id);

            ProfileRepository.Delete(id);

            CredentialService.DeleteById(profile.CredentialsId);
        }

        public void UpdateDescription(int id, string description)
        {
            ProfileRepository.UpdateDescription(id, description);
        }


        public void BuyAlbum(int credentialId, int albumId, short price)
        {
            //Load the profile
            Profile profile = GetById(credentialId);

            //Validate sum
            if (profile.GetSum() < price)
            {
                throw new NotEnoughFundsException("Du har ikke nok penge", profile.GetSum(), price);
            }

            if (profile.Purchases.Any(x => x.AlbumId == albumId))
            {
                throw new AlreadyPurchasedException("Du har allerede købt produktet", albumId);
            }

            ProfileRepository.AddAlbumPurchase(profile.ProfileId, albumId, price, DateTime.Now);
        }


        public void UpdatePicture(int id, HttpPostedFileBase picture)
        {
            var profile = GetByProfileId(id);

            var tempPictures = profile.Pictures.Select(x => new { Id = x.PictureId, Path = x.Path, FileName = x.FileName }).ToList();
            foreach (var pic in tempPictures)
            {
                PictureService.Delete(pic.Path, pic.FileName);
                PictureRepository.Delete(pic.Id);
            }

            UploadResizeAndSave(ref profile, picture);
        }


        public void AddActivity(int profileId, string activityName)
        {
            var activity = GetActivityByName(activityName);

            ProfileRepository.AddActivity(profileId, DateTime.Now, activity);
        }
    }
}
