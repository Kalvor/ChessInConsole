using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Display
{
    public static class DisplayTable
    {
        public static DisplayText HeaderText_1 => DisplayText.AtCenter("Chess Game", ConsoleColor.DarkGreen, ConsoleColor.Black, 1);
        public static DisplayText MainOption_1 => DisplayText.AtCenter("Play Alone", ConsoleColor.Red, ConsoleColor.DarkYellow, 3);
        public static DisplayText MainOption_2 => DisplayText.AtCenter("Send Game Invitations", ConsoleColor.Red, ConsoleColor.DarkYellow, 4);

    }
}
