using System;

namespace Miunie.Core
{
    public class LangResource
    {
        public string Key { get; set; }
        public string[] Pool { get; set; }

        public string GetValue(Random r)
        {
            if (Pool is null) { return String.Empty; }
            var index = r.Next(Pool.Length);
            return Pool[index];
        }
    }
}
