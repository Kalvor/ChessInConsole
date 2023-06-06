using Chess.Data;

namespace Chess.Pieces
{
    internal class Knight : Piece
    {
        public Knight(Color color) : base(color)
        {
        }

        public override string Symbol => "KN";

        protected override void SetRules()
        {
            _MoveRules.Add(new MoveRule(c => c.From.RankNumber == c.To.RankNumber + 2 && c.From.ColumnNumber == c.To.ColumnNumber + 1));
            _MoveRules.Add(new MoveRule(c => c.From.RankNumber == c.To.RankNumber + 2 && c.From.ColumnNumber == c.To.ColumnNumber - 1));
            _MoveRules.Add(new MoveRule(c => c.From.RankNumber == c.To.RankNumber - 2 && c.From.ColumnNumber == c.To.ColumnNumber + 1));
            _MoveRules.Add(new MoveRule(c => c.From.RankNumber == c.To.RankNumber - 2 && c.From.ColumnNumber == c.To.ColumnNumber - 1));

            _MoveRules.Add(new MoveRule(c => c.From.RankNumber == c.To.RankNumber + 1 && c.From.ColumnNumber == c.To.ColumnNumber + 2));
            _MoveRules.Add(new MoveRule(c => c.From.RankNumber == c.To.RankNumber + 1 && c.From.ColumnNumber == c.To.ColumnNumber - 2));
            _MoveRules.Add(new MoveRule(c => c.From.RankNumber == c.To.RankNumber - 1 && c.From.ColumnNumber == c.To.ColumnNumber + 2));
            _MoveRules.Add(new MoveRule(c => c.From.RankNumber == c.To.RankNumber - 1 && c.From.ColumnNumber == c.To.ColumnNumber - 2));
        }
    }
}
