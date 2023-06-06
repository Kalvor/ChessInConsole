using Chess.Tools;

namespace Chess.Data
{
    public sealed class Board
    {
        public FEN FEN;
        private Clock _Clock;
        public Board(FEN fen)
        {
            FEN = fen;
        }

        public static Board StartingBoard()
        {
            return new Board(new FEN("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"));
        }
    }
}
