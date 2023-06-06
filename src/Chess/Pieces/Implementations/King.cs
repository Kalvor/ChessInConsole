using Chess.Data;

namespace Chess.Pieces
{
    internal class King : Piece
    {
        public King(Color color) : base(color)
        {
        }

        public override string Symbol => "KG";

        protected override void SetRules()
        {
            _MoveRules.Add(new MoveRule(c => c.From.RankNumber == c.To.RankNumber + 1 && c.From.ColumnNumber == c.To.ColumnNumber));
            _MoveRules.Add(new MoveRule(c => c.From.RankNumber == c.To.RankNumber - 1 && c.From.ColumnNumber == c.To.ColumnNumber));
            _MoveRules.Add(new MoveRule(c => c.From.RankNumber == c.To.RankNumber && c.From.ColumnNumber == c.To.ColumnNumber - 1));
            _MoveRules.Add(new MoveRule(c => c.From.RankNumber == c.To.RankNumber && c.From.ColumnNumber == c.To.ColumnNumber + 1));

            _MoveRules.Add(new MoveRule(c => c.From.RankNumber == c.To.RankNumber + 1 && c.From.ColumnNumber == c.To.ColumnNumber + 1));
            _MoveRules.Add(new MoveRule(c => c.From.RankNumber == c.To.RankNumber + 1 && c.From.ColumnNumber == c.To.ColumnNumber - 1));
            _MoveRules.Add(new MoveRule(c => c.From.RankNumber == c.To.RankNumber -1 && c.From.ColumnNumber == c.To.ColumnNumber + 1));
            _MoveRules.Add(new MoveRule(c => c.From.RankNumber == c.To.RankNumber -1 && c.From.ColumnNumber == c.To.ColumnNumber - 1 ));
        }
    }
}
