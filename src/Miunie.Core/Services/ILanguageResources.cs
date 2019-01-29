namespace Miunie.Core
{
    public interface ILanguageResources
    {
        string GetPhrase(string key);
        string GetPhrase(string key, string lang);
        string GetFormatted(string key, params object[] objs);
        string GetTranslatedFormatted(
                                string key,
                                string lang,
                                params object[] objs);
    }
}
