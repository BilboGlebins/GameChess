using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    static class Cursor
    {
        static public int cursorPosV = 5;
        static public int cursorPosH = 5;

        static public void Move()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.NumPad8)
            {
                if (CheckCursorPosition(cursorPosV - 1, cursorPosH))
                {
                    cursorPosV -= 1;
                }
            }

            if (key.Key == ConsoleKey.NumPad2)
            {
                if (CheckCursorPosition(cursorPosV + 1, cursorPosH))
                {
                    cursorPosV += 1;
                }
            }

            if (key.Key == ConsoleKey.NumPad4)
            {
                if (CheckCursorPosition(cursorPosV, cursorPosH - 1))
                {
                    cursorPosH -= 1;
                }
            }

            if (key.Key == ConsoleKey.NumPad6)
            {
                if (CheckCursorPosition(cursorPosV, cursorPosH + 1))
                {
                    cursorPosH += 1;
                }
            }

            if (key.Key == ConsoleKey.Spacebar)
            {
                if (Game.chessBoard.Board[cursorPosV, cursorPosH].piece != null)
                {
                    if (Game.currentColor == Game.chessBoard.Board[cursorPosV, cursorPosH].piece.color)
                    {
                        if (Game.selected && Game.selectedPosV == cursorPosV && Game.selectedPosH == cursorPosH)
                        {
                            Game.selected = false;

                            Display.ClearCustomColor(false);
                        }
                        else
                        {
                            Display.ClearCustomColor(false);

                            Game.selectedPosV = cursorPosV;
                            Game.selectedPosH = cursorPosH;

                            //List<Position> positions = Game.chessBoard.Board[Game.selectedPosV, Game.selectedPosH].piece.GetAListOfMoves(Game.chessBoard, TypeMove.Move);

                            //Display.DrawCustomColor(positions, ConsoleColor.Blue, false);

                            Game.selected = true;
                        }
                    }
                }
            }

            if (key.Key == ConsoleKey.Enter)
            {
                if (Game.selected && Game.TrueMove())
                {

                    Game.chessBoard.Move(new Position { V = Game.selectedPosV, H = Game.selectedPosH }, new Position { V = cursorPosV, H = cursorPosH }, true);


                    //return false;
                }
            }


            DrawCursor();
        }

        internal static int SwitchNewPiece()
        {
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.D1)
                {
                    return 1;
                }

                if (key.Key == ConsoleKey.D2)
                {
                    return 2;
                }

                if (key.Key == ConsoleKey.D3)
                {
                    return 3;
                }

                if (key.Key == ConsoleKey.D4)
                {
                    return 4;
                }
            }
        }

        private static void DrawCursor()
        {
            Display.DrawCustomColor(cursorPosV, cursorPosH, ConsoleColor.Green, true);
        }

        static private bool CheckCursorPosition(int _cursorPosV, int _cursorPosH)
        {
            if (_cursorPosV > 0 && _cursorPosV < 9 && _cursorPosH > 0 && _cursorPosH < 9)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
