using System;
using System.Web.UI;

namespace Autofac.Integration.Web.Sample
{
    public partial class Main : MasterPage
    {
        protected IDependency Dependency { get; }

        public Main(IDependency dependency)
        {
            Dependency = dependency;
        }
    }
}