namespace Miunie.Core.Discord
{
    public interface IDiscordServers
    {
        string GetServerNameById(ulong id);
        string[] GetChannelNamesFromServer(ulong id);
    }
}

