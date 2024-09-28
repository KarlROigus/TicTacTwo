using System;

class Program
{
    static void Main()
    {
        int cursorX = 0;
        int cursorY = 0;
        bool enterPressed = false;

        Console.Clear();
        Console.WriteLine("Use arrow keys to move around. Press Enter to select a location.");

        // A grid representation or positions you want to allow movement in.
        Console.SetCursorPosition(0, 2);
        Console.WriteLine("X  X  X  X");
        Console.SetCursorPosition(0, 3);
        Console.WriteLine("X  X  X  X");
        Console.SetCursorPosition(0, 4);
        Console.WriteLine("X  X  X  X");

        while (!enterPressed)
        {
            Console.SetCursorPosition(cursorX, cursorY + 2); // Start from line 2 to avoid overwriting instructions
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);  // true to intercept key and not show it in console

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (cursorY > 0) cursorY--;
                    break;
                case ConsoleKey.DownArrow:
                    if (cursorY < 2) cursorY++;
                    break;
                case ConsoleKey.LeftArrow:
                    if (cursorX > 0) cursorX -= 3; // Move by 3 columns to avoid spaces
                    break;
                case ConsoleKey.RightArrow:
                    if (cursorX < 9) cursorX += 3; // Move by 3 columns to avoid spaces
                    break;
                case ConsoleKey.Enter:
                    enterPressed = true;
                    break;
            }
        }

        // Handle the position where Enter was pressed
        Console.Clear();
        Console.WriteLine($"You selected position: ({cursorX}, {cursorY})");
    }
}