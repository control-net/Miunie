using System.IO;
using Miunie.Core.Infrastructure;
using System.Reflection;

namespace Miunie.SystemInfrastructure
{
    public class SystemFileSystem : IFileSystem
    {
        public string DataStoragePath { get; }

        public SystemFileSystem()
        {
            DataStoragePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }
    }
}
