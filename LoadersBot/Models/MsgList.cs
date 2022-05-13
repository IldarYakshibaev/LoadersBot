using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadersBot.Models
{
    public class MsgList
    {
        public static void Add(string userId, string msgId, int? taskId = null)
        {
            Db.ExecuteNonQuery($"INSERT INTO dbo.TmpMsg(UserId, MsgId, TaskId) VALUES('{userId}', '{msgId}', {(taskId == null ? "NULL" : taskId.ToString())})");
        }
        public static void Del(string userId, string msgId)
        {
            Db.ExecuteNonQuery($"DELETE FROM dbo.TmpMsg WHERE UserId = '{userId}' AND MsgId = '{msgId}'");
        }
        public static List<List<string>> GetList(string userId)
        {
            List<List<string>> res = new List<List<string>>();
            foreach(DataRow i in Db.GetDataTable($"SELECT * FROM dbo.TmpMsg WHERE UserId = '{userId}'").Rows)
            {
                //res.Add(i["UserId"].ToString());
                //res.Add(i["MsgId"].ToString());
                res.Add(new List<string>{ i["UserId"].ToString(), i["MsgId"].ToString(), i["TaskId"].ToString() });
            }
            return res;
        }
    }
}
