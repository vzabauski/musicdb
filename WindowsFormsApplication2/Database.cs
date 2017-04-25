using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public class Database
    {
        private SqlConnection _con;
        public SqlCommand Cmd;
        public SqlCommandBuilder Cmdbuild;
        public SqlDataAdapter Da;
        public DataTable Dt;

        public Database()
        {
            _con = new SqlConnection(Settings1.Default.sqlconnect);
            _con.Open();
        }

        public void SqlQuery(string queryText)
        {
            Cmd = new SqlCommand(queryText, _con);           
        }

        public DataTable QueryEx()
        {
            Da = new SqlDataAdapter(Cmd);
            Dt = new DataTable();
            Da.Fill(Dt);
            return Dt;
        }

        public DataTable Update()
        {
            Cmdbuild = new SqlCommandBuilder(Da);
            Da.Update(Dt);
            return Dt;
        }

        public void NonQueryEx()
        {
            Cmd.ExecuteNonQuery();
        }

        
    }

}
