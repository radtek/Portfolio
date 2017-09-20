using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using Johnny.Library.Database;

namespace Johnny.CMS.DAL
{

    public class CommonProcess
    {

        // Static constants
        private const string SQL_DELETE_BYID = "DECLARE @ERR int; DELETE FROM {0} WHERE {1}={2};SELECT @ERR=@@ERROR; SELECT @ERR";
        private const string SQL_UPDATE_ISDISPLAY = "DECLARE @ERR int; UPDATE {0} SET IsDisplay={1} WHERE {2}={3};SELECT @ERR=@@ERROR; SELECT @ERR";

        public void DeleteById(string strTable, string strKey, int strId)
        {
            //MsgInfo msg = new MsgInfo();
            //msg.MsgId = "S00305";
            //msg.MsgText = MessageManager.GetMessage("S00305");

            SqlCommand cmd = new SqlCommand();

            string strSQL = string.Format(SQL_DELETE_BYID, strTable, strKey, strId);

            // Create the connection to the database
            using (SqlConnection conn = new SqlConnection(PubConstant.ConnectionString))
            {                
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL;

                // Read the output of the query, should return error count
                using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    // Read the returned @ERR
                    rdr.Read();

                    // If the error count is not zero throw an exception
                    if (rdr.GetInt32(0) != 0)
                    {
                       // msg.MsgId = "S00306";
                        //msg.MsgText = MessageManager.GetMessage("S00306");
                    }
                    //throw new ApplicationException("DATA INTEGRITY ERROR ON ORDER INSERT - ROLLBACK ISSUED");
                }
                //Clear the parameters
                cmd.Parameters.Clear();
            }

            //return msg;
        }

        public void SetIsDisplay(string lblText, string strTable, string strKey, int strId)
        {
            //MsgInfo msg = new MsgInfo();
            //msg.MsgId = "S00000";
            //msg.MsgText = MessageManager.GetMessage("S00000");

            SqlCommand cmd = new SqlCommand();

            string strSQL = string.Format(SQL_UPDATE_ISDISPLAY, strTable, lblText, strKey, strId);

            // Create the connection to the database
            using (SqlConnection conn = new SqlConnection(PubConstant.ConnectionString))
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL;

                // Read the output of the query, should return error count
                using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    // Read the returned @ERR
                    rdr.Read();

                    // If the error count is not zero throw an exception
                    if (rdr.GetInt32(0) != 0)
                    {
                        //msg.MsgId = "S00001";
                        //msg.MsgText = MessageManager.GetMessage("S00001");
                    }
                    //throw new ApplicationException("DATA INTEGRITY ERROR ON ORDER INSERT - ROLLBACK ISSUED");
                }
                //Clear the parameters
                cmd.Parameters.Clear();
            }

            //return msg;
        }

        public void ExchangeSequence(string tableName, string key, int id, int sequence, bool UpDown)
        {
            SqlParameter[] parms = new SqlParameter[] {
					new SqlParameter("@tbl", SqlDbType.NVarChar, 50),
					new SqlParameter("@primarykey", SqlDbType.NVarChar, 50),
					new SqlParameter("@id", SqlDbType.Int),
					new SqlParameter("@sequence", SqlDbType.Int),
					new SqlParameter("@direction", SqlDbType.NVarChar, 10)};
            parms[0].Value = tableName;
            parms[1].Value = key;
            parms[2].Value = id;
            parms[3].Value = sequence;
            if (UpDown)
                parms[4].Value = "Up";
            else
                parms[4].Value = "Down";

            DbHelperSQL.RunProcedure("C_SequenceMove", parms);
        }
    }
}
