using Chess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    internal class Knight : IPiece
    {
        public Knight(PlayerColor color)
        {
            Color = color;
        }
        public PlayerColor Color { get; private set; }

        public string DisplayChar => Color == PlayerColor.White ? "N" : "n";

        public Squere[] GetAvaliableSqueres(Board board)
        {
            throw new NotImplementedException();
        }
    }
}
