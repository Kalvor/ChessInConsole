using Chess.Data;

namespace Chess.Pieces
{
    public sealed class Rook : IPiece
    {
        public Rook(PlayerColor color)
        {
            Color = color;
        }
        public PlayerColor Color { get; private set; }

        public string DisplayChar => Color == PlayerColor.White ? "R" : "r";

        public Squere[] GetAvaliableSqueres(Board board)
        {
            throw new NotImplementedException();
        }
    }
}
