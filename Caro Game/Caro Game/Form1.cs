using System;
using System.Drawing;
using System.Windows.Forms;

namespace Caro_Game
{
    public partial class frmCaroBoard : Form
    {
        private CaroChess caroChess;
        private Graphics grs;
        public frmCaroBoard()
        {
            InitializeComponent();
            caroChess = new CaroChess();
            grs = pnl_Board.CreateGraphics();
        }

        private void tmMoveWord_Tick(object sender, EventArgs e)
        {
            lblRule.Location = new Point(lblRule.Location.X, lblRule.Location.Y - 1);
            if (lblRule.Location.Y + lblRule.Height < 0)
            {
                lblRule.Location = new Point(lblRule.Location.X, pnlRule.Height);
            }
        }

        private void frmCaroBoard_Load(object sender, EventArgs e)
        {
            lblRule.Text = "welcome my caro Game. Today, let's play \nfun and win";
            tmMoveWord.Enabled = true; // cho enble sau vì nếu chưa gán chữ thì đã chạy rồi

        }

        private void lblRule_Click(object sender, EventArgs e)
        {

        }

        private void pnl_Board_Paint(object sender, PaintEventArgs e)
        {
            caroChess.DrawBoard(grs);
        }

        private void pnl_Board_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}
