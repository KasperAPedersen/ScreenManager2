using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenManager2
{
    internal class Label : Object
    {
        public Label(string _text, List<object>? _styles = null, Parent? _parent = null, Pos? _pos = null) : base(_parent ?? new Parent(new Pos(0,0), new Dim(Console.WindowWidth, Console.WindowHeight)), _pos ?? new Pos(2, 1), new Dim(Console.WindowWidth, Console.WindowHeight))
        {
            this.Pos = new Pos(this.Pos.X + this.Parent.Pos.X, this.Pos.Y + this.Parent.Pos.Y);

            Render(this.Pos, Style.Set(Style.Set(_text, Style.Get([typeof(Padding)], _styles)), Style.Get([typeof(FontColor), typeof(FontStyling), typeof(FontBgColor)], _styles)));
        }
    }
}
