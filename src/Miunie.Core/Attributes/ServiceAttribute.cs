using System;

namespace Miunie.Core.Attributes
{
    public sealed class ServiceAttribute : Attribute
    {
        public ServiceType ServiceType { get; private set; }
        public ServiceAttribute(ServiceType serviceType = ServiceType.SINGLETON)
        {
            ServiceType = serviceType;
        }
    }
}
