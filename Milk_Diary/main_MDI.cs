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
    public partial class main_MDI : Form
    {
        public main_MDI()
        {
            InitializeComponent();
        }

        private void milkManToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fATSlabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addcustomer a = new addcustomer();
            a.Show();
        }

        private void sNFSlabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewCustomers a2 = new viewCustomers();
            a2.Show();
        }

        private void fATSlabToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FAT_Slab a3 = new FAT_Slab();
            a3.Show();
        }

        private void sNFSlabToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SNF_Slab a4 = new SNF_Slab();
            a4.Show();
        }

        private void rateCharToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ratechart2 a51 = new ratechart2();
            a51.Show();
        }

        private void buyMilkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Collect_milk a6 = new Collect_milk();
            a6.Show();
        }

        private void seeCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            see_milk_collection a7 = new see_milk_collection();
            a7.Show();
        }

        private void seeReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            See_Reports a8 = new See_Reports();
            a8.Show();
        }

        private void milkBillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Milk_Bill a9 = new Milk_Bill();
            a9.Show();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            change_pass a10 = new change_pass();
            a10.Show();
        }

        private void exitToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
