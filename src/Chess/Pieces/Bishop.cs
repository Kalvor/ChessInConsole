using Chess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    internal class Bishop : IPiece
    {
        public Bishop(PlayerColor color)
        {
            Color = color;
        }
        public PlayerColor Color { get; private set; }

        public string DisplayChar => Color == PlayerColor.White ? "B" : "b";

        public Squere[] GetAvaliableSqueres(Board board)
        {
            throw new NotImplementedException();
        }
    }
}
