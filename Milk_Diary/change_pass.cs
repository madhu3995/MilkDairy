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
    public partial class change_pass : Form
    {
        static string s = "server=DESKTOP-9AAFACB\\SQLEXPRESS;database=milkDairy;integrated security=true";
        SqlConnection con = new SqlConnection(s);
        public change_pass()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT user_name, password FROM change_password WHERE user_name=@username", con);
            cmd.Parameters.AddWithValue("@username", textBox3.Text);

            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                string name = dt.Rows[0]["user_name"].ToString();
                string pass = dt.Rows[0]["password"].ToString();

                if (textBox1.Text == pass)
                {
                    DialogResult result = MessageBox.Show("Do you want to change Password?", "Confirm", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        SqlCommand cmd1 = new SqlCommand("UPDATE change_password SET password=@newPassword, confirm_password=@confirmPassword WHERE user_name=@username", con);
                        cmd1.Parameters.AddWithValue("@username", textBox3.Text);
                        cmd1.Parameters.AddWithValue("@newPassword", textBox2.Text);
                        cmd1.Parameters.AddWithValue("@confirmPassword", textBox4.Text);

                        cmd1.ExecuteNonQuery();
                        MessageBox.Show("Password Changed Successfully!");
                    }
                }
                else
                {
                    MessageBox.Show("Enter correct old password.");
                }
            }
            else
            {
                MessageBox.Show("User not found.");
            }

            con.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
