
namespace Miunie.Core
{
    public interface ILanguageResources
    {
        string GetPhrase(string key);
        string GetFormatted(string key, params object[] objs);
    }
}