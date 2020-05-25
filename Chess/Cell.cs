using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Cell : ICloneable
    {
        public ConsoleColor defaultColor = ConsoleColor.Magenta;
        public ConsoleColor customColor = ConsoleColor.DarkMagenta;

        public string defaultIcon = " ";

        public Piece piece = null;

        public Cell()
        {

        }

        public Cell(Piece piece)
        {
            this.piece = piece;
        }

        public object Clone()
        {
            return new Cell
            {
                customColor = this.customColor,
                defaultColor = this.defaultColor,
                defaultIcon = this.defaultIcon,
                piece = this.piece != null ? (Piece)this.piece.Clone() : null
            };
        }
    }
}
