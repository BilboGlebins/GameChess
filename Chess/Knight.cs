using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Knight:Piece
    {
        public Knight()
        {

        }

        public Knight(ChessColor color, Position position)
        {
            this.color = color;
            switch (color)
            {
                case ChessColor.White:
                    this.icon = "\u2658";
                    break;
                case ChessColor.Black:
                    this.icon = "\u265E";
                    break;
                default:
                    break;
            }

            this.position = position;
        }

        public override object Clone()
        {
            return new Knight
            {
                color = this.color,
                icon = this.icon,
                position = (Position)this.position.Clone()
            };
        }

        private bool checkMove(Position position, ChessBoard chessBoard, TypeMove typeMove)
        {
            if (position.V > 0 && position.V < 9 && position.H > 0 && position.H < 9)
            {
                if (chessBoard.Board[position.V, position.H].piece == null
                        ||
                        chessBoard.Board[position.V, position.H].piece.color != this.color)
                {
                    if (typeMove == TypeMove.PossibleAttack)
                    {
                        return true;
                    }
                    ChessBoard tmpChessBoard = (ChessBoard)chessBoard.Clone();
                    tmpChessBoard.Move(this.position, position);
                    if (tmpChessBoard.CheckKingAttacked(this.color))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override List<Position> GetAListOfMoves(ChessBoard chessBoard, TypeMove typeMove)
        {
            List<Position> listOfMoves = new List<Position>();



            return listOfMoves;
        }
    }
}
