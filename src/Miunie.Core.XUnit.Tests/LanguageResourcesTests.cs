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
        private const string Collection = "LangResources";
        private Dictionary<string, string[]> Phrases;
        private const string PhraseKey = "HELLO_WORLD";
        private string[] PhraseValue = { "Hello world" };
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
            storage.StoreObject(Phrases, Collection, "PhrasesEn");
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
            var actual = langResources.GetPhrase(PhraseKey, "En");
            var expected = PhraseValue;
            Assert.Contains(actual, expected);
        }

        [Fact]
        public void ShouldGetFormattedPhraseWithSingleParameter()
        {
            string text = "Charly";
            var actual = langResources.GetFormatted(FormattedKey, text);
            var expected = FormatPhrases(FormattedValue, text);
            Assert.Contains(actual, expected);
        }

        [Fact]
        public void ShouldGetTranslatedFormattedPhrase()
        {
            string text = "Charly";
            var actual = langResources.GetTranslatedFormatted(
                                                FormattedKey,
                                                "En",
                                                text);
            var expected = FormatPhrases(FormattedValue, text);
            Assert.Contains(actual, expected);
        }

        [Fact]
        public void ShouldGetFormattedPhraseWithMultipleParameters()
        {
            string[] text = {"Charly", "TDDing"};
            var actual = langResources.GetFormatted(FormattedMultipleKey, text);
            var expected = FormatPhrases(FormattedMultipleValue, text);
            Assert.Contains(actual, expected);
        }

        [Fact]
        public void ShouldGetFormattedWithTooManyParameters()
        {
            string[] text = { "Charly", "TDDing", "extra param", "extra" };
            var actual = langResources.GetFormatted(FormattedMultipleKey, text);
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
            var actual = langResources.GetFormatted(key, args);
            var expected = String.Empty;
            Assert.Equal(actual, expected);
        }
    }
}
