using System.Threading.Tasks;

namespace Miunie.Core.Providers
{
    public interface IListDirectoryProvider
    {
        Task<DirectoryListing> OfAsync(MiunieUser user);
    }
}
