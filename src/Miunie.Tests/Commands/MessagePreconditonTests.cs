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
            var input = CreateInputWithMessage("Message without a prefix");
            var nextStep = new Mock<ICommandPipelineStep>();
            var step = new PreconditionCheckStep(nextStep.Object, botConfig);

            await step.ProcessAsync(input);

            AssertNextStepIsNotCalled(nextStep);
        }

        [Fact]
        public async Task MessageWithPrefixCallsNextStep()
        {
            const uint ExpectedPrefixOffset = 8;
            var botConfig = CreateBotConfigWithPrefix("MyPrefix");
            var input = CreateInputWithMessage("MyPrefix Message with a prefix");
            var nextStep = new Mock<ICommandPipelineStep>();
            var step = new PreconditionCheckStep(nextStep.Object, botConfig);

            await step.ProcessAsync(input);

            AssertNextStepIsCalled(nextStep);
            Assert.Equal(ExpectedPrefixOffset, input.PrefixOffset);
        }

        [Fact]
        public async Task NoPrefixSetIgnoreAllMessages()
        {
            const uint ExpectedPrefixOffset = 0;
            var botConfig = CreateBotConfigWithNoPrefix();
            var input = CreateInputWithMessage("Any Message");
            var nextStep = new Mock<ICommandPipelineStep>();
            var step = new PreconditionCheckStep(nextStep.Object, botConfig);

            await step.ProcessAsync(input);

            AssertNextStepIsCalled(nextStep);
            Assert.Equal(ExpectedPrefixOffset, input.PrefixOffset);
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
