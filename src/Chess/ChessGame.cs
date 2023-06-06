using Chess.Data;
using Chess.Tools;

namespace Chess
{
    public sealed class ChessGame
    {
        public FEN FEN;
        private Clock _Clock;
        public ChessGame(FEN fen)
        {
            FEN = fen;
        }

        public static ChessGame Begin()
        {
            return new ChessGame(new FEN("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"));
        }

        public void DoMove(Move move)
        {

        }
    }
}
