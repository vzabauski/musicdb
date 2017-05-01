using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MusicCatalogue
{
    public class Database
    {
        private SqlConnection _con;
        public SqlCommand Cmd;
        public SqlCommandBuilder Cmdbuild;
        public SqlDataAdapter Da;
        public DataTable Dt;

        public Database(string ConnString)
        {
            try
            {
                _con = new SqlConnection(ConnString);
                _con.Open();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message,
                "Exception",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
            }
        }

        public void SqlQuery(string queryText)
        {
            try
            {
                Cmd = new SqlCommand(queryText, _con);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message,
                "Exception",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
            }
        }

        public DataTable QueryEx()
        {
            try
            {
                Da = new SqlDataAdapter(Cmd);
                Dt = new DataTable();
                Da.Fill(Dt);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message,
                "Exception",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
            }

            return Dt;

        }

        public DataTable Update()
        {
            try
            {
                Cmdbuild = new SqlCommandBuilder(Da);
                Da.Update(Dt);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message,
                "Exception",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
            }
            return Dt;
        }

        public void NonQueryEx()
        {
            try
            {
                Cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message,
                "Exception",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
            }
        }

        public string QueryWithResult(string Query)
        {
            string Result;
            using (var con = new SqlConnection(Settings1.Default.sqlconnect))
            {

                using (var Cmd = new SqlCommand(Query, con))
                {
                    con.Open();
                    Result = (string)Cmd.ExecuteScalar().ToString();
                }

            }
            return Result;
        }

        public Array ReturnArray(string Query, string Pattern)
            {
            var listOfStrings = new List<string>();
            using (SqlConnection sqlConnection = new SqlConnection(Settings1.Default.sqlconnect))
                {
                SqlCommand sqlCmd = new SqlCommand(Query, sqlConnection);
                sqlConnection.Open();
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                

                while (sqlReader.Read())
                {
                    listOfStrings.Add(sqlReader[Pattern].ToString());
                }
                string[] arrayOfStrings = listOfStrings.ToArray();
                sqlReader.Close();
                return arrayOfStrings;
                }
            }

        public void Close()
        {
            _con.Close();
        }

        
    }

}
