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

using Miunie.Core.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Miunie.Core.Commands.PipelineSteps
{
    public class ServiceLocationStep : CommandPipelineStep
    {
        private readonly IMiunieServiceCollection _serviceCollection;
        private readonly ILogWriter _logWriter;

        public ServiceLocationStep(ICommandPipelineStep step, IMiunieServiceCollection serviceCollection, ILogWriter logWriter)
            : base(step)
        {
            _serviceCollection = serviceCollection;
            _logWriter = logWriter;
        }

        public override Task ProcessAsync(CommandProcessorInput input)
        {
            var commands = _serviceCollection.GetAvailableCommands();

            if (commands is null || !commands.Any())
            {
                _logWriter.Log("Service Location did not get any available commands.");
                return Task.CompletedTask;
            }

            var prompt = input.Message.Substring((int)input.PrefixOffset).Trim();

            input.TargetedCommands = commands.Where(c => prompt.StartsWith(c.Prompt));
            return NextStep.ProcessAsync(input);
        }
    }
}
