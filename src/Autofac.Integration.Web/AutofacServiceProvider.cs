using System;
using System.Reflection;
using System.Web;
using Autofac.Core.Lifetime;

namespace Autofac.Integration.Web
{
    public class AutofacServiceProvider : IServiceProvider
    {
        private readonly ILifetimeScope _rootContainer;

        public AutofacServiceProvider(ILifetimeScope rootContainer)
        {
            _rootContainer = rootContainer;
        }

        public object GetService(Type serviceType)
        {
            ILifetimeScope lifetimeScope;
            var currentHttpContext = HttpContext.Current;
            if (currentHttpContext != null)
            {

                lifetimeScope = (ILifetimeScope)currentHttpContext.Items[typeof(ILifetimeScope)];
                if (lifetimeScope == null)
                {
                    void CleanScope(object sender, EventArgs args)
                    {
                        if (sender is HttpApplication application)
                        {
                            application.RequestCompleted -= CleanScope;
                            lifetimeScope.Dispose();
                        }
                    }

                    lifetimeScope = _rootContainer.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
                    currentHttpContext.Items.Add(typeof(ILifetimeScope), lifetimeScope);
                    currentHttpContext.ApplicationInstance.RequestCompleted += CleanScope;
                }
            }
            else
            {
                lifetimeScope = _rootContainer;
            }

            if (lifetimeScope.IsRegistered(serviceType))
            {
                return lifetimeScope.Resolve(serviceType);
            }

            return Activator.CreateInstance(serviceType, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, null);
        }
    }
}