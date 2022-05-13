using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadersBot.Models
{
    public class TaskList
    {
        public static int Add()
        {
            return Convert.ToInt32(Db.ExecuteScalar($"INSERT INTO dbo.TmpTasks(DateCreate) VALUES(SYSDATETIME()) SELECT SCOPE_IDENTITY()"));
        }
        public static int UpdateClient(int taskId, string client)
        {
            return Convert.ToInt32(Db.ExecuteScalar($"UPDATE dbo.TmpTasks SET Client = N'{client}' WHERE Id = {taskId}"));
        }
        public static int UpdateDateDispatch(int taskId, string dateDispatch)
        {
            return Convert.ToInt32(Db.ExecuteScalar($"UPDATE dbo.TmpTasks SET DateDispatch = CONVERT(datetime, '{dateDispatch}', 104) WHERE Id = {taskId}"));
        }
        public static int UpdateAddress(int taskId, string address)
        {
            return Convert.ToInt32(Db.ExecuteScalar($"UPDATE dbo.TmpTasks SET Address = N'{address}' WHERE Id = {taskId}"));
        }
        public static int UpdateWhatToDo(int taskId, string whatToDo)
        {
            return Convert.ToInt32(Db.ExecuteScalar($"UPDATE dbo.TmpTasks SET WhatToDo = N'{whatToDo}' WHERE Id = {taskId}"));
        }
        public static int UpdateStartWork(int taskId, string startWork)
        {
            return Convert.ToInt32(Db.ExecuteScalar($"UPDATE dbo.TmpTasks SET StartWork = N'{startWork}' WHERE Id = {taskId}"));
        }
        public static int UpdateClientPay(int taskId, string clientPay)
        {
            return Convert.ToInt32(Db.ExecuteScalar($"UPDATE dbo.TmpTasks SET ClientPay = N'{clientPay}' WHERE Id = {taskId}"));
        }
        public static int UpdateInYourArms(int taskId, string inYourArms)
        {
            return Convert.ToInt32(Db.ExecuteScalar($"UPDATE dbo.TmpTasks SET InYourArms = N'{inYourArms}' WHERE Id = {taskId}"));
        }
        public static int UpdateYours(int taskId, string yours)
        {
            return Convert.ToInt32(Db.ExecuteScalar($"UPDATE dbo.TmpTasks SET Yours = N'{yours}' WHERE Id = {taskId}"));
        }
        public static int UpdateAdditional(int taskId, string additional)
        {
            return Convert.ToInt32(Db.ExecuteScalar($"UPDATE dbo.TmpTasks SET Additional = N'{additional}' WHERE Id = {taskId}"));
        }

        public static bool GetClient(int taskId)
        {
            return Convert.ToInt32(Db.ExecuteScalar($"SELECT CASE WHEN Client IS NULL THEN 0 WHEN Client = '' THEN 0 ELSE 1 END FROM dbo.TmpTasks WHERE Id = {taskId}")) == 1;
        }
        public static bool GetDateDispatch(int taskId)
        {
            return Convert.ToInt32(Db.ExecuteScalar($"SELECT CASE WHEN DateDispatch IS NULL THEN 0 ELSE 1 END FROM dbo.TmpTasks WHERE Id = {taskId}")) == 1;
        }
        public static bool GetAddress(int taskId)
        {
            return Convert.ToInt32(Db.ExecuteScalar($"SELECT CASE WHEN Address IS NULL THEN 0 WHEN Address = '' THEN 0 ELSE 1 END FROM dbo.TmpTasks WHERE Id = {taskId}")) == 1;
        }
        public static bool GetWhatToDo(int taskId)
        {
            return Convert.ToInt32(Db.ExecuteScalar($"SELECT CASE WHEN WhatToDo IS NULL THEN 0 WHEN WhatToDo = '' THEN 0 ELSE 1 END FROM dbo.TmpTasks WHERE Id = {taskId}")) == 1;
        }
        public static bool GetStartWork(int taskId)
        {
            return Convert.ToInt32(Db.ExecuteScalar($"SELECT CASE WHEN StartWork IS NULL THEN 0 WHEN StartWork = '' THEN 0 ELSE 1 END FROM dbo.TmpTasks WHERE Id = {taskId}")) == 1;
        }
        public static bool GetClientPay(int taskId)
        {
            return Convert.ToInt32(Db.ExecuteScalar($"SELECT CASE WHEN ClientPay IS NULL THEN 0 WHEN ClientPay = '' THEN 0 ELSE 1 END FROM dbo.TmpTasks WHERE Id = {taskId}")) == 1;
        }
        public static bool GetInYourArms(int taskId)
        {
            return Convert.ToInt32(Db.ExecuteScalar($"SELECT CASE WHEN InYourArms IS NULL THEN 0 WHEN InYourArms = '' THEN 0 ELSE 1 END FROM dbo.TmpTasks WHERE Id = {taskId}")) == 1;
        }
        public static bool GetYours(int taskId)
        {
            return Convert.ToInt32(Db.ExecuteScalar($"SELECT CASE WHEN Yours IS NULL THEN 0 WHEN Yours = '' THEN 0 ELSE 1 END FROM dbo.TmpTasks WHERE Id = {taskId}")) == 1;
        }
        public static bool GetAdditional(int taskId)
        {
            return Convert.ToInt32(Db.ExecuteScalar($"SELECT CASE WHEN Additional IS NULL THEN 0 WHEN Additional = '' THEN 0 ELSE 1 END FROM dbo.TmpTasks WHERE Id = {taskId}")) == 1;
        }
        public static void TmpTaskToTask(int taskId)
        {
            Db.ExecuteScalar($"INSERT INTO dbo.Tasks(DateCreate, Active, Address, WhatToDo, StartWork, ClientPay, InYourArms, Yours, Additional, Client, DateDispatch) SELECT DateCreate, 1, Address, WhatToDo, StartWork, ClientPay, InYourArms, Yours, Additional, Client, DateDispatch FROM dbo.TmpTasks WHERE Id = {taskId}");
        }
        public static void Del(int taskId)
        {
            Db.ExecuteNonQuery($"DELETE FROM dbo.TmpTasks WHERE Id = {taskId} ");
        }
    }
}
