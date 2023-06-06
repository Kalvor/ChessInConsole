using Chess.Data;
using System.Text;

namespace Game.Tools
{
    public sealed class BoardPrinter
    {
        private char WhiteSquere = ' ';
        private char BlackSquere = '■';
        public void Print(Board board)
        {
            Console.OutputEncoding = Encoding.Unicode;
            for (int i=8;i> 0;i--)
            {
                for(int j = 1; j <= 8; j++)
                {
                    if (board.FEN.Position[new(i,j)] == null)
                    {
                        if ((i + j) % 2 == 0) Console.Write(BlackSquere);
                        else Console.Write(WhiteSquere);
                    }
                    else
                    {
                        Console.Write(board.FEN.Position[new(i, j)]!.DisplayChar);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
