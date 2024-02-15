using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharpControls.Validation
{
    internal static partial class ValidationRegex
    {
        [GeneratedRegex("[^0-9.]")]
        internal static partial Regex OnlyNumbers();

        [GeneratedRegex("[^a-zA-Z]")]
        internal static partial Regex OnlyAlpha();

        [GeneratedRegex("[^a-zA-Z-]")]
        internal static partial Regex OnlyAlphaDash();
    }
}
