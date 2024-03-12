using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenManager2
{
    internal class Label : Object
    {
        public Label(string _text, Padding _padding, List<Styling>? _styles = null, Colors? _color = null, Parent? _parent = null, Pos? _pos = null) : base(_parent ?? new Parent(new Pos(0,0), new Dim(Console.WindowWidth, Console.WindowHeight)), _pos ?? new Pos(2, 1), new Dim(Console.WindowWidth, Console.WindowHeight))
        {
            this.Pos = new Pos(this.Pos.X + this.Parent.Pos.X, this.Pos.Y + this.Parent.Pos.Y);

            Render(this.Pos, Style.Set(Color.Set(Padder.Set(_text, _padding), _color), _styles));
        }
    }
}
