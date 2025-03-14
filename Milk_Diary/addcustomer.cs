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
    public partial class addcustomer : Form
    {
        static string s = "server=DESKTOP-9AAFACB\\SQLEXPRESS;database=milkDairy;integrated security=true";
        SqlConnection con = new SqlConnection(s);
        public addcustomer()
        {
            InitializeComponent();
        }

        private void addcustomer_Load(object sender, EventArgs e)
        {

        }
        void cleardata()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //add customer details
            errorProvider1.Clear();

            bool isValid = true;
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "This field is required.");
                isValid = false;
            }
            if (isValid)
            {
                MessageBox.Show("All fields are valid!");
            }
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into addcustomer values(@p1,@p2,@p3,@p4)";
            cmd.Parameters.AddWithValue("@p1", textBox1.Text);
            cmd.Parameters.AddWithValue("@p2", textBox2.Text);
            cmd.Parameters.AddWithValue("@p3", textBox3.Text);
            cmd.Parameters.AddWithValue("@p4", dateTimePicker1.Text);
            DialogResult res = MessageBox.Show("Do you want to Add New Customer", "Add Customer", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                cmd.ExecuteNonQuery();
            }
            con.Close();
            MessageBox.Show("Customer Added Successfully !!!!!!");
            cleardata();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Validate user input
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please enter a customer name.");
                return;
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM addcustomer WHERE name = @name", con))
                {
                    cmd.Parameters.AddWithValue("@name", textBox1.Text);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Customer Name not Found....");
                    }
                    else
                    {
                        textBox1.Text = dt.Rows[0]["name"].ToString();
                        textBox2.Text = dt.Rows[0]["mobile"].ToString();
                        textBox3.Text = dt.Rows[0]["address"].ToString();
                        dateTimePicker1.Text = dt.Rows[0]["date"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //update customer records
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update addcustomer set mobile=@p2,address=@p3,date=@p4 where name=@p1";
            cmd.Parameters.AddWithValue("@p1", textBox1.Text);
            cmd.Parameters.AddWithValue("@p2", textBox2.Text);
            cmd.Parameters.AddWithValue("@p3", textBox3.Text);
            cmd.Parameters.AddWithValue("@p4", dateTimePicker1.Text);
            DialogResult res = MessageBox.Show("Do you want to update customer Records", "Update Customer Records", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                cmd.ExecuteNonQuery();
            }
            con.Close();
            MessageBox.Show("Customer Record Updated Successfully !!!!!!");
            cleardata();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //delete customer record
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from addcustomer where name=@p1";
            cmd.Parameters.AddWithValue("@p1", textBox1.Text);
            DialogResult res = MessageBox.Show("Do you want to Delete customer Record", "Delete Customer Records", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                cmd.ExecuteNonQuery();
            }
            con.Close();
            MessageBox.Show("Customer Record Deleted Successfully !!!!!!");
            cleardata();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("?? Only Numbers are Allowed ??");
                textBox2.Focus();
            }
        }
        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 10)
            {

            }
            else
            {
                MessageBox.Show("?? Enter 10 Digit Mobile Number ??");
                textBox2.Focus();
            }
        }
    }
}
