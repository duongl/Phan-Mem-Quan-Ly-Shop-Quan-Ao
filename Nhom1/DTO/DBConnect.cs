using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DBConnect
    {
        SqlConnection con;

        public SqlConnection Con
        {
            get { return con; }
            set { con = value; }
        }
        DataSet ds = new DataSet();

        public DataSet Ds
        {
            get { return ds; }
            set { ds = value; }
        }
        string strConnect, strServerName, strDataBaseName, strUserName, strPassword;

        public string StrPassword
        {
            get { return strPassword; }
            set { strPassword = value; }
        }

        public string StrUserName
        {
            get { return strUserName; }
            set { strUserName = value; }
        }

        public string StrDataBaseName
        {
            get { return strDataBaseName; }
            set { strDataBaseName = value; }
        }

        public string StrServerName
        {
            get { return strServerName; }
            set { strServerName = value; }
        }

        public string StrConnect
        {
            get { return strConnect; }
            set { strConnect = value; }
        }

       
        public DBConnect()
        {
            //StrServerName = @"A108PC35";
            StrServerName = @"LAPTOP-EH1UPPUL\SQLEXPRESS";
            StrDataBaseName = "QL_ShopQuanAo";
            StrUserName = "sa";
            //StrPassword = "123";
            StrPassword = "sa2012";
            StrConnect = @"Data Source =" + StrServerName + "; Initial Catalog =" + StrDataBaseName + "; User ID =" + StrUserName + "; Password =" + StrPassword;
            con = new SqlConnection(StrConnect);
        }

        public DBConnect(string strConnect, string strServerName, string strDataBaseName, string strPassword)
        {
            this.StrConnect = strConnect;
            this.StrServerName = strServerName;
            this.StrDataBaseName = strDataBaseName;
            this.StrPassword = strPassword;
            StrConnect = @"Data Source =" + StrServerName + "; Initial Catalog =" + StrDataBaseName + "; User ID =" + StrUserName + "; Password =" + StrPassword;
            con = new SqlConnection(StrConnect);
        }
        public void openCon()
        {
            if (Con.State.ToString() == "Closed")
            {
                con.Open();
            }
        }
        public void closeCon()
        {
            if (Con.State.ToString() == "Open")
            {
                con.Close();
            }
        }
        public int executeNonQuery(string sql)
        {
            openCon();
            SqlCommand cmd = new SqlCommand(sql, con);
            int count = cmd.ExecuteNonQuery();
            closeCon();
            return count;
        }
        public int getCount_ExecuteScalar(string sql)
        {
            openCon();
            SqlCommand cmd = new SqlCommand(sql, con);
            int count = (int)cmd.ExecuteScalar();
            closeCon();
            return count;
        }
        public SqlDataReader getDataReader(string strSQL)
        {
            openCon();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = strSQL;
            SqlDataReader data = cmd.ExecuteReader();
            return data;
        }
        public SqlDataAdapter getDataAdapter(string strSQL, string tableName)
        {
            SqlDataAdapter ada = new SqlDataAdapter(strSQL, con);
            ada.Fill(Ds, tableName);
            return ada;
        }
        public DataTable getDataTable(string strSQL)
        {
            SqlDataAdapter ada = new SqlDataAdapter(strSQL, con);
            ada.Fill(Ds);
            return Ds.Tables[0];
        }
        public DataTable getDataTable(string strSQL, string tableName)
        {
            SqlDataAdapter ada = new SqlDataAdapter(strSQL, con);
            ada.Fill(Ds, tableName);
            return Ds.Tables[tableName];
        }

        public void Connect()
        {
            throw new NotImplementedException();
        }

        public bool CompareValue(string strDataTableName, string strDataColName, string strValue)
        {
            SqlCommand cmm;
            int count = 0;
            //Cnn.Open();
            string strReq = string.Format("select * from {0}", strDataTableName);
            cmm = new SqlCommand(strReq, Cnn);

            SqlDataReader reader = cmm.ExecuteReader();

            while (reader.Read())
            {
                if (reader[strDataColName].ToString() == strValue)
                    count++;
            }

            cmm.Dispose();
            reader.Dispose();

            if (count > 0)
                return true;
            else
                return false;
        }

        public SqlConnection Cnn { get; set; }
    }
}
