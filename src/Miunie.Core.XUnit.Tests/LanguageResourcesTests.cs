using System;
using Xunit;


namespace Miunie.Core.XUnit.Tests
{
    public class LanguageResourcesTests
    {
        LanguageResources langResources;
        DataStorageMock storage;
        string Collection = "Lang";
        string PhraseKey = "HELLO_WORLD";
        string PhraseValue = "Hello world";
        string FormattedKey = "WELCOME_MESSAGE";
        string FormattedValue = "Hello, {0}";
        string FormattedMultipleKey = "PERFORM_ACTION";
        string FormattedMultipleValue = "{0} is {1}";

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
