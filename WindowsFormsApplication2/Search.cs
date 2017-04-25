using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{

    public partial class Search : Form
    {
        private Database con;
        public Search()
        {

            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var caseSwitch = comboBox1.Text;
            label1.Text = "Enter " + comboBox1.Text.ToLower() + " name";
        }

        private void button1_Click(object sender, EventArgs e)
        {

            con = new Database();
            string Query;
            var caseSwitch = comboBox1.Text;

            label1.Text = "Enter " + comboBox1.Text.ToLower() + " name";
            string name = "%" + textBox1.Text.Trim() + "%";
            if (!string.IsNullOrWhiteSpace(name))
            {

                switch (caseSwitch)
                {
                    case "Band":
                        Query = String.Format("SELECT * FROM [music].[dbo].[search_artists]('{0}')", name);
                        con.SqlQuery(Query);
                        break;
                    case "Person":
                        Query = String.Format("SELECT * FROM [music].[dbo].[search_persons]('{0}')", name);
                        con.SqlQuery(Query);
                        break;
                    case "Album":
                        Query = String.Format("SELECT * FROM [music].[dbo].[search_albums]('{0}')", name);
                        con.SqlQuery(Query);
                        break;
                    case "Song":
                        Query = String.Format("SELECT * FROM [music].[dbo].[search_songs]('{0}')", name);
                        con.SqlQuery(Query);
                        break;
                    default:
                        con.SqlQuery("select * from [music].[dbo].[artists_view]");
                        break;
                }

                dataGridView1.DataSource = con.QueryEx();
            }
            else MessageBox.Show("Search field is empty!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
