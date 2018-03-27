using System;
using System.Web;

namespace Autofac.Integration.Web.Sample
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Dependency>().As<IDependency>().InstancePerRequest();
            builder.RegisterSource(new WebFormRegistrationSource());
            var container = builder.Build();
            var provider = new AutofacServiceProvider(container);
            HttpRuntime.WebObjectActivator = provider;
        }
    }
}