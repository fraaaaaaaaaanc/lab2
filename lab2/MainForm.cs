using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransportTask
{
    public partial class FormTransportTask : Form
    {
        public FormTransportTask()
        {
            InitializeComponent();
        }
        public bool isLim = false;
        private void btn_plus_row_Click(object sender, EventArgs e)
        {
            data_in.RowCount += 1;
            if (data_in.RowCount == 1 && data_in.ColumnCount == 1)
                data_in.Columns[0].HeaderCell.Value = "B" + data_in.ColumnCount;
            data_in.Rows[data_in.RowCount - 1].HeaderCell.Value = "A" + data_in.RowCount;
        }

        private void btn_minus_row_Click(object sender, EventArgs e)
        {
            if (data_in.RowCount > 1)
            {
                data_in.Rows.RemoveAt(data_in.Rows[data_in.RowCount - 1].Index);

            }
            else
            {
                data_in.Rows.Clear();
                data_in.Columns.Clear();
                data_in.Refresh();
            }
        }

        private void btn_plus_col_Click(object sender, EventArgs e)
        {
            data_in.Columns.Add("col" + (data_in.ColumnCount + 1), "B" + (data_in.ColumnCount + 1));
        }

        private void btn_minus_col_Click(object sender, EventArgs e)
        {
            if (data_in.ColumnCount > 1)
                data_in.ColumnCount -= 1;
            else
            {
                data_in.Rows.Clear();
                data_in.Columns.Clear();
                data_in.Refresh();
            }

        }

        private void remove_lim()
        {
            isLim = false;
            btn_plus_col.Enabled = true;
            btn_minus_col.Enabled = true;
            btn_plus_row.Enabled = true;
            btn_minus_row.Enabled = true;
            btn_lim.Text = "Добавить ограничения";
        }

        private void btn_lim_Click(object sender, EventArgs e)
        {
            if (data_in.RowCount >= 1 && data_in.ColumnCount >= 1)
            {
                if (!isLim)
                {
                    isLim = true;
                    data_in.RowCount += 1;
                    data_in.Rows[data_in.RowCount - 1].HeaderCell.Value = "LB";
                    data_in.Columns.Add("col" + (data_in.ColumnCount + 1), "LA");
                    btn_plus_col.Enabled = false;
                    btn_minus_col.Enabled = false;
                    btn_plus_row.Enabled = false;
                    btn_minus_row.Enabled = false;
                    btn_lim.Text = "Убрать ограничения";
                }
                else
                {
                    remove_lim();
                    data_in.RowCount += -1;
                    data_in.ColumnCount -= 1;
                }
            }

        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            data_in.Rows.Clear();
            data_in.Columns.Clear();
            data_in.Refresh();
            data_out.Rows.Clear();
            data_out.Columns.Clear();
            data_out.Refresh();
            remove_lim();
        }

        

        private void btn_solve_Click(object sender, EventArgs e)
        {
            if (isLim)
            {
                //Количество A
                int n = data_in.RowCount - 1;
                int[] a = new int[n];

                //Количество B
                int m = data_in.ColumnCount - 1;
                int[] b = new int[m];
                decimal[,] c = new decimal[n, m];
                for (int i = 0; i < a.Length; i++)
                {
                    a[i] = Convert.ToInt32(data_in.Rows[i].Cells[data_in.ColumnCount - 1].Value);
                }
                //b[j]
                for (int j = 0; j < b.Length; j++)
                {
                    b[j] = Convert.ToInt32(data_in.Rows[data_in.RowCount - 1].Cells[j].Value);
                }
                //c[i][j]
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        c[i, j] = Convert.ToDecimal(data_in.Rows[i].Cells[j].Value.ToString().Replace('.', ','));
                    }
                }
                TransportProblemSolver solver = new TransportProblemSolver(a, b, c);
                solver.solve();                
                data_out.RowCount = data_in.RowCount - 1;
                data_out.ColumnCount = data_in.ColumnCount - 1;
                data_out.ColumnHeadersVisible = false;
                data_out.RowHeadersVisible = false;
                //выводим массив во вторую таблицу
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (solver.allocation[i, j] > 0)
                            data_out.Rows[i].Cells[j].Value = solver.allocation[i, j];
                        else
                            data_out.Rows[i].Cells[j].Value = 0;
                    }
                    Console.WriteLine();
                }
                textBox1.Text = solver.result.ToString();
            }            
        }

        private void btn_auto_Click(object sender, EventArgs e)
        {            
            btn_remove_Click(sender, e);
            for (int i = 0; i < 4; i++)
            {
                btn_plus_row_Click(sender, e);
            }
            for (int j = 0; j < 5-1; j++)
            {
                btn_plus_col_Click(sender, e);
            }
            btn_lim_Click(sender, e);

            string[] lines = File.ReadAllLines("../../auto.txt");

            for (int i = 0; i < data_in.RowCount - 1; i++)
            {
                for (int j = 0; j < data_in.ColumnCount - 1; j++)
                {
                    data_in.Rows[i].Cells[j].Value = lines[i].Split(' ')[j];
                }
            }
            string lim_a = lines[lines.Length - 2];
            string lim_b = lines[lines.Length - 1];

            for (int i = 0; i < data_in.RowCount - 1; i++)
            {
                data_in.Rows[i].Cells[data_in.ColumnCount - 1].Value = lim_a.Split(' ')[i];
            }
            for (int j = 0; j < data_in.ColumnCount - 1; j++)
            {
                data_in.Rows[data_in.RowCount - 1].Cells[j].Value = lim_b.Split(' ')[j];
            }

        }
    }
}


