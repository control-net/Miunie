namespace Miunie.Core.Logging
{
    public interface ILogWriter
    {
        void Log(string message);
        void LogError(string message);
    }
}

