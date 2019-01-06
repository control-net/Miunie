using System;

namespace Miunie.Storage.XUnit.Tests
{
    public class JsonDataStorageFixture : IDisposable
    {
        public string ResourcesFolder { get; set; }
        public JsonDataStorage Storage { get; }

        public JsonDataStorageFixture()
        {
            ResourcesFolder = String.Concat("ResourcesTests", DateTimeOffset.Now.ToUnixTimeMilliseconds());
            Storage = new JsonDataStorage(ResourcesFolder);
        }
        public void Dispose()
        {
            Storage.WipeData();
        }
    }
}