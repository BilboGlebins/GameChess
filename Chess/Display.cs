using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    internal class Display
    {

        public static void Draw(int posV, int posH, ConsoleColor fontColor = ConsoleColor.DarkGray)
        {
            Console.BackgroundColor = Game.chessBoard.Board[posV, posH].defaultColor;
            Console.SetCursorPosition(posH, posV);

            Console.ForegroundColor = fontColor;

            if (Game.chessBoard.Board[posV, posH].piece != null)            
            {
                Console.Write(Game.chessBoard.Board[posV, posH].piece.icon);
            }
            else
            {
                Console.Write(Game.chessBoard.Board[posV, posH].defaultIcon);
            }

            Console.ResetColor();
            Console.SetCursorPosition(9, 9);
        }

        public static void DrawCustomColor(int posV, int posH, ConsoleColor color, bool clear = true)
        {
            if (clear)
            {
                ClearCustomColor();
            }

            Game.chessBoard.Board[posV, posH].customColor = color;

            Console.BackgroundColor = color;
            Console.SetCursorPosition(posH, posV);

            if (Game.chessBoard.Board[posV, posH].piece != null)
            {
                Console.Write(Game.chessBoard.Board[posV, posH].piece.icon);
            }
            else
            {
                Console.Write(Game.chessBoard.Board[posV, posH].defaultIcon);
            }

            Console.ResetColor();
            Console.SetCursorPosition(9, 9);
        }

        public static void ClearCustomColor(bool moves = true)
        {
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    if (Game.chessBoard.Board[i, j].customColor == ConsoleColor.Blue && moves)
                        continue;
                    if (Game.selected && Game.selectedPosV == i && Game.selectedPosH == j)
                        continue;
                    Game.chessBoard.Board[i, j].customColor = Game.chessBoard.Board[i, j].defaultColor;
                    Draw(i, j);
                }
            }
        }
    }
}