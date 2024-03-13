using ScreenManager2;

Box outerBox = new(new Dim(Console.WindowWidth - 5, Console.WindowHeight - 5), default, default, default);
Box innerBox = new(new Dim(Console.WindowWidth, Console.WindowHeight), default, outerBox.SetParent, default);
_ = new Label("Blah", [FontColor.blue, Styling.Crossed, Padding.Small], innerBox.SetParent, default);
_ = new Button("Create User", default, [BorderColor.green, BorderStyling.Blink, Padding.Small, FontColor.pink1], innerBox.SetParent, new Pos(Console.WindowWidth, 1));

ScreenManager2.Object.SetPos(new Pos(Console.WindowWidth - 3, Console.WindowHeight - 3));