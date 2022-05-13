using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace LoadersBot.Models
{
    public class Bot
    {
        private static TelegramBotClient client;
        private Menu menu;
        private bool status = false;

        public Bot()
        {
            client = new TelegramBotClient(Config.GetToken());
            menu = new Menu(client);
        }

        public void Start()
        {
            client.StartReceiving();
            client.OnMessage += OnMessage;
            client.OnCallbackQuery += OnCallBackQuery;
            client.OnMessageEdited += OnMessage;
            status = true;
        }

        public void Stop()
        {
            client.StopReceiving();
            status = false;
        }

        public bool GetStatus()
        {
            return status;
        }

        private async void OnMessage(object sender, MessageEventArgs e)
        {
            Telegram.Bot.Types.User user = e.Message.From;
            string chatId = user.Id.ToString();

            if (!User.Exists(chatId))
            {
                User.Add(user);
            }

            if (User.GetUserLastMenu(chatId) != 212
                && User.GetUserLastMenu(chatId) != 220 && User.GetUserLastMenu(chatId) != 221
                && User.GetUserLastMenu(chatId) != 222 && User.GetUserLastMenu(chatId) != 223
                && User.GetUserLastMenu(chatId) != 224 && User.GetUserLastMenu(chatId) != 225
                && User.GetUserLastMenu(chatId) != 226
                || User.GetUserPreviewId(chatId) != e.Message.MessageId && User.GetUserPreviewId(chatId) != 0)
            {
                try
                {
                    await client.DeleteMessageAsync(chatId, e.Message.MessageId);
                }
                catch (Exception ex)
                {

                }
            }
            try
            {
                if (User.Active(chatId))
                {
                    menu.ProccessMessage(e.Message);


                    //try
                    //{
                    //    await client.DeleteMessageAsync(chatId, e.Message.MessageId);
                    //}
                    //catch (Exception ex)
                    //{

                    //}
                }
            }
            catch (Exception ex)
            {
                User.SetUserZero(chatId);
                menu.ProccessMessage(chatId);
            }
        }

        private async void OnCallBackQuery(object sender, CallbackQueryEventArgs e)
        {
            Telegram.Bot.Types.User user = e.CallbackQuery.From;
            string chatId = user.Id.ToString();

            if (!User.Exists(chatId))
            {
                User.Add(user);
            }
            if (User.GetUserLastMenu(chatId) != 204)
            {
                foreach (var i in MsgList.GetList(user.Id.ToString()))
                {

                    if (i[2] == null || i[2] == "")
                    {
                        try
                        {
                            await client.DeleteMessageAsync(i[0], Convert.ToInt32(i[1]));
                        }
                        catch (Exception ex)
                        {

                        }
                        MsgList.Del(i[0], i[1]);
                    }
                }

                try
                {
                    await client.DeleteMessageAsync(chatId, e.CallbackQuery.Message.MessageId);
                }
                catch (Exception ex)
                {

                }
            }

            try
            {
                if (User.Active(chatId))
                {
                    //MsgList.Add();
                    menu.AnswerCallBackQuery(new InlineButtonData(e.CallbackQuery.Data), e.CallbackQuery.From.Id.ToString(), e);
                    await client.AnswerCallbackQueryAsync(e.CallbackQuery.Id);
                }
            }
            catch (Exception ex)
            {
                User.SetUserZero(chatId);
                menu.ProccessMessage(chatId);
            }
        }



        private static IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = "Стикер" }, new KeyboardButton { Text = "Картинка" } },
                    new List<KeyboardButton>{ new KeyboardButton { Text = "123" }, new KeyboardButton { Text = "456" } }
                }
            };
        }
    }
}
