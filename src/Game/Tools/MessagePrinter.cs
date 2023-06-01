using Game.Display;
using Networking.Models;

namespace Game.Tools
{
    public class MessagePrinter
    {
        public void PrintText(DisplayText text)
        {
            Console.ForegroundColor = text.Color;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(text.Location.X, text.Location.Y);
            Console.Write(text.StringToPrint);
        }

        public void ClearText(DisplayText text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(text.Location.X, text.Location.Y);
            Console.Write(new string(' ', Console.WindowWidth));
        }

        public void SelectText(DisplayText text)
        {
            Console.ForegroundColor = text.Color;
            Console.BackgroundColor = text.BackgroundColor;
            Console.SetCursorPosition(text.Location.X, text.Location.Y);
            Console.Write(text.StringToPrint);
        }
    }
}
