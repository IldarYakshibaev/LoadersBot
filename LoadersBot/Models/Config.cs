using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoadersBot.Models
{
    public static class Config
    {
        const string Pass = "z0p1m6k8";
        public static bool GetFileExist()
        {
            return System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\config.txt");
        }
        public static bool GetFileLength()
        {
            return new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "\\config.txt").Length == 0;
        }
        public static void AddFileConfig()
        {
            new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "\\config.txt").Create().Close();
        }
        public static string GetToken()
        {
            try
            {
                //FileStream fr = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\config.txt", FileMode.Open);
                byte[] data = File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + "\\config.txt");
                IDictionary<string, string> dic = JsonConvert.DeserializeObject<IDictionary<string, string>>(Crypto.Decrypt(data, Pass));
                return dic["Token"];
            }
            catch (Exception e)
            {
                return "";
            }
        }
        public static string GetToken(ref Label Lb_Status)
        {
            try
            {
                //FileStream fr = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\config.txt", FileMode.Open);
                byte[] data = File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + "\\config.txt");
                IDictionary<string, string> dic = JsonConvert.DeserializeObject<IDictionary<string, string>>(Crypto.Decrypt(data, Pass));
                return dic["Token"];
            }
            catch (Exception e)
            {
                Lb_Status.Text = "Произошла ошибка при чтении токена. " + e.Message;
                Lb_Status.Visible = true;
                return "";
            }
        }
        public static void SetToken(ref Label Lb_Status, string token)
        {
            try
            {
                //FileStream fr = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\config.txt", FileMode.Open);
                byte[] data = File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + "\\config.txt");
                IDictionary<string, string> dic = JsonConvert.DeserializeObject<IDictionary<string, string>>(Crypto.Decrypt(data, Pass));
                if(dic == null)
                {
                    dic = new Dictionary<string, string>();
                }
                if (dic.ContainsKey("Token"))
                {
                    dic["Token"] = token;
                }
                else
                {
                    dic.Add("Token", token);
                }
                data = Crypto.Encrypt(JsonConvert.SerializeObject(dic), Pass);
                File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "\\config.txt", data);
                Lb_Status.Text = "Токен сохранен.";
                Lb_Status.Visible = true;
            }
            catch (Exception e)
            {
                Lb_Status.Text = "Произошла ошибка при сохранении токена. " + e.Message;
                Lb_Status.Visible = true;
            }
        }
        public static string GetConnectionString()
        {
            try
            {
                byte[] data = File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + "\\config.txt");
                IDictionary<string, string> dic = JsonConvert.DeserializeObject<IDictionary<string, string>>(Crypto.Decrypt(data, Pass));
                return dic["ConnectionDB"];
            }
            catch (Exception e)
            {
                return "";
            }
        }
        public static string GetConnectionString(ref Label Lb_Status)
        {
            try
            {
                byte[] data = File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + "\\config.txt");
                IDictionary<string, string> dic = JsonConvert.DeserializeObject<IDictionary<string, string>>(Crypto.Decrypt(data, Pass));
                return dic["ConnectionDB"];
            }
            catch (Exception e)
            {
                Lb_Status.Text = "Произошла ошибка при чтении подключения к БД. " + e.Message;
                Lb_Status.Visible = true;
                return "";
            }
        }
        public static void SetConnectionString(ref Label Lb_Status, string cs)
        {
            try
            {
                byte[] data = File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + "\\config.txt");
                IDictionary<string, string> dic = JsonConvert.DeserializeObject<IDictionary<string, string>>(Crypto.Decrypt(data, Pass));
                if (dic == null)
                {
                    dic = new Dictionary<string, string>();
                }
                if (dic.ContainsKey("Token"))
                {
                    dic["ConnectionDB"] = cs;
                }
                else
                {
                    dic.Add("ConnectionDB", cs);
                }
                data = Crypto.Encrypt(JsonConvert.SerializeObject(dic), Pass);
                File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "\\config.txt", data);
                Lb_Status.Text = "Строка подключения сохранена.";
                Lb_Status.Visible = true;
            }
            catch (Exception e)
            {
                Lb_Status.Text = "Произошла ошибка при чтении подключения к БД. " + e.Message;
                Lb_Status.Visible = true;
            }
        }
        public static string GetTokenGoogle()
        {
            try
            {
                //FileStream fr = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\config.txt", FileMode.Open);
                byte[] data = File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + "\\config.txt");
                IDictionary<string, string> dic = JsonConvert.DeserializeObject<IDictionary<string, string>>(Crypto.Decrypt(data, Pass));
                return dic["TokenGoogle"];
            }
            catch (Exception e)
            {
                return "";
            }
        }
        public static string GetTokenGoogle(ref Label Lb_Status)
        {
            try
            {
                //FileStream fr = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\config.txt", FileMode.Open);
                byte[] data = File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + "\\config.txt");
                IDictionary<string, string> dic = JsonConvert.DeserializeObject<IDictionary<string, string>>(Crypto.Decrypt(data, Pass));
                return dic["TokenGoogle"];
            }
            catch (Exception e)
            {
                Lb_Status.Text = "Произошла ошибка при чтении токена гугла. " + e.Message;
                Lb_Status.Visible = true;
                return "";
            }
        }
        public static void SetTokenGoogle(ref Label Lb_Status, string token)
        {
            try
            {
                //FileStream fr = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\config.txt", FileMode.Open);
                byte[] data = File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + "\\config.txt");
                IDictionary<string, string> dic = JsonConvert.DeserializeObject<IDictionary<string, string>>(Crypto.Decrypt(data, Pass));
                if (dic == null)
                {
                    dic = new Dictionary<string, string>();
                }
                if (dic.ContainsKey("TokenGoogle"))
                {
                    dic["TokenGoogle"] = token;
                }
                else
                {
                    dic.Add("TokenGoogle", token);
                }
                data = Crypto.Encrypt(JsonConvert.SerializeObject(dic), Pass);
                File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "\\config.txt", data);
                Lb_Status.Text = "Токен гугла сохранен.";
                Lb_Status.Visible = true;
            }
            catch (Exception e)
            {
                Lb_Status.Text = "Произошла ошибка при чтении токена. " + e.Message;
                Lb_Status.Visible = true;
            }
        }
    }
}
