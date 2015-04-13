using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecordWebService.Models
{
    public class StaticMethods
    {   
        #region Static Methods

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }

        public static int[] StringToKey(string key)
        {
            string[] tokens = key.Split(',');

            return Array.ConvertAll<string, int>(tokens, int.Parse);
        }

        public static string KeyToString(int[] key)
        {
            string ret = key[0].ToString();

            for (int i = 1; i < key.Length; i++)
            {
                ret += "," + key[i].ToString();
            }

            return ret;
        }

        #endregion
    }
}