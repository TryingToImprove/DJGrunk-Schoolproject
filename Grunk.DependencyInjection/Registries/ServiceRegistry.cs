using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap.Configuration.DSL;
using Grunk.Domain.Services;
using Grunk.Services;
using System.Collections.Specialized;
using Grunk.Domain.Repositories;
using Grunk.Security;
using Grunk.Security.Authentication;

namespace Grunk.DependencyInjection.Registries
{
    public class ServiceRegistry : Registry
    {
        public ServiceRegistry(NameValueCollection appSettings)
        {
            For<ICustomFormsAuthentication>()
                .Use<CustomFormsAuthentication>()
                .Named("Custom Forms Authentication instance.");

            For<IHashService>()
                .Use(factory =>
                {
                    return new HashService(appSettings["hashPrefix"]);
                });

            For<ICredentialService>()
                .Use(factory =>
                {
                    return new CredentialService(factory.GetInstance<ICustomFormsAuthentication>(), factory.GetInstance<IHashService>(), factory.GetInstance<ICredentialRepository>());
                });

            For<IProductService>()
                .Use(factory =>
                {
                    return new ProductService(factory.GetInstance<IGenreRepository>(), new PictureService("Products"), factory.GetInstance<IPictureRepository>(), factory.GetInstance<IArtistRepository>(), factory.GetInstance<IAlbumRepository>(), factory.GetInstance<IPurchaseRepository>());
                });

            For<IProfileService>()
                .Use(factory =>
                {
                    return new ProfileService(factory.GetInstance<IProfileRepository>(), factory.GetInstance<IProfileActivityRepository>(), factory.GetInstance<ICredentialService>(), factory.GetInstance<IActivityTypeRepository>(), factory.GetInstance<IGrunkerRepository>(), new PictureService("Profiles"), factory.GetInstance<IPictureRepository>(), factory.GetInstance<IReviewService>(), factory.GetInstance<IProductService>());
                });


            For<IReviewService>()
                .Use(factory =>
                {
                    return new ReviewService(factory.GetInstance<IReviewRepository>(), new PictureService("Reviews"), factory.GetInstance<IPictureRepository>(), factory.GetInstance<IReviewLinkRepository>());
                });

            For<IStoreDetailService>()
                .Use(factory =>
                {
                    return new StoreDetailService(factory.GetInstance<IStoreDetailsRepository>());
                });

            For<IStaticTextService>()
                .Use(factory =>
                {
                    return new StaticTextService(factory.GetInstance<IStaticTextRepository>());
                });
        }
    }
}