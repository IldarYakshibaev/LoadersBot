using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadersBot.Models
{
    public static class Crypto
    {
        public static string Decrypt(byte[] data, string pass)
        {
            byte[] bOut = new byte[data.Length];
            int pl = pass.Length;
            byte[] bPass = Encoding.ASCII.GetBytes(pass);
            int[] biPass = new int[bPass.Length];
            bPass.CopyTo(biPass, 0);
            for(var i = 0; i < data.Length; i++)
            {
                bOut[i] = (byte)((256 + data[i] - biPass[i % pl]) % 256);
            }
            return Encoding.ASCII.GetString(bOut);
        }
        public static byte[] Encrypt(string text, string pass)
        {
            byte[] byteIn = Encoding.ASCII.GetBytes(text);
            byte[] byteOut = new byte[byteIn.Length];
            int pl = pass.Length;
            byte[] bPass = Encoding.ASCII.GetBytes(pass);
            int[] biPass = new int[bPass.Length];
            bPass.CopyTo(biPass, 0);
            for(var i = 0; i < byteIn.Length; i++)
            {
                byteOut[i] = (byte)((byteIn[i] + biPass[i % pl]) % 256);
            }
            return byteOut;
        }
    }
}
