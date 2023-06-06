namespace Chess.Data
{
    public sealed class Move
    {
        public Squere From { get; set; }
        public Squere To { get; set;}
        public bool WithCapture { get; set; }
        public bool ToEnPassantCapture { get; set; }
    }
}
