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
    public partial class see_milk_collection : Form
    {
        static string s = "server=DESKTOP-9AAFACB\\SQLEXPRESS;database=milkDairy;integrated security=true";
        SqlConnection con = new SqlConnection(s);
        public see_milk_collection()
        {
            InitializeComponent();
        }

        private void see_milk_collection_Load(object sender, EventArgs e)
        {
            data();
            label3.Text = DateTime.Now.ToString();
        }
        void data()
        {
            SqlDataAdapter adp = new SqlDataAdapter("select customer_id as 'Customer ID', name as 'Name',mobile as 'Mobile No.', date as 'Date', milk_weight as 'Milk     (in Litre)', fat as 'FAT',snf as 'SNF', rate as 'Rate     (per Li)', total as 'Total' from milk_collection order by date desc", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Show();
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
    }
}
