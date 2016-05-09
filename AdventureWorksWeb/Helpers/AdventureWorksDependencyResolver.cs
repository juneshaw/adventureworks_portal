using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace AdventureWorksWeb
{
    /// <summary>
    /// AdventureWorksDependencyResolver
    /// 
    /// Impelementation of the MVC Dependency Resolver
    /// The dependency resolver for this application be using a Unity container
    /// </summary>
    public class AdventureWorksDependencyResolver : IDependencyResolver
    {
        IUnityContainer _unityContainer;

        public AdventureWorksDependencyResolver(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public object GetService(Type serviceType)
        {
            if (!_unityContainer.IsRegistered(serviceType) && (serviceType.IsAbstract || serviceType.IsInterface))
                return null;

            return _unityContainer.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _unityContainer.ResolveAll(serviceType);
        }
    }
}