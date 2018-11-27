using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caro_Game
{
    class CaroChess
    {
        private Box[,] _ArrayBox;
        private Board _Board;
        public CaroChess()
        {
            _Board = new Board(20, 20);
        }

        public void DrawBoard(Graphics g)
        {
            _Board.DrawBoard(g);
        }

        public void InitArrayBox()
        {
            for (int i=0; i< _Board.NumberofRow; i++)
            {
                for (int j=0; j< _Board.NumberOfColumn; j++)
                {
                    _ArrayBox[i, j] = new Box();
                }
            }
        }
    }
}
