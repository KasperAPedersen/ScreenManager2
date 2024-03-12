using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenManager2
{
    internal class Aligner
    {
        public static string Align(int maxWidth, Alignment? alignment = Alignment.Left, string pattern = " ", string? text = null)
        {
            if (text == null) return string.Concat(Enumerable.Repeat(pattern, maxWidth));
            if (text.Length > maxWidth) return text;

            int diff = maxWidth - text.Length, remainder = (maxWidth - text.Length) % 2;
            string right, left;

            switch (alignment)
            {
                case Alignment.Right:
                    left = string.Concat(Enumerable.Repeat(pattern, (diff / pattern.Length)));

                    if (remainder != 0) left += string.Concat(pattern[0]);

                    return left + text;
                case Alignment.Center:
                    diff /= 2;
                    right = left = string.Concat(Enumerable.Repeat(pattern, (diff / pattern.Length)));

                    if (remainder != 0) right += string.Concat(pattern[0]);

                    return left + text + right;
                default:
                    right = string.Concat(Enumerable.Repeat(pattern, diff / pattern.Length));
                    if (remainder != 0) right += string.Concat(pattern[0]);

                    return text + right;
            }
        }
    }

    public enum Alignment
    {
        Left = 0,
        Center = 1,
        Right = 2
    }
}
