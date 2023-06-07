using Chess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    internal class Pawn : Piece
    {
        public Pawn(Color color) : base(color)
        {
        }

        public override string Symbol => "PA";

        protected override void SetRules()
        {
            if(Color== Color.White)
            {
                _MoveRules.Add(new MoveRule(
                    c => c.WithCapture == false &&
                         c.From.RankNumber + 1 == c.To.RankNumber  &&
                         c.From.ColumnNumber == c.To.ColumnNumber));

                _MoveRules.Add(new MoveRule(
                    c => c.WithCapture == false &&
                         c.From.RankNumber == 2 &&
                         c.From.RankNumber + 2 == c.To.RankNumber &&
                         c.From.ColumnNumber == c.To.ColumnNumber));

                _MoveRules.Add(new MoveRule(
                    c => /*c.WithCapture == true &&*/
                         c.From.RankNumber + 1 == c.To.RankNumber  &&
                         c.From.ColumnNumber == c.To.ColumnNumber + 1));

                _MoveRules.Add(new MoveRule(
                    c => /*c.WithCapture == true &&*/
                         c.From.RankNumber + 1 == c.To.RankNumber  &&
                         c.From.ColumnNumber == c.To.ColumnNumber - 1));
            }
            else
            {
                _MoveRules.Add(new MoveRule(
                    c => c.WithCapture == false &&
                         c.From.RankNumber -1 == c.To.RankNumber  &&
                         c.From.ColumnNumber == c.To.ColumnNumber));

                _MoveRules.Add(new MoveRule(
                    c => c.WithCapture == false &&
                         c.From.RankNumber == 7 &&
                         c.From.RankNumber -2 == c.To.RankNumber &&
                         c.From.ColumnNumber == c.To.ColumnNumber));

                _MoveRules.Add(new MoveRule(
                    c => /*c.WithCapture == true &&*/
                         c.From.RankNumber -1 == c.To.RankNumber &&
                         c.From.ColumnNumber == c.To.ColumnNumber + 1));

                _MoveRules.Add(new MoveRule(
                    c => /*c.WithCapture == true &&*/
                         c.From.RankNumber - 1 == c.To.RankNumber &&
                         c.From.ColumnNumber == c.To.ColumnNumber - 1));
            }
        }
    }
}
