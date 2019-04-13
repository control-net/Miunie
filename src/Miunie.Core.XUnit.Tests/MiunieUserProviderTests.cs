using Miunie.Core.Storage;
using Miunie.Core.Providers;
using Moq;
using Xunit;

namespace Miunie.Core.XUnit.Tests
{
    public class MiunieUserProviderTests
    {
        //private readonly IMiunieUserProvider _provider;
        //private readonly Mock<IPersistentStorage> _persistentStorageMock;

        //public MiunieUserProviderTests()
        //{
        //    _persistentStorageMock = new Mock<IPersistentStorage>();
        //    _provider = new MiunieUserProvider(_persistentStorageMock.Object);
        //}

        //[Fact]
        //public void NonExistentUserGetsCreated()
        //{
        //    var actual = _provider.GetById(100, 200);
        //    Assert.NotNull(actual);
        //    _persistentStorageMock
        //        .Verify(s => s.Store(It.IsAny<MiunieUser>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        //}
    }
}

