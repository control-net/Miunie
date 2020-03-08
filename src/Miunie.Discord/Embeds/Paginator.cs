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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Miunie.Discord.Embeds
{
    internal static class Paginator
    {
        public static IEnumerable<T> GroupAt<T>(IEnumerable<T> set, int index, int pageSize, bool defaultOnOverflow = false)
        {
            int maxPages = GetPageCount(set.Count(), pageSize);
            if (index < 0 || index >= maxPages)
            {
                return set;
            }

            var remainder = set.Skip(pageSize * (index - 1));

            List<T> group = new List<T>();
            for (int i = 0; i < pageSize; i++)
            {
                if (defaultOnOverflow)
                {
                    group.Add(remainder.ElementAtOrDefault(i));
                }
                else
                {
                    if (remainder.Count() - 1 < i)
                    {
                        continue;
                    }
                    else
                    {
                        group.Add(remainder.ElementAt(i));
                    }
                }
            }

            return group;
        }

        public static EmbedBuilder PaginateEmbed<T>(IEnumerable<T> set, EmbedBuilder embed, int index, int pageSize)
        {
            return embed.WithDescription(Paginate(set, index, pageSize))
                .WithFooter($"{(string.IsNullOrWhiteSpace(embed.Footer?.Text) ? string.Empty : $"{embed.Footer?.Text} | ")}{GetPageFooter(index, set.Count(), pageSize)}");
        }

        public static EmbedBuilder PaginateEmbed<T>(IEnumerable<T> set, EmbedBuilder embed, int index, int pageSize, Func<T, string> writer)
        {
            return embed.WithDescription(Paginate(set, index, pageSize, writer))
                .WithFooter($"{(string.IsNullOrWhiteSpace(embed.Footer?.Text) ? string.Empty : $"{embed.Footer?.Text} | ")}{GetPageFooter(index, set.Count(), pageSize)}");
        }

        public static string Paginate<T>(IEnumerable<T> set, int index, int pageSize)
        {
            var group = GroupAt(set, index, pageSize);
            StringBuilder page = new StringBuilder();

            foreach (T item in group)
            {
                _ = page.AppendLine(item.ToString());
            }

            return page.ToString();
        }

        public static string Paginate<T>(IEnumerable<T> set, int index, int pageSize, Func<T, string> writer)
        {
            var group = GroupAt(set, index, pageSize);
            StringBuilder page = new StringBuilder();

            foreach (T item in group)
            {
                _ = page.AppendLine(writer.Invoke(item));
            }

            return page.ToString();
        }

        private static string GetPageFooter(int index, int collectionSize, int pageSize)
        {
            return $"Page {index + 1} of {GetPageCount(collectionSize, pageSize)}";
        }

        private static int GetPageCount(int collectionSize, int pageSize)
        {
            return (int)Math.Ceiling((double)collectionSize / pageSize);
        }
    }
}
