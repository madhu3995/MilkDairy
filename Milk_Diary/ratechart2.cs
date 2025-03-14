using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Milk_Diary
{
    public partial class ratechart2 : Form
    {
        public ratechart2()
        {
            InitializeComponent();
            LoadDataGrid();
        }
        private void LoadDataGrid()
        {
            // Base rate for 3.5 fat and 8.5 SNF
            double baseRate = 27;

            // Fat and SNF ranges
            double[] fats = { 2.5, 2.6, 2.7, 2.8, 2.9, 3.0, 3.1, 3.2, 3.3, 3.4, 3.5, 3.6, 3.7, 3.8, 3.9, 4.0,4.1, 4.2, 4.3, 4.4, 4.5, };
            double[] snfs = { 7.0, 7.1, 7.2, 7.3, 7.4, 7.5, 7.6, 7.7, 7.8, 7.9, 8.0, 8.1, 8.2, 8.3, 8.4, 8.5, 8.6, 8.7, 8.8, 8.9, 9.0 };

            // Create a DataTable to hold the data
            DataTable dataTable = new DataTable();

            // Add columns to the DataTable
            dataTable.Columns.Add("Fat/SNF");
            foreach (var snf in snfs)
            {
                dataTable.Columns.Add(snf.ToString());
            }

            // Add rows to the DataTable
            foreach (var fat in fats)
            {
                var row = dataTable.NewRow();
                row["Fat/SNF"] = fat.ToString();

                foreach (var snf in snfs)
                {
                    row[snf.ToString()] = CalculateRate(baseRate, fat, snf).ToString("F2");
                }

                dataTable.Rows.Add(row);
            }

            // Bind the DataTable to the DataGridView
            dataGridView1.DataSource = dataTable;

            // Set column headers
            dataGridView1.Columns[0].HeaderText = "Fat/SNF";
            for (int i = 1; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].HeaderText = snfs[i - 1].ToString();
            }
        }

        private double CalculateRate(double baseRate, double fat, double snf)
        {
            double rate = baseRate;

            // Calculate the fat increment
            if (fat <= 3.5)
            {
                rate += (fat - 3.5) * 10 * 0.3;
            }
            else
            {
                rate += (3.5 - 3.5) * 10 * 0.3;  // Increment within 2.5 to 3.5
                rate += (fat - 3.5) * 10 * 0.2;  // Increment above 3.5
            }

            // Calculate the SNF increment
            if (snf <= 8.5)
            {
                rate += (snf - 8.5) * 10 * 0.3;
            }
            else
            {
                rate += (8.5 - 8.5) * 10 * 0.3;  // Increment within 7.0 to 8.5
                rate += (snf - 8.5) * 10 * 0.2;  // Increment above 8.5
            }

            return rate;
        }
    }

}
