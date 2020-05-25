using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    internal class Game
    {
        public static ChessColor currentColor = ChessColor.White;
        public static ChessBoard chessBoard = new ChessBoard();

        static public int selectedPosV = 0;
        static public int selectedPosH = 0;

        static public bool selected = false;

        public Game()
        {
            chessBoard.SetValues();
            chessBoard.SetColors();
            chessBoard.AddPieces();
            chessBoard.DrawChessBoard();

            TwoPlayer();
        }

        private void TwoPlayer()
        {
            while (true)
            {
                Cursor.Move();
            }
        }

        internal static bool TrueMove()
        {
            foreach (Position position in chessBoard.Board[selectedPosV, selectedPosH].piece.GetAListOfMoves(chessBoard, TypeMove.Move))
            {
                if (Cursor.cursorPosH == position.H && Cursor.cursorPosV == position.V)
                {
                    return true;
                }
            }

            return false;
        }
    }
}