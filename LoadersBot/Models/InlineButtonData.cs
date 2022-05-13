using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadersBot.Models
{
    public class InlineButtonData
    {
        public int buttonId;
        public EButtonType buttonType;
        public InlineButtonData(string data)
        {
            try
            {
                if (data.Length == 0)
                {
                    buttonId = 0;
                    buttonType = EButtonType.Menu;
                    // ObjectId = 0
                    return;
                }
                buttonId = Convert.ToInt32(data.Replace("\n", "").Split('\r')[0].Split(':')[1]);
                buttonType = (EButtonType)Convert.ToInt32(data.Replace("\n", "").Split('\r')[1].Split(':')[1]);
            }
            // ObjectId = Data.Split(vbCrLf)(2).Split(":")(1)
            catch (Exception ex)
            {
                buttonId = 0;
                buttonType = 0;
            }
        }

        public InlineButtonData(int btnId, EButtonType btnType)
        {
            buttonId = btnId;
            buttonType = btnType;
        }

        public override string ToString()
        {
            string res = "";

            res += $"ButtonId:{buttonId}\r\n";
            res += $"ButtonType:{Convert.ToInt32(buttonType)}\r\n";
            return res;
        }

    }
    public enum EButtonType
    {
        Menu,
        Service,
        Status,
        Task,
        Person,
        List,
    }
}
