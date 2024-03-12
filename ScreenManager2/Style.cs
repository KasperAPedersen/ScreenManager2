using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenManager2
{
    internal static  class Style
    {
        internal static string Set(string _text, List<Styling>? _styles = null)
        {
            if (_styles != null)
            {
                string tmp = "";
                foreach (Styling style in _styles)
                {
                    tmp += $"\u001b[{(int)style}m";
                }
                tmp += $"{_text}\u001b[0m";
                return tmp;
            }

            return _text;
        }
    }

    public enum Styling
    {
        None = 0,
        Bold = 1,
        Italic = 3,
        Underline = 4,
        Reversed = 7,
        Blink = 5,
        DoubleUnderline = 21,
        Crossed = 9,

        BgBlack = 40,
        BgRed = 41,
        BgGreen = 42,
        BgYellow = 43,
        BgBlue = 44,
        BgMagenta = 45,
        BgCyan = 46,
        BgWhite = 47
    }
}
