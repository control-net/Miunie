using System;

namespace Miunie.Core.Assertion
{
    public static class Assert
    {
        public static void NotNull(object obj, string message)
        {
            if(obj is null)
            {
                throw new ArgumentException(message);
            }
        }
    }
}
