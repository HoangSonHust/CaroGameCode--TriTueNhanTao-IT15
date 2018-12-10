namespace Caro_Game
{
    partial class Option
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        public void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.Name1_txt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.name2_txt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.PlayerName_txt = new System.Windows.Forms.TextBox();
            this.ComFirst_rbtn = new System.Windows.Forms.RadioButton();
            this.PlayerFirst_rbtn = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(80, 292);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Name1_txt
            // 
            this.Name1_txt.Location = new System.Drawing.Point(26, 55);
            this.Name1_txt.Name = "Name1_txt";
            this.Name1_txt.Size = new System.Drawing.Size(129, 20);
            this.Name1_txt.TabIndex = 1;
            this.Name1_txt.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name 1";
            // 
            // name2_txt
            // 
            this.name2_txt.Location = new System.Drawing.Point(26, 100);
            this.name2_txt.Name = "name2_txt";
            this.name2_txt.Size = new System.Drawing.Size(129, 20);
            this.name2_txt.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Name 2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Name ";
            // 
            // PlayerName_txt
            // 
            this.PlayerName_txt.Location = new System.Drawing.Point(26, 196);
            this.PlayerName_txt.Name = "PlayerName_txt";
            this.PlayerName_txt.Size = new System.Drawing.Size(129, 20);
            this.PlayerName_txt.TabIndex = 7;
            // 
            // ComFirst_rbtn
            // 
            this.ComFirst_rbtn.AutoSize = true;
            this.ComFirst_rbtn.Location = new System.Drawing.Point(197, 198);
            this.ComFirst_rbtn.Name = "ComFirst_rbtn";
            this.ComFirst_rbtn.Size = new System.Drawing.Size(68, 17);
            this.ComFirst_rbtn.TabIndex = 12;
            this.ComFirst_rbtn.Text = "Com First";
            this.ComFirst_rbtn.UseVisualStyleBackColor = true;
            // 
            // PlayerFirst_rbtn
            // 
            this.PlayerFirst_rbtn.AutoSize = true;
            this.PlayerFirst_rbtn.Location = new System.Drawing.Point(197, 236);
            this.PlayerFirst_rbtn.Name = "PlayerFirst_rbtn";
            this.PlayerFirst_rbtn.Size = new System.Drawing.Size(76, 17);
            this.PlayerFirst_rbtn.TabIndex = 13;
            this.PlayerFirst_rbtn.Text = "Player First";
            this.PlayerFirst_rbtn.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Name1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Name2";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 180);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Name";
            // 
            // Option
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 336);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.PlayerFirst_rbtn);
            this.Controls.Add(this.ComFirst_rbtn);
            this.Controls.Add(this.PlayerName_txt);
            this.Controls.Add(this.name2_txt);
            this.Controls.Add(this.Name1_txt);
            this.Controls.Add(this.button1);
            this.Name = "Option";
            this.Text = "Option";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

       public System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox Name1_txt;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox name2_txt;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox PlayerName_txt;
        public System.Windows.Forms.RadioButton ComFirst_rbtn;
        public System.Windows.Forms.RadioButton PlayerFirst_rbtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}