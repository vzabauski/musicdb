using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MusicCatalogue
{
    public partial class Add_album : Form
    {
        private Database con;
        public string Query;
        public Add_album()
        {
            InitializeComponent();
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            textBox1.Text = String.Empty;
        }

        private void textBox2_DoubleClick(object sender, EventArgs e)
        {
            textBox2.Text = String.Empty;
        }

        private void textBox3_DoubleClick(object sender, EventArgs e)
        {
            textBox3.Text = String.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con = new Database(Settings1.Default.sqlconnect);
                Query = String.Format("USE music; DECLARE @Response NVARCHAR(250);" +
                    "EXEC add_album @album_title = '{0}', @album_year = '{1}', @album_genre = '{2}', @pUserID={3}, @Response=@Response OUTPUT;" +
                    "SELECT @Response as N'@Response'", textBox1.Text.Trim(),textBox2.Text.Trim(),textBox3.Text.Trim(), Globals.UserID);
                string Response = con.QueryWithResult(Query);
                MessageBox.Show(Response);
                textBox1.Text = String.Empty;
                textBox2.Text = String.Empty;
                textBox3.Text = String.Empty;
            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
