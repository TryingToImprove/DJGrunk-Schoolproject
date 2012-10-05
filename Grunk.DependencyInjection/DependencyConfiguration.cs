using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap;
using System.Web.Mvc;
using Grunk.DependencyInjection.Registries;
using System.Collections.Specialized;
using Grunk.Domain;

namespace Grunk.DependencyInjection
{
    public class DependencyConfiguration
    {
        public static void Configure(NameValueCollection appSettings)
        {
            ObjectFactory.Initialize(
                x =>
                {
                    x.For<Entities>()
                        .Singleton()
                        .Use(_ =>
                        {
                            return new Entities();
                        });

                    x.AddRegistry(new ServiceRegistry(appSettings));
                    x.AddRegistry(new RepositoryRegistry());
                });

            DependencyResolver.SetResolver(new MvcDependencyHandler(ObjectFactory.Container));
        }
    }
}