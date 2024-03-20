using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenManager2
{
    internal class Button : Object
    {
        private readonly int currentHeight = 0;

        public Button(string _text, Dim? _dim = null, List<object>? _styles = null, Parent? _parent = null, Pos? _pos = null) : base(_parent ?? new Parent(new Pos(0, 0), new Dim(Console.WindowWidth, Console.WindowHeight)), _pos ?? new Pos(2, 1), _dim ?? new Dim(0, 3))
        {
            this.Pos = new Pos(this.Pos.X + this.Parent.Pos.X, this.Pos.Y + this.Parent.Pos.Y);
            this.Dim = new Dim(_text.Length + 2, 3);
            foreach(object o in Style.Get([typeof(Padding)], _styles))
            {
                this.Dim = new Dim(this.Dim.Width + ((int)o * 2), this.Dim.Height);
            }

            if (this.Pos.X + this.Dim.Width > this.Parent.Dim.Width) this.Pos = new Pos(this.Parent.Pos.X + this.Parent.Dim.Width - this.Dim.Width - 2, this.Pos.Y);

            Render(this.Pos, Style.Set($"{Border(Get.TopLeft)}{Aligner.Align(this.Dim.Width - 2, default, Border(Get.Horizontal), null)}{Border(Get.TopRight)}", Style.Get([typeof(BorderStyling), typeof(BorderBgColor), typeof(BorderColor)], _styles)));
            Render(new Pos(this.Pos.X, this.Pos.Y + ++currentHeight), Style.Set(Border(Get.Vertical), Style.Get([typeof(BorderStyling), typeof(BorderBgColor), typeof(BorderColor)], _styles)) + Style.Set(Style.Set(_text, Style.Get([typeof(Padding)], _styles)), Style.Get([typeof(FontColor)], _styles)) + Style.Set(Border(Get.Vertical), Style.Get([typeof(BorderStyling), typeof(BorderBgColor), typeof(BorderColor)], _styles)));
            Render(new Pos(this.Pos.X, this.Pos.Y + ++currentHeight), Style.Set($"{Border(Get.BottomLeft)}{Aligner.Align(this.Dim.Width - 2, default, Border(Get.Horizontal), null)}{Border(Get.BottomRight)}", Style.Get([typeof(BorderStyling), typeof(BorderBgColor), typeof(BorderColor)], _styles)));

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
