using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenManager2
{
    internal class Padder
    {
        internal static string Set(string _text, Padding _padding, string _pattern = " ")
        {
            return string.Concat(Enumerable.Repeat(_pattern, (int)_padding)) + _text + string.Concat(Enumerable.Repeat(_pattern, (int)_padding));
        }
    }

    public enum Padding
    {
        None = 0,
        Small = 2,
        Medium = 4,
        Large = 8
    }
}
