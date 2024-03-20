using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenManager2
{
    struct Pos(int _x, int _y)
    {
        public int X { get; set; } = _x;
        public int Y { get; set; } = _y;
    }

    struct Dim(int _width, int _height)
    {
        public int Width { get; set; } = _width;
        public int Height { get; set; } = _height;
    }

    struct Parent(Pos _pos, Dim _dim)
    {
        public Pos Pos { get; set; } = _pos;
        public Dim Dim { get; set; } = _dim;
    }

    internal class Object(Parent _parent, Pos _pos, Dim _dim)
    {
        public Parent Parent { get; set; } = _parent;
        public Pos Pos { get; set; } = _pos;

        public Dim Dim { get; set; } = _dim;

        public Parent SetParent { get { return new Parent(this.Pos, this.Dim); } }

        internal static void SetPos(Pos _pos)
        {
            Console.SetCursorPosition(_pos.X, _pos.Y);
        }

        internal static void Render(Pos _pos, string _text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            SetPos(_pos);
            Console.Write(_text);
            Console.ForegroundColor = ConsoleColor.Black;
        }

        internal static void Remove(Pos _pos, Dim _dim)
        {
            for(int i = 0; i < _dim.Height; i++)
            {
                SetPos(new Pos(_pos.X, _pos.Y + i));
                Console.Write(string.Concat(Enumerable.Repeat(" ", _dim.Width)));
            }
        }
    }
}
