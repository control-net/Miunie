// This file is part of Miunie.
//
//  Miunie is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Miunie is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with Miunie. If not, see <https://www.gnu.org/licenses/>.

using Miunie.Core.Entities;
using Miunie.Core.Entities.Discord;
using System;

namespace Miunie.Core.XUnit.Tests.Data
{
    public class DummyMiunieUsers
    {
        public static readonly ulong DummyGuildId = 420;

        public MiunieUser Senne { get; private set; } = new MiunieUser
        {
            UserId = 69420911,
            GuildId = DummyGuildId,
            Name = "Senne",
            Reputation = new Reputation
            {
                Value = 55
            }
        };

        public MiunieUser Peter { get; private set; } = new MiunieUser
        {
            UserId = 182941761801420802,
            GuildId = DummyGuildId,
            Name = "Peter",
            Reputation = new Reputation
            {
                Value = 100
            }
        };

        public MiunieUser Drax { get; private set; } = new MiunieUser
        {
            UserId = 123456789,
            GuildId = DummyGuildId,
            Name = "Drax",
            Reputation = new Reputation
            {
                Value = -1
            }
        };

        public MiunieUser DraxWithUtcTimeOffSet { get; private set; } = new MiunieUser
        {
            UserId = 123456789,
            GuildId = DummyGuildId,
            Name = "Drax",
            UtcTimeOffset = new TimeSpan(0, 0, 0)
        };

        public MiunieUser PeterWithNoTimeSet { get; private set; } = new MiunieUser
        {
            UserId = 182941761801420802,
            GuildId = DummyGuildId,
            Name = "Peter"
        };

        public MiunieUser PeterWithUtcPlusOneHourTimeSet { get; private set; } = new MiunieUser
        {
            UserId = 123456789,
            GuildId = DummyGuildId,
            Name = "Drax",
            UtcTimeOffset = new TimeSpan(1, 0, 0)
        };
    }
}
