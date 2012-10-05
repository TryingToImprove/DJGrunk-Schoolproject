using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap.Configuration.DSL;
using Grunk.Domain.Repositories;
using Grunk.DAL;
using Grunk.Domain;
using System.Xml;

namespace Grunk.DependencyInjection
{
    public class RepositoryRegistry : Registry
    {
        public RepositoryRegistry()
        {

            For<ICredentialRepository>()
                .Use(factory =>
                {
                    return new CredentialRepository(factory.GetInstance<Entities>());
                });


            For<IProfileRepository>()
                .Use(factory =>
                {
                    var repository = new ProfileRepository(factory.GetInstance<Entities>());
                    return repository;
                });

            For<IProfileActivityRepository>()
                .Use(factory =>
                {
                    var repository = new ProfileActivityRepository(factory.GetInstance<Entities>());
                    return repository;
                });

            For<IGrunkerRepository>()
                .Use(factory =>
                {
                    var repository = new GrunkerRepository(factory.GetInstance<Entities>());
                    return repository;
                });

            For<IActivityTypeRepository>()
                .Use(factory =>
                {
                    var repository = new ActivityTypeRepository(factory.GetInstance<Entities>());
                    return repository;
                });

            For<IPictureRepository>()
                .Use(factory =>
                {
                    var repository = new PictureRepository(factory.GetInstance<Entities>());
                    return repository;
                });

            For<IGenreRepository>()
                .Use(factory =>
                {
                    var repository = new GenreRepository(factory.GetInstance<Entities>());
                    return repository;
                });

            For<IArtistRepository>()
                .Use(factory =>
                {
                    var repository = new ArtistRepository(factory.GetInstance<Entities>());
                    return repository;
                });


            For<IAlbumRepository>()
                .Use(factory =>
                {
                    var repository = new AlbumRepository(factory.GetInstance<Entities>());
                    return repository;
                });

            For<IPurchaseRepository>()
                .Use(factory =>
                {
                    var repository = new PurchaseRepository(factory.GetInstance<Entities>());
                    return repository;
                });

            For<IReviewRepository>()
                .Use(factory =>
                {
                    var repository = new ReviewRepository(factory.GetInstance<Entities>());
                    return repository;
                });

            For<IReviewLinkRepository>()
                .Use(factory =>
                {
                    var repository = new ReviewLinkRepository(factory.GetInstance<Entities>());
                    return repository;
                });

            For<IStoreDetailsRepository>()
                .Use(factory =>
                {
                    var repository = new StoreDetailsRepository(HttpContext.Current.Server.MapPath("~/App_Data/storedetails.xml"));
                    return repository;
                });

            For<IStaticTextRepository>()
                .Use(factory =>
                {
                    var repository = new StaticTextRepository(factory.GetInstance<Entities>());
                    return repository;
                });
        }
    }
}