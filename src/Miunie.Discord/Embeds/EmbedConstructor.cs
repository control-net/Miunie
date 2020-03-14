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

using Discord;
using Miunie.Core.Entities;
using Miunie.Core.Entities.Discord;
using Miunie.Core.Providers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Miunie.Discord.Embeds
{
    internal static class EmbedConstructor
    {
        private static readonly int RepLogPageSize = 10;

        public static Embed CreateReputationLog(IEnumerable<ReputationEntry> entries, int index, ILanguageProvider lang)
        {
            var embed = Paginator.PaginateEmbed(
                entries,
                new EmbedBuilder()
                .WithColor(new Color(236, 64, 122))
                .WithTitle(lang.GetPhrase(PhraseKey.USER_EMBED_REP_LOG_TITLE.ToString())),
                index,
                RepLogPageSize,
                x => $"{(x.IsFromInvoker ? "**To:**" : "**From:**")} {x.TargetName} (**{FormatReputationType(x.Type)}**) {x.GivenAt:d} at {x.GivenAt:t} UTC");

            if (string.IsNullOrWhiteSpace(embed.Description))
            {
                _ = embed.WithDescription(lang.GetPhrase(PhraseKey.USER_EMBED_REP_LOG_EMPTY.ToString()));
            }

            return embed.Build();
        }

        public static Embed ToEmbed(this MiunieUser mUser, ILanguageProvider lang)
        {
            var realnessPhrase = lang.GetPhrase((mUser.IsBot ? PhraseKey.USER_EMBED_IS_BOT : PhraseKey.USER_EMBED_IS_HUMAN).ToString());

            return new EmbedBuilder()
                .WithColor(new Color(236, 64, 122))
                .WithTitle(lang.GetPhrase(PhraseKey.USER_EMBED_TITLE.ToString()))
                .WithThumbnailUrl(mUser.AvatarUrl)
                .AddField(lang.GetPhrase(PhraseKey.USER_EMBED_NAME_TITLE.ToString()), mUser.Name)
                .AddField(lang.GetPhrase(PhraseKey.USER_EMBED_REALNESS_TITLE.ToString()), realnessPhrase, true)
                .AddField(lang.GetPhrase(PhraseKey.USER_EMBED_REP_TITLE.ToString()), mUser.Reputation.Value.ToString(), true)
                .AddField(lang.GetPhrase(PhraseKey.USER_EMBED_ROLES_TITLE.ToString()), string.Join("\n", mUser.Roles.Select(r => r.Name)), true)
                .AddField(lang.GetPhrase(PhraseKey.USER_EMBED_JOINED_AT_TITLE.ToString()), $"{mUser.JoinedAt:d} at {mUser.JoinedAt:t} UTC")
                .AddField(lang.GetPhrase(PhraseKey.USER_EMBED_CREATED_AT_TITLE.ToString()), $"{mUser.CreatedAt:d} at {mUser.CreatedAt:t} UTC", true)
                .AddField(lang.GetPhrase(PhraseKey.USER_EMBED_TIME_TITLE.ToString()), mUser.UtcTimeOffset.HasValue ? lang.GetPhrase(PhraseKey.USER_EMBED_TIME.ToString(), DateTime.UtcNow + mUser.UtcTimeOffset) : lang.GetPhrase(PhraseKey.USER_EMBED_TIME_NOSET.ToString()), true)
                .Build();
        }

        public static Embed ToEmbed(this MiunieGuild mGuild, ILanguageProvider lang)
            => new EmbedBuilder()
                .WithColor(new Color(236, 64, 122))
                .WithThumbnailUrl(mGuild.IconUrl)
                .WithTitle(lang.GetPhrase(PhraseKey.GUILD_EMBED_TITLE.ToString()))
                .AddField(lang.GetPhrase(PhraseKey.GUILD_EMBED_NAME_TITLE.ToString()), mGuild.Name)
                .AddField(lang.GetPhrase(PhraseKey.GUILD_EMBED_STATS_TITLE.ToString()), mGuild.GetStats(), true)
                .AddField(lang.GetPhrase(PhraseKey.GUILD_EMBED_ROLES_TITLE.ToString()), string.Join(", ", mGuild.Roles.Select(r => r.Name.Replace("@", string.Empty))), true)
                .AddField(lang.GetPhrase(PhraseKey.GUILD_EMBED_CREATED_AT_TITLE.ToString()), $"{mGuild.CreationDate:d} at {mGuild.CreationDate:t} UTC")
                .Build();

        private static string FormatReputationType(ReputationType type)
        {
            switch (type)
            {
                case ReputationType.Plus:
                    return "+1";
                case ReputationType.Minus:
                    return "-1";
                default:
                    throw new ArgumentException("Unknown ReputationType.");
            }
        }
    }
}
