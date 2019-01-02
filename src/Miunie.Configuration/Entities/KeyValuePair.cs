namespace Miunie.Configuration.Entities
{
    public class KeyValuePair
    {
        public string Key { get; private set; }
        public string Value { get; private set; }

        public KeyValuePair(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
