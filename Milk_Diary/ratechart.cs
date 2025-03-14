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
    public partial class ratechart : Form
    {
        private const decimal BaseFat = 3.5m;
        private const decimal BaseSNF = 8.5m;
        public ratechart()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal baseRate = decimal.Parse(textBox1.Text);
            decimal fat = decimal.Parse(textBox2.Text);
            decimal snf = decimal.Parse(textBox3.Text);

            decimal fatDifference = GetFatDifference(fat);
            decimal snfDifference = GetSNFDifference(snf);

            decimal rate = baseRate + fatDifference + snfDifference;

            dataGridView1.Rows.Add(fat, snf, rate);
        }
        private decimal GetFatDifference(decimal fat)
        {
            // Fetch the difference from the fatslab table based on the fat value
            // Example query: SELECT difference FROM fatslab WHERE @fat BETWEEN from_fat AND to_fat
            // Assuming a method GetDifferenceFromTable which fetches the difference from the database
            return GetDifferenceFromTable("fatslab", fat);
        }

        private decimal GetSNFDifference(decimal snf)
        {
            // Fetch the difference from the snfslab table based on the snf value
            // Example query: SELECT difference FROM snfslab WHERE @snf BETWEEN from_snf AND to_snf
            // Assuming a method GetDifferenceFromTable which fetches the difference from the database
            return GetDifferenceFromTable("snfslab", snf);
        }

        private decimal GetDifferenceFromTable(string tableName, decimal value)
        {
            decimal difference = 0m;

            using (SqlConnection conn = new SqlConnection("server=DESKTOP-9AAFACB\\SQLEXPRESS;database=milkDairy;integrated security=true"))
            {
                conn.Open();
                string query = $"SELECT difference FROM {tableName} WHERE @value BETWEEN from_fat AND to_fat";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@value", value);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    difference = reader.GetDecimal(0);
                }
            }

            return difference;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection("server=DESKTOP-9AAFACB\\SQLEXPRESS;database=milkDairy;integrated security=true"))
            {
                conn.Open();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;

                    decimal fat = (decimal)row.Cells["fat"].Value;
                    decimal snf = (decimal)row.Cells["snf"].Value;
                    decimal rate = (decimal)row.Cells["rate"].Value;

                    string query = "INSERT INTO ratechart (fat, snf, rate) VALUES (@fat, @snf, @rate)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@fat", fat);
                    cmd.Parameters.AddWithValue("@snf", snf);
                    cmd.Parameters.AddWithValue("@rate", rate);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
