using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Caro_Game
{
    public enum FINISH
    {
        noOneWin, Player1, Player2, Computer
    }
    class CaroChess
    {
        public static Pen pen;
        public static SolidBrush sbWhite;
        public static SolidBrush sbBlack;
        public static SolidBrush SkyBlue;
        private Box[,] _ArrayBox; //khởi tạo mảng ô cờ
        private Board _Board;
        private int _Turn;
        private int _PlayMode;
        private bool _Ready;
        private Stack<Box> Stack_Step;
        private Stack<Box> Stack_Redo;
        private FINISH _Finish;

        public Option option;
        public string Name1;
        public string Name2;
        public string Name;
        public bool playerFist;
        public bool comFirst;


        public void optionForm1(string name1, string name2)
        {
            this.Name1 = name1;
            this.Name2 = name2;

        }
        public void optionForm2(string name, bool playerFist, bool comFirst)
        {

            this.Name = name;
            this.playerFist = playerFist;
            this.comFirst = comFirst;
        }
        //viết 1 đóng gói
        public bool Ready
        {
            get { return _Ready; }
        }


        public int PlayMode
        {
            get { return _PlayMode; }
        }

        public CaroChess()
        {
            pen = new Pen(Color.Red);
            sbBlack = new SolidBrush(Color.Black);
            sbWhite = new SolidBrush(Color.White);
            SkyBlue = new SolidBrush(Color.SkyBlue);
            _Board = new Board(20, 20);
            _ArrayBox = new Box[_Board.NumberofRow, _Board.NumberOfColumn]; // khởi tạo mảng có số giá trị là số cột và hàng
            Stack_Step = new Stack<Box>();
            Stack_Redo = new Stack<Box>();
            _Turn = 1;

        }

        public void DrawBoard(Graphics g)
        {
            _Board.DrawBoard(g);

        }

        public void InitArrayBox()
        {
            for (int i = 0; i < _Board.NumberofRow; i++) // i là số thứ tự của hàng mấy
            {
                for (int j = 0; j < _Board.NumberOfColumn; j++) // j là số thứ tự cột mấy
                {//new Point(j*Box._BoxWidth, i*Box._BoxHeight) : J là stt cột cho nên 
                 //để biết được ô thứ j tọa độ nào thì phải lấy j* với chiều rộng
                 //tương tự, lấy i*chiều cao
                    _ArrayBox[i, j] = new Box(i, j, new Point(j * Box._BoxWidth, i * Box._BoxHeight), 0); // truyền giá trị thuộc tính ô cờ
                }
            }
        }
        // phương thức chơi cờ, khi chuột click thì sẽ vẽ lên bàn cờ
        public bool PlayChess(int MouseX, int MouseY, Graphics g)
        {// nếu chuột chỉ vào đúng chỗ vạch kẻ(sẽ ko có ô nào được chọn)
         // có nghĩa là tọa độ X/chiều rộng, Y/dài ra đúng 1 số nguyên luôn
         // và số nguyên đó chính là vị trí ô
            if (MouseX % Box._BoxWidth == 0 || MouseY % Box._BoxHeight == 0)
            {
                return false;
            }
            //thuộc tính của ô cờ 
            int Column = MouseX / Box._BoxWidth; // vdu chuột có X=76, chiều rộng 25=> 76/25=3.04 lấy nguyên là 3=> cột 3
            int Row = MouseY / Box._BoxHeight;// tương tự như trên
                                              // truyền vào Board g(graphics), 

            if (_ArrayBox[Row, Column].OwnBy != 0)
            {
                return false;
            }
            switch (_Turn)
            {
                case 1:
                    _ArrayBox[Row, Column].OwnBy = 1;
                    _Board.DrawBox(g, _ArrayBox[Row, Column].Location, sbBlack);
                    _Turn = 2;
                    break;
                case 2:
                    _ArrayBox[Row, Column].OwnBy = 2;
                    _Board.DrawBox(g, _ArrayBox[Row, Column].Location, sbWhite);
                    _Turn = 1;
                    break;
                default:
                    MessageBox.Show("Have erro");
                    break;
            }
            Stack_Redo = new Stack<Box>();
            Box box = new Box(_ArrayBox[Row, Column].Row, _ArrayBox[Row, Column].Column, _ArrayBox[Row, Column].Location, _ArrayBox[Row, Column].OwnBy);
            Stack_Step.Push(box);
            return true;
        }
        public void RepainBox(Graphics g)
        {
            foreach (Box box in Stack_Step)
            {
                if (box.OwnBy == 1)
                {
                    _Board.DrawBox(g, box.Location, sbBlack);
                }
                else if (box.OwnBy == 2)
                {
                    _Board.DrawBox(g, box.Location, sbWhite);
                }
            }
        }
        #region start?
        public void StartPlayerVsPlayer(Graphics g)
        {
            _Ready = true;// nếu mới nhấn vào bắt đầu chơi thì là true
            InitArrayBox();// nếu có người chơi trc thì ta phải khởi tạo lại mảng ô cờ
            Stack_Step = new Stack<Box>();
            Stack_Redo = new Stack<Box>();
            _Turn = 1;
            _PlayMode = 1;
            InitArrayBox();
            DrawBoard(g);

        }
        public void StartPlayerVsCom(Graphics g)
        {
            _Ready = true;// nếu mới nhấn vào bắt đầu chơi thì là true
            InitArrayBox();// nếu có người chơi trc thì ta phải khởi tạo lại mảng ô cờ
            Stack_Step = new Stack<Box>();
            Stack_Redo = new Stack<Box>();
            _Turn = 1;
            _PlayMode = 2;
            InitArrayBox();
            DrawBoard(g);
            InitComputer(g);
        }
        #endregion start?
        #region AI
        private long[] ArrayAttackPoint = new long[7] { 0, 9, 81, 729, 6561, 6561, 531441 };
        private long[] ArrayDefendPoint = new long[7] { 0, 4, 256, 2048, 16384, 6561, 131072 };
        public void InitComputer(Graphics g)
        {
         /*   if (comFirst == true)
            {*/
                if (Stack_Step.Count == 0)
                {
                    PlayChess(_Board.NumberOfColumn / 2 * Box._BoxWidth + 1, _Board.NumberofRow / 2 * Box._BoxHeight + 1, g);
                }
                else
                {
                    Box box = FindTurn();
                    PlayChess(box.Location.X + 1, box.Location.Y + 1, g);
                }
           /* }
            if (playerFist == true)
            {

            }*/
        }

        private Box FindTurn()
        {
            Box boxResult = new Box();
            long totalMax = 0;

            for (int i = 0; i < _Board.NumberofRow; i++)
            {
                for (int j = 0; j < _Board.NumberOfColumn; j++)
                {

                    if (_ArrayBox[i, j].OwnBy == 0)
                    {
                        /* if (_ArrayBox[i, j].OwnBy == 2 && _ArrayBox[i + 1, j].OwnBy == 2 && _ArrayBox[i + 2, j].OwnBy == 2)
                         {
                             boxResult = new Box(_ArrayBox[i, j].Row, _ArrayBox[i, j].Column, _ArrayBox[i, j].Location, _ArrayBox[i, j].OwnBy);
                         }*/
                        long attackPoint = attackPoint_Vertical(i, j) + attackPoint_Horizontal(i, j) + attackPoint_DiagonalFromLeftToRight(i, j) + attackPoint_DiagonalFromRightToLeft(i, j);
                        long defendPoint = defendPoint_Vertical(i, j) + defendPoint_Horizontal(i, j) + defendPoint_DiagonalFromLeftToRight(i, j) + defendPoint_DiagonalFromRightToLeft(i, j);
                        long temPoint = attackPoint > defendPoint ? attackPoint : defendPoint;
                        /*  if (attackPoint <= defendPoint)
                          {
                              if (totalMax < defendPoint)
                              {
                                  totalMax = defendPoint;
                                  boxResult = new Box(_ArrayBox[i, j].Row, _ArrayBox[i, j].Column, _ArrayBox[i, j].Location, _ArrayBox[i, j].OwnBy);

                              }
                          }
                          else
                          {
                              if (totalMax < attackPoint) { 
                              totalMax = attackPoint;
                              }
                          }*/

                        if (totalMax < temPoint)
                        {
                            totalMax = temPoint;
                            boxResult = new Box(_ArrayBox[i, j].Row, _ArrayBox[i, j].Column, _ArrayBox[i, j].Location, _ArrayBox[i, j].OwnBy);
                        }
                    }
                }
            }

            return boxResult;
        }
        #region Attack
        private long attackPoint_Vertical(int currRow, int currColumn)
        {
            long totalPoint = 0;
            long PointTemp = 0;
            //  long attackPoint = 0;
            int boxPlayer = 0;
            int boxCom = 0;
            // bây giờ sẽ lấy box đang xét làm tâm để duyệt từ từ dưới lên và trên xuống tính xem cột đó có bao nhiêu quân ta, bao nhiêu quân địch và trống?
            //duyệt từ trên xuống
            //tranh trường hợp ta đang xét ở hàng 16 17 18 ... , nếu count =5 thì 16+5>20 gây tràn 
            for (int count = 1; count < 6 && currRow + count < _Board.NumberofRow; count++)
            {
                if (_ArrayBox[currRow + count, currColumn].OwnBy == 1)
                {
                    boxCom++;
                }
                else if (_ArrayBox[currRow + count, currColumn].OwnBy == 2)
                {
                    boxPlayer++;
                    PointTemp -= ArrayAttackPoint[1];
                    break;
                }
                else if (_ArrayBox[currRow + count, currColumn].OwnBy == 0)
                {
                    break;
                }
            }
            //duyệt từ dưới lên
            // tránh trường hợp xét ô thứ 4 3 .., nếu count =5 =>4-5<0 gây tràn, sai
            for (int count = 1; count < 6 && currRow - count >= 0; count++)
            {
                if (_ArrayBox[currRow - count, currColumn].OwnBy == 1)
                {
                    boxCom++;
                }
                else if (_ArrayBox[currRow - count, currColumn].OwnBy == 2)
                {
                    boxPlayer++;
                    PointTemp -= ArrayAttackPoint[1];
                    break;
                }
                else if (_ArrayBox[currRow - count, currColumn].OwnBy == 0)
                {
                    break;
                }
            }

            if (boxPlayer == 2)
            {//vì khi nếu 1 hướng có địch thì ta tăng số địch và break lặp => nếu có 2 thì => chặn trên và dưới rồi=> hướng dọc này như phế
                return 0;
            }
            //attackPoint = ArrayAttackPoint[boxCom];
            totalPoint = ArrayAttackPoint[boxCom] - ArrayAttackPoint[boxPlayer];
            totalPoint = totalPoint - PointTemp;
            return totalPoint;
        }
        private long attackPoint_Horizontal(int currRow, int currColumn)
        {
            long totalPoint = 0;
            long PointTemp = 0;
            // long attackPoint = 0;
            int boxPlayer = 0;
            int boxCom = 0;
            // bây giờ sẽ lấy box đang xét làm tâm để duyệt sang trái và sang phải tính xem dòng đó có bao nhiêu quân ta, bao nhiêu quân địch và trống?
            //duyệt giữa về trái
            //tranh trường hợp ta đang xét ở hàng 16 17 18 ... , nếu count =5 thì 16+5>20 gây tràn 
            for (int count = 1; count < 6 && currColumn - count >= 0; count++)
            {
                if (_ArrayBox[currRow, currColumn - count].OwnBy == 1)
                {
                    boxCom++;
                }
                else if (_ArrayBox[currRow, currColumn - count].OwnBy == 2)
                {
                    boxPlayer++;
                    PointTemp -= ArrayAttackPoint[1];
                    break;
                }
                else if (_ArrayBox[currRow, currColumn - count].OwnBy == 0)
                {
                    break;
                }
            }
            //duyệt từ giữa đi phải
            // tránh trường hợp xét ô thứ 4 3 .., nếu count =5 =>4-5<0 gây tràn, sai
            for (int count = 1; count < 6 && currRow + count < _Board.NumberOfColumn; count++)
            {
                if (_ArrayBox[currRow + count, currColumn].OwnBy == 1)
                {
                    boxCom++;
                }
                else if (_ArrayBox[currRow + count, currColumn].OwnBy == 2)
                {
                    boxPlayer++;
                    PointTemp -= ArrayAttackPoint[1];
                    break;
                }
                else if (_ArrayBox[currRow + count, currColumn].OwnBy == 0)
                {
                    break;
                }
            }

            if (boxPlayer == 2)
            {//vì khi nếu 1 hướng có địch thì ta tăng số địch và break lặp => nếu có 2 thì => chặn trên và dưới rồi=> hướng dọc này như phế
                return 0;
            }
            totalPoint = ArrayAttackPoint[boxCom] - ArrayAttackPoint[boxPlayer];
            totalPoint = totalPoint - PointTemp;
            return totalPoint;
        }
        private long attackPoint_DiagonalFromLeftToRight(int currRow, int currColumn)
        {
            long totalPoint = 0;
            long PointTemp = 0;
            //  long attackPoint = 0;
            int boxPlayer = 0;
            int boxCom = 0;

            for (int count = 1; count < 6 && /*currRow + count < _Board.NumberofRow*/  currRow - count >= 0 && currColumn + count < _Board.NumberOfColumn; count++)
            {
                if (_ArrayBox[currRow - count, currColumn + count].OwnBy == 1)
                {
                    boxCom++;
                }
                else if (_ArrayBox[currRow - count, currColumn + count].OwnBy == 2)
                {
                    boxPlayer++;
                    PointTemp -= ArrayAttackPoint[1];
                    break;
                }
                else if (_ArrayBox[currRow - count, currColumn + count].OwnBy == 0)
                {
                    break;
                }
            }

            for (int count = 1; count < 6 && currRow + count < _Board.NumberofRow && currColumn - count >= 0; count++)
            {
                if (_ArrayBox[currRow + count, currColumn - count].OwnBy == 1)
                {
                    boxCom++;
                }
                else if (_ArrayBox[currRow + count, currColumn - count].OwnBy == 2)
                {
                    boxPlayer++;
                    PointTemp -= ArrayAttackPoint[1];
                    break;
                }
                else if (_ArrayBox[currRow + count, currColumn - count].OwnBy == 0)
                {
                    break;
                }
            }

            if (boxPlayer == 2)
            {//vì khi nếu 1 hướng có địch thì ta tăng số địch và break lặp => nếu có 2 thì => chặn trên và dưới rồi=> hướng dọc này như phế
                return 0;
            }
            totalPoint = ArrayAttackPoint[boxCom] - ArrayAttackPoint[boxPlayer];
            totalPoint = totalPoint - PointTemp;
            return totalPoint;
        }
        private long attackPoint_DiagonalFromRightToLeft(int currRow, int currColumn)
        {
            long totalPoint = 0;
            long PointTemp = 0;
            //  long attackPoint = 0;
            int boxPlayer = 0;
            int boxCom = 0;

            for (int count = 1; count < 6 && currRow + count < _Board.NumberofRow && currColumn + count < _Board.NumberOfColumn; count++)
            {
                if (_ArrayBox[currRow + count, currColumn + count].OwnBy == 1)
                {
                    boxCom++;
                }
                else if (_ArrayBox[currRow + count, currColumn + count].OwnBy == 2)
                {
                    boxPlayer++;
                    PointTemp -= ArrayAttackPoint[1];
                    break;
                }
                else if (_ArrayBox[currRow + count, currColumn + count].OwnBy == 0)
                {
                    break;
                }
            }

            for (int count = 1; count < 6 && currRow - count >= 0 && currColumn - count >= 0; count++)
            {
                if (_ArrayBox[currRow - count, currColumn - count].OwnBy == 1)
                {
                    boxCom++;
                }
                else if (_ArrayBox[currRow - count, currColumn - count].OwnBy == 2)
                {
                    boxPlayer++;
                    PointTemp -= ArrayAttackPoint[1];
                    break;
                }
                else if (_ArrayBox[currRow - count, currColumn - count].OwnBy == 0)
                {
                    break;
                }
            }

            if (boxPlayer == 2)
            {//vì khi nếu 1 hướng có địch thì ta tăng số địch và break lặp => nếu có 2 thì => chặn trên và dưới rồi=> hướng dọc này như phế
                return 0;
            }
            totalPoint = ArrayAttackPoint[boxCom] - ArrayAttackPoint[boxPlayer];
            totalPoint = totalPoint - PointTemp;
            return totalPoint;
        }
        #endregion

        #region Defend
        private long defendPoint_Vertical(int currRow, int currColumn)
        {
            long totalPoint = 0;
            long defendPoint = 0;
            int boxPlayer = 0;
            int boxCom = 0;
            // bây giờ sẽ lấy box đang xét làm tâm để duyệt từ từ dưới lên và trên xuống tính xem cột đó có bao nhiêu quân ta, bao nhiêu quân địch và trống?
            //duyệt từ trên xuống
            //tranh trường hợp ta đang xét ở hàng 16 17 18 ... , nếu count =5 thì 16+5>20 gây tràn 
            for (int count = 1; count < 6 && currRow + count < _Board.NumberofRow; count++)
            {
                if (_ArrayBox[currRow + count, currColumn].OwnBy == 1)
                {
                    boxCom++;
                    break;
                }
                else if (_ArrayBox[currRow + count, currColumn].OwnBy == 2)
                {
                    boxPlayer++;

                }
                else if (_ArrayBox[currRow + count, currColumn].OwnBy == 0)
                {
                    break;
                }
            }
            //duyệt từ dưới lên
            // tránh trường hợp xét ô thứ 4 3 .., nếu count =5 =>4-5<0 gây tràn, sai
            for (int count = 1; count < 6 && currRow - count >= 0; count++)
            {
                if (_ArrayBox[currRow - count, currColumn].OwnBy == 1)
                {
                    boxCom++;
                    break;
                }
                else if (_ArrayBox[currRow - count, currColumn].OwnBy == 2)
                {
                    boxPlayer++;

                }
                else if (_ArrayBox[currRow - count, currColumn].OwnBy == 0)
                {
                    break;
                }
            }

            if (boxCom == 2)
            {//vì khi nếu 1 hướng có địch thì ta tăng số địch và break lặp => nếu có 2 thì => chặn trên và dưới rồi=> hướng dọc này như phế
                return 0;
            }
            // totalPoint = totalPoint - ArrayDefendPoint[boxPlayer + 1];
            defendPoint = ArrayDefendPoint[boxPlayer];
            totalPoint = totalPoint + defendPoint;
            if (boxPlayer > 0)
            {
                totalPoint = totalPoint - ArrayAttackPoint[boxCom] * 2;
            }
            return totalPoint;
        }
        private long defendPoint_Horizontal(int currRow, int currColumn)
        {
            long totalPoint = 0;
            long defendPoint = 0;
            int boxPlayer = 0;
            int boxCom = 0;
            // bây giờ sẽ lấy box đang xét làm tâm để duyệt sang trái và sang phải tính xem dòng đó có bao nhiêu quân ta, bao nhiêu quân địch và trống?
            //duyệt giữa về trái
            //tranh trường hợp ta đang xét ở hàng 16 17 18 ... , nếu count =5 thì 16+5>20 gây tràn 
            for (int count = 1; count < 6 && currColumn - count >= 0; count++)
            {
                if (_ArrayBox[currRow, currColumn - count].OwnBy == 1)
                {
                    boxCom++;
                    break;
                }
                else if (_ArrayBox[currRow, currColumn - count].OwnBy == 2)
                {
                    boxPlayer++;

                }
                else if (_ArrayBox[currRow, currColumn - count].OwnBy == 0)
                {
                    break;
                }
            }
            //duyệt từ giữa đi phải
            // tránh trường hợp xét ô thứ 4 3 .., nếu count =5 =>4-5<0 gây tràn, sai
            for (int count = 1; count < 6 && currColumn + count < _Board.NumberOfColumn; count++)
            {
                if (_ArrayBox[currRow, currColumn + count].OwnBy == 1)
                {
                    boxCom++;
                    break;
                }
                else if (_ArrayBox[currRow, currColumn + count].OwnBy == 2)
                {
                    boxPlayer++;

                }
                else if (_ArrayBox[currRow, currColumn + count].OwnBy == 0)
                {
                    break;
                }
            }

            if (boxCom == 2)
            {//vì khi nếu 1 hướng có địch thì ta tăng số địch và break lặp => nếu có 2 thì => chặn trên và dưới rồi=> hướng dọc này như phế
                return 0;
            }
            //   totalPoint = totalPoint - ArrayDefendPoint[boxPlayer + 1];
            defendPoint = ArrayDefendPoint[boxPlayer];
            totalPoint = totalPoint + defendPoint;
            if (boxPlayer > 0)
            {
                totalPoint = totalPoint - ArrayAttackPoint[boxCom] * 2;
            }
            return totalPoint;
        }
        private long defendPoint_DiagonalFromLeftToRight(int currRow, int currColumn)
        {
            long totalPoint = 0;
            long defendPoint = 0;
            int boxPlayer = 0;
            int boxCom = 0;

            for (int count = 1; count < 6 && currRow - count >= 0 && currColumn + count < _Board.NumberOfColumn; count++)
            {
                if (_ArrayBox[currRow - count, currColumn + count].OwnBy == 1)
                {
                    boxCom++;
                    break;
                }
                else if (_ArrayBox[currRow - count, currColumn + count].OwnBy == 2)
                {
                    boxPlayer++;

                }
                else if (_ArrayBox[currRow - count, currColumn + count].OwnBy == 0)
                {
                    break;
                }
            }

            for (int count = 1; count < 6 && currRow + count < _Board.NumberofRow && currColumn - count >= 0; count++)
            {
                if (_ArrayBox[currRow + count, currColumn - count].OwnBy == 1)
                {
                    boxCom++;
                    break;
                }
                else if (_ArrayBox[currRow + count, currColumn - count].OwnBy == 2)
                {
                    boxPlayer++;

                }
                else if (_ArrayBox[currRow + count, currColumn - count].OwnBy == 0)
                {
                    break;
                }
            }

            if (boxCom == 2)
            {//vì khi nếu 1 hướng có địch thì ta tăng số địch và break lặp => nếu có 2 thì => chặn trên và dưới rồi=> hướng dọc này như phế
                return 0;
            }
            // totalPoint = totalPoint - ArrayDefendPoint[boxPlayer + 1];
            defendPoint = ArrayDefendPoint[boxPlayer];
            totalPoint = totalPoint + defendPoint;
            if (boxPlayer > 0)
            {
                totalPoint = totalPoint - ArrayAttackPoint[boxCom] * 2;
            }
            return totalPoint;
        }
        private long defendPoint_DiagonalFromRightToLeft(int currRow, int currColumn)
        {
            long totalPoint = 0;
            long defendPoint = 0;
            int boxPlayer = 0;
            int boxCom = 0;

            for (int count = 1; count < 6 && currRow + count > _Board.NumberofRow && currColumn + count < _Board.NumberOfColumn; count++)
            {
                if (_ArrayBox[currRow + count, currColumn + count].OwnBy == 1)
                {
                    boxCom++;
                    break;
                }
                else if (_ArrayBox[currRow + count, currColumn + count].OwnBy == 2)
                {
                    boxPlayer++;

                }
                else if (_ArrayBox[currRow + count, currColumn + count].OwnBy == 0)
                {
                    break;
                }
            }

            for (int count = 1; count < 6 && currRow - count >= 0 && currColumn - count >= 0; count++)
            {
                if (_ArrayBox[currRow - count, currColumn - count].OwnBy == 1)
                {
                    boxCom++;
                    break;
                }
                else if (_ArrayBox[currRow - count, currColumn - count].OwnBy == 2)
                {
                    boxPlayer++;

                }
                else if (_ArrayBox[currRow - count, currColumn - count].OwnBy == 0)
                {
                    break;
                }
            }

            if (boxCom == 2)
            {//vì khi nếu 1 hướng có địch thì ta tăng số địch và break lặp => nếu có 2 thì => chặn trên và dưới rồi=> hướng dọc này như phế
                return 0;
            }
            // totalPoint = totalPoint - ArrayDefendPoint[boxPlayer + 1];
            defendPoint = ArrayDefendPoint[boxPlayer];
            totalPoint = totalPoint + defendPoint;
            if (boxPlayer > 0)
            {
                totalPoint = totalPoint - ArrayAttackPoint[boxCom] * 2;
            }
            return totalPoint;
        }
        #endregion
        #endregion

        #region Undo_Reddo
        public void Undo(Graphics g)
        {//p vs p
            if (_PlayMode == 1)
            {//nếu stack hết thì ta ko cho pop nữa
                if (Stack_Step.Count != 0)
                {
                    Box box = Stack_Step.Pop();
                    Stack_Redo.Push(new Box(box.Row, box.Column, box.Location, box.OwnBy));
                    _ArrayBox[box.Row, box.Column].OwnBy = 0;
                    _Board.DeleteBox(g, box.Location, SkyBlue);
                    if (_Turn == 1)
                    {
                        _Turn = 2;
                    }
                    else/* (_Turn == 2)*/
                    {
                        _Turn = 1;
                    }
                }
            }
            // p vs com
            if (PlayMode == 2)
            {
                if (Stack_Step.Count > 1)
                {
                    Box box = Stack_Step.Pop();                   // bỏ phần tử cuối cùng đi (nước vừa mới đánh)
                    Stack_Redo.Push(new Box(box.Row, box.Column, box.Location, box.OwnBy));     // thêm vào các nước Undo
                    _ArrayBox[box.Row, box.Column].OwnBy = 0;       // ô cờ đó được gán ở hữu bằng 0
                    _Board.DeleteBox(g, box.Location, SkyBlue);
                    if (Stack_Step.Count > 0)
                    {
                        box = Stack_Step.Pop();                   // bỏ phần tử cuối cùng đi (nước vừa mới đánh)
                        Stack_Redo.Push(new Box(box.Row, box.Column, box.Location, box.OwnBy));     // thêm vào các nước Undo
                        _ArrayBox[box.Row, box.Column].OwnBy = 0;       // ô cờ đó được gán ở hữu bằng 0
                        _Board.DeleteBox(g, box.Location, SkyBlue);
                    }
                }
            }
            // DrawBoard(g);
            //  RepainBox(g);
        }
        public void Redo(Graphics g)
        {//nếu stack hết thì ta ko cho pop nữa
            if (_PlayMode == 1)
            {
                if (Stack_Redo.Count > 0)
                {
                    Box box = Stack_Redo.Pop();
                    Stack_Step.Push(new Box(box.Row, box.Column, box.Location, box.OwnBy));
                    _ArrayBox[box.Row, box.Column].OwnBy = box.OwnBy;
                    _Board.DrawBox(g, box.Location, box.OwnBy == 1 ? sbBlack : sbWhite);
                    if (_Turn == 1)
                    {
                        _Turn = 2;
                    }
                    else
                    {
                        _Turn = 1;
                    }
                }
            }
            if (_PlayMode == 2)
            {
                Box box = Stack_Redo.Pop();
                Stack_Step.Push(new Box(box.Row, box.Column, box.Location, box.OwnBy));
                _ArrayBox[box.Row, box.Column].OwnBy = box.OwnBy;
                _Board.DrawBox(g, box.Location, box.OwnBy == 1 ? sbBlack : sbWhite);
                if (_Turn == 1)
                {
                    _Turn = 2;
                }
                else
                {
                    _Turn = 1;
                }
            }

        } //phải fix
        #endregion Undo_Redo
        #region ProcessWin
        public void FinishGame()
        {
            switch (_Finish)
            {
                case FINISH.noOneWin:
                    MessageBox.Show("No One Win");
                    break;
                case FINISH.Player1:
                    MessageBox.Show("Player 1 win");
                    break;
                case FINISH.Player2:
                    MessageBox.Show("Player 2 win");
                    break;
                case FINISH.Computer:
                    MessageBox.Show("Com win");
                    break;
            }
            _Ready = false;
        }

        public bool CheckWin()
        {
            if (Stack_Step.Count == _Board.NumberofRow * _Board.NumberOfColumn)
            {
                _Finish = FINISH.noOneWin;
                FinishGame();
                return true;
            }
            //XÉt các phương
            foreach (Box box in Stack_Step)
            {
                if (_PlayMode == 1)
                {
                    if (CheckVertical(box.Row, box.Column, box.OwnBy) || CheckHorizontal(box.Row, box.Column, box.OwnBy) || CheckDiagonalFromRightToLeft(box.Row, box.Column, box.OwnBy) || CheckDiagonalFromLeftToRight(box.Row, box.Column, box.OwnBy))
                    {
                        _Finish = box.OwnBy == 1 ? FINISH.Player1 : FINISH.Player2;
                        return true;
                    }
                }
                if (_PlayMode == 2)
                {
                    if (CheckVertical(box.Row, box.Column, box.OwnBy) || CheckHorizontal(box.Row, box.Column, box.OwnBy) || CheckDiagonalFromRightToLeft(box.Row, box.Column, box.OwnBy) || CheckDiagonalFromLeftToRight(box.Row, box.Column, box.OwnBy))
                    {
                        _Finish = box.OwnBy == 1 ? FINISH.Computer : FINISH.Player1;
                        return true;
                    }
                }
            }
            return false;
        }
        // duyệt theo chiều dọc
        private bool CheckVertical(int currRow, int currColumn, int currOwnBy)
        {
            if (currRow > _Board.NumberofRow - 5)
            {
                return false;
            }
            int CountBox;
            //chạy vòng lặp 4lần, 
            //nếu cái for bên trên return false rồi có nghĩa là giữa 5 con có 1 con ko phải của 1 người, 
            //và nếu nó chạy hết vòng lặp có nghĩa là nó có 5 con liên tiếp
            // nhưng vẫn còn trường hợp là nó chặn 2 đầu, mấy cái if dưới sẽ làm nhiệm vụ xét
            for (CountBox = 1; CountBox < 5; CountBox++)
            {// nếu cái hàng này cứ có 4 quân liên tiếp mà ko thuộc sở hữu của 1 người thì vẫn chưa được
                if (_ArrayBox[currRow + CountBox, currColumn].OwnBy != currOwnBy)
                {
                    return false;
                }

            }
            // currRow==0: ô hiện tại xét nằm ở hàng trên cùng, nên người khác ko thể chăn
            //currRow + CountBox == _Board.NumberofRow: ô ta đang xét đang nằm ở vị trí 15 (15+5=20) nên người khác ko thể chặn dưới 
            if (currRow == 0 || currRow + CountBox == _Board.NumberofRow)
            {
                return true;
            }
            //nếu đã có 5 con liên tiếp nhau tính từ con đang xét và con phía trên con đang ko có ai (ko phải đối thủ sở hữu)=> thắng
            // cái hoặc bởi vì: nếu đã ok vòng lặp thì CountBox=5 và khi cộng lên thì cách con đang xét 5 ô có nghĩa là ô thứ 6, ô này mà trống => auto ok
            if (_ArrayBox[currRow - 1, currColumn].OwnBy == 0 || _ArrayBox[currRow + CountBox, currColumn].OwnBy == 0)
            {
                return true;
            }
            return false;
        }
        private bool CheckHorizontal(int currRow, int currColumn, int currOwnBy)
        {
            if (currColumn > _Board.NumberOfColumn - 5)
            {
                return false;
            }
            int CountBox;
            //chạy vòng lặp 4lần, 
            //nếu cái for bên trên return false rồi có nghĩa là giữa 5 con có 1 con ko phải của 1 người, 
            //và nếu nó chạy hết vòng lặp có nghĩa là nó có 5 con liên tiếp
            // nhưng vẫn còn trường hợp là nó chặn 2 đầu, mấy cái if dưới sẽ làm nhiệm vụ xét
            for (CountBox = 1; CountBox < 5; CountBox++)
            {// nếu cái hàng này cứ có 4 quân liên tiếp mà ko thuộc sở hữu của 1 người thì vẫn chưa được
                if (_ArrayBox[currRow, currColumn + CountBox].OwnBy != currOwnBy)
                {
                    return false;
                }

            }
            // currRow==0: ô hiện tại xét nằm ở hàng trên cùng, nên người khác ko thể chăn
            //currRow + CountBox == _Board.NumberofRow: ô ta đang xét đang nằm ở vị trí 15 (15+5=20) nên người khác ko thể chặn dưới 
            if (currColumn == 0 || currColumn + CountBox == _Board.NumberOfColumn)
            {
                return true;
            }
            //nếu đã có 5 con liên tiếp nhau tính từ con đang xét và con phía trên con đang ko có ai (ko phải đối thủ sở hữu)=> thắng
            // cái hoặc bởi vì: nếu đã ok vòng lặp thì CountBox=5 và khi cộng lên thì cách con đang xét 5 ô có nghĩa là ô thứ 6, ô này mà trống => auto ok
            if (_ArrayBox[currRow, currColumn - 1].OwnBy == 0 || _ArrayBox[currRow, currColumn + CountBox].OwnBy == 0)
            {
                return true;
            }
            return false;
        }

        private bool CheckDiagonalFromLeftToRight(int currRow, int currColumn, int currOwnBy)
        {
            if (currColumn > _Board.NumberOfColumn - 5 || currRow > _Board.NumberofRow - 5)
            {
                return false;
            }
            int CountBox;
            //chạy vòng lặp 4lần, 
            //nếu cái for bên trên return false rồi có nghĩa là giữa 5 con có 1 con ko phải của 1 người, 
            //và nếu nó chạy hết vòng lặp có nghĩa là nó có 5 con liên tiếp
            // nhưng vẫn còn trường hợp là nó chặn 2 đầu, mấy cái if dưới sẽ làm nhiệm vụ xét
            for (CountBox = 1; CountBox < 5; CountBox++)
            {// nếu cái hàng này cứ có 4 quân liên tiếp mà ko thuộc sở hữu của 1 người thì vẫn chưa được
                if (_ArrayBox[currRow + CountBox, currColumn + CountBox].OwnBy != currOwnBy)
                {
                    return false;
                }

            }
            // currRow==0: ô hiện tại xét nằm ở hàng trên cùng, nên người khác ko thể chăn
            //currRow + CountBox == _Board.NumberofRow: ô ta đang xét đang nằm ở vị trí 15 (15+5=20) nên người khác ko thể chặn dưới 
            if (currRow == 0 || currRow + CountBox == _Board.NumberofRow || currColumn == 0 || currColumn + CountBox == _Board.NumberOfColumn)
            {
                return true;
            }
            //nếu đã có 5 con liên tiếp nhau tính từ con đang xét và con phía trên con đang ko có ai (ko phải đối thủ sở hữu)=> thắng
            // cái hoặc bởi vì: nếu đã ok vòng lặp thì CountBox=5 và khi cộng lên thì cách con đang xét 5 ô có nghĩa là ô thứ 6, ô này mà trống => auto ok
            if (_ArrayBox[currRow - 1, currColumn - 1].OwnBy == 0 || _ArrayBox[currRow + CountBox, currColumn + CountBox].OwnBy == 0)
            {
                return true;
            }
            return false;
        }

        private bool CheckDiagonalFromRightToLeft(int currRow, int currColumn, int currOwnBy)
        {
            if (currRow < 4 || currColumn > _Board.NumberOfColumn - 5)
            {
                return false;
            }
            int CountBox;

            for (CountBox = 1; CountBox < 5; CountBox++)
            {
                if (_ArrayBox[currRow - CountBox, currColumn + CountBox].OwnBy != currOwnBy)
                {
                    return false;
                }

            }
            if (currRow == 4 || currRow == _Board.NumberofRow - 1 || currColumn == 0 || currColumn + CountBox == _Board.NumberOfColumn)
            {
                return true;
            }
            if (_ArrayBox[currRow + 1, currColumn - 1].OwnBy == 0 || _ArrayBox[currRow - CountBox, currColumn + CountBox].OwnBy == 0)
            {
                return true;
            }
            return false;
        }


        #endregion
    }
}
