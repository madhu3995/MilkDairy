using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Milk_Diary
{
    public partial class See_Reports : Form
    {
        static string s = "server=DESKTOP-9AAFACB\\SQLEXPRESS;database=milkDairy;integrated security=true";
        SqlConnection con = new SqlConnection(s);
        public See_Reports()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "select customer_id as 'Customer ID', name as 'Name',mobile as 'Mobile No.', date as 'Date', milk_weight as 'Milk        (in Litre)', fat as 'FAT',snf as 'SNF', rate as 'Rate       (per Li)', total as 'Total' from milk_collection where date between @p1 and @p2";
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter(query, con);
                adp.SelectCommand.Parameters.AddWithValue("@p1", dateTimePicker1.Value.Date);
                adp.SelectCommand.Parameters.AddWithValue("@p2", dateTimePicker2.Value.Date);

                DataTable dt = new DataTable();
                adp.Fill(dt);

                dataGridView1.DataSource = dt;
                dataGridView1.Show();
                // Calculate the sum of the last column (total column)
                double totalSum = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["total"].Value != null)
                    {
                        totalSum += Convert.ToDouble(row.Cells["total"].Value);
                    }
                }
                textBox1.Text = totalSum.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
    
}
