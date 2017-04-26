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
                con.SqlQuery("dbo.add_person");
                con.Cmd.CommandType = CommandType.StoredProcedure;
                con.Cmd.Parameters.AddWithValue("@first_name", textBox1.Text.Trim());
                con.Cmd.Parameters.AddWithValue("@last_name", textBox2.Text.Trim());
                con.Cmd.Parameters.AddWithValue("@gender", textBox3.Text.Trim());
                con.Cmd.Parameters.AddWithValue("@band_name", textBox4.Text.Trim());
                con.NonQueryEx();
                MessageBox.Show("Success!");
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
