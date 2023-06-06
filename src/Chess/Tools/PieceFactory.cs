using Chess.Data;
using Chess.Pieces;

namespace Chess.Tools
{
    public static class PieceFactory
    {
        public static IPiece Produce(char pieceSymbol)
        {
            PlayerColor playerColor = Char.IsUpper(pieceSymbol) ? PlayerColor.White : PlayerColor.Black;
            return pieceSymbol.ToString().ToLower() switch
            {
                "k" => new King(playerColor),
                "q" => new Queen(playerColor),
                "r" => new Rook(playerColor),
                "b" => new Bishop(playerColor),
                "n" => new Knight(playerColor),
                "p" => new Pawn(playerColor),
                _=>throw new NotImplementedException(),
            };
        }
    }
}
