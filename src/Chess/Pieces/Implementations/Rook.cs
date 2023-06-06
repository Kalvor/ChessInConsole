using Chess.Data;

namespace Chess.Pieces
{
    internal class Rook : Piece
    {
        public Rook(Color color) : base(color)
        {
        }

        public override string Symbol => "RK";

        protected override void SetRules()
        {
            _MoveRules.Add(new MoveRule(c => c.From.ColumnNumber == c.To.ColumnNumber));
            _MoveRules.Add(new MoveRule(c => c.From.RankNumber == c.To.RankNumber));
        }
    }
}
