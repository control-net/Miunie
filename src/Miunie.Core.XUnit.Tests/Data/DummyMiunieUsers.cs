using Miunie.Core.Entities.Discord;
using System;

namespace Miunie.Core.XUnit.Tests.Data
{
    public class DummyMiunieUsers
    {
        public static readonly ulong DummyGuildId = 420;

        public readonly MiunieUser Senne = new MiunieUser
        {
            UserId = 69420911,
            GuildId = DummyGuildId,
            Name = "Senne",
            Reputation = new Reputation
            {
                Value = 55
            }
        };

        public readonly MiunieUser Peter = new MiunieUser
        {
            UserId = 182941761801420802,
            GuildId = DummyGuildId,
            Name = "Peter",
            Reputation = new Reputation
            {
                Value = 100
            }
        };

        public readonly MiunieUser Drax = new MiunieUser
        {
            UserId = 123456789,
            GuildId = DummyGuildId,
            Name = "Drax",
            Reputation = new Reputation
            {
                Value = -1
            }
        };

        public readonly MiunieUser DraxWithUtcTimeOffSet = new MiunieUser
        {
            UserId = 123456789,
            GuildId = DummyGuildId,
            Name = "Drax",
            UtcTimeOffset = new TimeSpan(0, 0, 0)
        };

        public readonly MiunieUser PeterWithNoTimeSet = new MiunieUser
        {
            UserId = 182941761801420802,
            GuildId = DummyGuildId,
            Name = "Peter"
        };

        public readonly MiunieUser PeterWithUtcPlusOneHourTimeSet = new MiunieUser
        {
            UserId = 123456789,
            GuildId = DummyGuildId,
            Name = "Drax",
            UtcTimeOffset = new TimeSpan(1, 0, 0)
        };
    }
}
