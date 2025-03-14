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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Milk_Diary
{
    public partial class Milk_Bill : Form
    {
        static string s = "server=DESKTOP-9AAFACB\\SQLEXPRESS;database=milkDairy;integrated security=true";
        SqlConnection con = new SqlConnection(s);
        public Milk_Bill()
        {
            InitializeComponent();
            Bindname();
        }

        private void Milk_Bill_Load(object sender, EventArgs e)
        {
            //Bindname();
        }
        void Bindname()
        {
            SqlDataAdapter adp = new SqlDataAdapter("select name from addcustomer", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "name";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();  

            string query = "select * from milk_collection where name=@name and date=@date";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@name", comboBox1.Text);
                cmd.Parameters.AddWithValue("@date", dateTimePicker1.Value.Date);
                try
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    bool recordFound = false;

                    while (dr.Read())
                    {
                        string shift = dr["shift"].ToString();
                        recordFound = true;

                        if (shift.Equals("Morning", StringComparison.OrdinalIgnoreCase))
                        {
                            textBox1.Text = dr["milk_weight"].ToString();
                            textBox2.Text = dr["fat"].ToString();
                            textBox3.Text = dr["snf"].ToString();
                            textBox4.Text = dr["rate"].ToString();
                            textBox5.Text = dr["total"].ToString();
                        }
                        else if (shift.Equals("Evening", StringComparison.OrdinalIgnoreCase))
                        {
                            textBox7.Text = dr["milk_weight"].ToString();
                            textBox8.Text = dr["fat"].ToString();
                            textBox6.Text = dr["snf"].ToString();
                            textBox10.Text = dr["rate"].ToString();
                            textBox9.Text = dr["total"].ToString();
                        }
                    }

                    if (!recordFound)
                    {
                        MessageBox.Show("No records found for the selected date and customer.");
                    }
                    float morningTotal = 0;
                    float eveningTotal = 0;

                    if (float.TryParse(textBox5.Text, out float morningQuantity))
                    {
                        morningTotal = morningQuantity;
                    }

                    if (float.TryParse(textBox9.Text, out float eveningQuantity))
                    {
                        eveningTotal = eveningQuantity;
                    }

                    float finalTotal = morningTotal + eveningTotal;
                    textBox11.Text = finalTotal.ToString();
                }

                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }

            }
        }

        private void ClearTextBoxes()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
        }
    }
        
    
}
