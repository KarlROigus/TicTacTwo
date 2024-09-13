// See https://aka.ms/new-console-template for more information


using MenuSystem;

var mainMenu = new Menu("TIC-TAC-TOE", new List<MenuItem>()
{
     new MenuItem()
     {
         Shortcut = "O",
         Title = "Options"
     },
     
     new MenuItem() {
         
         Shortcut = "N",
         Title = "New game", 
     },
     

});

mainMenu.Run();


return;

// =====================

static void MenuMain()
{
    MenuStart();
    Console.WriteLine("O) OPTIONS");
    Console.WriteLine("N) NEW GAME");
    Console.WriteLine("L) LOAD GAME");
    Console.WriteLine("E) EXIT");
    Console.WriteLine(">");
    MenuEnd();
}

static void MenuEnd()
{
    Console.WriteLine();
    Console.WriteLine(">");
}

static void MenuOptions()
{
    Console.Clear();
    MenuStart();
    Console.WriteLine("Choose symbol for player one (X)");
    Console.WriteLine("Choose symbol for player one (O)");
    MenuEnd();
}

static void MenuStart()
{
    Console.WriteLine("TIC-TAC-TOE");
    Console.WriteLine("===========");
}



