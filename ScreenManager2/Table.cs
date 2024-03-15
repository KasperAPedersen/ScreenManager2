using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenManager2
{
    struct TableItems(List<string> _headers, List<string> _content)
    {
        public List<string> headers = _headers;
        public List<string> content = _content;

    }

    internal class Table : Object
    {
        private int usePadding = 0;
        public Table(Dim _dim, TableItems _tableItems, List<object>? _styles = null, Parent? _parent = null, Pos? _pos = null) : base(_parent ?? new Parent(new Pos(0, 0), new Dim(Console.WindowWidth, Console.WindowHeight)), _pos ?? new Pos(2, 1), _dim)
        {

            // get padding style - if any
            if (_styles != null)
            {
                List<object> padding = Style.Get([typeof(Padding)], _styles);
                if (padding != null)
                {
                    foreach (object o in padding)
                    {
                        usePadding = (int)o;
                    }
                }
            }
            Render(new Pos(this.Parent.Pos.X + this.Pos.X, this.Parent.Pos.Y + this.Pos.Y), string.Concat(Enumerable.Repeat("-", this.Parent.Dim.Width - this.Pos.X - 2)));
        }
    }
}
