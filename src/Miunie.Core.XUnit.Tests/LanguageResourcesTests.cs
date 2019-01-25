using System;
using Xunit;


namespace Miunie.Core.XUnit.Tests
{
    public class LanguageResourcesTests
    {
        LanguageResources langResources;
        DataStorageMock storage;
        string languageCollection = "Lang";
        string PhraseKey = "HELLO_WORLD";
        string PhraseValue = "Hello world";
        string FormattedKey = "WELCOME_MESSAGE";
        string FormattedValue = "Hello, {0}";

        public LanguageResourcesTests()
        {
            storage = new DataStorageMock();
            langResources = new LanguageResources(storage);
            InitializeStorage();
        }

        private void InitializeStorage()
        {
            storage.StoreObject(PhraseValue, languageCollection, PhraseKey);
            storage.StoreObject(FormattedValue, languageCollection, FormattedKey);
        }
        
        [Fact]
        public void ShouldGetPhrase()
        {
            var actual = langResources.GetPhrase(PhraseKey);
            var expected = PhraseValue;
            Assert.Equal(actual, expected);
        }
    }
}
