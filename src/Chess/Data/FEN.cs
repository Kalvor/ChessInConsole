using Chess.Pieces;
using Chess.Tools;

namespace Chess.Data
{
    public class FEN
    {
        public Dictionary<Squere, Piece?> Position { get; set; }
        public Color ActiveColor { get; set; }
        public CastlingAbility WhitesCastling { get; set; } 
        public CastlingAbility BlacksCastling { get; set; } 
        public Squere? EnPassantSquere { get; set; }
        public int HalfMovesCount { get; set; }
        public int MovesCount { get; set; }

        public FEN(string fen)
        {
            var splittedFen = fen.Split(" ");
            string[] ranks = splittedFen[0].Split("/");

            Position = new();
            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    var letterNotation = (char)(96 + j);
                    string squereNotation = letterNotation.ToString() + i;
                    Position.Add(new(squereNotation), null);
                }
            }

            for (int rankNumber = 8; rankNumber > 0; rankNumber-- )
            {
                int rankNumberIndex = 8 - rankNumber;
                int currentColumnNumber = 1;
                for (int j = 1; j <= ranks[rankNumberIndex].Length; j++)
                {
                    bool isNumeric = Char.IsNumber(ranks[rankNumberIndex][j - 1]);
                    if (isNumeric)
                    {
                        int parsedValue = int.Parse(ranks[rankNumberIndex][j - 1].ToString());
                        for(int z = 0; z < parsedValue; z++)
                        {
                            Position[new(rankNumber, z + currentColumnNumber)] = null;
                        }
                        currentColumnNumber += (parsedValue - 1);
                    }
                    else
                    {
                        char pieceToPlace = ranks[rankNumberIndex][j - 1];
                        Position[new(rankNumber, currentColumnNumber)] = PieceFactory.Produce(pieceToPlace);
                    }
                    currentColumnNumber++;
                }
            }

            ActiveColor = splittedFen[1] == "w" ? Color.White : Color.Black;
            WhitesCastling = new(new string(splittedFen[2].Where(Char.IsUpper).ToArray()));
            BlacksCastling = new(new string(splittedFen[2].Where(c=>!Char.IsUpper(c)).ToArray()));
            EnPassantSquere = splittedFen[3] == "-" ? null : new(splittedFen[3]);
            HalfMovesCount = int.Parse(splittedFen[4]);
            MovesCount = int.Parse(splittedFen[5]);
        }

        public override string ToString()
        {
            return "";
        }
    }

    public class CastlingAbility
    {
        public bool CanQueenSide { get; set; }
        public bool CanKingSide { get; set; }
        public CastlingAbility(string fenFragment)
        {
            if(fenFragment.ToLower().Contains("k"))
                CanKingSide= true;
            if(fenFragment.ToLower().Contains("q"))
                CanQueenSide= true;
        }
    }
}
