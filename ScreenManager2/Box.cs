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
            if (this.Dim.Width + this.Pos.X >= this.Parent.Dim.Width) this.Dim = new(this.Parent.Dim.Width - this.Pos.X - Parent.Pos.X, this.Dim.Height);
            if (this.Dim.Height + this.Pos.Y >= this.Parent.Dim.Height) this.Dim = new(this.Dim.Width, this.Parent.Dim.Height - this.Pos.Y - Parent.Pos.Y);

            this.Pos = new Pos(this.Pos.X + this.Parent.Pos.X, this.Pos.Y + this.Parent.Pos.Y);



            Render(this.Pos, Style.Set($"{Border(Get.TopLeft)}{Aligner.Align(this.Dim.Width - 2, default, Border(Get.Horizontal), null)}{Border(Get.TopRight)}", _styles));
            for (int i = 0; i < this.Dim.Height; i++) Render(new Pos(this.Pos.X, this.Pos.Y + ++currentHeight), Style.Set(Border(Get.Vertical), _styles) + Aligner.Align(this.Dim.Width - 2, default, " ", default) + Style.Set(Border(Get.Vertical), _styles));
            Render(new Pos(this.Pos.X, this.Pos.Y + ++currentHeight), Style.Set($"{Border(Get.BottomLeft)}{Aligner.Align(this.Dim.Width - 2, default, Border(Get.Horizontal), null)}{Border(Get.BottomRight)}", _styles));
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
