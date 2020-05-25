using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Rook : Piece
    {
        public Rook()
        {

        }

        public Rook(ChessColor color, Position position)
        {
            this.color = color;

            switch (color)
            {
                case ChessColor.White:
                    this.icon = "\u2656";
                    break;
                case ChessColor.Black:
                    this.icon = "\u265C";
                    break;
                default:
                    break;
            }
            this.position = position;
        }
        public override object Clone()
        {
            return new Rook
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

            //UP
            bool up = checkMove(new Position { V = this.position.V - 1, H = this.position.H }, chessBoard, typeMove);
            for (int i = this.position.V - 1, j = this.position.H; up && i > 0; i--)
            {
                if (chessBoard.Board[i, j].piece == null)
                {
                    listOfMoves.Add(new Position { V = i, H = j });
                }
                else
                {
                    if (chessBoard.Board[i, j].piece.color != this.color)
                    {
                        listOfMoves.Add(new Position { V = i, H = j });
                    }
                    break;
                }
            }
            //DOWN
            bool down = checkMove(new Position { V = this.position.V + 1, H = this.position.H }, chessBoard, typeMove);
            for (int i = this.position.V + 1, j = this.position.H; down && i < 9; i++)
            {
                if (chessBoard.Board[i, j].piece == null)
                {
                    listOfMoves.Add(new Position { V = i, H = j });
                }
                else
                {
                    if (chessBoard.Board[i, j].piece.color != this.color)
                    {
                        listOfMoves.Add(new Position { V = i, H = j });
                    }
                    break;
                }
            }
            //LEFT
            bool left = checkMove(new Position { V = this.position.V, H = this.position.H - 1 }, chessBoard, typeMove);
            for (int i = this.position.V, j = this.position.H - 1; left && j > 0; j--)
            {
                if (chessBoard.Board[i, j].piece == null)
                {
                    listOfMoves.Add(new Position { V = i, H = j });
                }
                else
                {
                    if (chessBoard.Board[i, j].piece.color != this.color)
                    {
                        listOfMoves.Add(new Position { V = i, H = j });
                    }
                    break;
                }
            }
            //RIGHT
            bool right = checkMove(new Position { V = this.position.V, H = this.position.H + 1 }, chessBoard, typeMove);
            for (int i = this.position.V, j = this.position.H + 1; right && j < 9; j++)
            {
                if (chessBoard.Board[i, j].piece == null)
                {
                    listOfMoves.Add(new Position { V = i, H = j });
                }
                else
                {
                    if (chessBoard.Board[i, j].piece.color != this.color)
                    {
                        listOfMoves.Add(new Position { V = i, H = j });
                    }
                    break;
                }
            }

            return listOfMoves;
        }
    }
}
