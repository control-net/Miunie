namespace Miunie.Core.XUnit.Tests
{
    public class DummyMiunieUsers
    {
        public static readonly ulong DummyGuildId = 420;

        public readonly MiunieUser Senne = new MiunieUser
        {
            Id = 69420911,
            GuildId = DummyGuildId,
            Name = "Senne",
            Reputation = new Reputation
            {
                Value = 55
            }
        };

        public readonly MiunieUser Peter = new MiunieUser
        {
            Id = 182941761801420802,
            GuildId = DummyGuildId,
            Name = "Peter",
            Reputation = new Reputation
            {
                Value = 100
            }
        };

        public readonly MiunieUser Drax = new MiunieUser
        {
            Id = 123456789,
            GuildId = DummyGuildId,
            Name = "Drax",
            Reputation = new Reputation
            {
                Value = -1
            }
        };
    }
}
