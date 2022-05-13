using LoadersBot.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoadersBot.Forms
{
    public partial class Form1 : Form
    {
        private Bot bot;
        public Form1()
        {
            InitializeComponent();
            if (!Config.GetFileExist() || Config.GetFileLength())
            {
                DialogResult dialogResult = MessageBox.Show("Это првый запуск программы заполните поля подключения к БД и токен для Телеграм Бота", "Внимание", MessageBoxButtons.OK);
            }
            else
            {
                TB_TokenBot.Text = Config.GetToken(ref Lbl_Status);
                TB_ConnectionString.Text = Config.GetConnectionString(ref Lbl_Status);
                //TB_TokenGoogle.Text = Config.GetTokenGoogle(ref Lbl_Status);
                if (Lbl_Status.Text == "")
                {
                    Lbl_StatusDB.Text = "Не работает";
                    Lbl_StatusBot.Text = "Не работает";
                }
                else
                {
                    string cs = Config.GetConnectionString(ref Lbl_Status);
                    SqlConnection conn = new SqlConnection(cs);
                    try
                    {
                        conn.Open();
                            Lbl_StatusDB.Text = "Работает";
                            conn.Close();
                    }
                    catch(Exception ex)
                    {
                        Lbl_StatusDB.Text = "Не работает";
                    }
                }
            }
        }

        private void Btn_SaveToken_Click(object sender, EventArgs e)
        {
            if (Config.GetFileExist())
            {
                Config.SetToken(ref Lbl_Status, TB_TokenBot.Text);
            }
            else
            {
                Config.AddFileConfig();
                Config.SetToken(ref Lbl_Status, TB_TokenBot.Text);
            }
        }

        private void Btn_SaveConnectString_Click(object sender, EventArgs e)
        {
            if (Config.GetFileExist())
            {
                Config.SetConnectionString(ref Lbl_Status, TB_ConnectionString.Text);
            }
            else
            {
                Config.AddFileConfig();
                Config.SetConnectionString(ref Lbl_Status, TB_ConnectionString.Text);
            }
        }

        private void Btn_SaveTokenGoogle_Click(object sender, EventArgs e)
        {
            if (Config.GetFileExist())
            {
                //Config.SetTokenGoogle(ref Lbl_Status, TB_TokenGoogle.Text);
            }
            else
            {
                Config.AddFileConfig();
                //Config.SetTokenGoogle(ref Lbl_Status, TB_TokenGoogle.Text);
            }
        }

        private void Btn_RecheckConnectDB_Click(object sender, EventArgs e)
        {
            string cs = Config.GetConnectionString(ref Lbl_Status);
            SqlConnection conn = new SqlConnection(cs);
            try
            {
                conn.Open();
                Lbl_StatusDB.Text = "Работает";
                conn.Close();
            }
            catch (Exception ex)
            {
                Lbl_StatusDB.Text = "Не работает";
            }
        }

        private void Btn_OnOffBot_Click(object sender, EventArgs e)
        {
            if(bot != null)
            {
                if (bot.GetStatus())
                {
                    bot.Stop();
                    Lbl_StatusBot.Text = "Не работает";
                }
                else
                {
                    bot.Start();
                    Lbl_StatusBot.Text = "Работает";
                }
            }
            else
            {
                bot = new Bot();
                bot.Start();
                Lbl_StatusBot.Text = "Работает";
            }
            //if (!Bot.GetClient().IsReceiving)
            //{
            //    Bot.GetClient().StartReceiving();
            //    Lbl_StatusDB.Text = "Работает";
            //}
            //else
            //{
            //    Bot.GetClient().StopReceiving();
            //    Lbl_StatusDB.Text = "Не работает";
            //}

        }
    }
}
