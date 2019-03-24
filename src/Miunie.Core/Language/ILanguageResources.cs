namespace Miunie.Core.Language
{
    public interface ILanguageResources
    {
        string GetPhrase(string key, params object[] objs);
    }
}
