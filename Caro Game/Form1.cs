﻿using System;
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
            caroChess.InitArrayBox();
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
            lblRule.Text = "welcome my caro Game. Today, let's play \nfun and win";
            tmMoveWord.Enabled = true; // cho enble sau vì nếu chưa gán chữ thì đã chạy rồi

        }

       

        private void pnl_Board_Paint(object sender, PaintEventArgs e)
        {
            caroChess.DrawBoard(grs);
            caroChess.RepainBox(grs);
        }
        // click vào bàn cờ để lấy vị trí của chuột
        private void pnl_Board_MouseClick(object sender, MouseEventArgs e)
        {// gửi vào 3 tham số là vị trí x y của chuột và grs là Graphics
            if (!caroChess.Ready)
            {
                return;
            }
            caroChess.PlayChess(e.X, e.Y, grs);
            if (caroChess.CheckWin())
            {
                caroChess.FinishGame();
            }
        }

        private void playerVsPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayerVsPlayer(sender, e);
        }


        private void PlayerVsPlayer(object sender, EventArgs e)
        {
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
            PlayerVsPlayer(sender,e);
        }
    }
}
