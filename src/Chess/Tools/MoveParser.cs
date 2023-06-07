using Chess.Data;

namespace Chess.Tools
{
    internal sealed class MoveParser
    {
        public Move Parse(string notation) 
        {
            string[] notationSplit = notation.Split(" ");
            return new Move()
            {
                From = new Squere(notationSplit[0]),
                To = new Squere(notationSplit[1])
            };
        }
    }
}
