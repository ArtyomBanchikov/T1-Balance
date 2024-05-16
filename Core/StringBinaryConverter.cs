using System;
using System.Text;

namespace T1Balance.Core
{
    public static class StringBinaryConverter
    {
        public static byte[] ToByteArray(this string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }

        public static string ToHexString(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        public static string InString(this byte[] bytes)
        {
            string str = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                str += bytes[i];
                if (i < bytes.Length - 1)
                    str += " ";
            }

            return str;
        }
        public static byte[] InByteArray(this string str)
        {
            string[] subs = str.Split(' ');
            byte[] bytes = new byte[subs.Length];

            for (int i = 0; i < subs.Length; i++)
            {
                bytes[i] = Convert.ToByte(subs[i]);
            }
            return bytes;
        }
    }
}
