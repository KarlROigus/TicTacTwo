using System;

class Program
{
    static void Main()
    {
        Console.Clear();
        for (int i = 0; i < 10; i++)
        {
            Console.SetCursorPosition(i + 1, i + 1);
            Console.WriteLine(i);
        }
    }
}