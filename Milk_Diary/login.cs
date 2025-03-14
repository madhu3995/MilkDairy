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
    public partial class login : Form
    {
        static string s = "server=DESKTOP-9AAFACB\\SQLEXPRESS;database=milkDairy;integrated security=true";
        SqlConnection con = new SqlConnection(s);
        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateUser(textBox1.Text, textBox2.Text))
                {
                    MessageBox.Show(" !! Login Successfully !! ");
                    this.Hide();
                    main_MDI mainForm = new main_MDI();
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show(" !! Invalid Login Details !!");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox1.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        private bool ValidateUser(string username, string password)
        {
            using (SqlConnection con = new SqlConnection(s))
            {
                string query = "SELECT user_name, password FROM change_password WHERE user_name = @username AND password = @password";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    string dbUsername = dr["user_name"].ToString();
                    string dbPassword = dr["password"].ToString();

                    return username == dbUsername && password == dbPassword;
                }
                return false;
            }
        }
    }
}
