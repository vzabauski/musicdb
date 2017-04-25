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
    public partial class Auth : Form
    {
        private Database con;
        public Auth()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            textBox1.Text = String.Empty;
        }

        private void textBox2_DoubleClick(object sender, EventArgs e)
        {
            textBox2.Text = String.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con = new Database(Settings1.Default.sqlconnect);
                string Query = String.Format("SELECT COUNT (*) FROM[music].[sys].[database_principals] WHERE name = '{0}'", textBox1.Text);
                //MessageBox.Show(con.QueryWithResult(Query));
                if (con.QueryWithResult(Query) == "1")
                {
                    MessageBox.Show(con.QueryWithResult(Query));
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Invalid login or/and password!", "Try again?", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.No)
                    {
                        Close();
                    }
                }
                //con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
               "Exception",
               MessageBoxButtons.OK,
               MessageBoxIcon.Error,
               MessageBoxDefaultButton.Button1);
            }
        }
    }
}
