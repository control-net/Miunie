using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Miunie.Storage.XUnit.Tests.Helpers
{
    public class TestDataHelper
    {
        private Dictionary<string, DummyDataHolder> testKeyDataPairs;
        private const string DummyDataPath = "Resources/DummyDataHolder/Test/Collection/";

        public TestDataHelper()
        {
            CreateTestKeyDataPairs();
            GenerateTestFiles();
        }

        public void DeleteTestData()
            => Directory.Delete("Resources", true);

        ///<summary>
        /// Returns a stored test object by key for
        /// the "expected" value of a test.
        ///</summary>
        public DummyDataHolder GetDummyByKey(string key)
            => testKeyDataPairs
                .First(p => p.Key == key)
                .Value;

        public IEnumerable<DummyDataHolder> GetAllDummyFiles()
            => testKeyDataPairs.Values.ToArray();

        public IEnumerable<DummyDataHolder> GetDummyByKeyContains(string pattern)
            => testKeyDataPairs
                .Where(p => p.Key.Contains(pattern))
                .Select(p => p.Value);

        private void CreateTestKeyDataPairs()
        {
            testKeyDataPairs = new Dictionary<string, DummyDataHolder>
            {
                {
                    "DataKey-A",
                    new DummyDataHolder
                    {
                        Text = "Data A",
                        Number = 1
                    }
                },
                {
                    "DataKey-B",
                    new DummyDataHolder
                    {
                        Text = "Data B",
                        Number = 2
                    }
                },
                {
                    "SpecialKey",
                    new DummyDataHolder
                    {
                        Text = "Special",
                        Number = 99
                    }
                }
            };
        }

        private void GenerateTestFiles()
        {
            foreach(var pair in testKeyDataPairs)
            {
                GenerateJsonFileFor(pair.Value, pair.Key);
            }
        }

        private void GenerateJsonFileFor(DummyDataHolder data, string key)
        {
            var json = GetJsonOf(data);
            var path = GetDummyDataPath(key);
            EnsurePathExists(path);
            File.WriteAllText(path, json);
        }

        private string GetJsonOf(DummyDataHolder data)
            => $"{{\"Text\":\"{data.Text}\",\"Number\":{data.Number}}}";

        private string GetDummyDataPath(string key)
            => Path.Combine(DummyDataPath, $"{key}.json");

        private void EnsurePathExists(string path)
            => Directory.CreateDirectory(Path.GetDirectoryName(path));    
    }
}

