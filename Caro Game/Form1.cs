using System;
using System.Drawing;
using System.Windows.Forms;

namespace Caro_Game
{
    public partial class frmCaroBoard : Form
    {
        private CaroChess caroChess;
        private Graphics grs;
        private Board board;
        private int mode;
        private Option option;
        public frmCaroBoard()
        {

            InitializeComponent();
            caroChess = new CaroChess();
            board = new Board();
            caroChess.InitArrayBox();
            option = new Option();
            grs = pnl_Board.CreateGraphics();

        }
        // cho chữ di chuyển 
        private void tmMoveWord_Tick(object sender, EventArgs e)
        { // đặt cái label có tọa độ ntn và nếu khi chạy quá tọa độ này thì cho quay lại 
            lblRule.Location = new Point(lblRule.Location.X, lblRule.Location.Y - 1); // lbl cần 2 giá trị x và y để định vị trí
            if (lblRule.Location.Y + lblRule.Height < 0)
            {
                lblRule.Location = new Point(lblRule.Location.X, pnlRule.Height);
            }
        }

        private void frmCaroBoard_Load(object sender, EventArgs e)
        {
            /*if (mode == 1)
            {*/
                lblRule.Text = "welcome" + " my caro Game. Today, let's play \nfun and win";
                tmMoveWord.Enabled = true; // cho enble sau vì nếu chưa gán chữ thì đã chạy rồi
           /* }
            if (mode == 2)
            {


                lblRule.Text = "welcome" + "my caro Game. Today, let's play \nfun and win";
                tmMoveWord.Enabled = true; // cho enble sau vì nếu chưa gán chữ thì đã chạy rồi
            }*/

        }


        //hàm để vẽ bàn cờ
        private void pnl_Board_Paint(object sender, PaintEventArgs e)
        {// vẽ bàn cờ
            caroChess.DrawBoard(grs);
            //repain bàn cờ
            caroChess.RepainBox(grs);
        }
        // click vào bàn cờ để lấy vị trí của chuột
        private void pnl_Board_MouseClick(object sender, MouseEventArgs e)
        {// gửi vào 3 tham số là vị trí x y của chuột và grs là Graphics
            if (!caroChess.Ready)
            {
                return;
            }
            if (caroChess.PlayChess(e.X, e.Y, grs) == true)
            {
                ;
                if (caroChess.CheckWin())
                {
                    caroChess.FinishGame();
                }
                else
                {
                    if (caroChess.PlayMode == 2)
                    {
                        caroChess.InitComputer(grs);
                        if (caroChess.CheckWin())
                        {
                            caroChess.FinishGame();
                        }
                    }
                }
            }
        }

        private void playerVsPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayerVsPlayer(sender, e);
        }


        private void PlayerVsPlayer(object sender, EventArgs e)
        {
            this.mode = 1;
            
            option.setmode(1);
            //option.Show();
            option.Name1_txt.Enabled = true;
            option.name2_txt.Enabled = true;
            option.PlayerName_txt.Enabled = false;
            option.PlayerFirst_rbtn.Enabled = false;
            option.ComFirst_rbtn.Enabled = false;
            grs.Clear(pnl_Board.BackColor);
            caroChess.StartPlayerVsPlayer(grs);



        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            caroChess.Undo(grs);
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            caroChess.Redo(grs);
        }

        private void btnPlayerVsPlayer_Click(object sender, EventArgs e)
        {
            PlayerVsPlayer(sender, e);
        }
        private void PlayerVscom(object sender, EventArgs e)
        {
            this.mode = 2;

            option.setmode(2);
            //option.Show();
            option.Name1_txt.Enabled = false;
            option.name2_txt.Enabled = false;
            option.PlayerName_txt.Enabled = true;
            option.PlayerFirst_rbtn.Enabled = true;
            option.ComFirst_rbtn.Enabled = true;
            grs.Clear(pnl_Board.BackColor);
            caroChess.StartPlayerVsCom(grs);
        }
        private void BtnPlayerVsComputer_Click(object sender, EventArgs e)
        {
            PlayerVscom(sender, e);
        }

        private void playerVsComputerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayerVscom(sender, e);
        }

        private void BtnOption_Click(object sender, EventArgs e)
        {
            // option.Show();
        }
    }
}
