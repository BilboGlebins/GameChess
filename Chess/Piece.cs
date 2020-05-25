using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public enum ChessColor { White, Black };
    public enum TypeMove { Move, PossibleAttack };
    public struct Position : ICloneable
    {
        public int V;
        public int H;

        public Position(int posV, int PosH)
        {
            this.V = posV;
            this.H = PosH;
        }

        public static bool operator ==(Position left, Position right)
        {
            if (left.V == right.V && left.H == right.H)
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(Position left, Position right)
        {
            if (left.V != right.V || left.H != right.H)
            {
                return true;
            }
            return false;
        }

        public object Clone()
        {
            return new Position
            {
                V = this.V,
                H = this.H
            };
        }
    }

    abstract public class Piece : ICloneable
    {
        public Position position;
        public string icon;
        public ChessColor color;

        public abstract object Clone();

        public abstract List<Position> GetAListOfMoves(ChessBoard chessBoard, TypeMove typeMove);
    }
}