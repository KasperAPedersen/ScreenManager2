using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ScreenManager2
{
    struct TableItems()
    {
        public List<string> headers = [];
        public List<string[]> content = [];
    }

    internal class Table : Object
    {
        TableItems Items = new();
        private int currentHeight = 0, userID = 0;
        private readonly int maxContentPerPage = 29;
        public int Active { get; set; } = 0;
        public int ActiveSelect { get; set; } = 7;
        public int CurrentPage { get; set; } = 1;


        public Table(Dim _dim, Parent? _parent, Pos? _pos) : base(_parent ?? new Parent(new Pos(0, 0), new Dim(Console.WindowWidth, Console.WindowHeight)), _pos ?? new Pos(2, 1), _dim)
        {
            if (Dim.Width + Pos.X >= Parent.Dim.Width) Dim = new Dim(Parent.Dim.Width - Parent.Pos.X + 1, Dim.Height);
            if (Dim.Height + Pos.Y >= Parent.Dim.Height) Dim = new Dim(Dim.Width, Parent.Dim.Height - Pos.Y);
            Pos = new Pos(Pos.X + Parent.Pos.X, Pos.Y + Parent.Pos.Y);

            Update();
        }

        public Table(Dim _dim, string[] _headers, Parent? _parent, Pos? _pos) : base(_parent ?? new Parent(new Pos(0, 0), new Dim(Console.WindowWidth, Console.WindowHeight)), _pos ?? new Pos(2, 1), _dim)
        {
            if (Dim.Width + Pos.X >= Parent.Dim.Width) Dim = new Dim(Parent.Dim.Width - Parent.Pos.X + 1, Dim.Height);
            if (Dim.Height + Pos.Y >= Parent.Dim.Height) Dim = new Dim(Dim.Width, Parent.Dim.Height - Pos.Y);
            Pos = new Pos(Pos.X + Parent.Pos.X, Pos.Y + Parent.Pos.Y);

            foreach (string s in _headers) Items.headers.Add(s);

            Update();
        }

        internal void Update()
        {
            currentHeight = 0;
            Remove(Pos, Dim);

            BuildHeader();
            BuildContent();
            BuildFooter();

            Dim = new Dim(Dim.Width, currentHeight);
        }

        internal void Update(int _active)
        {
            int lastPage = Items.content.Count / maxContentPerPage + 1;
            int nextPage = CurrentPage * maxContentPerPage;
            int prevPage = nextPage - maxContentPerPage;

            Active = _active > Items.content.Count - 1 ? Items.content.Count - 1 : (_active >= 0 ? _active : Active);
            CurrentPage += Active >= nextPage ? (CurrentPage < lastPage ? 1 : 0) : (Active < prevPage && Active > 0 ? -1 : 0);

            Update();

        }

        internal void Change()
        {
            switch(ActiveSelect)
            {
                case 7:
                    Items.content.RemoveAt(Active);
                    break;
                case 8:
                    // edit
                    break;
                default:
                    break;
            }
            Update();
        }

        internal void BuildHeader()
        {
            int fieldWidth = (Dim.Width - 2) / Items.headers.Count;
            int fieldWidthRem = (Dim.Width - 2) % Items.headers.Count;

            // Build border
            string tmp = Border(Get.TopLeft);
            for (int i = 0; i < Items.headers.Count; i++)
            {
                bool useRem = false;
                if (fieldWidthRem > 0)
                {
                    fieldWidthRem -= 1;
                    useRem = true;
                }
                tmp += Aligner.Align(fieldWidth - 1 + (useRem ? 1 : 0), Alignment.Center, Border(Get.Horizontal), default);
                tmp += i != Items.headers.Count - 1 ? Border(Get.HorizontalDown) : Border(Get.TopRight);
            }
            Render(new Pos(Pos.X, Pos.Y + currentHeight++), tmp);

            // Build border
            fieldWidthRem = (Dim.Width - 2) % Items.headers.Count;
            tmp = Border(Get.Vertical);
            for (int i = 0; i < Items.headers.Count; i++)
            {
                bool useRem = false;
                if (fieldWidthRem > 0)
                {
                    fieldWidthRem -= 1;
                    useRem = true;
                }
                tmp += Aligner.Align(fieldWidth - 1 + (useRem ? 1 : 0), Alignment.Center, " ", Items.headers[i]);
                tmp += Border(Get.Vertical);
            }
            Render(new Pos(Pos.X, Pos.Y + currentHeight++), tmp);

            // Border
            fieldWidthRem = (Dim.Width - 2) % Items.headers.Count;
            tmp = Border(Get.VerticalLeft);
            for (int i = 0; i < Items.headers.Count; i++)
            {
                bool useRem = false;
                if (fieldWidthRem > 0)
                {
                    fieldWidthRem -= 1;
                    useRem = true;
                }
                tmp += Aligner.Align(fieldWidth - 1 + (useRem ? 1 : 0), Alignment.Center, Border(Get.Horizontal), default);
                tmp += i != Items.headers.Count - 1 ? Border(Get.Cross) : Border(Get.VerticalRight);
            }
            Render(new Pos(Pos.X, Pos.Y + currentHeight++), tmp);
        }

        internal void BuildContent()
        {
            int firstPage = maxContentPerPage * CurrentPage - maxContentPerPage;
            int nextPage = maxContentPerPage * CurrentPage;

            for (int i = firstPage; i <= nextPage; i++)
            {
                if (i < Items.content.Count && i < nextPage)
                {
                    int fieldWidth = (Dim.Width - 2) / Items.headers.Count;
                    int fieldWidthRem = (Dim.Width - 2) % Items.headers.Count;
                    string tmp = Border(Get.Vertical);
                    for (int o = 0; o < Items.content[i].Length; o++)
                    {
                        bool useRem = false;
                        if (fieldWidthRem > 0)
                        {
                            fieldWidthRem -= 1;
                            useRem = true;
                        }

                        int activeTextLength = Style.Set(Items.content[i][o], [FontColor.red]).Length - Items.content[i][o].Length;
                        string text = Active == i && ActiveSelect == o ? $"> {Style.Set(Items.content[i][o], [FontColor.red])}" : Items.content[i][o];
                        tmp += Aligner.Align(fieldWidth - 1 + (useRem ? 1 : 0) + (Active == i && ActiveSelect == o ? activeTextLength : 0), Alignment.Center, " ", text);
                        tmp += Border(Get.Vertical);
                    }
                    Render(new Pos(Pos.X, Pos.Y + currentHeight++), tmp);
                }
            }
        }

        internal void BuildFooter()
        {
            int fieldWidth = (Dim.Width - 2) / Items.headers.Count;
            int fieldWidthRem = (Dim.Width - 2) % Items.headers.Count;

            // Build border
            string tmp = Border(Get.VerticalLeft);
            for (int i = 0; i < Items.headers.Count; i++)
            {
                bool useRem = false;
                if (fieldWidthRem > 0)
                {
                    fieldWidthRem -= 1;
                    useRem = true;
                }
                tmp += Aligner.Align(fieldWidth - 1 + (useRem ? 1 : 0), Alignment.Center, Border(Get.Horizontal), default);
                tmp += i != Items.headers.Count - 1 ? Border(Get.HorizontalUp) : Border(Get.VerticalRight);
            }
            Render(new Pos(Pos.X, Pos.Y + currentHeight++), tmp);

            tmp = Aligner.Align(Dim.Width - 3, Alignment.Center, " ", $"Page: {CurrentPage} / {(Items.content.Count - 1) / maxContentPerPage + 1} | Active: {Active}");
            Render(new Pos(Pos.X, Pos.Y + currentHeight++), Border(Get.Vertical) + tmp + Border(Get.Vertical));

            tmp = Aligner.Align(Dim.Width - 3, Alignment.Center, Border(Get.Horizontal), default);
            Render(new Pos(Pos.X, Pos.Y + currentHeight++), Border(Get.BottomLeft) + tmp + Border(Get.BottomRight));
        }

        internal void AddHeader(string _text)
        {
            Items.headers.Add(_text);
            Update();
        }

        internal void AddContent(string[] _text)
        {
            if (_text.Length > Items.headers.Count) return;

            if(_text.Length < Items.headers.Count)
            {
                List<string> tmp = [];
                int diff = Items.headers.Count - _text.Length - 1;

                tmp.Add((++userID).ToString());
                foreach(string s in _text) tmp.Add(s);
                for (int i = 2; i < diff; i++) tmp.Add("");

                tmp.Add("Slet");
                tmp.Add("Edit");

                Items.content.Add(tmp.ToArray());
            }
            Update();
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
