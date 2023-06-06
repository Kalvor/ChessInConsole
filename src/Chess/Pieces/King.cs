using Chess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    internal class King : IPiece
    {
        public King(PlayerColor color)
        {
            Color = color;
        }
        public PlayerColor Color { get; private set; }

        public string DisplayChar => Color == PlayerColor.White ? "K" : "k";

        public Squere[] GetAvaliableSqueres(Board board)
        {
            throw new NotImplementedException();
        }
    }
}
