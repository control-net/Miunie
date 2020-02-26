using System;

namespace Miunie.Core
{
    public sealed class ServiceAttribute : Attribute
    {
        public ServiceType ServiceType { get; private set; }
        public ServiceAttribute(ServiceType serviceType = ServiceType.Singleton)
        {
            ServiceType = serviceType;
        }
    }
}
