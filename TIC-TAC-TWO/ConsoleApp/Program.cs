
using System.Threading.Channels;
using GameBrain;
using MenuSystem;

var gameInstance = new TicTacTwoBrain(11);

var deepMenu = new Menu(EMenuLevel.Deep, "TIC-TAC-TWO DEEP", new Dictionary<string, MenuItem>
{
    {"Y", new MenuItem()
    {
        Title = "YYYYYY",
        Shortcut = "Y",
        MenuItemAction = null
    }},
});


var optionsMenu = new Menu(EMenuLevel.Secondary, "TIC-TAC-TWO OPTIONS", new Dictionary<string, MenuItem>
{
    {"X", new MenuItem()
    {
        Title = "X Starts",
        Shortcut = "X",
        MenuItemAction = deepMenu.Run
    }},
    {"O", new MenuItem()
    {
        Title = "O Starts",
        Shortcut = "O",
        MenuItemAction = null
    }}
});



var mainMenu = new Menu(EMenuLevel.Main, "TIC-TAC-TWO", new Dictionary<string, MenuItem>
{
    {"O", new MenuItem()
    {
        Title = "Options",
        Shortcut = "O",
        MenuItemAction = optionsMenu.Run
    }},
    {"N", new MenuItem()
    {
        Title = "New game",
        Shortcut = "N",
        MenuItemAction = NewGame
    }}
});

mainMenu.Run();

return;


// ==================================

string NewGame()
{
    
    ConsoleUI.Visualizer.DrawBoard(gameInstance);
    ConsoleUI.Visualizer.DrawGrid(gameInstance);
    
    return "Hi";
}

// int cursorX = 0;
// int cursorY = 0;
// bool enterPressed = false;
//
// Console.Clear();
// Console.WriteLine("Use arrow keys to move around. Press Enter to select a location.");
//
// // A grid representation or positions you want to allow movement in.
// Console.SetCursorPosition(0, 2);
// Console.WriteLine("X  X  X  X");
// Console.SetCursorPosition(0, 3);
// Console.WriteLine("X  X  X  X");
// Console.SetCursorPosition(0, 4);
// Console.WriteLine("X  X  X  X");
//     
//
// while (!enterPressed)
// {
//     Console.SetCursorPosition(cursorX, cursorY + 2); // Start from line 2 to avoid overwriting instructions
//     ConsoleKeyInfo keyInfo = Console.ReadKey(true);  // true to intercept key and not show it in console
//
//     switch (keyInfo.Key)
//     {
//         case ConsoleKey.UpArrow:
//             if (cursorY > 0) cursorY--;
//             break;
//         case ConsoleKey.DownArrow:
//             if (cursorY < 2) cursorY++;
//             break;
//         case ConsoleKey.LeftArrow:
//             if (cursorX > 0) cursorX -= 3; // Move by 3 columns to avoid spaces
//             break;
//         case ConsoleKey.RightArrow:
//             if (cursorX < 9) cursorX += 3; // Move by 3 columns to avoid spaces
//             break;
//         case ConsoleKey.Enter:
//             enterPressed = true;
//             break;
//     }
// }
//
// // Handle the position where Enter was pressed
// Console.Clear();
// Console.WriteLine($"You selected position: ({cursorX}, {cursorY})");





