using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Milk_Diary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            label2.Text = DateTime.Now.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value += 1;
            label3.Text = progressBar1.Value.ToString() + "%";
            if(progressBar1.Value==progressBar1.Maximum)
            {
                timer1.Enabled = false;
                MessageBox.Show(" !! Welcome To Milk Dairy !!");
                this.Hide();
                login l = new login();
                l.Show();
            }
        }
    }
}
