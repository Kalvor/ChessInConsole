using Chess.Data;

namespace Chess.Pieces
{
    internal class Bishop : Piece
    {
        public Bishop(Color color) : base(color)
        {
        }

        public override string Symbol => "BS";

        protected override void SetRules()
        {
            _MoveRules.Add(new MoveRule(c => c.From.RankNumber - c.To.RankNumber == c.From.ColumnNumber - c.To.ColumnNumber));
            _MoveRules.Add(new MoveRule(c => c.From.RankNumber - c.To.RankNumber == -(c.From.ColumnNumber - c.To.ColumnNumber)));
        }
    }
}
