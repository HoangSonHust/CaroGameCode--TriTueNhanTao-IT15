using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caro_Game
{
    class Board
    {
        private int _NumberOfRow;
        private int _NumberOfColumn;
        public int NumberofRow
        {
            get { return _NumberOfRow; }
        }

        public int NumberOfColumn
        {
            get { return _NumberOfColumn; }
        }

        public Board()



        {
            _NumberOfRow = 0;
            _NumberOfColumn = 0;
        }

        public Board(int Row, int Column)
        {
            _NumberOfRow = Row;
            _NumberOfColumn = Column;
        }
        public void DrawBoard(Graphics g)
        {
            for (int Column = 0; Column <= _NumberOfColumn; Column++)
            {
                g.DrawLine(Program.pen, Column * Box._BoxWidth, 0, Column * Box._BoxWidth, _NumberOfColumn * Box._BoxHeight);
            }

            for (int Row = 0; Row <= _NumberOfRow; Row++)
            {
                g.DrawLine(Program.pen, 0, Row * Box._BoxHeight, _NumberOfRow * Box._BoxHeight, Row * Box._BoxHeight);
            }
        }
    }
}
