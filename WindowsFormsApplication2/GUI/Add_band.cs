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
    public partial class Add_band : Form
    {
        private Database con;
        public string Query;
        public Add_band()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con = new Database(Settings1.Default.sqlconnect);
                Query = String.Format("USE music; DECLARE @Response NVARCHAR(250);" +
                    "EXEC add_band @band_name = '{0}', @country_name = '{1}', @pUserID = '{2}', @Response=@Response OUTPUT;" +
                    "SELECT @Response as N'@Response'", textBox1.Text.Trim(), textBox2.Text.Trim(), Globals.UserID);
                string Response = con.QueryWithResult(Query);
                MessageBox.Show(Response);
                textBox1.Text = String.Empty;
                textBox2.Text = String.Empty;
            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            textBox1.Text = String.Empty;
        }

        private void textBox2_DoubleClick(object sender, EventArgs e)
        {
            textBox2.Text = String.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
