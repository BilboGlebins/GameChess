using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class King:Piece
    {
        public bool castling;

        public King()
        {
        }

        public King(ChessColor color, Position position, bool castling = true)
        {
            this.color = color;
            this.castling = castling;
            switch (color)
            {
                case ChessColor.White:
                    this.icon = "\u2654";
                    break;
                case ChessColor.Black:
                    this.icon = "\u265A";
                    break;
                default:
                    break;
            }

            this.position = position;
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

            //UP
            if (checkMove(new Position { V = this.position.V - 1, H = this.position.H }, chessBoard, typeMove))
            {
                listOfMoves.Add(new Position { V = this.position.V - 1, H = this.position.H });
            }
            //DOWN
            if (checkMove(new Position { V = this.position.V + 1, H = this.position.H }, chessBoard, typeMove))
            {
                listOfMoves.Add(new Position { V = this.position.V + 1, H = this.position.H });
            }
            //LEFT           
            if (checkMove(new Position { V = this.position.V, H = this.position.H - 1 }, chessBoard, typeMove))
            {
                listOfMoves.Add(new Position { V = this.position.V, H = this.position.H - 1 });
            }
            //RIGHT            
            if (checkMove(new Position { V = this.position.V, H = this.position.H + 1 }, chessBoard, typeMove))
            {
                listOfMoves.Add(new Position { V = this.position.V, H = this.position.H + 1 });
            }
            //UL           
            if (checkMove(new Position { V = this.position.V - 1, H = this.position.H - 1 }, chessBoard, typeMove))
            {
                listOfMoves.Add(new Position { V = this.position.V - 1, H = this.position.H - 1 });
            }
            //UR           
            if (checkMove(new Position { V = this.position.V - 1, H = this.position.H + 1 }, chessBoard, typeMove))
            {
                listOfMoves.Add(new Position { V = this.position.V - 1, H = this.position.H + 1 });
            }
            //DL           
            if (checkMove(new Position { V = this.position.V + 1, H = this.position.H - 1 }, chessBoard, typeMove))
            {
                listOfMoves.Add(new Position { V = this.position.V + 1, H = this.position.H - 1 });
            }
            //DR           
            if (checkMove(new Position { V = this.position.V + 1, H = this.position.H + 1 }, chessBoard, typeMove))
            {
                listOfMoves.Add(new Position { V = this.position.V + 1, H = this.position.H + 1 });
            }
            /*
            if (castling)
            {
                //LEFT CASTLING
                List<Position> leftCastlingList = new List<Position>();
                leftCastlingList.Add(new Position { V = this.position.V, H = this.position.H - 1 });
                leftCastlingList.Add(new Position { V = this.position.V, H = this.position.H - 2 });

                if (
                    chessBoard.Board[this.position.V, this.position.H - 1].piece == null
                    &&
                    chessBoard.Board[this.position.V, this.position.H - 2].piece == null
                    &&
                    chessBoard.Board[this.position.V, this.position.H - 3].piece == null
                    &&
                    chessBoard.Board[this.position.V, this.position.H - 4].piece is Rook
                    &&
                    ((Rook)chessBoard.Board[this.position.V, this.position.H - 4].piece).castling
                    &&
                    chessBoard.CheckCellsIsAttacked(leftCastlingList, this.color)
                   )
                {
                    listOfMoves.Add(new Position { V = this.position.V, H = this.position.H - 2 });
                }
                //RIGHT CASTLING
                List<Position> rightCastlingList = new List<Position>();
                leftCastlingList.Add(new Position { V = this.position.V, H = this.position.H + 1 });
                leftCastlingList.Add(new Position { V = this.position.V, H = this.position.H + 2 });
                if (
                    chessBoard.Board[this.position.V, this.position.H + 1].piece == null
                    &&
                    chessBoard.Board[this.position.V, this.position.H + 2].piece == null
                    &&
                    chessBoard.Board[this.position.V, this.position.H + 3].piece is Rook
                    &&
                    ((Rook)chessBoard.Board[this.position.V, this.position.H + 3].piece).castling
                    &&
                    chessBoard.CheckCellsIsAttacked(rightCastlingList, this.color)
                   )
                {
                    listOfMoves.Add(new Position { V = this.position.V, H = this.position.H + 2 });
                }
            }
            */
            return listOfMoves;
        }

        public override object Clone()
        {
            return new King
            {
                color = this.color,
                icon = this.icon,
                position = (Position)this.position.Clone(),
                castling = this.castling
            };
        }
    }
}