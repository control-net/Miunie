using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;

namespace Miunie.Core.XUnit.Tests
{
    public class LanguageResourcesTests
    {
        private readonly ILanguageResources _langResources;
        private readonly DataStorageMock _storage;

        private LangResource[] _phrases;

        private const string PhraseKey = "HELLO_WORLD";
        private readonly string[] _phrasesEng = { "Hello world" };
        private readonly string[] _phrasesEs = { "Hola mundo" };

        private const string FormattedKey = "WELCOME_MESSAGE";
        private readonly string[] _formattedMultipleValues = { "{0} is {1}" };
        private readonly string[] _formattedValues = {
            "Hello, {0}",
            "Hey, {0}"
        };

        private const string Collection = "Lang";
        private const string FormattedMultipleKey = "PERFORM_ACTION";

        public LanguageResourcesTests()
        {
            _storage = new DataStorageMock();
            _langResources = new LanguageResources(_storage, new Random());
            InitializeStorage();
        }

        private void InitializeStorage()
        {
            _phrases = new List<LangResource>()
            {
                new LangResource {
                    Key = PhraseKey,
                    Pool = _phrasesEng
                },
                new LangResource {
                    Key = FormattedKey,
                    Pool = _formattedValues
                },
                new LangResource {
                    Key = FormattedMultipleKey,
                    Pool = _formattedMultipleValues
                }
            }.ToArray();

            var phrasesInEs = new List<LangResource>()
            {
                new LangResource {
                    Key = PhraseKey,
                    Pool = _phrasesEs
                }
            }.ToArray();

            _storage.StoreObject(_phrases, Collection, "PhrasesEn");
            _storage.StoreObject(phrasesInEs, Collection, "PhrasesEs");
        }

        [Fact]
        public void ShouldGetPhrase()
        {
            var actual = _langResources.GetPhrase(PhraseKey);
            var expected = _phrasesEng;
            Assert.Contains(actual, expected);
        }

        [Fact]
        public void ShouldGetTranslatedPhrase()
        {
            _langResources.SetLanguage("Es");
            var actual = _langResources.GetPhrase(PhraseKey);
            var expected = _phrasesEs;
            Assert.Contains(actual, expected);
        }

        [Fact]
        public void ShouldGetFormattedPhraseWithSingleParameter()
        {
            const string text = "Charly";
            var actual = _langResources.GetPhrase(FormattedKey, text);
            var expected = FormatPhrases(_formattedValues, text);
            Assert.Contains(actual, expected);
        }

        [Fact]
        public void ShouldGetFormattedPhraseWithMultipleParameters()
        {
            object[] text = {"Charly", "TDDing"};
            var actual = _langResources.GetPhrase(FormattedMultipleKey, text);
            var expected = FormatPhrases(_formattedMultipleValues, text);
            Assert.Contains(actual, expected);
        }

        [Fact]
        public void ShouldGetFormattedWithTooManyParameters()
        {
            object[] text = { "Charly", "TDDing", "extra param", "extra" };
            var actual = _langResources.GetPhrase(FormattedMultipleKey, text);
            var expected = FormatPhrases(_formattedMultipleValues, text);
            Assert.Contains(actual, expected);
        }

        [Fact]
        public void GetPhraseShouldReturnEmptyStringIfNotFound()
        {
            var key = DateTime.Now.ToLongTimeString();
            var actual = _langResources.GetPhrase(key);
            var expected = string.Empty;
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void GetFormattedShouldReturnEmptyStringIfNotFound()
        {
            var key = DateTime.Now.ToLongTimeString();
            object[] args = { "hello", "world" };
            var actual = _langResources.GetPhrase(key, args);
            var expected = string.Empty;
            Assert.Equal(actual, expected);
        }

        private static IEnumerable<string> FormatPhrases(IEnumerable<string> phrases, params object[] values)
            => phrases.Select(p => string.Format(p, values)).ToArray();
    }
}
