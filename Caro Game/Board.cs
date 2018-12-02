using System.Drawing;

namespace Caro_Game
{
    class Board
    {
        private int _NumberOfRow; // số hàng
        private int _NumberOfColumn; // số cột
        public int NumberofRow
        {
            get { return _NumberOfRow; }
        }

        public int NumberOfColumn
        {
            get { return _NumberOfColumn; }
        }

        public Board() // hàm khởi tạo
        {
            _NumberOfRow = 0;
            _NumberOfColumn = 0;
        }

        public Board(int Row, int Column) // hàm khởi tạo để chuyền tham số bàn cờ
        {
            _NumberOfRow = 20;
            _NumberOfColumn = 20;
        }
        public void DrawBoard(Graphics g) // vẽ bàn cờ
        {//chạy vòng lặp để vẽ các cột
            for (int Column = 0; Column <= _NumberOfColumn; Column++)
            {
                g.DrawLine(CaroChess.pen, Column * Box._BoxWidth, 0, Column * Box._BoxWidth, _NumberOfColumn * Box._BoxHeight);
            }
            // vẽ các hàng 
            for (int Row = 0; Row <= _NumberOfRow; Row++)
            {
                g.DrawLine(CaroChess.pen, 0, Row * Box._BoxHeight, _NumberOfRow * Box._BoxHeight, Row * Box._BoxHeight);
            }
        }
        public void DrawBox(Graphics g, Point point, SolidBrush sb)
        {
            g.FillEllipse(sb, point.X+2, point.Y+2, Box._BoxWidth-4, Box._BoxHeight-4);
        }

        public void DeleteBox(Graphics g, Point point, SolidBrush sb)
        {
            g.FillRectangle(sb, point.X+1, point.Y+1, Box._BoxWidth-2, Box._BoxHeight-2);
        }
    }
}

