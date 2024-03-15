using ScreenManager2;

Box outerBox = new(new Dim(Console.WindowWidth - 5, Console.WindowHeight - 5), default, default, default);
Box innerBox = new(new Dim(Console.WindowWidth, Console.WindowHeight), default, outerBox.SetParent, default);
Label title = new("Blah", [FontColor.blue, Styling.Crossed, Padding.Small], innerBox.SetParent, default);
_ = new Button("Create User", default, [BorderColor.green, BorderStyling.Blink, Padding.Small, FontColor.pink1], innerBox.SetParent, new Pos(Console.WindowWidth, 1));

_ = new Table(new Dim(10, 10), new TableItems(["ID", "Fornavn", "Efternavn", "EmailAdr", "Mobil", "Addresse", "Titel", "Slet", "Edit"], ["ghi", "jkl"]), default, innerBox.SetParent, new Pos(2, 4));

ScreenManager2.Object.SetPos(new Pos(Console.WindowWidth - 3, Console.WindowHeight - 3));