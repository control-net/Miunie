using Miunie.Core.Providers;
using Miunie.Core.Storage;
using Moq;
using Xunit;

namespace Miunie.Core.XUnit.Tests.Providers
{
    public class RemoteRepositoryProviderTests
    {
        private readonly Mock<IPersistentStorage> _persistentStorageMock;
        private readonly IRemoteRepositoryProvider _provider;

        public RemoteRepositoryProviderTests()
        {
            _persistentStorageMock = new Mock<IPersistentStorage>();
            _provider = new RemoteRepositoryProvider(_persistentStorageMock.Object);
        }

        [Fact]
        public void ShouldSaveRemoteUrl()
        {
            _provider.SetRemoteUrl("remote url");
            _persistentStorageMock
                .Verify(s => s.Store(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }
    }
}

