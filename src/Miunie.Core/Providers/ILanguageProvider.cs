namespace Miunie.Core.Providers
{
    public interface ILanguageProvider
    {
        string GetPhrase(string key, params object[] objs);
    }
}