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

        private LangResource[] Phrases;

        private const string PhraseKey = "HELLO_WORLD";
        private string[] PhrasesEng = { "Hello world" };
        private string[] PhrasesEs = { "Hola mundo" };

        private const string FormattedKey = "WELCOME_MESSAGE";
        private readonly string[] FormattedMultipleValues = { "{0} is {1}" };
        private readonly string[] FormattedValues = {
            "Hello, {0}",
            "Hey, {0}"
        };

        private const string Collection = "Lang";
        private const string FormattedMultipleKey = "PERFORM_ACTION";

        public LanguageResourcesTests()
        {
            storage = new DataStorageMock();
            langResources = new LanguageResources(storage, new Random());
            InitializeStorage();
        }

        private void InitializeStorage()
        {
            Phrases = new List<LangResource>()
            {
                new LangResource {
                    Key = PhraseKey,
                    Pool = PhrasesEng
                },
                new LangResource {
                    Key = FormattedKey,
                    Pool = FormattedValues
                },
                new LangResource {
                    Key = FormattedMultipleKey,
                    Pool = FormattedMultipleValues
                }
            }.ToArray();

            var PhrasesInEs = new List<LangResource>()
            {
                new LangResource {
                    Key = PhraseKey,
                    Pool = PhrasesEs
                }
            }.ToArray();

            storage.StoreObject(Phrases, Collection, "PhrasesEn");
            storage.StoreObject(PhrasesInEs, Collection, "PhrasesEs");
        }

        [Fact]
        public void ShouldGetPhrase()
        {
            var actual = langResources.GetPhrase(PhraseKey);
            var expected = PhrasesEng;
            Assert.Contains(actual, expected);
        }

        [Fact]
        public void ShouldGetTranslatedPhrase()
        {
            langResources.SetLanguage("Es");
            var actual = langResources.GetPhrase(PhraseKey);
            var expected = PhrasesEs;
            Assert.Contains(actual, expected);
        }

        [Fact]
        public void ShouldGetFormattedPhraseWithSingleParameter()
        {
            string text = "Charly";
            var actual = langResources.GetPhrase(FormattedKey, text);
            var expected = FormatPhrases(FormattedValues, text);
            Assert.Contains(actual, expected);
        }

        [Fact]
        public void ShouldGetFormattedPhraseWithMultipleParameters()
        {
            string[] text = {"Charly", "TDDing"};
            var actual = langResources.GetPhrase(FormattedMultipleKey, text);
            var expected = FormatPhrases(FormattedMultipleValues, text);
            Assert.Contains(actual, expected);
        }

        [Fact]
        public void ShouldGetFormattedWithTooManyParameters()
        {
            string[] text = { "Charly", "TDDing", "extra param", "extra" };
            var actual = langResources.GetPhrase(FormattedMultipleKey, text);
            var expected = FormatPhrases(FormattedMultipleValues, text);
            Assert.Contains(actual, expected);
        }

        [Fact]
        public void GetPhraseShouldReturnEmptyStringIfNotFound()
        {
            var key = DateTime.Now.ToLongTimeString();
            var actual = langResources.GetPhrase(key);
            var expected = string.Empty;
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void GetFormattedShouldReturnEmptyStringIfNotFound()
        {
            var key = DateTime.Now.ToLongTimeString();
            string[] args = { "hello", "world" };
            var actual = langResources.GetPhrase(key, args);
            var expected = string.Empty;
            Assert.Equal(actual, expected);
        }

        private string[] FormatPhrases(string[] phrases, params string[] values)
            => phrases.Select(p => String.Format(p, values)).ToArray();
    }
}
