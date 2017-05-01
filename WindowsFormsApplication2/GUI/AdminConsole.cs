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
    public partial class AdminConsole : Form
    {
        private Database con;
        public string Query;
        
        public void RefreshUserList()
        {
            listBox1.Items.Clear();
            Query = "SELECT login FROM music.dbo.users WHERE login != 'admin'";
            con = new Database(Settings1.Default.sqlconnect);
            Array List = con.ReturnArray(Query, "login");
            foreach (string item in List)
            {
                listBox1.Items.Add(item);

            }
            listBox1.SelectedIndex = 0;
        }

        public void RunQuery(string Query)
        {
            try
            {
                string Response = con.QueryWithResult(Query);
                MessageBox.Show(Response, "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public AdminConsole()
        {
            InitializeComponent();
            comboBox1.Items.Add("Change password"); // Index 0
            comboBox1.Items.Add("Delete user");
            comboBox1.Items.Add("Create user");
            label3.Visible = false;
            label4.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;

            RefreshUserList();
            
        }

        private void AdminConsole_Load(object sender, EventArgs e)
        {
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    label3.Visible = true;
                    label3.Text = "Enter new password";
                    label4.Visible = true;
                    label4.Text = "Enter new password again";
                    textBox1.Visible = true;
                    textBox2.Visible = true;
                    break;
                case 1:
                    label3.Visible = false;
                    label4.Visible = false;
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    break;
                case 2:
                    label3.Visible = true;
                    label3.Text = "Enter new user name";
                    label4.Visible = true;
                    label4.Text = "Enter new password";
                    textBox1.Visible = true;
                    textBox1.UseSystemPasswordChar = false;
                    textBox2.Visible = true;
                    break;
            }
            
        }


        private void button4_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    if (listBox1.Text != String.Empty)
                    {
                        if (textBox1.Text == "" && textBox2.Text == "")
                        {
                            MessageBox.Show("Password fields shouldn't be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (textBox1.Text == textBox2.Text)
                        {
                            Query = String.Format("USE music; DECLARE @responseMessage NVARCHAR(250);" +
                            "EXEC [dbo].change_password @pLogin='{0}', @pPassword='{1}', @responseMessage=@responseMessage OUTPUT;" +
                            "SELECT @responseMessage as N'@responseMessage'", listBox1.Text, textBox1.Text);
                            RunQuery(Query);

                        }
                        else
                        {
                            MessageBox.Show("Passwords don't match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Select user first", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    textBox1.Text = String.Empty;
                    textBox2.Text = String.Empty;
                    break;

                case 1:
                    Query = String.Format("USE music; DECLARE @responseMessage NVARCHAR(250);" +
                        "EXEC [dbo].delete_user @pLogin='{0}', @responseMessage=@responseMessage OUTPUT;" +
                        "SELECT @responseMessage as N'@responseMessage'", listBox1.Text);
                    RunQuery(Query);
                    RefreshUserList();
                    break;

                case 2:

                    if (textBox1.Text != "" && textBox2.Text != "")
                    {
                        Query = String.Format("USE music; DECLARE @responseMessage NVARCHAR(250);" +
                            "EXEC [dbo].add_user @pLogin='{0}', @pPassword='{1}', @responseMessage=@responseMessage OUTPUT;" +
                            "SELECT @responseMessage as N'@responseMessage'", textBox1.Text, textBox2.Text);
                        RunQuery(Query);
                        RefreshUserList();
                    }
                        textBox1.Text = String.Empty;
                        textBox2.Text = String.Empty;
                    break;


            }

        }
    }
}
