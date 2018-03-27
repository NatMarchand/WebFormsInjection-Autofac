using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Autofac.Builder;
using Autofac.Core;

namespace Autofac.Integration.Web
{
    public class WebFormRegistrationSource : IRegistrationSource
    {
        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
        {
            if (service is IServiceWithType serviceWithType && serviceWithType.ServiceType.Namespace.StartsWith("ASP", true, CultureInfo.InvariantCulture))
            {
                return new[]
                {
                    RegistrationBuilder.ForType(serviceWithType.ServiceType).CreateRegistration()
                };
            }

            return Enumerable.Empty<IComponentRegistration>();
        }

        public bool IsAdapterForIndividualComponents => true;
    }
}
