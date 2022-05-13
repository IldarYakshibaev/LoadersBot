using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadersBot.Models
{
    public static class User
    {
        public static void Add(Telegram.Bot.Types.User u)
        {
            Db.ExecuteNonQuery($"INSERT INTO dbo.Users(UserId, " +
                $"DateCreate, " +
                $"IsAdmin, " +
                $"Active, " +
                $"Username, " +
                $"FirstName, " +
                $"LastName, " +
                $"LastMenu, " +
                $"LastUserId, " +
                $"PageUser, " +
                $"LastTaskId, " +
                $"PageTask, " +
                $"PreviewId) " +
                $"VALUES('{u.Id}', " +
                $"SYSDATETIME(), " +
                $"0, " +
                $"1, " +
                $"'{(String.IsNullOrEmpty(u.Username) ? "" : u.Username)}', " +
                $"N'{u.FirstName}', " +
                $"N'{u.LastName}', " +
                $"1, " +
                $"0, " +
                $"10, " +
                $"0, " +
                $"10, " +
                $"0)");
        }
        public static bool Active(string userId)
        {
            return Convert.ToInt32(Db.ExecuteScalar($"SELECT COUNT(*) FROM dbo.Users as u WHERE u.UserId = '{userId}' AND u.Active = 1")) == 1;
        }
        public static bool Exists(string userId)
        {
            return Convert.ToInt32(Db.ExecuteScalar($"SELECT COUNT(*) FROM dbo.Users as u WHERE u.UserId = '{userId}'")) == 1;
        }
        public static bool IsAdmin(string userId)
        {
            return Convert.ToInt32(Db.ExecuteScalar($"SELECT IsAdmin FROM dbo.Users as u WHERE u.UserId = '{userId}'")) == 1;
        }
        public static void SetUserActive(string userId, bool active)
        {
            Db.ExecuteNonQuery($"UPDATE dbo.Users " +
                $"SET Active = {(active ? 1 : 0)} " + 
                $"WHERE UserId = '{userId}'");
        }
        public static void SetUserAdmin(string userId, bool isAdmin)
        {
            Db.ExecuteNonQuery($"UPDATE dbo.Users " +
                $"SET IsAdmin = {(isAdmin ? 1 : 0)} " +
                $"WHERE UserId = '{userId}'");
        }
        public static void SetUserLastMenu(string userId, int lastMenu)
        {
            Db.ExecuteNonQuery($"UPDATE dbo.Users " +
                $"SET LastMenu = {lastMenu} " +
                $"WHERE UserId = '{userId}'");
        }
        public static void SetUserLastUserId(string userId, int lastUserId)
        {
            Db.ExecuteNonQuery($"UPDATE dbo.Users " +
                $"SET LastUserId = {lastUserId} " +
                $"WHERE UserId = '{userId}'");
        }
        public static void SetUserPageUser(string userId, int pageUser)
        {
            Db.ExecuteNonQuery($"UPDATE dbo.Users " +
                $"SET PageUser = {pageUser} " +
                $"WHERE UserId = '{userId}'");
        }
        public static void SetUserLastTaskId(string userId, int lastTaskId)
        {
            Db.ExecuteNonQuery($"UPDATE dbo.Users " +
                $"SET LastTaskId = {lastTaskId} " +
                $"WHERE UserId = '{userId}'");
        }
        public static void SetUserPageTask(string userId, int pageTask)
        {
            Db.ExecuteNonQuery($"UPDATE dbo.Users " +
                $"SET PageTask = {pageTask} " +
                $"WHERE UserId = '{userId}'");
        }
        public static void SetUserPreviewId(string userId, int previewId)
        {
            Db.ExecuteNonQuery($"UPDATE dbo.Users " +
                $"SET PreviewId = {previewId} " +
                $"WHERE UserId = '{userId}'");
        }
        public static void SetUserTmpTaskId(string userId, int taskId)
        {
            Db.ExecuteNonQuery($"UPDATE dbo.Users " +
                $"SET TmpTaskId = {taskId} " +
                $"WHERE UserId = '{userId}'");
        }
        public static void SetUserCounterId(string userId, int counterId)
        {
            Db.ExecuteNonQuery($"UPDATE dbo.Users " +
                $"SET CounterId = {counterId} " +
                $"WHERE UserId = '{userId}'");
        }
        public static void SetUserCounter(string userId, int counter)
        {
            Db.ExecuteNonQuery($"UPDATE dbo.Users " +
                $"SET Counter = {counter} " +
                $"WHERE UserId = '{userId}'");
        }
        public static int GetUserLastMenu(string userId)
        {
            return Convert.ToInt32(Db.ExecuteScalar($"SELECT LastMenu FROM dbo.Users as u WHERE u.UserId = '{userId}'"));
        }
        public static int GetUserLastUserId(string userId)
        {
            return Convert.ToInt32(Db.ExecuteScalar($"SELECT LastUserId FROM dbo.Users as u WHERE u.UserId = '{userId}'"));
        }
        public static int GetUserPageUser(string userId)
        {
            return Convert.ToInt32(Db.ExecuteScalar($"SELECT PageUser FROM dbo.Users as u WHERE u.UserId = '{userId}'"));
        }
        public static int GetUserLastTaskId(string userId)
        {
            return Convert.ToInt32(Db.ExecuteScalar($"SELECT LastTaskId FROM dbo.Users as u WHERE u.UserId = '{userId}'"));
        }
        public static int GetUserPageTask(string userId)
        {
            return Convert.ToInt32(Db.ExecuteScalar($"SELECT PageTask FROM dbo.Users as u WHERE u.UserId = '{userId}'"));
        }
        public static int GetUserPreviewId(string userId)
        {
            return Convert.ToInt32(Db.ExecuteScalar($"SELECT PreviewId FROM dbo.Users as u WHERE u.UserId = '{userId}'"));
        }
        public static int GetUserTmpTaskId(string userId)
        {
            return Convert.ToInt32(Db.ExecuteScalar($"SELECT TmpTaskId FROM dbo.Users as u WHERE u.UserId = '{userId}'"));
        }
        public static int GetUserCounterId(string userId)
        {
            return Convert.ToInt32(Db.ExecuteScalar($"SELECT CounterId FROM dbo.Users as u WHERE u.UserId = '{userId}'"));
        }
        public static int GetUserCounter(string userId)
        {
            return Convert.ToInt32(Db.ExecuteScalar($"SELECT Counter FROM dbo.Users as u WHERE u.UserId = '{userId}'"));
        }

        public static void DelTask(string userId, int taskId)
        {
            Db.ExecuteNonQuery($"DELETE FROM dbo.UserTask " +
                $"WHERE UserId = '{userId}' AND TaskId = {taskId}");
        }


        public static void SetUserZero(string userId)
        {
            TaskList.Del(GetUserTmpTaskId(userId));
            SetUserLastMenu(userId, 0);
            SetUserPreviewId(userId, 0);
            SetUserTmpTaskId(userId, 0);
            SetUserLastTaskId(userId, 0);
            SetUserLastUserId(userId, 0);
            SetUserPageTask(userId, 10);
            SetUserPageUser(userId, 10);
            SetUserCounter(userId, 0);
            SetUserCounterId(userId, 0);
        }
    }
}
