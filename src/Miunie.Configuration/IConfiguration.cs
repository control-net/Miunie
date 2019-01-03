namespace Miunie.Configuration
{
    public interface IConfiguration
    {
        string GetValueFor(string key);
        void SetValueFor(string key, string value);
    }
}
