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

using Discord.Commands;
using Miunie.Core.Providers;
using Miunie.Discord.Attributes;
using Miunie.Discord.Embeds;
using System.Threading.Tasks;

namespace Miunie.Discord.CommandModules
{
    [Name("Help")]
    public class HelpCommand : ModuleBase<SocketCommandContext>
    {
        private readonly CommandHelpProvider _helpProvider;

        public HelpCommand(CommandService commandService, ILanguageProvider lang)
        {
            _helpProvider = new CommandHelpProvider(commandService, lang);
        }

        [Command("help")]
        [Summary("I'm here for you.")]
        [Examples("help")]
        public async Task GetHelp()
        {
            var helpResult = _helpProvider.ForAllCommands();
            _ = await Context.Channel.SendMessageAsync(embed: EmbedConstructor.CreateHelpEmbed(helpResult));
        }

        [Command("help")]
        [Summary("Gets help for a specified command.")]
        [Examples("help repo")]
        public async Task GetHelp([Remainder]string input)
        {
            var helpResult = _helpProvider.FromInput(input);
            _ = await Context.Channel.SendMessageAsync(embed: EmbedConstructor.CreateHelpEmbed(helpResult));
        }
    }
}
