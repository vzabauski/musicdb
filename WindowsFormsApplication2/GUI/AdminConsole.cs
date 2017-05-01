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
        public AdminConsole()
        {
            InitializeComponent();
            comboBox1.Items.Add("Change password");
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
            string Query = "SELECT login FROM music.dbo.users WHERE login != 'admin'";
            con = new Database(Settings1.Default.sqlconnect);
            Array List = con.ReturnArray(Query, "login");
            foreach (string item in List )
            {
                listBox1.Items.Add(item);

            }

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
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
