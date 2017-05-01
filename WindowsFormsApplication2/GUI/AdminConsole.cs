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
        
        public AdminConsole()
        {
            InitializeComponent();
            comboBox1.Items.Add("Change password"); // Index 0
            comboBox1.Items.Add("Delete user");
            label3.Visible = false;
            label4.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            

            /*using (SqlConnection sqlConnection = new SqlConnection(Settings1.Default.sqlconnect))
            {
                SqlCommand sqlCmd = new SqlCommand("SELECT login FROM music.dbo.users WHERE login != 'admin'", sqlConnection);
                sqlConnection.Open();
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    comboBox1.Items.Add(sqlReader["login"].ToString());
                }

                sqlReader.Close();
            }
            */
            Query = "SELECT login FROM music.dbo.users WHERE login != 'admin'";
            con = new Database(Settings1.Default.sqlconnect);
            Array List = con.ReturnArray(Query, "login");
            foreach (string item in List )
            {
                listBox1.Items.Add(item);

            }
            listBox1.SelectedIndex = 0;
        }

        private void usersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.usersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.users);

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
            if (comboBox1.SelectedIndex == 0)
            {
                label3.Visible = true;
                label4.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
            }
            else {
                label3.Visible = false;
                label4.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    if (listBox1.Text != String.Empty)
                    {
                        if (textBox1.Text == textBox2.Text)
                        {
                            Query = String.Format("USE music; DECLARE @responseMessage NVARCHAR(250);" +
                            "EXEC [dbo].change_password @pLogin='{0}', @pPassword='{1}', @responseMessage=@responseMessage OUTPUT;" +
                            "SELECT @responseMessage as N'@responseMessage'", listBox1.Text, textBox1.Text);
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
                    {
                        
                    }
                    break;
                
            }

        }
    }
}
