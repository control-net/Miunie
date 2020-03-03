using Moq;
using System;
using Xunit;

namespace Miunie.Core.XUnit.Tests
{
    public class LangResourceTests
    {
        private readonly Mock<Random> _randMock;

        public LangResourceTests()
        {
            _randMock = new Mock<Random>();
        }

        [Fact]
        public void NullPool_ShouldReturnEmptyString()
        {
            var rand = GetRandomReturning(0);
            var res = new LangResource
            {
                Key = "Key",
                Pool = null
            };

            Assert.Equal(string.Empty, res.GetValue(rand));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void ValidPool_ShouldReturnCorrectElement(int element)
        {
            var rand = GetRandomReturning(element);
            var pool = new string[] { "FIRST", "SECOND", "THIRD" };
            var res = new LangResource
            {
                Key = "Key",
                Pool = pool
            };

            Assert.Equal(pool[element], res.GetValue(rand));
        }

        private Random GetRandomReturning(int value)
        {
            _randMock.Setup(r => r.Next(It.IsAny<int>())).Returns(value);
            return _randMock.Object;
        }
    }
}
