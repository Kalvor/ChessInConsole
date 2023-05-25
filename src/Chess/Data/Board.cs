using Chess.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Data
{
    public sealed class Board
    {
        private FEN _CurrentPossition;
        private Clock _Clock;
        public Board(FEN startingPosition)
        {
            _CurrentPossition = startingPosition;
        }
    }
}
