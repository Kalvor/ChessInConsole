namespace Game.Display
{
    public static class DisplayTable
    {
        public static DisplayText Header_Main => DisplayText.AtCenter("Chess Game", ConsoleColor.DarkGreen, ConsoleColor.Black, 1);
        public static DisplayText MenuOption_Main_1 => DisplayText.AtCenter("Play Alone", ConsoleColor.Red, ConsoleColor.DarkYellow, 3);
        public static DisplayText MenuOption_Main_2 => DisplayText.AtCenter("Send Game Invitations", ConsoleColor.Red, ConsoleColor.DarkYellow, 4);

        public static DisplayText Header_Sub_CreateInvitation => DisplayText.AtCenter("Game invitation creation", ConsoleColor.DarkBlue, ConsoleColor.Black, 2);
        public static DisplayText Input_Name_CreateInvitation => DisplayText.New("Name : ", ConsoleColor.DarkMagenta, ConsoleColor.Black, new(10,4));
        public static DisplayText Input_ClockBase_CreateInvitation => DisplayText.New("Clock base : ", ConsoleColor.DarkMagenta, ConsoleColor.Black, new(10, 5));
        public static DisplayText Input_ClockAdd_CreateInvitation => DisplayText.New("Clock additional time : ", ConsoleColor.DarkMagenta, ConsoleColor.Black, new(10, 6));
        public static DisplayText Input_PiecesColor_Create_Invitation => DisplayText.New("Pieces Color : ", ConsoleColor.DarkMagenta, ConsoleColor.Black, new(10, 7));
        public static DisplayText Input_PiecesColorBlack_Create_Invitation => DisplayText.New("Black", ConsoleColor.Red, ConsoleColor.DarkYellow, new(10, 8));
        public static DisplayText Input_PiecesColorWhite_Create_Invitation => DisplayText.New("White", ConsoleColor.Red, ConsoleColor.DarkYellow, new(10, 9));
        public static DisplayText Input_PiecesColorRandom_Create_Invitation => DisplayText.New("Random", ConsoleColor.Red, ConsoleColor.DarkYellow, new(10, 10));
        public static DisplayText Input_ReceiverIP_CreateInvitation => DisplayText.New("Reveiver IP : ", ConsoleColor.DarkMagenta, ConsoleColor.Black, new(10, 11));


    }
}
