using Chess.Data;

namespace Chess.Pieces
{
    public sealed class MoveRule
    {
        private Predicate<Move> _predicte;
        public MoveRule(Predicate<Move> predicate)
        {
            _predicte = predicate;
        }

        public bool IsValid(Move move)
        {
            return _predicte.Invoke(move);
        }
    }
}
