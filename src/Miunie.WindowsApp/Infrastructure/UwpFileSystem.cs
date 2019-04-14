using Miunie.Core.Infrastructure;
using Windows.Storage;

namespace Miunie.WindowsApp.Infrastructure
{
    public class UwpFileSystem : IFileSystem
    {
        public string DataStoragePath { get; }

        public UwpFileSystem()
        {
            var storageFolder = ApplicationData.Current.LocalFolder;
            DataStoragePath = storageFolder.Path;
        }
    }
}
