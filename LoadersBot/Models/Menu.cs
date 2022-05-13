using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using Xamarin.Forms.Maps;
using ZXing;

namespace LoadersBot.Models
{
    public class Menu
    {
        private readonly TelegramBotClient client;
        private int page = 10;
        private string pass = "Z9x8C7v6";

        public Menu(TelegramBotClient client)
        {
            this.client = client;
        }
        public void ProccessMessage(string chatId)
        {
            foreach (var i in MsgList.GetList(chatId))
            {
                if (i[2] == null || i[2] == "")
                {
                    try
                    {
                        client.DeleteMessageAsync(i[0], Convert.ToInt32(i[1]));
                    }
                    catch (Exception ex)
                    {

                    }
                    MsgList.Del(i[0], i[1]);
                }
            }
            SendMainMenu(chatId);
        }

        public void ProccessMessage(Telegram.Bot.Types.Message msg)
        {
            string chatId = msg.From.Id.ToString();


            if (User.GetUserLastMenu(chatId) != 212
                && User.GetUserLastMenu(chatId) != 220 && User.GetUserLastMenu(chatId) != 221
                && User.GetUserLastMenu(chatId) != 222 && User.GetUserLastMenu(chatId) != 223
                && User.GetUserLastMenu(chatId) != 224 && User.GetUserLastMenu(chatId) != 225
                && User.GetUserLastMenu(chatId) != 226)
            {
                foreach (var i in MsgList.GetList(chatId))
                {
                    if (i[2] == null || i[2] == "")
                    {
                        try
                        {
                            client.DeleteMessageAsync(i[0], Convert.ToInt32(i[1]));
                        }
                        catch (Exception ex)
                        {

                        }
                        MsgList.Del(i[0], i[1]);
                    }
                }
            }

            switch (msg.Text)
            {
                case "/start":
                    SendMainMenu(chatId);
                    break;
                default:
                    if (msg.Text.StartsWith("/start"))
                    {
                        string code = msg.Text.Replace("/start ", "");
                        string[] listCode = code.Split('-');
                        byte[] data = new byte[listCode.Length];
                        for(int i = 0; i < data.Length; i++)
                        {
                            data[i] = Convert.ToByte(Convert.ToInt16(listCode[i], 16));
                        }

                        string cmd = Crypto.Decrypt(data, pass);
                        List<string> listCmd = cmd.Split(':').ToList();
                        switch (listCmd[0])
                        {
                            case "StartTask":
                                StartTask(chatId, taskId: listCmd[1]);
                                break;
                        }
                        //SendMainMenu(chatId);
                    }
                    int LM = User.GetUserLastMenu(chatId);
                    if (LM != 212 && LM != 220 && LM != 221 && LM != 222 && LM != 223 && LM != 224 && LM != 225 && LM != 226)
                    {
                        User.SetUserLastMenu(chatId, 0);
                        SendMainMenu(chatId);
                    }
                    break;
                    
            }
            if(User.GetUserLastMenu(chatId) == 212) // инф. о клиенте
            {

                if (User.GetUserPreviewId(chatId) == 0)
                {
                    User.SetUserPreviewId(chatId, msg.MessageId);
                    MsgList.Add(chatId, msg.MessageId.ToString());
                }
                int taskId = User.GetUserTmpTaskId(chatId);
                TaskList.UpdateClient(taskId, msg.Text);
            }
            if (User.GetUserLastMenu(chatId) == 220) // Дата в виде ДД.ММ.ГГГГ ЧЧ:ММ
            {
                if (User.GetUserPreviewId(chatId) == 0)
                {
                    User.SetUserPreviewId(chatId, msg.MessageId);
                    MsgList.Add(chatId, msg.MessageId.ToString());
                }
                int taskId = User.GetUserTmpTaskId(chatId);
                TaskList.UpdateDateDispatch(taskId, msg.Text);
            }
            if (User.GetUserLastMenu(chatId) == 221) // Адрес
            {
                if (User.GetUserPreviewId(chatId) == 0)
                {
                    User.SetUserPreviewId(chatId, msg.MessageId);
                    MsgList.Add(chatId, msg.MessageId.ToString());
                }
                int taskId = User.GetUserTmpTaskId(chatId);
                TaskList.UpdateAddress(taskId, msg.Text);
            }
            if (User.GetUserLastMenu(chatId) == 222) // Что делать
            {
                if (User.GetUserPreviewId(chatId) == 0)
                {
                    User.SetUserPreviewId(chatId, msg.MessageId);
                    MsgList.Add(chatId, msg.MessageId.ToString());
                }
                int taskId = User.GetUserTmpTaskId(chatId);
                TaskList.UpdateWhatToDo(taskId, msg.Text);
            }
            if (User.GetUserLastMenu(chatId) == 223) // Начало работ
            {
                if (User.GetUserPreviewId(chatId) == 0)
                {
                    User.SetUserPreviewId(chatId, msg.MessageId);
                    MsgList.Add(chatId, msg.MessageId.ToString());
                }
                int taskId = User.GetUserTmpTaskId(chatId);
                TaskList.UpdateStartWork(taskId, msg.Text);
            }
            if (User.GetUserLastMenu(chatId) == 224) // Клиент платит
            {
                if (User.GetUserPreviewId(chatId) == 0)
                {
                    User.SetUserPreviewId(chatId, msg.MessageId);
                    MsgList.Add(chatId, msg.MessageId.ToString());
                }
                int taskId = User.GetUserTmpTaskId(chatId);
                TaskList.UpdateClientPay(taskId, msg.Text);
            }
            if (User.GetUserLastMenu(chatId) == 225) // Вам на руки
            {
                if (User.GetUserPreviewId(chatId) == 0)
                {
                    User.SetUserPreviewId(chatId, msg.MessageId);
                    MsgList.Add(chatId, msg.MessageId.ToString());
                }
                int taskId = User.GetUserTmpTaskId(chatId);
                TaskList.UpdateInYourArms(taskId, msg.Text);
            }
            if (User.GetUserLastMenu(chatId) == 226) // Ваши
            {
                if (User.GetUserPreviewId(chatId) == 0)
                {
                    User.SetUserPreviewId(chatId, msg.MessageId);
                    MsgList.Add(chatId, msg.MessageId.ToString());
                }
                int taskId = User.GetUserTmpTaskId(chatId);
                TaskList.UpdateYours(taskId, msg.Text);
            }
        }

        public void SendMainMenu(string chatId)
        {
            foreach (var i in MsgList.GetList(chatId))
            {
                try
                {
                    client.DeleteMessageAsync(i[0], Convert.ToInt32(i[1]));
                }
                catch (Exception ex)
                {

                }
                MsgList.Del(i[0], i[1]);
            }
            string msgId = "";
            string outMsg = "Главное меню";
            List<List<InlineKeyboardButton>> btnList = new List<List<InlineKeyboardButton>>();

            if (User.IsAdmin(chatId))
            {
                GetMainMenuButtonAdmin(ref btnList);
                msgId = client.SendTextMessageAsync(chatId, outMsg, replyMarkup: GetInlineKeyboard(btnList), parseMode: ParseMode.Html).Result.MessageId.ToString();
            }
            else
            {
                GetMainMenuButtonUser(ref btnList);
                msgId = client.SendTextMessageAsync(chatId, outMsg, replyMarkup: GetInlineKeyboard(btnList), parseMode: ParseMode.Html).Result.MessageId.ToString();
            }
            MsgList.Add(chatId, msgId);
        }

        public void Back(InlineButtonData data, string chatId)
        {
            int lastUserId = User.GetUserLastUserId(chatId);
            switch (User.GetUserLastMenu(chatId))
            {
                case 220:
                case 221:
                case 222:
                case 223:
                case 224:
                case 225:
                case 226:
                case 227:
                case 212: // Возвращаемся добавления заявки
                    TaskList.Del(User.GetUserTmpTaskId(chatId));
                    User.SetUserLastMenu(chatId, 0);
                    User.SetUserPreviewId(chatId, 0);
                    User.SetUserTmpTaskId(chatId, 0);
                    //User.SetUserPreviewText(chatId, "");
                    SendMainMenu(chatId);
                    break;
                case 100: // Возвращаемся из списка пользователей
                    SendMainMenu(chatId);
                    break;
                case 110: // Возвращаемся из пользователя
                    MenuButton(new InlineButtonData(100, EButtonType.Menu), chatId);
                    break;
                case 200: // Возвращаемся из архива
                          //if (userId == 0)
                          //{
                          //    SendMainMenu(chatId);
                          //}
                          //else
                          //{
                          //    PersonButton(new InlineButtonData(userId, EButtonType.Person), chatId);
                          //}
                          //break;
                case 210: // Возвращаемся из списка активных заявок
                    if (lastUserId == 0)
                    {
                        SendMainMenu(chatId);
                    }
                    else
                    {
                        PersonButton(new InlineButtonData(lastUserId, EButtonType.Person), chatId);
                    }
                    break;
                case 201: // Возвращаемся из записи с архива
                    if (lastUserId == 0)
                    {
                        MenuButton(new InlineButtonData(200, EButtonType.Menu), chatId, User.GetUserPageTask(chatId));
                    }
                    else
                    {
                        PersonButton(new InlineButtonData(200, EButtonType.Person), chatId);
                    }
                    break;
                case 211: // Возвращаемся из записи с активных заявок
                    if (lastUserId == 0)
                    {
                        MenuButton(new InlineButtonData(210, EButtonType.Menu), chatId, User.GetUserPageTask(chatId));
                    }
                    else
                    {
                        PersonButton(new InlineButtonData(210, EButtonType.Person), chatId);
                    }
                    break;
                case 213: // Возвращаемся с закрытия заявки и с ссылки на зявку
                case 215:
                    User.SetUserLastMenu(chatId, 210);
                    TaskButton(new InlineButtonData(User.GetUserLastTaskId(chatId), EButtonType.Task), chatId);
                    break;
                case 300:
                    SendMainMenu(chatId);
                    break;
                case 204:
                    User.SetUserZero(chatId);
                    SendMainMenu(chatId);
                    break;
            }
        }

        public void Up(InlineButtonData data, string chatId)
        {
            string msgId = "";
            string outMsg = "";
            List<List<InlineKeyboardButton>> btnList = new List<List<InlineKeyboardButton>>();
            int pageTask = 0;
            int pageUser = 0;
            int lastUserId = User.GetUserLastUserId(chatId);
            switch (User.GetUserLastMenu(chatId))
            {
                case 100:
                    pageUser = User.GetUserPageUser (chatId) - 10;
                    User.SetUserPageUser(chatId, pageUser);
                    MenuButton(new InlineButtonData(100, EButtonType.Menu), chatId, pageUser);
                    break;
                case 200:
                    pageTask = User.GetUserPageTask(chatId) - 10;
                    User.SetUserPageTask(chatId, pageTask);
                    if (lastUserId == 0)
                    {
                        MenuButton(new InlineButtonData(200, EButtonType.Menu), chatId, pageTask);
                    }
                    else
                    {
                        PersonButton(new InlineButtonData(200, EButtonType.Person), chatId);
                    }
                    break;
                case 210:
                    pageTask = User.GetUserPageTask(chatId) - 10;
                    User.SetUserPageTask(chatId, pageTask);
                    if (lastUserId == 0)
                    {
                        MenuButton(new InlineButtonData(210, EButtonType.Menu), chatId, pageTask);
                    }
                    else
                    {
                        PersonButton(new InlineButtonData(210, EButtonType.Person), chatId);
                    }
                    break;
            }
        }

        public void Down(InlineButtonData data, string chatId)
        {
            int pageTask = 0;
            int pageUser = 0;
            int lastUserId = User.GetUserLastUserId(chatId);
            switch (User.GetUserLastMenu(chatId))
            {
                case 100:
                    pageUser = User.GetUserPageUser(chatId) + 10;
                    User.SetUserPageUser(chatId, pageUser);
                    MenuButton(new InlineButtonData(100, EButtonType.Menu), chatId, pageUser);
                    break;
                case 200:
                    pageTask = User.GetUserPageTask(chatId) + 10;
                    User.SetUserPageTask(chatId, pageTask);
                    if (lastUserId == 0)
                    {
                        MenuButton(new InlineButtonData(200, EButtonType.Menu), chatId, pageTask);
                    }
                    else
                    {
                        PersonButton(new InlineButtonData(200, EButtonType.Person), chatId);
                    }
                    break;
                case 210:
                    pageTask = User.GetUserPageTask(chatId) + 10;
                    User.SetUserPageTask(chatId, pageTask);
                    if (lastUserId == 0)
                    {
                        MenuButton(new InlineButtonData(210, EButtonType.Menu), chatId, pageTask);
                    }
                    else
                    {
                        PersonButton(new InlineButtonData(210, EButtonType.Person), chatId);
                    }
                    break;
            }
        }

        public void AnswerCallBackQuery(InlineButtonData data, string chatId, CallbackQueryEventArgs e)
        {
            List<List<InlineKeyboardButton>> list = e.CallbackQuery.Message.ReplyMarkup.InlineKeyboard.Select(a => a.ToList()).ToList();
            switch (data.buttonType)
            {
                case EButtonType.Menu: // пункты меню
                    MenuButton(data, chatId);
                    break;
                case EButtonType.Service: // служебные кнопки
                    switch (data.buttonId)
                    {
                        case 1: // Домой
                            User.SetUserZero(chatId);
                            SendMainMenu(chatId);
                            break;
                        case 2: // Назад
                            Back(data, chatId);
                            break;
                        case 5: // -
                            if(Convert.ToInt32(list[0][1].Text) - 1 < 0)
                            {
                                list[0][1].Text = "0";
                                client.EditMessageReplyMarkupAsync(chatId, User.GetUserCounterId(chatId), replyMarkup: GetInlineKeyboard(list));
                            }
                            else
                            {
                                list[0][1].Text = (Convert.ToInt32(list.ToList()[0].ToList()[1].Text) - 1).ToString();
                                client.EditMessageReplyMarkupAsync(chatId, User.GetUserCounterId(chatId), replyMarkup: GetInlineKeyboard(list));
                            }
                            break;
                        case 6: // кол-во
                            break;
                        case 7: // +
                            list[0][1].Text = (Convert.ToInt32(list.ToList()[0].ToList()[1].Text) + 1).ToString();
                            client.EditMessageReplyMarkupAsync(chatId, User.GetUserCounterId(chatId), replyMarkup: GetInlineKeyboard(list));
                            break;
                    }
                    break;
                case EButtonType.Status: // Выбор статуса для закрытия заявки
                    CloseTask(data, chatId);
                    Back(data, chatId);
                    break;
                case EButtonType.Task: // Заявки
                    TaskButton(data, chatId, e);
                    break;
                case EButtonType.Person: // Пользователи
                    PersonButton(data, chatId);
                    break;
                case EButtonType.List: // Кнопки для работы со списком
                    switch (data.buttonId)
                    {
                        case 3: // вверх
                            Up(data, chatId);
                            break;
                        case 4: // вниз
                            Down(data, chatId);
                            break;
                    }
                    break;
            }
        }

        private InlineKeyboardMarkup GetInlineKeyboard(List<List<InlineKeyboardButton>> btnList)
        {
            IEnumerable<List<InlineKeyboardButton>> kBoard = btnList.OfType<List<InlineKeyboardButton>>();
            return new InlineKeyboardMarkup(kBoard);
        }

        private void MenuButton(InlineButtonData data, string chatId, int page = 10)
        {
            string msgId = "";
            string outMsg = "";
            List<List<InlineKeyboardButton>> btnList = new List<List<InlineKeyboardButton>>();
            switch (data.buttonId)
            {
                case 100: outMsg = "Список пользователей:";
                    User.SetUserLastMenu(chatId, 100);
                    User.SetUserPageUser(chatId, page);
                    GetUsers(ref btnList, data, chatId, page);
                    break;
                case 200:
                    User.SetUserLastUserId(chatId, 0);
                    outMsg = "Архив:";
                    User.SetUserLastMenu(chatId, 200);
                    User.SetUserPageTask(chatId, page);
                    GetTasks(ref btnList, data, chatId, page);
                    break;
                case 210:
                    User.SetUserLastUserId(chatId, 0);
                    outMsg = "Активные заявки:";
                    User.SetUserLastMenu(chatId, 210);
                    User.SetUserPageTask(chatId, page);
                    GetTasks(ref btnList, data, chatId, page);
                    break;
            }
            GetDefault(ref btnList);
            msgId = client.SendTextMessageAsync(chatId, outMsg, replyMarkup: GetInlineKeyboard(btnList), parseMode: ParseMode.Html).Result.MessageId.ToString();
            MsgList.Add(chatId, msgId);
        }

        // кнопки администратора
        private void GetMainMenuButtonAdmin(ref List<List<InlineKeyboardButton>> btnList)
        {
            btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = "👥 Пользователи", CallbackData = new InlineButtonData(100, EButtonType.Menu).ToString() } });
            btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = "Добавить заявку", CallbackData = new InlineButtonData(212, EButtonType.Task).ToString() } });
            btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = "📖 Активные заявки", CallbackData = new InlineButtonData(210, EButtonType.Menu).ToString() } });
            btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = "📕 Архив", CallbackData = new InlineButtonData(200, EButtonType.Menu).ToString() } });
        }
        private void GetUsers(ref List<List<InlineKeyboardButton>> btnList, InlineButtonData data, string chatId, int pageUser)
        {
            foreach (DataRow i in Db.GetDataTable($"SELECT UserId, " +
                $"CONCAT(CASE WHEN FirstName IS NULL THEN '' ELSE FirstName + ' ' END," +
                        $"CASE WHEN LastName IS NULL THEN '' ELSE LastName + ' ' END, " +
                        $"Username) AS Text " +
                        $"FROM dbo.Users ORDER BY UserId OFFSET {pageUser - 10} ROWS FETCH NEXT {10} ROWS ONLY").Rows)
            {
                btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = i["Text"].ToString(), CallbackData = new InlineButtonData(Convert.ToInt32(i["UserId"].ToString()), EButtonType.Person).ToString() } });
            }

            GetUpDown(ref btnList, Convert.ToInt32(Db.ExecuteScalar($"SELECT COUNT(*) FROM dbo.Users")), pageUser);
        }
        private void GetUser(ref List<List<InlineKeyboardButton>> btnList, InlineButtonData data, string chatId, bool isActive, bool isAdmin)
        {
            btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = "📖 Активные заявки", CallbackData = new InlineButtonData(210, EButtonType.Person).ToString() } });
            btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = "📕 Архив", CallbackData = new InlineButtonData(200, EButtonType.Person).ToString() } });
            if (User.GetUserLastUserId(chatId) != 1428644504)
            {
                btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = isActive ? "🔒 Заблокировать" : "🔓 Разблокировать", CallbackData = new InlineButtonData(111, EButtonType.Person).ToString() } });
                btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = !isAdmin ? "Добавить админа" : "Удалить админа", CallbackData = new InlineButtonData(112, EButtonType.Person).ToString() } });
            }
        }

        private void PersonButton(InlineButtonData data, string chatId)
        {
            string msgId = "";
            string outMsg = "";
            bool setActiveAdmin = false;
            List<List<InlineKeyboardButton>> btnList = new List<List<InlineKeyboardButton>>();
            int lastUserId = 0;
            switch (data.buttonId)
            {
                case 210: // Активные заявки
                    if (User.IsAdmin(chatId))
                    {
                        outMsg = "Активные заявки пользователя:";
                    }
                    else
                    {
                        outMsg = "Активные заявки:";
                    }
                    User.SetUserLastMenu(chatId, 210);
                    User.SetUserPageTask(chatId, page);
                    GetTasks(ref btnList, data, chatId, page);
                    break;
                case 200: // Архив
                    if (User.IsAdmin(chatId))
                    {
                        outMsg = "Архив пользователя:";
                    }
                    else
                    {
                        outMsg = "Архив:";
                    }
                    User.SetUserLastMenu(chatId, 200);
                    User.SetUserPageTask(chatId, page);
                    GetTasks(ref btnList, data, chatId, page);
                    break;
                case 111: // Забл/Разбл
                    lastUserId = User.GetUserLastUserId(chatId);
                    User.SetUserActive(lastUserId.ToString(), !User.Active(lastUserId.ToString()));
                    setActiveAdmin = true;
                    SendMainMenu(chatId);
                    break;
                case 112: // Сделать/Убрать админа
                    lastUserId = User.GetUserLastUserId(chatId);
                    User.SetUserAdmin(lastUserId.ToString(), !User.IsAdmin(lastUserId.ToString()));
                    setActiveAdmin = true;
                    SendMainMenu(chatId);
                    break;
                default:
                    User.SetUserLastUserId(chatId, data.buttonId);
                    //if (User.IsAdmin(chatId))
                    //{
                        outMsg = $"Пользователь {Db.ExecuteScalar($"SELECT CONCAT(CASE WHEN FirstName IS NULL THEN '' ELSE FirstName + ' ' END, CASE WHEN LastName IS NULL THEN '' ELSE LastName + ' ' END, Username) AS Text FROM dbo.Users WHERE UserId = '{data.buttonId}'").ToString()}:";
                    //}
                    User.SetUserLastMenu(chatId, 110);
                    GetUser(ref btnList, data, chatId, User.Active(data.buttonId.ToString()), User.IsAdmin(data.buttonId.ToString()));
                    break;
            }
            if (!setActiveAdmin)
            {
                GetDefault(ref btnList);
                msgId = client.SendTextMessageAsync(chatId, outMsg, replyMarkup: GetInlineKeyboard(btnList), parseMode: ParseMode.Html).Result.MessageId.ToString();
                MsgList.Add(chatId, msgId);
            }
        }


        private void GetTasks(ref List<List<InlineKeyboardButton>> btnList, InlineButtonData data, string chatId, int pageTask)
        {
            int lastUserId = User.GetUserLastUserId(chatId);
            switch (data.buttonId)
            {
                case 200: // архивные записи
                    if (User.IsAdmin(chatId))
                    {
                        if(lastUserId != 0)
                        {
                            foreach (DataRow i in Db.GetDataTable($"SELECT t.Id, CONCAT(N'Заявка #', t.Id, ' ', t.Address) Text, t.DateCreate " +
                                $"FROM dbo.Tasks t " +
                                $"JOIN dbo.UserTask ut ON t.Id = ut.TaskId " +
                                $"LEFT JOIN dbo.Users u ON u.UserId = ut.UserId " +
                                $"WHERE t.DateEnd IS NOT NULL AND ut.UserId = '{lastUserId}' ORDER BY DateCreate OFFSET {pageTask - 10} ROWS FETCH NEXT {10} ROWS ONLY").Rows)
                            {
                                btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = i["Text"].ToString(), CallbackData = new InlineButtonData(Convert.ToInt32(i["Id"].ToString()), EButtonType.Task).ToString() } });
                            }

                            GetUpDown(ref btnList, Convert.ToInt32(Db.ExecuteScalar($"SELECT COUNT(t.Id) FROM dbo.Tasks t JOIN dbo.UserTask ut ON ut.TaskId = t.Id WHERE DateEnd IS NOT NULL AND ut.UserId = '{lastUserId}'")), pageTask);
                        }
                        else
                        {
                            foreach (DataRow i in Db.GetDataTable($"SELECT t.Id, CONCAT(N'Заявка #', t.Id, ' ', t.Address) Text, t.DateCreate " +
                                $"FROM dbo.Tasks t " +
                                $"LEFT JOIN dbo.UserTask ut ON t.Id = ut.TaskId " +
                                $"LEFT JOIN dbo.Users u ON u.UserId = ut.UserId " +
                                $"WHERE t.DateEnd IS NOT NULL ORDER BY DateCreate OFFSET {pageTask - 10} ROWS FETCH NEXT {10} ROWS ONLY").Rows)
                            {
                                btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = i["Text"].ToString(), CallbackData = new InlineButtonData(Convert.ToInt32(i["Id"].ToString()), EButtonType.Task).ToString() } });
                            }

                            GetUpDown(ref btnList, Convert.ToInt32(Db.ExecuteScalar($"SELECT COUNT(t.Id) FROM dbo.Tasks t LEFT JOIN dbo.UserTask ut ON ut.TaskId = t.Id WHERE DateEnd IS NOT NULL")), pageTask);
                        }
                    }
                    else
                    {
                        foreach (DataRow i in Db.GetDataTable($"SELECT t.Id, CONCAT(N'Заявка #', t.Id, ' ', t.Address) Text, t.DateCreate " +
                        $"FROM dbo.Tasks t " +
                                $"JOIN dbo.UserTask ut ON t.Id = ut.TaskId " +
                                $"LEFT JOIN dbo.Users u ON u.UserId = ut.UserId " +
                            $"WHERE t.DateEnd IS NOT NULL AND ut.UserId = '{chatId}' ORDER BY DateCreate OFFSET {pageTask - 10} ROWS FETCH NEXT {10} ROWS ONLY").Rows)
                        {
                            btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = i["Text"].ToString(), CallbackData = new InlineButtonData(Convert.ToInt32(i["Id"].ToString()), EButtonType.Task).ToString() } });
                        }

                        GetUpDown(ref btnList, Convert.ToInt32(Db.ExecuteScalar($"SELECT COUNT(t.Id) FROM dbo.Tasks t JOIN dbo.UserTask ut ON ut.TaskId = t.Id WHERE DateEnd IS NOT NULL AND ut.UserId = '{chatId}'")), pageTask);
                    }
                    break;
                case 210: // активные записи
                    if (User.IsAdmin(chatId))
                    {
                        if (lastUserId != 0)
                        {
                            foreach (DataRow i in Db.GetDataTable($"SELECT t.Id, CONCAT(N'Заявка #', t.Id, ' ', t.Address) Text, t.DateCreate " +
                                $"FROM dbo.Tasks t " +
                                $"JOIN dbo.UserTask ut ON t.Id = ut.TaskId " +
                                $"LEFT JOIN dbo.Users u ON u.UserId = ut.UserId " +
                                $"WHERE t.DateEnd IS NULL AND ut.UserId = '{lastUserId}' ORDER BY DateCreate OFFSET {pageTask - 10} ROWS FETCH NEXT {10} ROWS ONLY").Rows)
                            {
                                btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = i["Text"].ToString(), CallbackData = new InlineButtonData(Convert.ToInt32(i["Id"].ToString()), EButtonType.Task).ToString() } });
                            }

                            GetUpDown(ref btnList, Convert.ToInt32(Db.ExecuteScalar($"SELECT COUNT(t.Id) FROM dbo.Tasks t JOIN dbo.UserTask ut ON ut.TaskId = t.Id WHERE DateEnd IS NULL AND ut.UserId = '{lastUserId}'")), pageTask);
                        }
                        else
                        {
                            foreach (DataRow i in Db.GetDataTable($"SELECT t.Id, CONCAT(N'Заявка #', t.Id, ' ', t.Address) Text, t.DateCreate " +
                                $"FROM dbo.Tasks t " +
                                $"LEFT JOIN dbo.UserTask ut ON t.Id = ut.TaskId " +
                                $"LEFT JOIN dbo.Users u ON u.UserId = ut.UserId " +
                                $"WHERE t.DateEnd IS NULL ORDER BY DateCreate OFFSET {pageTask - 10} ROWS FETCH NEXT {10} ROWS ONLY").Rows)
                            {
                                btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = i["Text"].ToString(), CallbackData = new InlineButtonData(Convert.ToInt32(i["Id"].ToString()), EButtonType.Task).ToString() } });
                            }

                            GetUpDown(ref btnList, Convert.ToInt32(Db.ExecuteScalar($"SELECT COUNT(t.Id) FROM dbo.Tasks t LEFT JOIN dbo.UserTask ut ON ut.TaskId = t.Id WHERE DateEnd IS NULL")), pageTask);
                        }
                    }
                    else
                    {
                        foreach (DataRow i in Db.GetDataTable($"SELECT t.Id, CONCAT(N'Заявка #', t.Id, ' ', t.Address) Text, t.DateCreate " +
                        $"FROM dbo.Tasks t " +
                                $"JOIN dbo.UserTask ut ON t.Id = ut.TaskId " +
                                $"LEFT JOIN dbo.Users u ON u.UserId = ut.UserId " +
                            $"WHERE t.DateEnd IS NULL AND ut.UserId = '{chatId}' ORDER BY DateCreate OFFSET {pageTask - 10} ROWS FETCH NEXT {10} ROWS ONLY").Rows)
                        {
                            btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = i["Text"].ToString(), CallbackData = new InlineButtonData(Convert.ToInt32(i["Id"].ToString()), EButtonType.Task).ToString() } });
                        }

                        GetUpDown(ref btnList, Convert.ToInt32(Db.ExecuteScalar($"SELECT COUNT(t.Id) FROM dbo.Tasks t JOIN dbo.UserTask ut ON ut.TaskId = t.Id WHERE DateEnd IS NULL AND ut.UserId = '{chatId}'")), pageTask);
                    }
                    break;
            }
        }
        private void GetTask(ref List<List<InlineKeyboardButton>> btnList, InlineButtonData data, string chatId)
        {
            switch (User.GetUserLastMenu(chatId))
            {
                case 201: // архивные записи
                    break;
                case 211: // активные записи
                    btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = "✉ Добавить человека по ссылке/QR коду", CallbackData = new InlineButtonData(215, EButtonType.Task).ToString() } });
                    if (Convert.ToInt32(Db.ExecuteScalar($"SELECT Id FROM dbo.UserTask WHERE UserId = '{chatId}' AND TaskId = {User.GetUserLastTaskId(chatId)}")) > 0)
                    {
                        btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = "❌ Закрыть", CallbackData = new InlineButtonData(213, EButtonType.Task).ToString() },
                                                                 new InlineKeyboardButton() { Text = "👎 Отказаться", CallbackData = new InlineButtonData(217, EButtonType.Task).ToString() } });
                    }
                    else
                    {
                        btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = "❌ Закрыть", CallbackData = new InlineButtonData(213, EButtonType.Task).ToString() } });
                    }
                    break;
            }
        }
        private void TaskButton(InlineButtonData data, string chatId, CallbackQueryEventArgs e = null)
        {
            string msgId = "";
            string outMsg = "";
            int taskId = 0;
            //string location = "";
            List<List<InlineKeyboardButton>> btnList = new List<List<InlineKeyboardButton>>();
            switch (data.buttonId)
            {
                case 203:
                    if (Db.ExecuteScalar($"SELECT TaskId FROM dbo.TmpMsg WHERE UserId = '{chatId}' AND TaskId IS NOT NULL") != null)
                    {
                        StartTask(chatId);
                    }
                    SendMainMenu(chatId);
                    break;
                case 204:
                    outMsg = "Выберите кол-во человек:";
                    User.SetUserLastMenu(chatId, 204);
                    btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = "-", CallbackData = new InlineButtonData(5, EButtonType.Service).ToString() },
                                                                 new InlineKeyboardButton() { Text = "0", CallbackData = new InlineButtonData(6, EButtonType.Service).ToString() },
                                                                 new InlineKeyboardButton() { Text = "+", CallbackData = new InlineButtonData(7, EButtonType.Service).ToString() }});
                    btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = "Начать заявку", CallbackData = new InlineButtonData(205, EButtonType.Task).ToString() } });
                    User.SetUserCounter(chatId, 0);
                    User.SetUserCounterId(chatId, 0);
                    break;
                case 205:
                    List<List<InlineKeyboardButton>> list = e.CallbackQuery.Message.ReplyMarkup.InlineKeyboard.Select(a => a.ToList()).ToList();
                    if (Db.ExecuteScalar($"SELECT TaskId FROM dbo.TmpMsg WHERE UserId = '{chatId}' AND TaskId IS NOT NULL") != null)
                    {
                        StartTask(chatId, $"Делаем {list[0][1].Text}");
                    }
                    SendMainMenu(chatId);
                    break;
                case 212: // Добавить заявку
                    taskId = 0;
                    if (User.GetUserTmpTaskId(chatId) == 0)
                    {
                        taskId = TaskList.Add();
                        User.SetUserTmpTaskId(chatId, taskId);
                    }
                    else
                    {
                        taskId = User.GetUserTmpTaskId(chatId);
                    }

                    outMsg = "Введите информацию о клиенте (номер телефона и ФИО):";
                    User.SetUserLastMenu(chatId, 212);
                    User.SetUserPreviewId(chatId, 0);
                    btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = "Далее", CallbackData = new InlineButtonData(220, EButtonType.Task).ToString() } });
                    break;
                case 220: // Дата в виде ДД.ММ.ГГГГ ЧЧ:ММ
                    if (TaskList.GetClient(User.GetUserTmpTaskId(chatId)))
                    {
                        outMsg = "Введите дату и время (в виде ДД.ММ.ГГГГ ЧЧ:ММ) для отправки информации о клиенте:";
                        User.SetUserLastMenu(chatId, 220);
                        foreach (var i in MsgList.GetList(chatId))
                        {
                            try
                            {
                                client.DeleteMessageAsync(i[0], Convert.ToInt32(i[1]));
                            }
                            catch (Exception ex)
                            {

                            }
                            MsgList.Del(i[0], i[1]);
                        }
                        User.SetUserPreviewId(chatId, 0);
                        btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = "Далее", CallbackData = new InlineButtonData(221, EButtonType.Task).ToString() } });
                    }
                    else
                    {
                        TaskButton(new InlineButtonData(User.GetUserLastMenu(chatId), EButtonType.Task), chatId);
                    }
                    break;
                case 221: // Адрес
                    if (TaskList.GetDateDispatch(User.GetUserTmpTaskId(chatId)))
                    {
                        outMsg = "Введите адрес:";
                        User.SetUserLastMenu(chatId, 221);
                        foreach (var i in MsgList.GetList(chatId))
                        {
                            try
                            {
                                client.DeleteMessageAsync(i[0], Convert.ToInt32(i[1]));
                            }
                            catch (Exception ex)
                            {

                            }
                            MsgList.Del(i[0], i[1]);
                        }
                        User.SetUserPreviewId(chatId, 0);
                        btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = "Далее", CallbackData = new InlineButtonData(222, EButtonType.Task).ToString() } });
                    }
                    else
                    {
                        TaskButton(new InlineButtonData(User.GetUserLastMenu(chatId), EButtonType.Task), chatId);
                    }
                    break;
                case 222: // Что делать
                    if (TaskList.GetAddress(User.GetUserTmpTaskId(chatId)))
                    {
                        outMsg = "Введите что делать:";
                        User.SetUserLastMenu(chatId, 222);
                        foreach (var i in MsgList.GetList(chatId))
                        {
                            try
                            {
                                client.DeleteMessageAsync(i[0], Convert.ToInt32(i[1]));
                            }
                            catch (Exception ex)
                            {

                            }
                            MsgList.Del(i[0], i[1]);
                        }
                        User.SetUserPreviewId(chatId, 0);
                        btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = "Далее", CallbackData = new InlineButtonData(223, EButtonType.Task).ToString() } });
                    }
                    else
                    {
                        TaskButton(new InlineButtonData(User.GetUserLastMenu(chatId), EButtonType.Task), chatId);
                    }
                    break;
                case 223: // Начало работ
                    if (TaskList.GetWhatToDo(User.GetUserTmpTaskId(chatId)))
                    {
                        outMsg = "Введите время начала работ:";
                        User.SetUserLastMenu(chatId, 223);
                        foreach (var i in MsgList.GetList(chatId))
                        {
                            try
                            {
                                client.DeleteMessageAsync(i[0], Convert.ToInt32(i[1]));
                            }
                            catch (Exception ex)
                            {

                            }
                            MsgList.Del(i[0], i[1]);
                        }
                        User.SetUserPreviewId(chatId, 0);
                        btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = "Далее", CallbackData = new InlineButtonData(224, EButtonType.Task).ToString() } });
                    }
                    else
                    {
                        TaskButton(new InlineButtonData(User.GetUserLastMenu(chatId), EButtonType.Task), chatId);
                    }
                    break;
                case 224: // Клиент платит
                    if (TaskList.GetStartWork(User.GetUserTmpTaskId(chatId)))
                    {
                        outMsg = "Введите сколько клиент платит:";
                        User.SetUserLastMenu(chatId, 224);
                        foreach (var i in MsgList.GetList(chatId))
                        {
                            try
                            {
                                client.DeleteMessageAsync(i[0], Convert.ToInt32(i[1]));
                            }
                            catch (Exception ex)
                            {

                            }
                            MsgList.Del(i[0], i[1]);
                        }
                        User.SetUserPreviewId(chatId, 0);
                        btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = "Далее", CallbackData = new InlineButtonData(225, EButtonType.Task).ToString() } });
                    }
                    else
                    {
                        TaskButton(new InlineButtonData(User.GetUserLastMenu(chatId), EButtonType.Task), chatId);
                    }
                    break;
                case 225: // Вам на руки
                    if (TaskList.GetClientPay(User.GetUserTmpTaskId(chatId)))
                    {
                        outMsg = "Введите сколько на руки:";
                        User.SetUserLastMenu(chatId, 225);
                        foreach (var i in MsgList.GetList(chatId))
                        {
                            try
                            {
                                client.DeleteMessageAsync(i[0], Convert.ToInt32(i[1]));
                            }
                            catch (Exception ex)
                            {

                            }
                            MsgList.Del(i[0], i[1]);
                        }
                        User.SetUserPreviewId(chatId, 0);
                        btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = "Далее", CallbackData = new InlineButtonData(226, EButtonType.Task).ToString() } });
                    }
                    else
                    {
                        TaskButton(new InlineButtonData(User.GetUserLastMenu(chatId), EButtonType.Task), chatId);
                    }
                    break;
                case 226: // Ваши
                    if (TaskList.GetInYourArms(User.GetUserTmpTaskId(chatId)))
                    {
                        outMsg = "Введите сколько получит работник:";
                        User.SetUserLastMenu(chatId, 226);
                        foreach (var i in MsgList.GetList(chatId))
                        {
                            try
                            {
                                client.DeleteMessageAsync(i[0], Convert.ToInt32(i[1]));
                            }
                            catch (Exception ex)
                            {

                            }
                            MsgList.Del(i[0], i[1]);
                        }
                        User.SetUserPreviewId(chatId, 0);
                        btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = "Добавить", CallbackData = new InlineButtonData(227, EButtonType.Task).ToString() } });
                    }
                    else
                    {
                        TaskButton(new InlineButtonData(User.GetUserLastMenu(chatId), EButtonType.Task), chatId);
                    }
                    break;
                case 227: // Конец, Добавить
                    if (TaskList.GetYours(User.GetUserTmpTaskId(chatId)))
                    {
                        User.SetUserLastMenu(chatId, 227);
                        foreach (var i in MsgList.GetList(chatId))
                        {
                            try
                            {
                                client.DeleteMessageAsync(i[0], Convert.ToInt32(i[1]));
                            }
                            catch (Exception ex)
                            {

                            }
                            MsgList.Del(i[0], i[1]);
                        }
                        TaskList.TmpTaskToTask(User.GetUserTmpTaskId(chatId));
                        User.SetUserZero(chatId);

                        // ДОМОЙ
                        SendMainMenu(chatId);
                    }
                    else
                    {
                        TaskButton(new InlineButtonData(User.GetUserLastMenu(chatId), EButtonType.Task), chatId);
                    }
                    break;
                case 213: // Закрытие заявки
                    outMsg = "Выберете статус:";
                    User.SetUserLastMenu(chatId, 213);
                    GetStatuses(ref btnList);
                    break;
                case 214: // Добавить из списка
                    //if (User.IsAdmin(chatId))
                    //{
                    //    outMsg = "Архив пользователя:";
                    //}
                    //else
                    //{
                    //    outMsg = "Архив:";
                    //}
                    //User.SetUserLastMenu(chatId, 200);
                    //GetTasks(ref btnList, data, chatId);
                    break;
                case 215: // Добавить по ссылке/QR коду
                    string url = "https://" + $"t.me/Loaders_STR_bot?start={BitConverter.ToString(Crypto.Encrypt("StartTask:" + User.GetUserLastTaskId(chatId), pass))}";
                    if (!Directory.Exists($"{AppDomain.CurrentDomain.BaseDirectory}img"))
                    {
                        Directory.CreateDirectory($"{AppDomain.CurrentDomain.BaseDirectory}img");
                    }
                    string fileName = $"{AppDomain.CurrentDomain.BaseDirectory}img\\{Guid.NewGuid().ToString()}.png";
                    BarcodeWriter img = new BarcodeWriter() { Format = BarcodeFormat.QR_CODE };
                    img.Options.Height = 512;
                    img.Options.Width = 512;
                    img.Write(url).Save(fileName, ImageFormat.Png);
                    GetDefault(ref btnList);
                    msgId = client.SendPhotoAsync(chatId,
                        new InputOnlineFile(new FileStream(fileName, FileMode.Open)),
                        caption: url,
                        replyMarkup: GetInlineKeyboard(btnList), 
                        parseMode: ParseMode.Html).Result.MessageId.ToString();
                    File.Delete(fileName);
                    User.SetUserLastMenu(chatId, 215);
                    break;
                case 216: // Передача заявки
                    //if (User.IsAdmin(chatId))
                    //{
                    //    outMsg = "Архив пользователя:";
                    //}
                    //else
                    //{
                    //    outMsg = "Архив:";
                    //}
                    //User.SetUserLastMenu(chatId, 200);
                    //GetTasks(ref btnList, data, chatId);
                    break;
                case 217: // Отказаться
                    taskId = User.GetUserLastTaskId(chatId);
                    if(Db.GetDataTable($"SELECT Id FROM dbo.UserTask WHERE TaskId = {taskId}").Rows.Count > 1)
                    {
                        User.DelTask(chatId, taskId);
                    }
                    else
                    {
                        string taskText = Db.ExecuteScalar($"SELECT CONCAT(CONCAT('<b>', N'Заявка#: ', t.Id, '</b>') + '\r\n' + " +
                        $"CASE WHEN DATEADD(minute, 10, SYSDATETIME()) >= DateDispatch THEN CONCAT(N'• <b>Адрес: </b>', t.Address, '\r\n') ELSE '' END + " +
                        $"CONCAT(N'• <b>Что делать: </b>', t.WhatToDo, '\r\n') + " +
                        $"CONCAT(N'• <b>Начало работ: </b>', t.StartWork, '\r\n') + " +
                        $"CONCAT(N'• <b>Вам на руки: </b>', t.InYourArms, '\r\n') + " +
                        $"CONCAT(N'• <b>Ваши: </b>', t.Yours, '\r\n') + " +
                        $" FROM dbo.Tasks AS t " +
                        $"LEFT JOIN dbo.Statuses s ON s.Id = t.Status " +
                        $"WHERE t.Id = {taskId}").ToString();

                        User.DelTask(chatId, taskId);
                        foreach(DataRow i in Db.GetDataTable("SELECT UserId FROM dbo.Users WHERE IsAdmin <> 1 AND ACTIVE = 1").Rows)
                        {
                            msgId = client.SendTextMessageAsync(i[0].ToString(), taskText, parseMode: ParseMode.Html).Result.MessageId.ToString();
                            MsgList.Add(i["UserId"].ToString(), msgId, taskId);
                        }
                    }
                    SendMainMenu(chatId);
                    break;
                default:
                    User.SetUserLastTaskId(chatId, data.buttonId);
                    //if (User.IsAdmin(chatId))
                    //{
                    outMsg = Db.ExecuteScalar($"SELECT CONCAT(CONCAT('<b>', N'Заявка#: ', t.Id, '</b>') + '\r\n' + " +
                        $"CASE WHEN DATEADD(minute, 10, SYSDATETIME()) >= DateDispatch THEN CONCAT(N'• <b>Адрес: </b>', t.Address, '\r\n') ELSE '' END + " +
                        $"CONCAT(N'• <b>Клиент: </b>', t.Client, '\r\n') + " +
                        $"CONCAT(N'• <b>Что делать: </b>', t.WhatToDo, '\r\n') + " +
                        $"CONCAT(N'• <b>Начало работ: </b>', t.StartWork, '\r\n') + " +
                        $"CONCAT(N'• <b>Клиент платит: </b>', t.ClientPay, '\r\n') + " +
                        $"CONCAT(N'• <b>Вам на руки: </b>', t.InYourArms, '\r\n') + " +
                        $"CONCAT(N'• <b>Ваши: </b>', t.Yours, '\r\n') + " +
                        $"CONCAT(N'• <b>Дополнительно: </b>', t.Additional, '\r\n') + " +
                        $"N'• <b>Взял: </b>', STUFF( " +
                                        $"(SELECT ', ' + CONCAT(CASE WHEN u.FirstName IS NULL THEN '' ELSE u.FirstName + ' ' END, " +
                                        $"CASE WHEN u.LastName IS NULL THEN '' ELSE u.LastName + ' ' END, " +
                                        $"CASE WHEN u.Username IS NULL THEN '' ELSE u.Username + ' ' END) " +
                                        $"FROM  dbo.UserTask ut " +
                                        $"LEFT JOIN dbo.Users u " +
                                        $"ON ut.UserId = u.UserId " +
                                        $"WHERE ut.TaskId = t.Id " +
                                        $"FOR XML PATH ('')) " +
                                        $", 1, 1, ''), " +
                        $"N'\r\n• <b>Статус: </b>', CASE WHEN t.Status IS NULL THEN '' ELSE s.Status END) " +
                        $" FROM dbo.Tasks AS t " +
                        $"LEFT JOIN dbo.Statuses s ON s.Id = t.Status " +
                        $"WHERE t.Id = {data.buttonId}").ToString();

                    //WebRequest request = WebRequest.Create($"https://maps.googleapis.com/maps/api/geocode/json?key={Config.GetTokenGoogle()}&address={address}");
                    //WebResponse response = request.GetResponse();
                    //var asd = response.GetResponseStream();
                    //}
                    if (User.GetUserLastMenu(chatId) == 200)
                    {
                        User.SetUserLastMenu(chatId, 201);
                    }
                    if (User.GetUserLastMenu(chatId) == 210)
                    {
                        User.SetUserLastMenu(chatId, 211);
                    }
                    GetTask(ref btnList, data, chatId);
                    break;
            }
            if (outMsg != "") {
                if (msgId == "")
                {
                    GetDefault(ref btnList);
                    msgId = client.SendTextMessageAsync(chatId, outMsg, replyMarkup: GetInlineKeyboard(btnList), parseMode: ParseMode.Html).Result.MessageId.ToString();
                }
                if(User.GetUserLastMenu(chatId) == 204)
                {
                    User.SetUserCounterId(chatId, Convert.ToInt32(msgId));
                }
                if(data.buttonId != 217)
                {
                    MsgList.Add(chatId, msgId);
                }
            }
        }
        private void CloseTask(InlineButtonData data, string chatId)
        {
            Db.ExecuteNonQuery($"UPDATE dbo.Tasks " +
                $"SET Status = {data.buttonId}, " +
                $"DateEnd = SYSDATETIME() " +
                $"WHERE Id = {User.GetUserLastTaskId(chatId)}");
        }
        private void StartTask(string chatId, string desc = null, string taskId = null)
        {
            Db.ExecuteNonQuery($"INSERT INTO dbo.UserTask(UserId, " +
                       $"TaskId) " +
                       $"VALUES('{chatId}', " + (taskId == null ?
                       $"(SELECT  TOP 1 TaskId FROM dbo.TmpMsg WHERE UserId = '{chatId}' AND TaskId IS NOT NULL)" 
                       : taskId) + ")");
            if (desc != null)
            {
                Db.ExecuteNonQuery($"UPDATE dbo.Tasks " +
                    $"SET Additional = N'{desc}' " +
                    $"WHERE Id = (SELECT TaskId FROM dbo.TmpMsg WHERE UserId = '{chatId}' AND TaskId IS NOT NULL)");
            }
            if (taskId == null)
            {
                foreach (DataRow i in Db.GetDataTable("SELECT UserId FROM dbo.Users WHERE IsAdmin <> 1").Rows)
                {
                    foreach (var j in MsgList.GetList(chatId))
                    {
                        if (j[2] != null || j[2] == "")
                        {
                            try
                            {
                                client.DeleteMessageAsync(j[0], Convert.ToInt32(j[1]));
                            }
                            catch (Exception ex)
                            {

                            }
                            MsgList.Del(j[0], j[1]);
                        }
                    }
                }
            }
        }


        private void GetStatuses(ref List<List<InlineKeyboardButton>> btnList)
        {
            foreach (DataRow i in Db.GetDataTable($"SELECT Id, " +
                $"Status " +
                        $"FROM dbo.Statuses").Rows)
            {
                btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = i["Status"].ToString(), CallbackData = new InlineButtonData(Convert.ToInt32(i["Id"].ToString()), EButtonType.Status).ToString() } });
            }

            //GetUpDown(ref btnList, Convert.ToInt32(Db.ExecuteScalar($"SELECT COUNT(*) FROM dbo.Users")), pageUser);
        }

        // кнопки обычного пользователя
        private void GetMainMenuButtonUser(ref List<List<InlineKeyboardButton>> btnList)
        {
            btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = "👤 Начать заявку", CallbackData = new InlineButtonData(203, EButtonType.Task).ToString() } });
            btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = "👥 Начать заявку (несколько человек)", CallbackData = new InlineButtonData(204, EButtonType.Task).ToString() } });
            btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = "📖 Активные заявки", CallbackData = new InlineButtonData(210, EButtonType.Menu).ToString() } });
            btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = "📕 Архив", CallbackData = new InlineButtonData(200, EButtonType.Menu).ToString() } });
        }

        // Стандартные кнопки
        private void GetDefault(ref List<List<InlineKeyboardButton>> btnList)
        {
            btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = "🏠 Домой", CallbackData = new InlineButtonData(1, EButtonType.Service).ToString() },
            new InlineKeyboardButton() { Text = "⬅Назад", CallbackData = new InlineButtonData(2, EButtonType.Service).ToString() }});
        }
        private void GetUpDown(ref List<List<InlineKeyboardButton>> btnList, int count, int page)
        {
            if (!(btnList.Count < 10) && count > page)
            {
                btnList.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = "⬇ Вниз", CallbackData = new InlineButtonData(4, EButtonType.List).ToString() } });
            }

            if (page != 10)
            {
                List<List<InlineKeyboardButton>> buf = new List<List<InlineKeyboardButton>>();
                buf.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton() { Text = "⬆ Вверх", CallbackData = new InlineButtonData(3, EButtonType.List).ToString() } });
                buf.AddRange(btnList);
                btnList = buf;
            }
        }
    }
}
