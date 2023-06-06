namespace Chess.Data
{
    public class Squere
    {
        public Squere(string notation)
        {
            notation = notation.ToLower();
            ColumnCharacter = notation[0];
            ColumnNumber = notation[0] - 96;
            RankNumber = int.Parse(notation[1].ToString());
        }
        public Squere(int rankNumber, int columnNumber)
        {
            ColumnCharacter = (char)(columnNumber + 96);
            ColumnNumber = columnNumber;
            RankNumber = rankNumber;
        }

        public int ColumnNumber { get; set; }
        public char ColumnCharacter { get; set; }
        public int RankNumber { get; set; }

        public override int GetHashCode()
        {
            return RankNumber + ColumnNumber + ColumnCharacter;
        }
        public override bool Equals(object? obj)
        {
            if (Object.ReferenceEquals(this, obj))
                return true;

            if (obj is not Squere other)
                return false;

            return other.ColumnNumber == this.ColumnNumber && other.RankNumber == this.RankNumber && other.ColumnCharacter == this.ColumnCharacter;
        }
    }
}
