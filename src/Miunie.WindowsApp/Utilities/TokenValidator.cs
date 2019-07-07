using System.Linq;

namespace Miunie.WindowsApp.Utilities
{
    public class TokenValidator
    {
        public bool StringHasValidTokenStructure(string possibleToken)
            => possibleToken.Length == 59 && possibleToken.ElementAt(24) == '.' && possibleToken.ElementAt(31) == '.';
    }
}
