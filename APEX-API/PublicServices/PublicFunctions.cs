using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APEX_API.PublicServices
{
    public class PublicFunctions
    {
        /// <summary>判斷是否為帶小數點的字串</summary>
        /// <param name="l_str">待判斷字串</param>
        public  Boolean FCheck_Single(string l_str)
        {
            Boolean l_result;
            l_result = false;
            if (l_str.Length != 0)
            {
                l_result = true;
                string[] l_List = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "." };
                for (int i = 0; i <= l_str.Length - 1; i++)
                {
                    if (Array.IndexOf(l_List, l_str.Substring(i, 1)) == -1)
                    {
                        l_result = false;
                        break;
                    }
                }
            }
            else
            {
                l_result = false;
            }

            return l_result;

        }
    }
}
