﻿using Chess.Data;

namespace Chess.Pieces
{
    public abstract class Piece
    {
        protected ICollection<MoveRule> _MoveRules;

        public Piece(Color color)
        {
            _MoveRules = new HashSet<MoveRule>();
            Color = color;

            SetRules();
        }

        public abstract string Symbol { get; }
        protected abstract void SetRules();
        protected Color Color { get; }

        public string Display => Symbol + (Color == Color.Black ? "B" : "W");

        public bool IsMoveValid(Move move)
        {
            return _MoveRules.Any(c=>c.IsValid(move));
        }
    }
}
