using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Pawn:Piece
    {
        public bool takeOnTheAisle = false;
        public bool first = true;

        public Pawn()
        {
        }

        public Pawn(ChessColor color, Position position)
        {
            this.color = color;
            switch (color)
            {
                case ChessColor.White:
                    this.icon = "\u2659";
                    break;
                case ChessColor.Black:
                    this.icon = "\u265F";
                    break;
                default:
                    break;
            }

            this.position = position;
        }

        public override List<Position> GetAListOfMoves(ChessBoard chessBoard, TypeMove typeMove)
        {
            List<Position> listOfMoves = new List<Position>();

            int side;
            if (this.color == ChessColor.White)
            {
                side = -1;
            }
            else
            {
                side = +1;
            }


            if (typeMove == TypeMove.Move)
            {
                ChessBoard tmpChessBoard = (ChessBoard)chessBoard.Clone();
                tmpChessBoard.Move(this.position, new Position { V = this.position.V + (1 * side), H = this.position.H });
                //UP
                if (chessBoard.Board[this.position.V + (1 * side), this.position.H].piece == null
                    &&
                    tmpChessBoard.CheckKingAttacked(this.color)
                    )
                {
                    listOfMoves.Add(new Position { V = this.position.V + (1 * side), H = this.position.H });
                    //UP2
                    if (first && chessBoard.Board[this.position.V + (2 * side), this.position.H].piece == null)
                    {
                        listOfMoves.Add(new Position { V = this.position.V + (2 * side), H = this.position.H });
                    }
                }


                //L
                if (this.position.H - 1 > 0)
                {
                    if (
                        (
                        chessBoard.Board[this.position.V + (1 * side), this.position.H - 1].piece != null
                        &&
                        chessBoard.Board[this.position.V + (1 * side), this.position.H - 1].piece.color != this.color
                        )
                        ||
                         (
                          chessBoard.Board[this.position.V, this.position.H - 1].piece is Pawn
                          &&
                          ((Pawn)chessBoard.Board[this.position.V, this.position.H - 1].piece).takeOnTheAisle
                          &&
                          chessBoard.Board[this.position.V, this.position.H - 1].piece.color != this.color
                         )
                       )
                    {
                        tmpChessBoard = (ChessBoard)chessBoard.Clone();
                        tmpChessBoard.Move(this.position, new Position { V = this.position.V + (1 * side), H = this.position.H - 1 });

                        if (tmpChessBoard.CheckKingAttacked(this.color))
                        {
                            listOfMoves.Add(new Position { V = this.position.V + (1 * side), H = this.position.H - 1 });
                        }
                    }
                }
                //R
                if (this.position.H + 1 < 9)
                {
                    if (
                      (
                      chessBoard.Board[this.position.V + (1 * side), this.position.H + 1].piece != null
                      &&
                      chessBoard.Board[this.position.V + (1 * side), this.position.H + 1].piece.color != this.color
                      )
                      ||
                       (
                        chessBoard.Board[this.position.V, this.position.H + 1].piece is Pawn
                        &&
                        ((Pawn)chessBoard.Board[this.position.V, this.position.H + 1].piece).takeOnTheAisle
                        &&
                        chessBoard.Board[this.position.V, this.position.H + 1].piece.color != this.color
                       )
                     )
                    {
                        tmpChessBoard = (ChessBoard)chessBoard.Clone();
                        tmpChessBoard.Move(this.position, new Position { V = this.position.V + (1 * side), H = this.position.H + 1 });
                        if (tmpChessBoard.CheckKingAttacked(this.color))
                        {
                            listOfMoves.Add(new Position { V = this.position.V + (1 * side), H = this.position.H + 1 });
                        }
                    }
                }
            }
            else
            {
                //L
                if (this.position.H - 1 > 0)
                {
                    listOfMoves.Add(new Position { V = this.position.V + (1 * side), H = this.position.H - 1 });
                }
                //R
                if (this.position.H + 1 < 9)
                {
                    listOfMoves.Add(new Position { V = this.position.V + (1 * side), H = this.position.H + 1 });
                }
            }

            return listOfMoves;
        }

        public override object Clone()
        {
            return new Pawn
            {
                color = this.color,
                icon = this.icon,
                position = (Position)this.position.Clone(),
                first = this.first,
                takeOnTheAisle = this.takeOnTheAisle
            };
        }
    }
}
