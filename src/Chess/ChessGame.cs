using Chess.Data;
using Chess.Pieces;
using Chess.Tools;
using System.Data.Common;

namespace Chess
{
    public sealed class ChessGame
    {
        public FEN FEN { get; private set; }
        public bool IsPending { get; set; }
        public Color CurrentPlayerColor { get; set; }
        
        private Clock _Clock;
        private MoveParser _MoveParser;

        public ChessGame(FEN fen)
        {
            FEN = fen;
            _Clock = new Clock();
            _MoveParser = new MoveParser();
            CurrentPlayerColor = Color.White;
            IsPending = true;
        }

        public static ChessGame Begin(Color currentPlayerColor)
        {
            return new ChessGame(new FEN("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"));
        }

        public Move ReadMove() 
        {
            string notation = Console.ReadLine()!;
            return _MoveParser.Parse(notation);
        }

        public bool CanDoMove(Move move)
        {
            if (move.From.RankNumber < 1 || move.From.RankNumber > 8 || move.From.ColumnNumber < 1 || move.From.ColumnNumber > 8)
            {
                return false;
            }

            if (move.To.RankNumber < 1 || move.To.RankNumber > 8 || move.To.ColumnNumber < 1 || move.To.ColumnNumber > 8)
            {
                return false;
            }

            var piece = FEN.Position[move.From];
            if (piece == null || piece.Color != CurrentPlayerColor || !piece.IsMoveValid(move))
            {
                return false;
            }

            if (FEN.Position[move.To] != null && FEN.Position[move.To]!.Color == CurrentPlayerColor)
            {
                return false;
            }

            if (!(piece is Knight) && !checkIfPathIsFree(move))
            {
                return false;
            }


            return true;
        }

        public void DoMove(Move move)
        {

            FEN.Position[move.To] = FEN.Position[move.From];
            FEN.Position[move.From] = null;
            CurrentPlayerColor = 1 - CurrentPlayerColor;
        }

        private bool checkIfPathIsFree(Move move) //todo
        {          

            return true;
        }
    }
}
