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

namespace WindowsFormsApplication2
{
    public partial class Add_song : Form
    {
        private Database con;
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
                con = new Database(Settings1.Default.sqlconnect);
                con.SqlQuery("add_song");
                con.Cmd.CommandType = CommandType.StoredProcedure;
                con.Cmd.Parameters.AddWithValue("@song_title", textBox1.Text.Trim());
                con.Cmd.Parameters.AddWithValue("@song_duration", textBox2.Text.Trim());
                con.Cmd.Parameters.AddWithValue("@album_title", textBox3.Text.Trim());
                con.NonQueryEx();
                MessageBox.Show("Success!");
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
