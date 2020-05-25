using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class ChessBoard : ICloneable
    {
        public Cell[,] Board = new Cell[10, 10];

        public List<Piece> WhitePieces = new List<Piece>();
        public List<Piece> BlackPieces = new List<Piece>();

        public King WhiteKing;
        public King BlackKing;

        public ChessBoard()
        {
            InitializeBoard();
        }

        public object Clone()
        {
            ChessBoard cloneChessBoard = new ChessBoard();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    cloneChessBoard.Board[i, j] = (Cell)this.Board[i, j].Clone();
                    if (cloneChessBoard.Board[i, j].piece != null)
                    {
                        if (cloneChessBoard.Board[i, j].piece.color == ChessColor.White)
                        {
                            if (cloneChessBoard.Board[i, j].piece is King)
                            {
                                cloneChessBoard.WhiteKing = (King)cloneChessBoard.Board[i, j].piece;
                            }
                            cloneChessBoard.WhitePieces.Add(cloneChessBoard.Board[i, j].piece);
                        }
                        else
                        {
                            if (cloneChessBoard.Board[i, j].piece is King)
                            {
                                cloneChessBoard.BlackKing = (King)cloneChessBoard.Board[i, j].piece;
                            }
                            cloneChessBoard.BlackPieces.Add(cloneChessBoard.Board[i, j].piece);
                        }
                    }
                }
            }

            return cloneChessBoard;
        }

        public void Move(Position start, Position end, bool human = false, int newPiece = 1)
        {
            if (this.Board[start.V, start.H].piece is Pawn)
            {

                int side;
                if (this.Board[start.V, start.H].piece.color == ChessColor.White)
                {
                    side = -1;
                }
                else
                {
                    side = +1;
                }

                ((Pawn)this.Board[start.V, start.H].piece).first = false;

                if (Math.Abs(end.V - start.V) == 2)
                {
                    ((Pawn)this.Board[start.V, start.H].piece).takeOnTheAisle = true;
                }

                if (start.H != end.H)
                {
                    if (start.H - end.H > 0)
                    {
                        //LEFT ATTACK
                        if (this.Board[start.V + (1 * side), start.H - 1].piece == null)
                        {
                            DeletePieceFromList(new Position { V = start.V, H = start.H - 1 });
                        }
                        if (human)
                        {
                            Display.Draw(start.V, start.H - 1);
                        }
                    }
                    else
                    {
                        //RIGHT ATTACK
                        if (this.Board[start.V + (1 * side), start.H + 1].piece == null)
                        {
                            DeletePieceFromList(new Position { V = start.V, H = start.H + 1 });
                        }
                        if (human)
                        {
                            Display.Draw(start.V, start.H + 1);
                        }
                    }
                }

                if (end.V == 1 || end.V == 8)
                {
                    this.Board[start.V, start.H].piece = SelectNewPiece(start, human, newPiece);
                    if (this.Board[start.V, start.H].piece.color == ChessColor.White)
                    {
                        for (int i = 0; i < WhitePieces.Count(); i++)
                        {
                            if (WhitePieces[i].position == this.Board[start.V, start.H].piece.position)
                            {
                                WhitePieces[i] = this.Board[start.V, start.H].piece;
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < BlackPieces.Count(); i++)
                        {
                            if (BlackPieces[i].position == this.Board[start.V, start.H].piece.position)
                            {
                                BlackPieces[i] = this.Board[start.V, start.H].piece;
                                break;
                            }
                        }
                    }
                }
            }
            /*
            if (this.Board[start.V, start.H].piece is King)
            {
                if (((King)this.Board[start.V, start.H].piece).castling && Math.Abs(end.H - start.H) == 2)
                {

                    if (start.H - end.H > 0)
                    {
                        //LeftCastling
                        if (this.Board[start.V, 1].piece != null && this.Board[start.V, 1].piece is Rook && ((Rook)this.Board[start.V, 1].piece).castling)
                        {
                            this.Board[start.V, 1].piece.position = new Position { V = start.V, H = 4 };
                            this.Board[start.V, 4].piece = this.Board[start.V, 1].piece;
                            this.Board[start.V, 1].piece = null;
                        }
                        if (human)
                        {
                            Display.Draw(start.V, 1);
                            Display.Draw(start.V, 4);
                        }
                    }
                    else
                    {
                        //RightCastling
                        if (this.Board[start.V, 8].piece != null && this.Board[start.V, 8].piece is Rook && ((Rook)this.Board[start.V, 8].piece).castling)
                        {
                            this.Board[start.V, 8].piece.position = new Position { V = start.V, H = 6 };
                            this.Board[start.V, 6].piece = this.Board[start.V, 8].piece;
                            this.Board[start.V, 8].piece = null;
                        }
                        if (human)
                        {
                            Display.Draw(start.V, 8);
                            Display.Draw(start.V, 6);
                        }
                    }


                }
                ((King)this.Board[start.V, start.H].piece).castling = false;
            }

            if (this.Board[start.V, start.H].piece is Rook)
            {
                ((Rook)this.Board[start.V, start.H].piece).castling = false;
            }

            */
            //ATTACK
            if (this.Board[end.V, end.H].piece != null)
            {
                DeletePieceFromList(end);
            }

            /*
            this.Board[start.V, start.H].piece.position = new Position { V = end.V, H = end.H };
            */
            this.Board[start.V, start.H].piece.position.V = end.V;
            this.Board[start.V, start.H].piece.position.H = end.H;

            this.Board[end.V, end.H].piece = this.Board[start.V, start.H].piece;
            this.Board[start.V, start.H].piece = null;
            if (human)
            {
                Display.Draw(end.V, end.H);
                Display.Draw(start.V, start.H);
            }
        }


        private Piece SelectNewPiece(Position position, bool human = false, int newPiece = 1)
        {/*
            if (human)
            {
                //Console.WriteLine("!!!!!");///////
                //Display.DrawSelectNewPieceMenu();
                switch (Cursor.SwitchNewPiece())
                {
                    case 1:
                        return new Queen(this.Board[position.V, position.H].piece.color, new Position { V = position.V, H = position.H });
                    case 2:
                        return new Rook(this.Board[position.V, position.H].piece.color, new Position { V = position.V, H = position.H });
                    case 3:
                        return new Knight(this.Board[position.V, position.H].piece.color, new Position { V = position.V, H = position.H });
                    case 4:
                        return new Bishop(this.Board[position.V, position.H].piece.color, new Position { V = position.V, H = position.H });
                    default:
                        break;
                }
            }
            else
            {
                switch (newPiece)
                {
                    case 1:
                        return new Queen(this.Board[position.V, position.H].piece.color, new Position { V = position.V, H = position.H });
                    case 2:
                        return new Rook(this.Board[position.V, position.H].piece.color, new Position { V = position.V, H = position.H });
                    case 3:
                        return new Knight(this.Board[position.V, position.H].piece.color, new Position { V = position.V, H = position.H });
                    case 4:
                        return new Bishop(this.Board[position.V, position.H].piece.color, new Position { V = position.V, H = position.H });
                    default:
                        break;
                }
            }*/
            return null;
        }

        private void DeletePieceFromList(Position position)
        {
            if (this.Board[position.V, position.H].piece.color == ChessColor.White)
            {
                for (int i = 0; i < WhitePieces.Count(); i++)
                {
                    if (WhitePieces[i].position == position)
                    {
                        WhitePieces.Remove(WhitePieces[i]);
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < BlackPieces.Count(); i++)
                {
                    if (BlackPieces[i].position == position)
                    {
                        BlackPieces.Remove(BlackPieces[i]);
                        break;
                    }
                }
            }


            Board[position.V, position.H].piece = null;
        }

        public List<Position> GetAListOfAllMoves(ChessColor chessColor, TypeMove typeMove)
        {
            List<Position> allMoves = new List<Position>();

            if (chessColor == ChessColor.White)
            {
                foreach (Piece piece in WhitePieces)
                {
                    allMoves.AddRange(piece.GetAListOfMoves(this, typeMove));
                }
            }
            else
            {
                foreach (Piece piece in BlackPieces)
                {
                    allMoves.AddRange(piece.GetAListOfMoves(this, typeMove));
                }
            }
            allMoves = allMoves.Distinct().ToList();
            return allMoves;
        }

        public bool CheckKingAttacked(ChessColor chessColor)
        {
            ChessColor enemyChessColor = chessColor == ChessColor.White ? ChessColor.Black : ChessColor.White;
            List<Position> attackPositions = GetAListOfAllMoves(enemyChessColor, TypeMove.PossibleAttack);

            if (chessColor == ChessColor.White)
            {
                foreach (Position attackPosition in attackPositions)
                {
                    if (WhiteKing.position == attackPosition)
                        return false;
                }
            }
            else
            {
                foreach (Position attackPosition in attackPositions)
                {
                    if (BlackKing.position == attackPosition)
                        return false;
                }
            }
            return true;
        }

        public void DrawChessBoard()
        {
            for (int i = 0; i < 10; i++)
            {
                Display.Draw(i, 0, ConsoleColor.Black);
                Display.Draw(i, 9, ConsoleColor.Black);
                Display.Draw(0, i, ConsoleColor.Black);
                Display.Draw(9, i, ConsoleColor.Black);
            }

            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    Display.Draw(i, j);
                }
            }
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Board[i, j] = new Cell();
                }
            }
        }

        public void SetValues()
        {
            Board[0, 0].defaultIcon = "#";
            Board[0, 9].defaultIcon = "#";
            Board[9, 0].defaultIcon = "#";
            Board[9, 9].defaultIcon = "#";

            for (int i = 1; i < 9; i++)
            {
                Board[i, 0].defaultIcon = (9 - i).ToString();
                Board[i, 9].defaultIcon = (9 - i).ToString();
                Board[0, i].defaultIcon = (Convert.ToChar('A' + i - 1)).ToString();
                Board[9, i].defaultIcon = (Convert.ToChar('A' + i - 1)).ToString();
            }

            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    Board[i, j].defaultIcon = " ";
                }
            }
        }

        public void SetColors()
        {
            Board[0, 0].defaultColor = ConsoleColor.Gray;
            Board[0, 9].defaultColor = ConsoleColor.Gray;
            Board[9, 0].defaultColor = ConsoleColor.Gray;
            Board[9, 9].defaultColor = ConsoleColor.Gray;

            for (int i = 1; i < 9; i++)
            {
                Board[i, 0].defaultColor = ConsoleColor.Gray;
                Board[i, 9].defaultColor = ConsoleColor.Gray;
                Board[0, i].defaultColor = ConsoleColor.Gray;
                Board[9, i].defaultColor = ConsoleColor.Gray;
            }

            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        Board[i, j].defaultColor = ConsoleColor.White;
                    }
                    else
                    {
                        Board[i, j].defaultColor = ConsoleColor.Black;
                    }
                }
            }
        }

        public void AddPieces()
        {
            //Pawns (пешка)
            for (int i = 1; i <= 8; i++)
            {
                Board[2, i].piece = new Pawn(ChessColor.Black, new Position(2, i));
                BlackPieces.Add(Board[2, i].piece);

                Board[7, i].piece = new Pawn(ChessColor.White, new Position(7, i));
                WhitePieces.Add(Board[7, i].piece);
            }

            //Kings (король)
            Board[1, 5].piece = new King(ChessColor.Black, new Position(1, 5));
            BlackPieces.Add(Board[1, 5].piece);
            BlackKing = (King)Board[1, 5].piece;

            Board[8, 5].piece = new King(ChessColor.White, new Position(8, 5));
            WhitePieces.Add(Board[8, 5].piece);
            WhiteKing = (King)Board[8, 5].piece;

            //Queens (королева)
            Board[1, 4].piece = new Queen(ChessColor.Black, new Position(1, 4));
            BlackPieces.Add(Board[1, 4].piece);

            Board[8, 4].piece = new Queen(ChessColor.White, new Position(8, 4));
            WhitePieces.Add(Board[8, 4].piece);

            //Bishops (слон)
            Board[1, 3].piece = new Bishop(ChessColor.Black, new Position(1, 3));
            BlackPieces.Add(Board[1, 3].piece);
            Board[1, 6].piece = new Bishop(ChessColor.Black, new Position(1, 6));
            BlackPieces.Add(Board[1, 6].piece);

            Board[8, 3].piece = new Bishop(ChessColor.White, new Position(8, 3));
            WhitePieces.Add(Board[8, 3].piece);
            Board[8, 6].piece = new Bishop(ChessColor.White, new Position(8, 6));
            WhitePieces.Add(Board[8, 6].piece);

            //Knights (конь)
            Board[1, 2].piece = new Knight(ChessColor.Black, new Position(1, 2));
            BlackPieces.Add(Board[1, 2].piece);
            Board[1, 7].piece = new Knight(ChessColor.Black, new Position(1, 7));
            BlackPieces.Add(Board[1, 7].piece);

            Board[8, 2].piece = new Knight(ChessColor.White, new Position(8, 2));
            WhitePieces.Add(Board[8, 2].piece);
            Board[8, 7].piece = new Knight(ChessColor.White, new Position(8, 7));
            WhitePieces.Add(Board[8, 7].piece);

            //Rooks (ладья)
            Board[1, 1].piece = new Rook(ChessColor.Black, new Position(1, 1));
            BlackPieces.Add(Board[1, 1].piece);
            Board[1, 8].piece = new Rook(ChessColor.Black, new Position(1, 8));
            BlackPieces.Add(Board[1, 8].piece);

            Board[8, 1].piece = new Rook(ChessColor.White, new Position(8, 1));
            WhitePieces.Add(Board[8, 1].piece);
            Board[8, 8].piece = new Rook(ChessColor.White, new Position(8, 8));
            WhitePieces.Add(Board[8, 8].piece);
        }
    }
}
