namespace Miunie.Core
{
    public interface ILanguageResources
    {
        string GetPhrase(string key, params object[] objs);
        void SetLanguage(string langKey);
    }
}
