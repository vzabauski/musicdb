using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form2 : Form
    {
        private Database con;
        public Form2()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            con = new Database(Settings1.Default.sqlconnect);
            var caseSwitch = comboBox1.Text;
            string id_field;
            switch (caseSwitch)
            {
                case "Persons":
                    con.SqlQuery("EXEC dbo.get_persons");
                    break;
                case "Bands":
                    con.SqlQuery("EXEC dbo.get_bands");
                    break;
                case "Albums":
                    con.SqlQuery("EXEC dbo.get_albums");
                    break;
                case "Songs":
                    con.SqlQuery("EXEC dbo.get_songs");
                    break;
                default:
                    con.SqlQuery("EXEC dbo.get_bands");
                    break;
            }

            dataGridView1.DataSource = con.QueryEx();
            id_field = caseSwitch.TrimEnd(caseSwitch[caseSwitch.Length - 1]).ToLower() + "_id";
            dataGridView1.Columns[id_field].Visible = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var inputForm = new Add_person();
            inputForm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var inputForm = new Add_band();
            inputForm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var inputForm = new Add_album();
            inputForm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var inputForm = new Add_song();
            inputForm.ShowDialog();
        }

        private void textBox1_KeyUp(object sender, EventArgs e)
        {
            con = new Database(Settings1.Default.sqlconnect);
            string Query;
            var caseSwitch = comboBox1.Text;

            string name = "%" + textBox1.Text.Trim() + "%";
            if (!string.IsNullOrWhiteSpace(name))
            {

                switch (caseSwitch)
                {
                    case "Bands":
                        Query = String.Format("SELECT [band_id],[band_name] AS [Band name] FROM [music].[dbo].[bands] WHERE band_name LIKE '{0}'", name);
                        con.SqlQuery(Query);
                        break;
                    case "Persons":
                        Query = String.Format("SELECT [person_id],[last_name],[first_name],[gender] FROM [music].[dbo].[persons] WHERE last_name LIKE '{0}' OR first_name LIKE '{0}'", name);
                        con.SqlQuery(Query);
                        break;
                    case "Albums":
                        Query = String.Format("SELECT [album_id],[album_title],[album_genre],[album_year] FROM [music].[dbo].[albums] WHERE album_title LIKE '{0}'", name);
                        con.SqlQuery(Query);
                        break;
                    case "Songs":
                        Query = String.Format("SELECT [song_id],[song_title],[song_duration] FROM [music].[dbo].[songs] WHERE song_title LIKE '{0}'", name);
                        con.SqlQuery(Query);
                        break;
                }

                dataGridView1.DataSource = con.QueryEx();
            }
        }
    }
}
