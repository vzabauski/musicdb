using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace MusicCatalogue
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
                string Query = String.Format("USE music; DECLARE @responseMessage NVARCHAR(250); EXEC login @pLoginName='{0}', @pPassword='{1}', @responseMessage=@responseMessage OUTPUT; SELECT @responseMessage as N'@responseMessage'", textBox1.Text, textBox2.Text);
                //con.SqlQuery("DECLARE @responseMessage NVARCHAR(250); EXEC [dbo].Login @pLoginName='admin', @pPassword='Password@123', @responseMessage=@responseMessage OUTPUT; SELECT @responseMessage as N'@responseMessage'");
                //con.Cmd.CommandType = CommandType.StoredProcedure;
                //con.Cmd.Parameters.AddWithValue("@pLoginName", textBox1.Text.Trim());
                //con.Cmd.Parameters.AddWithValue("@pPassword", textBox2.Text.Trim());
                //con.QueryWithResult(Query);
                var Response = con.QueryWithResult(Query);
                Regex rgx = new Regex("^[1 - 9].*$");
                if (rgx.IsMatch(Response))
                {
                    MessageBox.Show("Succesfully logged in");
                    this.Hide();
                    var form1 = new Form1();
                    form1.Closed += (s, args) => this.Close();
                    form1.Show();
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show(Response, "Try again?", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.No)
                    {
                        Close();
                    }
                }
                //con.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception",
               MessageBoxButtons.OK, MessageBoxIcon.Error,
               MessageBoxDefaultButton.Button1);
            }
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (textBox1.Text != "^[A-z]*\\d*$")
                e.Cancel = true;
                MessageBox.Show("Only letters and digits are allowed.", 
                "Invalid login format!", MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }
    }
}
