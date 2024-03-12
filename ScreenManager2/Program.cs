using ScreenManager2;

Box outerBox = new(new Dim(Console.WindowWidth - 5, Console.WindowHeight - 5), [Styling.Blink], Colors.cyan, default, default);
Box innerBox = new(new Dim(Console.WindowWidth, Console.WindowHeight), default, Colors.red, outerBox.SetParent, default);
_ = new Label("Blah", Padding.Small, [Styling.Italic, Styling.Crossed], Colors.red, innerBox.SetParent, default);
Button btn = new("Create User", Padding.Small, default, [Styling.Blink], [Styling.Italic], Colors.aquamarine1, innerBox.SetParent, new Pos(Console.WindowWidth, 1));



ScreenManager2.Object.SetPos(new Pos(Console.WindowWidth - 3, Console.WindowHeight - 3));