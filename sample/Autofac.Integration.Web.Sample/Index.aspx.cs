using System;
using System.Web.UI;

namespace Autofac.Integration.Web.Sample
{
    public partial class Index : Page
    {
        protected IDependency Dependency { get; }

        public Index(IDependency dependency)
        {
            Dependency = dependency;
        }
    }
}