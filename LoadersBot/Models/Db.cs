using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadersBot.Models
{
    public static class Db
    {
        public static void ExecuteNonQuery(string sql)
        {
            SqlConnection conn = new SqlConnection(Config.GetConnectionString());
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public static object ExecuteScalar(string sql)
        {
            SqlConnection conn = new SqlConnection(Config.GetConnectionString());
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            var res = cmd.ExecuteScalar();
            conn.Close();
            return res;
        }
        public static DataTable GetDataTable(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(Config.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();

                        SqlDataReader r = cmd.ExecuteReader();

                        dt.Load(r);
                        //for (var i = 0; i < r.FieldCount; i++)
                        //{
                        //    dt.Columns.Add(r.GetName(i), r.GetFieldType(i));
                        //}

                        //int j = 0;
                        //while (r.Read())
                        //{
                        //    dt.Rows.Add();
                        //    for (var i = 0; i < r.FieldCount; i++)
                        //    {
                        //        try
                        //        {
                        //            dt.Rows[j].ItemArray[i] = r.GetValue(i);
                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            dt.Rows[j].ItemArray[i] = "";
                        //        }
                        //    }
                        //}
                        r.Close();
                        conn.Close();
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return dt;
        }
    }
}
