using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace AdventureWorksWeb
{
    /// <summary>
    /// AdventureWorksUnityContainer
    /// 
    /// Concrete static implementation of the Unity container for this application
    /// </summary>
    public static class AdventureWorksUnityContainer
    {
        static IUnityContainer _instance;

        static AdventureWorksUnityContainer()
        {
            // Load the Unity confirmation for this application from the adventurework Unity section in web.config
            UnityContainer adventurWorksContainer = new UnityContainer();
            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            section.Configure(adventurWorksContainer, "adventureWorks");
            _instance = adventurWorksContainer;
        }

        public static IUnityContainer Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}