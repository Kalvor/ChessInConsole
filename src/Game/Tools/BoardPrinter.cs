using Chess;
using System.Text;

namespace Game.Tools
{
    public sealed class BoardPrinter
    {
        public void Print(ChessGame chessGame)
        {
            string emptySquere = "   ";//three
            string pipe = " | ";//three
            int singleColumnSize = 6;
            int fullRowSize = (singleColumnSize * 8) + 3;
            Console.WriteLine(String.Join("", Enumerable.Repeat("-", fullRowSize)));

            for (int i = 8; i > 0; i--)
            {
                Console.Write(i + pipe);
                for (int j = 1; j <= 8; j++)
                {
                    if (chessGame.FEN.Position[new(i, j)] == null)
                    {
                        Console.Write(emptySquere + pipe);
                    }
                    else
                    {
                        Console.Write(chessGame.FEN.Position[new(i, j)]!.Display + pipe);
                    }
                }
                Console.WriteLine();
                Console.WriteLine(String.Join("", Enumerable.Repeat("-", fullRowSize)));
            }

            for(int i=1; i<= fullRowSize; i++)
            {
                if (i % singleColumnSize == 0)
                    Console.Write((char)((i / singleColumnSize) - 1 + 65));
                else
                    Console.Write(" ");
            }
        }
    }
}
