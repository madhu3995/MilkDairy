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
    public partial class viewCustomers : Form
    {
        static string s = "server=DESKTOP-9AAFACB\\SQLEXPRESS;database=milkDairy;integrated security=true";
        SqlConnection con = new SqlConnection(s);
        public viewCustomers()
        {
            InitializeComponent();
        }

        private void viewCustomers_Load(object sender, EventArgs e)
        {
            LoadCustomerData();
            //label2.Text = DateTime.Now.ToString();
        }
        void LoadCustomerData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(s))
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter("SELECT name as 'Name', mobile as 'Mobile', address as 'Addresss', date as 'Date' FROM addcustomer", con))
                    {
                        DataSet ds = new DataSet();
                        adp.Fill(ds);
                        dataGridView1.DataSource = ds.Tables[0];
                        dataGridView1.Show();
                        // Set the DataGridView to fill the space of the form
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dataGridView1.Dock = DockStyle.Fill;
                        dataGridView1.Show();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("An error occurred while loading data: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message);
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
