namespace Miunie.Configuration
{
    public interface IConfiguration
    {
        string GetValueFor(string key);
    }
}

