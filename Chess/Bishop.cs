﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Bishop:Piece
    {
        public Bishop()
        {

        }

        public Bishop(ChessColor color, Position position)
        {
            this.color = color;
            switch (color)
            {
                case ChessColor.White:
                    this.icon = "\u2657";
                    break;
                case ChessColor.Black:
                    this.icon = "\u265D";
                    break;
                default:
                    break;
            }

            this.position = position;
        }

        public override object Clone()
        {
            return new Bishop
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

            //UL
            bool ul = checkMove(new Position { V = this.position.V - 1, H = this.position.H - 1 }, chessBoard, typeMove);
            for (int i = this.position.V - 1, j = this.position.H - 1; ul && i > 0 && j > 0; i--, j--)
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
            //UR
            bool ur = checkMove(new Position { V = this.position.V - 1, H = this.position.H + 1 }, chessBoard, typeMove);
            for (int i = this.position.V - 1, j = this.position.H + 1; ur && i > 0 && j < 9; i--, j++)
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
            //DL
            bool dl = checkMove(new Position { V = this.position.V + 1, H = this.position.H - 1 }, chessBoard, typeMove);
            for (int i = this.position.V + 1, j = this.position.H - 1; dl && i < 9 && j > 0; i++, j--)
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
            //DR
            bool dr = checkMove(new Position { V = this.position.V + 1, H = this.position.H + 1 }, chessBoard, typeMove);
            for (int i = this.position.V + 1, j = this.position.H + 1; dr && i < 9 && j < 9; i++, j++)
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
