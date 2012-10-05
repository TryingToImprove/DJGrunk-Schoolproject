using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap;

namespace Grunk.DependencyInjection
{
    public class MvcDependencyHandler : System.Web.Mvc.IDependencyResolver
    {
        private IContainer Container;

        public MvcDependencyHandler(IContainer container)
        {
            this.Container = container;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null)
            {
                return null;
            }

            return (serviceType.IsAbstract || serviceType.IsInterface) ? this.Container.TryGetInstance(serviceType) : this.Container.GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.Container.GetAllInstances(serviceType).Cast<Object>();
        }
    }
}