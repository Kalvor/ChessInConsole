using System.Drawing;

namespace Game.Display
{
    public record DisplayText
    {
        public string StringToPrint { get; set; }
        public ConsoleColor Color { get; set; }
        public ConsoleColor BackgroundColor { get; set; }
        public Point Location { get; set; }

        public static DisplayText New(string stringToPrint, ConsoleColor color, ConsoleColor backgroundColor, Point location)
        {
            return new DisplayText(stringToPrint, color, backgroundColor, location);
        }
        public static DisplayText AtCenter(string stringToPrint, ConsoleColor color, ConsoleColor backgroundColor, int yCord)
        {
            return new DisplayText(stringToPrint, color, backgroundColor, new((Console.WindowWidth - stringToPrint.Length) / 2, yCord));
        }

        private DisplayText(string stringToPrint, ConsoleColor color, ConsoleColor backgroundColor, Point location)
        {
            StringToPrint = stringToPrint;
            Color = color;
            BackgroundColor = backgroundColor;
            Location = location;
        }
    }
}
