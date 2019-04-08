namespace Miunie.Core.Providers
{
    public interface IRemoteRepositoryProvider
    {
        string GetRemoteUrl();
        void SetRemoteUrl(string url);
    }
}

