using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Milk_Diary
{
    public partial class FAT_Slab : Form
    {
        static string s = "server=DESKTOP-9AAFACB\\SQLEXPRESS;database=milkDairy;integrated security=true";
        SqlConnection con = new SqlConnection(s);
        public FAT_Slab()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        void cleardata()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }
        void data()
        {
            SqlDataAdapter adp = new SqlDataAdapter("select id as 'Sr.No.', from_fat as'From FAT', to_fat as 'To FAT', differrence as 'Difference' from fatslab", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Show();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //update fatslab
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update fatslab set from_fat=@p2,to_fat=@p3,differrence=@p4 where id=@p1";
            cmd.Parameters.AddWithValue("@p1", textBox1.Text);
            cmd.Parameters.AddWithValue("@p2", textBox2.Text);
            cmd.Parameters.AddWithValue("@p3", textBox3.Text);
            cmd.Parameters.AddWithValue("@p4", textBox4.Text);
            DialogResult res = MessageBox.Show("Do you want to Update FAT Slab", "Update FAT Slab", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                cmd.ExecuteNonQuery();
            }
            con.Close();
            MessageBox.Show("FAT Slab Updated Successfully !!!!!!");
            cleardata();
            data();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //add FAT Slab
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into fatslab values(@p1,@p2,@p3,@p4)";
            cmd.Parameters.AddWithValue("@p1", textBox1.Text);
            cmd.Parameters.AddWithValue("@p2", textBox2.Text);
            cmd.Parameters.AddWithValue("@p3", textBox3.Text);
            cmd.Parameters.AddWithValue("@p4", textBox4.Text);
            DialogResult res = MessageBox.Show("Do you want to Add New FAT Slab", "Add FAT Slab", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                cmd.ExecuteNonQuery();
            }
            con.Close();
            MessageBox.Show("FAT Slab Added Successfully !!!!!!");
            cleardata();
            data();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void FAT_Slab_Load(object sender, EventArgs e)
        {
            data();
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dataGridView1.CurrentCell.RowIndex;
            textBox1.Text = dataGridView1.Rows[row].Cells["id"].Value.ToString();
            textBox2.Text = dataGridView1.Rows[row].Cells["from_fat"].Value.ToString();
            textBox3.Text = dataGridView1.Rows[row].Cells["to_fat"].Value.ToString();
            textBox4.Text = dataGridView1.Rows[row].Cells["differrence"].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //delete fatslab
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from fatslab where id=@p1";
            cmd.Parameters.AddWithValue("@p1", textBox1.Text);
            DialogResult res = MessageBox.Show("Do you want to Delete FAT Slab", "Delete FAT Slab", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                cmd.ExecuteNonQuery();
            }
            con.Close();
            MessageBox.Show("FAT Slab Deleted Successfully !!!!!!");
            cleardata();
            data();
        }
    }
}
