using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    public class Position
    {
        public int Row = -1;
        public int Col = -1;
        public static Position Empty
        {
            get { return new Position(-1, -1); }
        }
        public Position(int pRow, int pCol)
        {
            Row = pRow;
            Col = pCol;
        }
        public int GetHashCode()
        {
            return (Row.ToString() + "_" + Col.ToString()).GetHashCode();
        }
        public Position Clone()
        {
            Position NewPostion = new Position(this.Row, this.Col);
            return NewPostion;
        }
        public string PositionString()
        {
            return Row.ToString() + "," + Col.ToString();
        }
    }
}
