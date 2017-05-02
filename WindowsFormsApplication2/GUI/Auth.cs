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

        public interface IForm
        {
            bool GetResult();
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
                string Query = String.Format("USE music; DECLARE @Response NVARCHAR(250);" +
                    "EXEC login @pLoginName='{0}', @pPassword='{1}', @Response=@Response OUTPUT;" +
                    " SELECT @Response as N'@Response'", textBox1.Text, textBox2.Text);
                var Response = con.QueryWithResult(Query);
                Regex rgx = new Regex("^[1-9].*$");
                if (rgx.IsMatch(Response))
                {
                    Globals.UserID = Response;
                    Query = String.Format("USE music; DECLARE @Response NVARCHAR(10); " +
                        "EXEC is_admin @pLogin='{0}', @Response=@Response OUTPUT; " +
                        "SELECT @Response as N'@Response'", textBox1.Text);
                    Response = con.QueryWithResult(Query);
                    if (Response == "1")
                    {
                        Globals.AdminMode = true;
                        MessageBox.Show("Logged with administrative privileges." + "UserID="+ Globals.UserID, "Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Succesfully logged in." + "UserID="+ Globals.UserID);
                    }
                    this.Hide();
                    var form1 = new Form1();
                    form1.Closed += (s, args) => this.Close();
                    form1.Show();
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show(Response, "Try again?", MessageBoxButtons.YesNo,MessageBoxIcon.Error);
                    if (dialogResult == DialogResult.No)
                    {
                        Close();
                    }
                    else
                    {
                        textBox1.Text = String.Empty;
                        textBox2.Text = String.Empty;
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
