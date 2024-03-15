using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenManager2
{

    internal class Box : Object
    {
        private readonly int currentHeight = 0;
        
        public Box(Dim _dim, List<object>? _styles = null, Parent? _parent = null, Pos? _pos = null) : base(_parent ?? new Parent(new Pos(0, 0), new Dim(Console.WindowWidth, Console.WindowHeight)), _pos ?? new Pos(2, 1), _dim)
        {
            // Check width & height
            if (this.Dim.Width + this.Pos.X >= this.Parent.Dim.Width) this.Dim = new(this.Parent.Dim.Width - this.Pos.X - 2, this.Dim.Height);
            if (this.Dim.Height + this.Pos.Y >= this.Parent.Dim.Height) this.Dim = new(this.Dim.Width, this.Parent.Dim.Height - this.Pos.Y - 1);

            this.Pos = new Pos(this.Pos.X + this.Parent.Pos.X, this.Pos.Y + this.Parent.Pos.Y);


            Render(this.Pos, Style.Set($"{Border(Get.TopLeft)}{Aligner.Align(this.Dim.Width - 2, default, Border(Get.Horizontal), null)}{Border(Get.TopRight)}", _styles));
            for (int i = 0; i < this.Dim.Height; i++) Render(new Pos(this.Pos.X, this.Pos.Y + ++currentHeight), Style.Set(Border(Get.Vertical), _styles) + Aligner.Align(this.Dim.Width - 2, default, " ", default) + Style.Set(Border(Get.Vertical), _styles));
            Render(new Pos(this.Pos.X, this.Pos.Y + ++currentHeight), Style.Set($"{Border(Get.BottomLeft)}{Aligner.Align(this.Dim.Width - 2, default, Border(Get.Horizontal), null)}{Border(Get.BottomRight)}", _styles));
        }

        public Box(Dim _dim, string _text, List<object>? _styles = null, Parent? _parent = null, Pos? _pos = null) : base(_parent ?? new Parent(new Pos(0, 0), new Dim(Console.WindowWidth, Console.WindowHeight)), _pos ?? new Pos(2, 1), _dim)
        {
            // Check width & height
            if (this.Dim.Width + this.Pos.X >= this.Parent.Dim.Width) this.Dim = new(this.Parent.Dim.Width - this.Pos.X - 2, this.Dim.Height);
            if (this.Dim.Height + this.Pos.Y >= this.Parent.Dim.Height) this.Dim = new(this.Dim.Width, this.Parent.Dim.Height - this.Pos.Y - 1);

            if (this.Dim.Width < _text.Length + 2) this.Dim = new Dim(_text.Length + 2, this.Dim.Height);

            List<object> _padding = Style.Get([typeof(Padding)], _styles);
            int borderPadding = 0;
            if(_padding != null)
            {
                foreach(object o in _padding)
                {
                    borderPadding += this.Dim.Width + ((int)o * 2);
                }
            }


            this.Pos = new Pos(this.Pos.X + this.Parent.Pos.X, this.Pos.Y + this.Parent.Pos.Y);


            Render(this.Pos, Style.Set($"{Border(Get.TopLeft)}{Aligner.Align(borderPadding - 2, default, Border(Get.Horizontal), null)}{Border(Get.TopRight)}", _styles));
            for (int i = 0; i < this.Dim.Height; i++)
            {
                Render(new Pos(this.Pos.X, this.Pos.Y + ++currentHeight), Style.Set(Border(Get.Vertical), _styles) + Aligner.Align(this.Dim.Width - 2, default, " ", _text) + Style.Set(Border(Get.Vertical), _styles));
            }
            Render(new Pos(this.Pos.X, this.Pos.Y + ++currentHeight), Style.Set($"{Border(Get.BottomLeft)}{Aligner.Align(borderPadding - 2, default, Border(Get.Horizontal), null)}{Border(Get.BottomRight)}", _styles));
        }

        internal static string Border(Get _part)
        {
            return _part switch
            {
                Get.TopLeft => "┌",
                Get.TopRight => "┐",
                Get.BottomLeft => "└",
                Get.BottomRight => "┘",
                Get.Horizontal => "─",
                Get.HorizontalDown => "┬",
                Get.HorizontalUp => "┴",
                Get.Vertical => "│",
                Get.VerticalLeft => "├",
                Get.VerticalRight => "┤",
                Get.Cross => "┼",
                Get.ArrowDown => "↓",
                _ => throw new InvalidOperationException("Unknown Global.Border part."),
            };
        }

        internal enum Get
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight,
            Horizontal,
            HorizontalDown,
            HorizontalUp,
            Vertical,
            VerticalLeft,
            VerticalRight,
            Cross,
            ArrowDown
        }
    }   
}
