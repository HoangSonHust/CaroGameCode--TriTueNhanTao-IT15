using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caro_Game
{
    public partial class MessageInputName1 : Form
    {
        public MessageInputName1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String name = Name1_txt.Text;
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
