using Chess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    internal class Pawn : IPiece
    {
        public Pawn(PlayerColor color)
        {
            Color = color;
        }
        public PlayerColor Color { get; private set; }

        public string DisplayChar => Color == PlayerColor.White ? "P" : "p";

        public Squere[] GetAvaliableSqueres(Board board)
        {
            throw new NotImplementedException();
        }
    }
}
