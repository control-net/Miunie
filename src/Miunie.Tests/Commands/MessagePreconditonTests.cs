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
using Miunie.Core.Configuration;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Miunie.Tests.Commands
{
    public class MessagePreconditonTests
    {
        [Fact]
        public async Task MessageWithNoPrefixIsIgnored()
        {
            var botConfig = CreateBotConfigWithPrefix("MyPrefix");
            var firstStepContext = CreateInputWithMessage("Message without a prefix");
            var nextStep = new Mock<ICommandPipelineStep>();
            var step = new PreconditionCheckStep(nextStep.Object, botConfig);

            await step.ProcessAsync(firstStepContext);

            AssertNextStepIsNotCalled(nextStep);
        }

        [Fact]
        public async Task MessageWithPrefixCallsNextStep()
        {
            var botConfig = CreateBotConfigWithPrefix("MyPrefix");
            var firstStepContext = CreateInputWithMessage("MyPrefix Message with a prefix");
            var nextStep = new Mock<ICommandPipelineStep>();
            var step = new PreconditionCheckStep(nextStep.Object, botConfig);

            await step.ProcessAsync(firstStepContext);

            AssertNextStepIsCalled(nextStep);
        }

        [Fact]
        public async Task NoPrefixSetIgnoreAllMessages()
        {
            var botConfig = CreateBotConfigWithNoPrefix();
            var firstStepContext = CreateInputWithMessage("Any Message");
            var nextStep = new Mock<ICommandPipelineStep>();
            var step = new PreconditionCheckStep(nextStep.Object, botConfig);

            await step.ProcessAsync(firstStepContext);

            AssertNextStepIsCalled(nextStep);
        }

        private void AssertNextStepIsNotCalled(Mock<ICommandPipelineStep> step)
            => step.Verify(s => s.ProcessAsync(It.IsAny<CommandProcessorInput>()), Times.Never());

        private void AssertNextStepIsCalled(Mock<ICommandPipelineStep> step)
            => step.Verify(s => s.ProcessAsync(It.IsAny<CommandProcessorInput>()), Times.Once());

        private IBotConfiguration CreateBotConfigWithPrefix(string prefix)
            => new BotConfiguration
            {
                CommandPrefix = prefix
            };

        private IBotConfiguration CreateBotConfigWithNoPrefix()
            => new BotConfiguration
            {
                CommandPrefix = string.Empty
            };

        private CommandProcessorInput CreateInputWithMessage(string message)
            => new CommandProcessorInput
            {
                Message = message
            };
    }
}
