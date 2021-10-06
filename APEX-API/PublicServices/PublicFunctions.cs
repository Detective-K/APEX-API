using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APEX_API.PublicServices
{
    public class PublicFunctions
    {
        #region OrderService        
        public int get_NO(double Dia)
        {
            int Get_NO_temp = 0;
            if (Dia <= 8.05)
            {
                Get_NO_temp = 8;
            }

            if (Dia > 8.05 && Dia <= 11.05)
            {
                Get_NO_temp = 11;
            }

            if (Dia > 11.05 && Dia <= 14.05)
            {
                Get_NO_temp = 14;
            }

            if (Dia > 14.05 && Dia <= 19.05)
            {
                Get_NO_temp = 19;
            }

            if (Dia > 19.05 && Dia <= 24.05)
            {
                Get_NO_temp = 24;
            }

            if (Dia > 24.05 && Dia <= 28.05)
            {
                Get_NO_temp = 28;
            }

            if (Dia > 28.05 && Dia <= 32.05)
            {
                Get_NO_temp = 32;
            }

            if (Dia > 32.05 && Dia <= 35.05)
            {
                Get_NO_temp = 35;
            }

            if (Dia > 35.05 && Dia <= 38.05)
            {
                Get_NO_temp = 38;
            }

            if (Dia > 38.05 && Dia <= 42.05)
            {
                Get_NO_temp = 42;
            }

            if (Dia > 42.05 && Dia <= 48.05)
            {
                Get_NO_temp = 48;
            }

            if (Dia > 48.05 && Dia <= 55.05)
            {
                Get_NO_temp = 55;
            }

            if (Dia > 55.05 && Dia <= 60.05)
            {
                Get_NO_temp = 60;
            }

            if (Dia > 60.05 && Dia <= 65.05)
            {
                Get_NO_temp = 65;
            }

            if (Dia > 65.05 && Dia <= 75.05)
            {
                Get_NO_temp = 75;
            }

            if (Dia > 75.05 && Dia <= 80.05)
            {
                Get_NO_temp = 80;
            }

            if (Dia > 80.05 && Dia <= 85.05)
            {
                Get_NO_temp = 85;
            }

            if (Dia > 85.05)
            {
                Get_NO_temp = 85;
            }

            return Get_NO_temp;
        }
        #endregion
    }
}
