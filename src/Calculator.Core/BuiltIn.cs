using System;

namespace Calculator
{
    // Todo: Move to another place!
    internal static class BuiltIn
    {
        internal static decimal Factorial(decimal value)
        {
            var result = 1m;

            while (value != 1)
            {
                result *= value;
                value -= 1;
            }

            return result;
        }
    }
}