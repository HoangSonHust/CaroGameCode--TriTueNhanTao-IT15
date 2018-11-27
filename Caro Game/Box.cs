using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caro_Game
{
    class Box
    {
        public const int _BoxHeight = 25; // chiều cao của 1 thuộc tính ô cờ
        public const int _BoxWidth = 25; // chiều rộng của ô cờ => cao= rộng => hình vuông
        private int _Row; 
        private int _Column;
        private Point _Location;
        private int _OwnBy; // được sở hữu bởi ai
        public int Row { get => _Row; set => _Row = value; }
        public int Column { get => _Column; set => _Column = value; }
        public Point Location { get => _Location; set => _Location = value; }
        public int OwnBy { get => _OwnBy; set => _OwnBy = value; }
        public Box(int Row, int Column, Point Location, int OwnBy)
        {
            _Row = Row;
            _Column = Column;
            _Location = Location;
            _OwnBy = OwnBy;
        }

        public Box()
        {
        }
    }
}
