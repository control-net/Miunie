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
using Miunie.Core.Entities;
using Miunie.Core.Providers;
using Miunie.Discord.Attributes;
using Miunie.Discord.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Miunie.Discord
{
    public class CommandHelpProvider
    {
        private readonly CommandService _commandService;
        private readonly ILanguageProvider _lang;

        public CommandHelpProvider(CommandService commandService, ILanguageProvider lang)
        {
            _commandService = commandService;
            _lang = lang;
        }

        public HelpResult ForAllCommands()
            => new HelpResult
            {
                Title = _lang.GetPhrase(PhraseKey.USER_EMBED_HELP_TITLE.ToString()),
                Sections = _commandService.Modules.Select(x => GetSection(x))
            };

        public HelpResult FromInput(string input)
            => new HelpResult()
            {
                Sections = GetCommands(input).Select(x => GetSection(x))
            };

        private IEnumerable<CommandInfo> GetCommands(string input)
        {
            SearchResult result = _commandService.Search(input);

            if (!result.IsSuccess)
            {
                throw new Exception(result.ErrorReason);
            }

            return result.Commands.Select(x => x.Command);
        }

        private HelpSection GetSection(ModuleInfo module)
            => new HelpSection(module.Name, GetModuleCommandBlocks(module));

        private string GetModuleCommandBlocks(ModuleInfo module)
        {
            var commands = module.Commands
                .GroupBy(x => x.Name)
                .Select(x => $"`{x.Key}`");

            return string.Join(" ", commands);
        }

        private HelpSection GetSection(CommandInfo command)
        {
            string title = GetSectionTitle(command);
            string summary = GetSummary(command);
            string examples = GetExamples(command);

            var content = new StringBuilder();

            _ = content.Append(_lang.GetPhrase(PhraseKey.HELP_SUMMARY_TITLE.ToString()))
                .AppendLine(summary)
                .Append(_lang.GetPhrase(PhraseKey.HELP_EXAMPLE_TITLE.ToString()))
                .Append(examples);

            return new HelpSection(title, content.ToString());
        }

        private string GetSectionTitle(CommandInfo command)
            => $"{command.Name} {GetSectionParameters(command)}";

        private string GetSectionParameters(CommandInfo command)
        {
            var parameters = command.Parameters
                .OrderBy(x => x.IsOptional)
                .Select(x => GetParameterBlock(x));

            return string.Join(" ", parameters);
        }

        private string GetParameterBlock(ParameterInfo parameter)
            => parameter.IsOptional ? $"[{parameter.Name}]" : $"<{parameter.Name}>";

        private string GetExamples(CommandInfo command)
        {
            var examples = command.FindAttribute<ExamplesAttribute>()?.Examples;

            if (examples is null || !examples.Any())
            {
                return _lang.GetPhrase(PhraseKey.HELP_EXAMPLE_EMPTY.ToString());
            }

            return string.Join(", ", examples.Select(x => $"`{x}`"));
        }

        private string GetSummary(CommandInfo command)
        {
            if (string.IsNullOrWhiteSpace(command.Summary))
            {
                return _lang.GetPhrase(PhraseKey.HELP_SUMMARY_EMPTY.ToString());
            }

            return command.Summary;
        }
    }
}
