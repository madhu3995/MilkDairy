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
    public partial class Collect_milk : Form
    {
        static string s = "server=DESKTOP-9AAFACB\\SQLEXPRESS;database=milkDairy;integrated security=true";
        SqlConnection con = new SqlConnection(s);
        public Collect_milk()
        {
            InitializeComponent();
            Bindname();
        }
        void Bindname()
        {
            SqlDataAdapter adp = new SqlDataAdapter("select name from addcustomer", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            comboBox1.DataSource = ds.Tables[0];
            comboBox1.Show();

        }
        void cleardata()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox7.Clear();
            textBox8.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select * from addcustomer where name=@name";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", comboBox1.Text);
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    textBox1.Text = dr[0].ToString();
                    textBox2.Text = dr[2].ToString();
                    textBox3.Text = DateTime.Now.Date.ToString();
                    textBox9.Text = dr[3].ToString();
                }
                dr.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            con.Close();
        }

        private void textBox7_Click(object sender, EventArgs e)
        {
            string query = "SELECT Rate FROM MilkRateChart WHERE Fat = @fat AND SNF = @snf";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@fat", comboBox3.Text);
                cmd.Parameters.AddWithValue("@snf", comboBox2.Text);
                try
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        textBox7.Text = dr["Rate"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Rate not found for the given Fat and SNF values.");
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error loading milk rate: " + ex.Message);
                }
                con.Close();
            }
        }

        private void textBox8_Click(object sender, EventArgs e)
        {
            if (float.TryParse(textBox4.Text, out float quantity) && float.TryParse(textBox7.Text, out float rate))
            {
                float total = quantity * rate;
                textBox8.Text = total.ToString();
            }
            else
            {
                MessageBox.Show("Invalid quantity or rate value.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string shift;
            if (radioButton1.Checked == true)
            {
                shift = radioButton1.Text;
            }
            else
            {
                shift = radioButton2.Text;
            }
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into milk_collection values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)";
            cmd.Parameters.AddWithValue("@p1", comboBox1.Text);
            cmd.Parameters.AddWithValue("@p2", textBox1.Text);
            cmd.Parameters.AddWithValue("@p3", textBox2.Text);
            cmd.Parameters.AddWithValue("@p4", textBox9.Text);
            cmd.Parameters.AddWithValue("@p5", textBox3.Text);
            cmd.Parameters.AddWithValue("@p6", shift);
            cmd.Parameters.AddWithValue("@p7", textBox4.Text);
            cmd.Parameters.AddWithValue("@p8", comboBox2.Text);
            cmd.Parameters.AddWithValue("@p9", comboBox3.Text);
            cmd.Parameters.AddWithValue("@p10", textBox7.Text);
            cmd.Parameters.AddWithValue("@p11", textBox8.Text);
            DialogResult res = MessageBox.Show("Do you want to Add Milk Collection", "Add Milk Collection", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                cmd.ExecuteNonQuery();
            }
            con.Close();
            MessageBox.Show("milk Collection Added Successfully !!!!!!");
            cleardata();
            string query = "select name as 'Name', date as 'Date', milk_weight as 'Milk        (in Litre)', fat as 'FAT',snf as 'SNF', rate as 'Rate       (per Li)', total as 'Total' from milk_collection where name=@p1 ";
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.SelectCommand.Parameters.AddWithValue("@p1", comboBox1.Text);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView1.DataSource = dt;
                dataGridView1.Show();
                // Calculate the sum of the last column (total column)
                //double totalSum = 0;
                //foreach (DataGridViewRow row in dataGridView1.Rows)
                //{
                //    if (row.Cells["total"].Value != null)
                //    {
                //        totalSum += Convert.ToDouble(row.Cells["total"].Value);
                //    }
                //}
                //textBox1.Text = totalSum.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("?? Only Numbers are Allowed ??");
                textBox4.Focus();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}