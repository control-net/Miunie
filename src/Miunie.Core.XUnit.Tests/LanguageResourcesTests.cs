using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;


namespace Miunie.Core.XUnit.Tests
{
    public class LanguageResourcesTests
    {
        private readonly ILanguageResources langResources;
        private readonly DataStorageMock storage;
        private const string Collection = "Lang";
        private Dictionary<string, string[]> Phrases;
        private const string PhraseKey = "HELLO_WORLD";
        private string[] PhraseValue = { "Hello world" };
        private string[] PhraseValueEs = { "Hola mundo" };
        private const string FormattedKey = "WELCOME_MESSAGE";
        private readonly string[] FormattedValue = { "Hello, {0}", "Hey, {0}" };
        private const string FormattedMultipleKey = "PERFORM_ACTION";
        private readonly string[] FormattedMultipleValue = { "{0} is {1}" };

        public LanguageResourcesTests()
        {
            storage = new DataStorageMock();
            var rand = new Random();
            langResources = new LanguageResources(storage, rand);
            InitializeStorage();
        }
        private void InitializeStorage()
        {

            Phrases = new Dictionary<string, string[]>()
            {
                {PhraseKey, PhraseValue},
                {FormattedKey, FormattedValue},
                {FormattedMultipleKey, FormattedMultipleValue}
            };

            var PhrasesEs = new Dictionary<string, string[]>()
            {
                {PhraseKey, PhraseValueEs}
            };

            storage.StoreObject(Phrases, Collection, "PhrasesEn");
            storage.StoreObject(PhrasesEs, Collection, "PhrasesEs");
        }
        
        [Fact]
        public void ShouldGetPhrase()
        {
            var actual = langResources.GetPhrase(PhraseKey);
            var expected = PhraseValue;
            Assert.Contains(actual, expected);
        }

        [Fact]
        public void ShouldGetTranslatedPhrase()
        {
            langResources.SetLanguage("Es");
            var actual = langResources.GetPhrase(PhraseKey);
            var expected = PhraseValueEs;
            Assert.Contains(actual, expected);
        }

        [Fact]
        public void ShouldGetFormattedPhraseWithSingleParameter()
        {
            string text = "Charly";
            var actual = langResources.GetPhrase(FormattedKey, text);
            var expected = FormatPhrases(FormattedValue, text);
            Assert.Contains(actual, expected);
        }

        [Fact]
        public void ShouldGetFormattedPhraseWithMultipleParameters()
        {
            string[] text = {"Charly", "TDDing"};
            var actual = langResources.GetPhrase(FormattedMultipleKey, text);
            var expected = FormatPhrases(FormattedMultipleValue, text);
            Assert.Contains(actual, expected);
        }

        [Fact]
        public void ShouldGetFormattedWithTooManyParameters()
        {
            string[] text = { "Charly", "TDDing", "extra param", "extra" };
            var actual = langResources.GetPhrase(FormattedMultipleKey, text);
            var expected = FormatPhrases(FormattedMultipleValue, text);
            Assert.Contains(actual, expected);
        }

        private string[] FormatPhrases(string[] phrases, params string[] values)
            => phrases.Select(p => String.Format(p, values)).ToArray();

        [Fact]
        public void GetPhraseShouldReturnEmptyStringIfNotFound()
        {
            var key = DateTime.Now.ToLongTimeString();
            var actual = langResources.GetPhrase(key);
            var expected = String.Empty;
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void GetFormattedShouldReturnEmptyStringIfNotFound()
        {
            var key = DateTime.Now.ToLongTimeString();
            string[] args = {"hello", "world"};
            var actual = langResources.GetPhrase(key, args);
            var expected = String.Empty;
            Assert.Equal(actual, expected);
        }
    }
}
