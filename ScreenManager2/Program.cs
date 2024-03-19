using ScreenManager2;

Box outerBox = new(new Dim(Console.WindowWidth, Console.WindowHeight - 5), default, default, default);
Box innerBox = new(new Dim(Console.WindowWidth, Console.WindowHeight), default, outerBox.SetParent, default);
_ = new Label("Blah", [FontColor.blue, Styling.Crossed, Padding.Small], innerBox.SetParent, default);
_ = new Button("Create User", default, [Padding.Small], innerBox.SetParent, new Pos(Console.WindowWidth, 1));

Table tt = new(new Dim(Console.WindowWidth, Console.WindowHeight), ["ID", "Fornavn", "Efternavn", "EmailAdr", "Mobil", "Addresse", "Titel", "Slet", "Edit"], innerBox.SetParent, new Pos(2, 4));

ScreenManager2.Object.SetPos(new Pos(Console.WindowWidth - 3, Console.WindowHeight - 3));

bool keepRunning = true;
while (keepRunning)
{
    Console.CursorVisible = false;
    switch(Console.ReadKey().Key)
    {
        case ConsoleKey.Enter:
            tt.Change();
            break;
        case ConsoleKey.C:
            tt.AddContent(["awn", "aobudoabnw", "gfthvjbm", "oabeubw"]);
            break;
        case ConsoleKey.LeftArrow:
            tt.ActiveSelect = (tt.ActiveSelect == 7 ? 8 : 7);
            tt.Update();
            break;
        case ConsoleKey.RightArrow:
            tt.ActiveSelect = (tt.ActiveSelect == 8 ? 7 : 8);
            tt.Update();
            break;
        case ConsoleKey.UpArrow:
            tt.Update(tt.Active - 1);
            break;
        case ConsoleKey.DownArrow:
            tt.Update(tt.Active + 1);
            break;
        default:
            break;
    }
}

