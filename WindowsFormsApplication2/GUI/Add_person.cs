using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MusicCatalogue
{
    public partial class Add_person : Form
    {
        private Database con;
        public string Query;
        public Add_person()
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

        private void textBox4_DoubleClick(object sender, EventArgs e)
        {
            textBox4.Text = String.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con = new Database(Settings1.Default.sqlconnect);
                Query = String.Format("USE music; DECLARE @Response NVARCHAR(250);" +
                    "EXEC add_person @first_name='{0}',@last_name='{1}',@band_name='{2}',@gender='{3}',@pUserID={4}, @Response=@Response OUTPUT;" +
                    "SELECT @Response as N'@Response'", textBox1.Text.Trim(), textBox2.Text.Trim(), textBox3.Text.Trim(), textBox4.Text.Trim(),Globals.UserID);
                string Response = con.QueryWithResult(Query);
                MessageBox.Show(Response);
                textBox1.Text = String.Empty;
                textBox2.Text = String.Empty;
                textBox3.Text = String.Empty;
                textBox4.Text = String.Empty;
            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
