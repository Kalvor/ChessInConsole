using Game.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Game.Tools
{
    public sealed class OptionsPicker
    {
        private readonly MessagePrinter _Printer;
        public OptionsPicker(MessagePrinter printer)
        {
            _Printer = printer;
        }

        public int PickOptions(params DisplayText[] options)
        {
            var iterator = 0;
            for (int i = 0; i < options.Length; i++)
            {
               ((Action<DisplayText>)(i == iterator ? _Printer.SelectText : _Printer.PrintText))(options[i]);
            }
            ConsoleKey key = ConsoleKey.NoName;
            while (key != ConsoleKey.Enter)
            {
                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.DownArrow)
                {
                    if (iterator == options.Length - 1)
                        iterator = 0;
                    else
                        iterator++;
                }
                if (key == ConsoleKey.UpArrow)
                {
                    if (iterator == 0)
                        iterator = options.Length - 1;
                    else
                        iterator--;
                }

                for(int i=0; i < options.Length;i++)
                {
                    ((Action<DisplayText>)(i == iterator ? _Printer.SelectText : _Printer.PrintText))(options[i]);
                }
            }
             return iterator;
        }
    }
}
