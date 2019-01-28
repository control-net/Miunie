using System;
using Xunit;


namespace Miunie.Core.XUnit.Tests
{
    public class LanguageResourcesTests
    {
        private readonly LanguageResources langResources;
        private readonly DataStorageMock storage;
        private const string Collection = "Lang";
        private const string PhraseKey = "HELLO_WORLD";
        private const string PhraseValue = "Hello world";
        private const string FormattedKey = "WELCOME_MESSAGE";
        private const string FormattedValue = "Hello, {0}";
        private const string FormattedMultipleKey = "PERFORM_ACTION";
        private const string FormattedMultipleValue = "{0} is {1}";

        public LanguageResourcesTests()
        {
            storage = new DataStorageMock();
            langResources = new LanguageResources(storage);
            InitializeStorage();
        }

        private void InitializeStorage()
        {
            storage.StoreObject(PhraseValue, Collection, PhraseKey);
            storage.StoreObject(FormattedValue, Collection, FormattedKey);
            storage.StoreObject(
                FormattedMultipleValue, 
                Collection,
                FormattedMultipleKey);
        }
        
        [Fact]
        public void ShouldGetPhrase()
        {
            var actual = langResources.GetPhrase(PhraseKey);
            var expected = PhraseValue;
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void ShouldGetFormattedPhraseWithSingleParameter()
        {
            string text = "Charly";
            var actual = langResources.GetFormatted(FormattedKey, text);
            var expected = String.Format(FormattedValue, text);
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void ShouldGetFormattedPhraseWithMultipleParameters()
        {
            string[] text = {"Charly", "TDDing"};
            var actual = langResources.GetFormatted(FormattedMultipleKey, text);
            var expected = String.Format(FormattedMultipleValue, text);
            Assert.Equal(actual, expected);
        }
    }
}
