using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace MusicCatalogue
{
    public partial class Form1 : Form
    {
        private Database con;
        public string Query;
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            
            if (Globals.AdminMode == false )
            {
                button5.Visible = false;
            }
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var inputForm = new EditDatabase();
            inputForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Update();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            toolStripStatusLabel1.Text = this.dataGridView1.CurrentCell.ColumnIndex.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            con = new Database(Settings1.Default.sqlconnect);
            var caseSwitch = comboBox1.Text;
            switch (caseSwitch)
            {
                case "Artists":
                    Query = String.Format("USE music; EXEC get_artists_view @pUserID='{0}'", Globals.UserID);
                    con.SqlQuery(Query);
                    break;
                case "Albums":
                    Query = String.Format("USE music; EXEC get_albums_view @pUserID='{0}'", Globals.UserID);
                    con.SqlQuery(Query);
                    break;
                case "Genres":
                    con.SqlQuery("select * from [music].[dbo].[genres_view]");
                    break;
                case "Songs":
                    Query = String.Format("USE music; EXEC get_songs_view @pUserID = '{0}'", Globals.UserID);
                    con.SqlQuery(Query);
                    break;
                default:
                    Query = String.Format("USE music; EXEC get_artists_view @pUserID='{0}'", Globals.UserID);
                    con.SqlQuery(Query);
                    break;
            }

            dataGridView1.DataSource = con.QueryEx();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
            SaveFileDialog1.InitialDirectory = "C:";
            SaveFileDialog1.Title = "Save to Excel file";
            SaveFileDialog1.FileName = comboBox1.Text;
            SaveFileDialog1.Filter = "Excel Files(2003)|*.xls|Excel Files (2007)|*.xlsx";
            if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                ExcelApp.Application.Workbooks.Add(Type.Missing);
                ExcelApp.Columns.ColumnWidth = 20;
                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    ExcelApp.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                }

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        ExcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();

                    }
                }
                ExcelApp.ActiveWorkbook.SaveCopyAs(SaveFileDialog1.FileName.ToString());
                ExcelApp.ActiveWorkbook.Saved = true;
                ExcelApp.Quit();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var inputForm = new Search();
            inputForm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var inputForm = new MusicCatalogue.AdminConsole();
            inputForm.ShowDialog();
        }
    }
}

