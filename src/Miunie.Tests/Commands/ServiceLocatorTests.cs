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

using Miunie.Core.Commands;
using Miunie.Core.Commands.PipelineSteps;
using Miunie.Core.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Miunie.Tests.Commands
{
    public class ServiceLocatorTests
    {
        private readonly Mock<ICommandPipelineStep> _nextStepMock;
        private readonly Mock<IMiunieServiceCollection> _servicesMock;
        private readonly Mock<ILogWriter> _logWriterMock;
        private readonly ICommandPipelineStep _serviceLocationStep;

        public ServiceLocatorTests()
        {
            _nextStepMock = new Mock<ICommandPipelineStep>();
            _servicesMock = new Mock<IMiunieServiceCollection>();
            _logWriterMock = new Mock<ILogWriter>();
            _serviceLocationStep = new ServiceLocationStep(_nextStepMock.Object, _servicesMock.Object, _logWriterMock.Object);
        }

        [Fact]
        public async Task NullCommandCollection_ShouldNotContinueOrThrow()
        {
            SetAvailableCommands(null);
            var input = GetInputFromMessage("Any message.");

            await _serviceLocationStep.ProcessAsync(input);

            AssertNextStepNotCalled();
            AssertLogsAdded(1);
        }

        [Fact]
        public async Task NoAvailableCommands_ShouldNotContinue()
        {
            SetAvailableCommands(new MiunieServiceCommand[0]);
            var input = GetInputFromMessage("Any message.");

            await _serviceLocationStep.ProcessAsync(input);

            AssertNextStepNotCalled();
        }

        [Theory]
        [InlineData("command")]
        [InlineData("command  ")]
        [InlineData("command 1 2 3 4")]
        [InlineData("command @User")]
        public async Task NoPrefix_MatchingCommand_ShouldContinue_WithValidTargetServices(string command)
        {
            SetAvailableCommandsFromStrings("command", "different", "command ANOTHER");
            var input = GetInputFromMessage(command);

            await _serviceLocationStep.ProcessAsync(input);

            AssertTargetedCommands("command");
        }

        [Theory]
        [InlineData("@prefix command")]
        [InlineData("@prefixcommand")]
        [InlineData("@prefix command  ")]
        [InlineData("@prefixcommand 1 2 3 4")]
        [InlineData("@prefix command @User")]
        public async Task Prefix_MatchingCommand_ShouldContinue_WithValidTargetServices(string command)
        {
            SetAvailableCommandsFromStrings("command", "different", "command ANOTHER");
            var input = GetInputFromMessage(command, "@prefix");

            await _serviceLocationStep.ProcessAsync(input);

            AssertTargetedCommands("command");
        }

        [Theory]
        [InlineData("command 1")]
        [InlineData("command 1 ")]
        [InlineData("command 1 2 3 4")]
        [InlineData("command 1 @User")]
        public async Task NoPrefix_MatchingCommands_ShouldContinue_WithValidTargetServices(string command)
        {
            SetAvailableCommandsFromStrings("command", "command 1", "command ANOTHER");
            var input = GetInputFromMessage(command);

            await _serviceLocationStep.ProcessAsync(input);

            AssertTargetedCommands("command", "command 1");
        }

        [Fact]
        public async Task NoMatchingCommands_ShouldContinue_WithEmptyTargetServices()
        {
            SetAvailableCommandsFromStrings("command", "command 2");
            var input = GetInputFromMessage("Normal message.");

            await _serviceLocationStep.ProcessAsync(input);

            AssertTargetedCommands(new string[0]);
        }

        private static CommandProcessorInput GetInputFromMessage(string message)
        {
            return new CommandProcessorInput
            {
                Message = message
            };
        }

        private static CommandProcessorInput GetInputFromMessage(string message, string prefix)
        {
            return new CommandProcessorInput
            {
                Message = message,
                PrefixOffset = (uint)prefix.Length
            };
        }

        private void SetAvailableCommandsFromStrings(params string[] commands)
        {
            var commandObjects = commands.Select(c => new MiunieServiceCommand { Prompt = c });
            SetAvailableCommands(commandObjects);
        }

        private void AssertTargetedCommands(params string[] commands)
            => _nextStepMock.Verify(s => s.ProcessAsync(It.Is<CommandProcessorInput>(i => GetInputMatchesCommands(i, commands))), Times.Once);

        private bool GetInputMatchesCommands(CommandProcessorInput i, string[] commands)
            => i.TargetedCommands.Select(c => c.Prompt).SequenceEqual(commands);

        private void AssertLogsAdded(int count)
        {
            _logWriterMock.Verify(lw => lw.Log(It.IsAny<string>()), Times.Exactly(count));
        }

        private void SetAvailableCommands(IEnumerable<MiunieServiceCommand> miunieServiceCommands)
        {
            _ = _servicesMock.Setup(s => s.GetAvailableCommands()).Returns(miunieServiceCommands);
        }

        private void AssertNextStepNotCalled()
        {
            _nextStepMock.VerifyNoOtherCalls();
        }
    }
}
