using System;
using System.Windows.Forms;


namespace Caro_Game
{

    public partial class Option : Form
    {

        public int mode;
        
       public void setmode(int mode)
        {
            this.mode = mode;
        }
        // private frmCaroBoard form;
        private CaroChess carochess;

        public Option()
        {

            InitializeComponent();
            carochess = new CaroChess();
            

        }
        

        private void button1_Click(object sender, EventArgs e)
        {

            if (mode == 1) //player với player
            {
                string name1 = Name1_txt.Text;
                string name2 = name2_txt.Text;
                carochess.optionForm1(name1, name2);
                Close();
            }
            if (mode == 2)
            {
                string name = PlayerName_txt.Text;
                bool playerFist = PlayerFirst_rbtn.Checked;
                bool comFirst = ComFirst_rbtn.Checked;
                carochess.optionForm2(name, playerFist, comFirst);
                Close();
            }




        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
