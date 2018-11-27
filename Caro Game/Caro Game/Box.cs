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
        public const int _BoxHeight = 25;
        public const int _BoxWidth = 25;
        private int _Row;
        private int _Column;
        private Point _Location;
        private int _OwnBy;
        public int Row { get => _Row; set => _Row = value; }
        public int Column { get => _Column; set => _Column = value; }
        public Point Location { get => _Location; set => _Location = value; }
        public int OwnBy { get => _OwnBy; set => _OwnBy = value; }
        //hoang son
    }
}
