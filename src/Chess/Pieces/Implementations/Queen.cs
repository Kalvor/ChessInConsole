using Chess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    internal class Queen : Piece
    {
        public Queen(Color color) : base(color)
        {
        }

        public override string Symbol => "QN";

        protected override void SetRules()
        {
            _MoveRules.Add(new MoveRule(c => c.From.ColumnNumber == c.To.ColumnNumber));
            _MoveRules.Add(new MoveRule(c => c.From.RankNumber == c.To.RankNumber));
            _MoveRules.Add(new MoveRule(c => c.From.RankNumber-c.To.RankNumber == c.From.ColumnNumber - c.To.ColumnNumber));
            _MoveRules.Add(new MoveRule(c => c.From.RankNumber-c.To.RankNumber == -(c.From.ColumnNumber - c.To.ColumnNumber)));
        }
    }
}
