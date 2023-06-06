using Chess.Data;
using Chess.Pieces;

namespace Chess.Tools
{
    public static class PieceFactory
    {
        public static Piece Produce(char pieceSymbol)
        {
            Color pieceColor = Char.IsUpper(pieceSymbol) ? Color.White : Color.Black;
            return pieceSymbol.ToString().ToLower() switch
            {
                "k" => new King(pieceColor),
                "q" => new Queen(pieceColor),
                "r" => new Rook(pieceColor),
                "b" => new Bishop(pieceColor),
                "n" => new Knight(pieceColor),
                "p" => new Pawn(pieceColor),
                _=>throw new NotImplementedException(),
            };
        }
    }
}
