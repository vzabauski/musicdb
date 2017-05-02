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
using System.Globalization;

namespace MusicCatalogue
{
    public partial class Add_song : Form
    {
        private Database con;
        public string Query;
        public DateTime duration;
        public string SongDuration;
        public Add_song()
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

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text == "Song duration" || textBox2.Text==String.Empty)
                {
                    SongDuration = String.Empty;
                }
                else
                {
                    DateTime duration = DateTime.ParseExact(textBox2.Text.Trim(), "HH:mm:ss",
                                        CultureInfo.InvariantCulture);
                    SongDuration = duration.TimeOfDay.ToString();
                }
                con = new Database(Settings1.Default.sqlconnect);
                Query = String.Format("USE music; DECLARE @Response NVARCHAR(250);" +
                    "EXEC dbo.add_song @song_title='{0}', @song_duration='{1}', @album_title='{2}', @pUserID={3},@Response=@Response OUTPUT;" +
                    "SELECT @Response as N'@Response'", textBox1.Text.Trim(), SongDuration, textBox3.Text.Trim(), Globals.UserID);
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
    }
}
