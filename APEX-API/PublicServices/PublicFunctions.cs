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

        // <summary>取大訂單折扣，GetDiscount(i=幣別,j=金額,k=數量,ima01=料號)     
        public double GetDiscount(string i, double j, Int16 k, string ima01)
        {
            double temp1 = 0;

            if (i == "USD")
            {
                if (j <= 300 && j > 0)
                {
                    if (k >= 1 && k <= 20)
                    {
                        temp1 = 0;
                    }

                    if (k >= 21 && k <= 50)
                    {
                        temp1 = 0.05;
                    }

                    if (k >= 51 && k <= 100)
                    {
                        temp1 = 0.08;
                    }

                    if (k >= 101 && k <= 300)
                    {
                        temp1 = 0.1;
                    }

                    if (k >= 301)
                    {
                        temp1 = 0.12;
                    }
                }

                if (j >= 301 && j <= 400)
                {
                    if (k >= 1 && k <= 10)
                    {
                        temp1 = 0;
                    }

                    if (k >= 11 && k <= 50)
                    {
                        temp1 = 0.05;
                    }

                    if (k >= 51 && k <= 100)
                    {
                        temp1 = 0.08;
                    }

                    if (k >= 101 && k <= 300)
                    {
                        temp1 = 0.1;
                    }

                    if (k >= 301)
                    {
                        temp1 = 0.12;
                    }
                }

                if (j >= 401 && j <= 550)
                {
                    if (k >= 1 && k <= 10)
                    {
                        temp1 = 0;
                    }

                    if (k >= 11 && k <= 30)
                    {
                        temp1 = 0.05;
                    }

                    if (k >= 31 && k <= 100)
                    {
                        temp1 = 0.08;
                    }

                    if (k >= 101 && k <= 200)
                    {
                        temp1 = 0.1;
                    }

                    if (k >= 201)
                    {
                        temp1 = 0.12;
                    }
                }

                if (j >= 551 && j <= 1100)
                {
                    if (k >= 1 && k <= 10)
                    {
                        temp1 = 0;
                    }

                    if (k >= 11 && k <= 30)
                    {
                        temp1 = 0.05;
                    }

                    if (k >= 31 && k <= 50)
                    {
                        temp1 = 0.08;
                    }

                    if (k >= 51 && k <= 100)
                    {
                        temp1 = 0.1;
                    }

                    if (k >= 101)
                    {
                        temp1 = 0.12;
                    }
                }

                if (j >= 1101 && j <= 2200)
                {
                    if (k >= 1 && k <= 7)
                    {
                        temp1 = 0;
                    }

                    if (k >= 8 && k <= 15)
                    {
                        temp1 = 0.05;
                    }

                    if (k >= 16 && k <= 30)
                    {
                        temp1 = 0.08;
                    }

                    if (k >= 31 && k <= 50)
                    {
                        temp1 = 0.1;
                    }

                    if (k >= 51)
                    {
                        temp1 = 0.12;
                    }
                }

                if (j >= 2201 && j <= 4500)
                {
                    if (k >= 1 && k <= 5)
                    {
                        temp1 = 0;
                    }

                    if (k >= 6 && k <= 10)
                    {
                        temp1 = 0.05;
                    }

                    if (k >= 11 && k <= 15)
                    {
                        temp1 = 0.08;
                    }

                    if (k >= 16 && k <= 20)
                    {
                        temp1 = 0.1;
                    }

                    if (k >= 21)
                    {
                        temp1 = 0.12;
                    }
                }

                if (j > 4500)
                {
                    if (k >= 1 && k <= 3)
                    {
                        temp1 = 0;
                    }

                    if (k >= 4 && k <= 6)
                    {
                        temp1 = 0.05;
                    }

                    if (k >= 7 && k <= 10)
                    {
                        temp1 = 0.08;
                    }

                    if (k >= 11 && k <= 15)
                    {
                        temp1 = 0.1;
                    }

                    if (k >= 16)
                    {
                        temp1 = 0.12;
                    }
                }

            }

            if (i == "EUR")
            {
                if (j > 0 && j <= 250)
                {
                    if (k >= 1 && k <= 20)
                    {
                        temp1 = 0;
                    }

                    if (k >= 21 && k <= 50)
                    {
                        temp1 = 0.05;
                    }

                    if (k >= 51 && k <= 100)
                    {
                        temp1 = 0.08;
                    }

                    if (k >= 101 && k <= 300)
                    {
                        temp1 = 0.1;
                    }

                    if (k >= 301)
                    {
                        temp1 = 0.12;
                    }
                }

                if (j >= 251 && j <= 350)
                {
                    if (k >= 1 && k <= 10)
                    {
                        temp1 = 0;
                    }

                    if (k >= 11 && k <= 50)
                    {
                        temp1 = 0.05;
                    }

                    if (k >= 51 && k <= 100)
                    {
                        temp1 = 0.08;
                    }

                    if (k >= 101 && k <= 300)
                    {
                        temp1 = 0.1;
                    }

                    if (k >= 301)
                    {
                        temp1 = 0.12;
                    }
                }

                if (j >= 351 && j <= 460)
                {
                    if (k >= 1 && k <= 10)
                    {
                        temp1 = 0;
                    }

                    if (k >= 11 && k <= 30)
                    {
                        temp1 = 0.05;
                    }

                    if (k >= 31 && k <= 100)
                    {
                        temp1 = 0.08;
                    }

                    if (k >= 101 && k <= 200)
                    {
                        temp1 = 0.1;
                    }

                    if (k >= 201)
                    {
                        temp1 = 0.12;
                    }
                }

                if (j >= 461 && j <= 920)
                {
                    if (k >= 1 && k <= 10)
                    {
                        temp1 = 0;
                    }

                    if (k >= 11 && k <= 30)
                    {
                        temp1 = 0.05;
                    }

                    if (k >= 31 && k <= 50)
                    {
                        temp1 = 0.08;
                    }

                    if (k >= 51 && k <= 100)
                    {
                        temp1 = 0.1;
                    }

                    if (k >= 101)
                    {
                        temp1 = 0.12;
                    }
                }

                if (j >= 921 && j <= 1920)
                {
                    if (k >= 1 && k <= 7)
                    {
                        temp1 = 0;
                    }

                    if (k >= 8 && k <= 15)
                    {
                        temp1 = 0.05;
                    }

                    if (k >= 16 && k <= 30)
                    {
                        temp1 = 0.08;
                    }

                    if (k >= 31 && k <= 50)
                    {
                        temp1 = 0.1;
                    }

                    if (k >= 51)
                    {
                        temp1 = 0.12;
                    }
                }

                if (j >= 1921 && j <= 3900)
                {
                    if (k >= 1 && k <= 5)
                    {
                        temp1 = 0;
                    }

                    if (k >= 6 && k <= 10)
                    {
                        temp1 = 0.05;
                    }

                    if (k >= 11 && k <= 15)
                    {
                        temp1 = 0.08;
                    }

                    if (k >= 16 && k <= 20)
                    {
                        temp1 = 0.1;
                    }

                    if (k >= 21)
                    {
                        temp1 = 0.12;
                    }
                }

                if (j > 3900)
                {
                    if (k >= 1 && k <= 3)
                    {
                        temp1 = 0;
                    }

                    if (k >= 4 && k <= 6)
                    {
                        temp1 = 0.05;
                    }

                    if (k >= 7 && k <= 10)
                    {
                        temp1 = 0.08;
                    }

                    if (k >= 11 && k <= 15)
                    {
                        temp1 = 0.1;
                    }

                    if (k >= 16)
                    {
                        temp1 = 0.12;
                    }
                }
            }

            if (i == "TWD")
            {
                if (j <= 9800 && j > 0)
                {
                    if (k >= 1 && k <= 20)
                    {
                        temp1 = 0;
                    }

                    if (k >= 21 && k <= 50)
                    {
                        temp1 = 0.05;
                    }

                    if (k >= 51 && k <= 100)
                    {
                        temp1 = 0.08;
                    }

                    if (k >= 101 && k <= 300)
                    {
                        temp1 = 0.1;
                    }

                    if (k >= 301)
                    {
                        temp1 = 0.12;
                    }
                }

                if (j >= 9801 && j <= 13000)
                {
                    if (k >= 1 && k <= 10)
                    {
                        temp1 = 0;
                    }

                    if (k >= 11 && k <= 50)
                    {
                        temp1 = 0.05;
                    }

                    if (k >= 51 && k <= 100)
                    {
                        temp1 = 0.08;
                    }

                    if (k >= 101 && k <= 300)
                    {
                        temp1 = 0.1;
                    }

                    if (k >= 301)
                    {
                        temp1 = 0.12;
                    }
                }

                if (j >= 13001 && j <= 17800)
                {
                    if (k >= 1 && k <= 10)
                    {
                        temp1 = 0;
                    }

                    if (k >= 11 && k <= 30)
                    {
                        temp1 = 0.05;
                    }

                    if (k >= 31 && k <= 100)
                    {
                        temp1 = 0.08;
                    }

                    if (k >= 101 && k <= 200)
                    {
                        temp1 = 0.1;
                    }

                    if (k >= 201)
                    {
                        temp1 = 0.12;
                    }
                }

                if (j >= 17801 && j <= 36000)
                {
                    if (k >= 1 && k <= 10)
                    {
                        temp1 = 0;
                    }

                    if (k >= 11 && k <= 30)
                    {
                        temp1 = 0.05;
                    }

                    if (k >= 31 && k <= 50)
                    {
                        temp1 = 0.08;
                    }

                    if (k >= 51 && k <= 100)
                    {
                        temp1 = 0.1;
                    }

                    if (k >= 101)
                    {
                        temp1 = 0.12;
                    }
                }

                if (j >= 36001 && j <= 66200)
                {
                    if (k >= 1 && k <= 7)
                    {
                        temp1 = 0;
                    }

                    if (k >= 8 && k <= 15)
                    {
                        temp1 = 0.05;
                    }

                    if (k >= 16 && k <= 30)
                    {
                        temp1 = 0.08;
                    }

                    if (k >= 31 && k <= 50)
                    {
                        temp1 = 0.1;
                    }

                    if (k >= 51)
                    {
                        temp1 = 0.12;
                    }
                }

                if (j >= 66201 && j <= 135000)
                {
                    if (k >= 1 && k <= 5)
                    {
                        temp1 = 0;
                    }

                    if (k >= 6 && k <= 10)
                    {
                        temp1 = 0.05;
                    }

                    if (k >= 11 && k <= 15)
                    {
                        temp1 = 0.08;
                    }

                    if (k >= 16 && k <= 20)
                    {
                        temp1 = 0.1;
                    }

                    if (k >= 21)
                    {
                        temp1 = 0.12;
                    }
                }

                if (j > 135000)
                {
                    if (k >= 1 && k <= 3)
                    {
                        temp1 = 0;
                    }

                    if (k >= 4 && k <= 6)
                    {
                        temp1 = 0.05;
                    }

                    if (k >= 7 && k <= 10)
                    {
                        temp1 = 0.08;
                    }

                    if (k >= 11 && k <= 15)
                    {
                        temp1 = 0.1;
                    }

                    if (k >= 16)
                    {
                        temp1 = 0.12;
                    }
                }
            }

            if (i == "CNY")
            {
                if (j <= 1950 && j > 0)
                {
                    if (k >= 1 && k <= 20)
                    {
                        temp1 = 0;
                    }

                    if (k >= 21 && k <= 50)
                    {
                        temp1 = 0.05;
                    }

                    if (k >= 51 && k <= 100)
                    {
                        temp1 = 0.08;
                    }

                    if (k >= 101 && k <= 300)
                    {
                        temp1 = 0.1;
                    }

                    if (k >= 301)
                    {
                        temp1 = 0.12;
                    }
                }


                if (j >= 1951 && j <= 2700)
                {
                    if (k >= 1 && k <= 10)
                    {
                        temp1 = 0;
                    }

                    if (k >= 11 && k <= 50)
                    {
                        temp1 = 0.05;
                    }

                    if (k >= 51 && k <= 100)
                    {
                        temp1 = 0.08;
                    }

                    if (k >= 101 && k <= 300)
                    {
                        temp1 = 0.1;
                    }

                    if (k >= 301)
                    {
                        temp1 = 0.12;
                    }
                }


                if (j >= 2701 && j <= 3700)
                {
                    if (k >= 1 && k <= 10)
                    {
                        temp1 = 0;
                    }

                    if (k >= 11 && k <= 30)
                    {
                        temp1 = 0.05;
                    }

                    if (k >= 31 && k <= 100)
                    {
                        temp1 = 0.08;
                    }

                    if (k >= 101 && k <= 200)
                    {
                        temp1 = 0.1;
                    }

                    if (k >= 201)
                    {
                        temp1 = 0.12;
                    }
                }

                if (j >= 3701 && j <= 7500)
                {
                    if (k >= 1 && k <= 10)
                    {
                        temp1 = 0;
                    }

                    if (k >= 11 && k <= 30)
                    {
                        temp1 = 0.05;
                    }

                    if (k >= 31 && k <= 50)
                    {
                        temp1 = 0.08;
                    }

                    if (k >= 51 && k <= 100)
                    {
                        temp1 = 0.1;
                    }

                    if (k >= 101)
                    {
                        temp1 = 0.12;
                    }
                }


                if (j >= 7501 && j <= 14800)
                {
                    if (k >= 1 && k <= 7)
                    {
                        temp1 = 0;
                    }

                    if (k >= 8 && k <= 15)
                    {
                        temp1 = 0.05;
                    }

                    if (k >= 16 && k <= 30)
                    {
                        temp1 = 0.08;
                    }

                    if (k >= 31 && k <= 50)
                    {
                        temp1 = 0.1;
                    }

                    if (k >= 51)
                    {
                        temp1 = 0.12;
                    }
                }

                if (j >= 14801 && j <= 30000)
                {
                    if (k >= 1 && k <= 5)
                    {
                        temp1 = 0;
                    }

                    if (k >= 6 && k <= 10)
                    {
                        temp1 = 0.05;
                    }

                    if (k >= 11 && k <= 15)
                    {
                        temp1 = 0.08;
                    }

                    if (k >= 16 && k <= 20)
                    {
                        temp1 = 0.1;
                    }

                    if (k >= 21)
                    {
                        temp1 = 0.12;
                    }
                }

                if (j > 30000)
                {
                    if (k >= 1 && k <= 3)
                    {
                        temp1 = 0;
                    }

                    if (k >= 4 && k <= 6)
                    {
                        temp1 = 0.05;
                    }

                    if (k >= 7 && k <= 10)
                    {
                        temp1 = 0.08;
                    }

                    if (k >= 11 && k <= 15)
                    {
                        temp1 = 0.1;
                    }

                    if (k >= 16)
                    {
                        temp1 = 0.12;
                    }
                }
            }

            //P2代的價格已經包含大訂單折扣,所以不再折扣
            //20170510 AH(RA9),AHK(RB4),AP(J9),APK(J5) 不給大訂單折扣
            //20171220 AH AHK & AP APC APCK 今天起 暫時與A系列有大訂單折扣
            //20180710 AFH(R53),AFHK(R54,RF1) 不給大訂單折扣
            //string[] P2_List = new string[] { "69", "D4", "66", "D2", "67", "D3", "65", "D1", "86", "D5", "40", "B3", "E1", "E2", "D9", "E9", "21", "22", "23", "24", "64", "A9", "B4", "J9", "J5" };
            string[] P2_List = new string[] { "69", "D4", "66", "D2", "67", "D3", "65", "D1", "86", "D5", "E1", "E2", "D9", "E9", "21", "22", "23", "24", "64" };
            if (Array.IndexOf(P2_List, ima01.Substring(1, 2)) != -1 && (ima01.Substring(0, 1) == "A" || ima01.Substring(0, 1) == "C"))
            {
                temp1 = 0;
            }

            return temp1;
        }
        public double GetChangeOilPrice(string PartNo, double DiscountPrice, string LubData, string CustId, string Currency, string Spec)
        {
            //LubData 需確認提供資料格式
            string CheckPartNo = "65,66,67,69,86,D1,D2,D3,D4,D5,E1,D9,E2,E9";
            Double tmp_chang_lub_price;
            Double FinalCharge = 0;


            if (CustId != "BAJ003")
            {
                if (CheckPartNo.Contains(PartNo.Substring(1, 2)))
                {
                    if (LubData != "Grease")
                    {
                        tmp_chang_lub_price = System.Math.Round((DiscountPrice * 0.15), 0, MidpointRounding.AwayFromZero);
                        if (Currency == "EUR")
                        {
                            if (tmp_chang_lub_price < 18)
                            {
                                tmp_chang_lub_price = 18;
                            }
                        }
                        else if (Currency == "USD")
                        {
                            if (tmp_chang_lub_price < 20)
                            {
                                tmp_chang_lub_price = 20;
                            }
                        }
                        else if (Currency == "TWD")
                        {
                            if (tmp_chang_lub_price < 600)
                            {
                                tmp_chang_lub_price = 600;
                            }
                        }
                        else if (Currency == "CNY")
                        {
                            if (tmp_chang_lub_price < 130)
                            {
                                tmp_chang_lub_price = 130;
                            }
                        }

                        FinalCharge = System.Math.Round((DiscountPrice + tmp_chang_lub_price), 0, MidpointRounding.AwayFromZero);
                    }
                }
                else if (Spec.Substring(0, 2) == "AT")
                {
                    if (LubData == "Low Temp. Grease" || LubData == "Food Grade Grease")
                    {
                        //加收10%並且4捨五入
                        FinalCharge = System.Math.Round((DiscountPrice * 1.1), 0, MidpointRounding.AwayFromZero);
                    }
                }
                else if (PartNo.Substring(1, 2) == "G4" || PartNo.Substring(1, 2) == "G5")
                {
                    if (LubData != "Food Grade Grease")
                    {
                        //加收10%並且4捨五入
                        //tmp_price2 = System.Math.Round((tmp_price2 * 1.1), 0, MidpointRounding.AwayFromZero);
                    }
                }
                else if (PartNo.Substring(1, 2) == "42")
                {
                    if (LubData != "Grease")
                    {
                        //加收10%並且4捨五入
                        FinalCharge = System.Math.Round((DiscountPrice * 1.1), 0, MidpointRounding.AwayFromZero);
                    }
                }
                else
                {
                    if (LubData == "Grease")
                    {
                        FinalCharge = System.Math.Round((DiscountPrice * 1.05), 0, MidpointRounding.AwayFromZero);
                    }
                    else if (LubData == "Low Temp. Grease" || LubData == "Food Grade Grease")
                    {
                        //加收10%並且4捨五入
                        FinalCharge = System.Math.Round((DiscountPrice * 1.1), 0, MidpointRounding.AwayFromZero);
                    }
                }
            }
            else
            {
                if (LubData == "")
                {
                    //加收10%並且4捨五入
                    FinalCharge = System.Math.Round((DiscountPrice * 1.1), 0, MidpointRounding.AwayFromZero);
                }
            }

            return FinalCharge;
        }
        public string Get_C12(string p_LZ1)
        {
            string l_return = "0=0";
            if (p_LZ1 == "M3")
            {
                l_return = " and tc_mma29 = 3.4 ";
            }
            if (p_LZ1 == "M4")
            {
                l_return = " and tc_mma29 = 4.5 ";
            }
            if (p_LZ1 == "M5")
            {
                l_return = " and tc_mma29 = 5.5 ";
            }
            if (p_LZ1 == "M6")
            {
                l_return = " and tc_mma29 = 6.6 ";
            }
            if (p_LZ1 == "M8")
            {
                l_return = " and tc_mma29 = 9 ";
            }
            if (p_LZ1 == "M10")
            {
                l_return = " and tc_mma29 = 11 ";
            }
            if (p_LZ1 == "M12")
            {
                l_return = " and tc_mma29 = 14 ";
            }
            if (p_LZ1 == "M14")
            {
                l_return = " and tc_mma29 = 16 ";
            }
            if (p_LZ1 == "M16")
            {
                l_return = " and tc_mma29 = 18 ";
            }
            if (p_LZ1 == "M18")
            {
                l_return = " and tc_mma29 = 20 ";
            }
            if (p_LZ1 == "M20")
            {
                l_return = " and tc_mma29 = 22 ";
            }
            if (p_LZ1 == "M22")
            {
                l_return = " and tc_mma29 = 24 ";
            }
            if (p_LZ1 == "M24")
            {
                l_return = " and tc_mma29 = 26 ";
            }
            if (p_LZ1 == "NO.4-40 UNC")
            {
                l_return = " and tc_mma29 = 3.4 ";
            }
            if (p_LZ1 == "NO.5-40 UNC")
            {
                l_return = " and tc_mma29 = 3.4 ";
            }
            if (p_LZ1 == "NO.6-32 UNC")
            {
                l_return = " and tc_mma29 = 4.5 ";
            }
            if (p_LZ1 == "NO.8-32 UNC")
            {
                l_return = " and tc_mma29 = 4.5 ";
            }
            if (p_LZ1 == "10-24 UNC")
            {
                l_return = " and tc_mma29 = 5.5 ";
            }
            if (p_LZ1 == "NO.12-24 UNC")
            {
                l_return = " and tc_mma29 = 6.6 ";
            }
            if (p_LZ1 == "1/4-20 UNC")
            {
                l_return = " and tc_mma29 = 6.6 ";
            }
            if (p_LZ1 == "5/16-28 UNC")
            {
                l_return = " and tc_mma29 = 9 ";
            }
            if (p_LZ1 == "3/8-16 UNC")
            {
                l_return = " and tc_mma29 = 11 ";
            }
            if (p_LZ1 == "7/16-14 UNC")
            {
                l_return = " and tc_mma29 = 14 ";
            }
            if (p_LZ1 == "1/2-13 UNC")
            {
                l_return = " and tc_mma29 = 14 ";
            }
            if (p_LZ1 == "9/16-12 UNC")
            {
                l_return = " and tc_mma29 = 16 ";
            }
            if (p_LZ1 == "5/8-11 UNC")
            {
                l_return = " and tc_mma29 = 18 ";
            }
            if (p_LZ1 == "3/4-10 UNC")
            {
                l_return = " and tc_mma29 = 22 ";
            }
            if (p_LZ1 == "7/8-9 UNC")
            {
                l_return = " and tc_mma29 = 24 ";
            }

            return l_return;
        }
        public string Get_M(double Dia)
        {
            string l_return = "0";
            if (Dia < 3.2)
            {
                l_return = "3";
            }
            if (Dia >= 3.2 && Dia < 4.3)
            {
                l_return = "3";
            }
            if (Dia >= 4.3 && Dia < 5.3)
            {
                l_return = "4";
            }
            if (Dia >= 5.3 && Dia < 6.3)
            {
                l_return = "5";
            }
            if (Dia >= 6.3 && Dia < 8.3)
            {
                l_return = "6";
            }
            if (Dia >= 8.3 && Dia < 10.5)
            {
                l_return = "8";
            }
            if (Dia >= 10.5 && Dia < 13)
            {
                l_return = "10";
            }
            if (Dia >= 13 && Dia < 14.7)
            {
                l_return = "12";
            }
            if (Dia >= 14.7 && Dia < 17)
            {
                l_return = "14";
            }
            if (Dia >= 17 && Dia <= 19)
            {
                l_return = "16";
            }
            if (Dia > 19 && Dia < 21)
            {
                l_return = "18";
            }
            if (Dia >= 21 && Dia < 23)
            {
                l_return = "20";
            }
            if (Dia >= 23 && Dia < 25)
            {
                l_return = "22";
            }
            if (Dia >= 25 && Dia < 27)
            {
                l_return = "24";
            }
            if (Dia >= 27)
            {
                l_return = "0";
            }

            return l_return;
        }
        public string Get_C12_NEW(string p_LZ1)
        {
            string l_return = "0";
            if (p_LZ1 == "M3")
            {
                l_return = "3.4";
            }
            if (p_LZ1 == "M4")
            {
                l_return = "4.5";
            }
            if (p_LZ1 == "M5")
            {
                l_return = "5.5";
            }
            if (p_LZ1 == "M6")
            {
                l_return = "6.6";
            }
            if (p_LZ1 == "M8")
            {
                l_return = "9";
            }
            if (p_LZ1 == "M10")
            {
                l_return = "11";
            }
            if (p_LZ1 == "M12")
            {
                l_return = "14";
            }
            if (p_LZ1 == "M14")
            {
                l_return = "16";
            }
            if (p_LZ1 == "M16")
            {
                l_return = "18";
            }
            if (p_LZ1 == "M18")
            {
                l_return = "20";
            }
            if (p_LZ1 == "M20")
            {
                l_return = "22";
            }
            if (p_LZ1 == "M22")
            {
                l_return = "24";
            }
            if (p_LZ1 == "M24")
            {
                l_return = "26";
            }
            if (p_LZ1 == "NO.4-40 UNC")
            {
                l_return = "3.4";
            }
            if (p_LZ1 == "NO.5-40 UNC")
            {
                l_return = "3.4";
            }
            if (p_LZ1 == "NO.6-32 UNC")
            {
                l_return = "4.5";
            }
            if (p_LZ1 == "NO.8-32 UNC")
            {
                l_return = "4.5";
            }
            if (p_LZ1 == "10-24 UNC")
            {
                l_return = "5.5";
            }
            if (p_LZ1 == "NO.12-24 UNC")
            {
                l_return = "6.6";
            }
            if (p_LZ1 == "1/4-20 UNC")
            {
                l_return = "6.6";
            }
            if (p_LZ1 == "5/16-28 UNC")
            {
                l_return = "9";
            }
            if (p_LZ1 == "3/8-16 UNC")
            {
                l_return = "11";
            }
            if (p_LZ1 == "7/16-14 UNC")
            {
                l_return = "14";
            }
            if (p_LZ1 == "1/2-13 UNC")
            {
                l_return = "14";
            }
            if (p_LZ1 == "9/16-12 UNC")
            {
                l_return = "16";
            }
            if (p_LZ1 == "5/8-11 UNC")
            {
                l_return = "18";
            }
            if (p_LZ1 == "3/4-10 UNC")
            {
                l_return = "22 ";
            }
            if (p_LZ1 == "7/8-9 UNC")
            {
                l_return = "24";
            }

            return l_return;
        }
        public double Ceiling(double value, double significance)
        {
            if ((value % significance) != 0)
            {
                return ((int)(value / significance) * significance) + significance;
            }

            return Convert.ToDouble(value);
        }
        //找出螺絲的總牙長    
        public int getScrewT(string Dia)
        {
            int l_return = 0;
            switch (Dia)
            {
                case "3":
                    l_return = 6;
                    break;

                case "4":
                    l_return = 9;
                    break;

                case "5":
                    l_return = 10;
                    break;

                case "6":
                    l_return = 12;
                    break;

                case "8":
                    l_return = 15;
                    break;

                case "10":
                    l_return = 18;
                    break;

                case "12":
                    l_return = 21;    //20181107 22->21
                    break;

                case "14":
                    l_return = 24;   //20181107 25->24
                    break;

                case "16":
                    l_return = 28;
                    break;

                case "18":
                    l_return = 33;
                    break;

                case "20":
                    l_return = 36;
                    break;

                default:
                    l_return = 0;
                    break;

            }

            return l_return;
        }
        public double  Get_Screw_Max_Dia(double p_C12)
        {
            double l_return = 0;
            if (p_C12 == 3.4)
            {
                l_return = 6.4;
            }
            if (p_C12 == 4.5)
            {
                l_return = 8.1;
            }
            if (p_C12 == 5.5)
            {
                l_return = 9.2;
            }
            if (p_C12 == 6.6)
            {
                l_return = 11.5;
            }
            if (p_C12 == 9)
            {
                l_return = 15;
            }
            if (p_C12 == 11)
            {
                l_return = 19.6;
            }
            if (p_C12 == 14)
            {
                l_return = 21.9;
            }
            if (p_C12 == 16)
            {
                l_return = 25.4;
            }
            if (p_C12 == 18)
            {
                l_return = 27.7;
            }

            return l_return;
        }

        protected void formula(string Adptable, string LBck , int rblAdpCount)
        {
            //int l_adapter_list_count = 3; //可供選擇的連接板數量

            //int g_count = 0;

            //string[] l_adpter_type = { "P", "O" };

            //string tmp_newPartNo = "None";

            //foreach (string l_string in l_adpter_type)
            //{
            //    if (tmp_newPartNo != "None")
            //    {
            //        continue;
            //    }

            //    double tmp = 40;

            //    double LAtmp = 0.1;

            //    while (tmp <= 40 && rblAdpCount < l_adapter_list_count)
            //    {
            //        if (LBck == "LBstd")
            //        {
            //            g_sql = "SELECT * FROM " + Adptable + " Where upper(tc_mma23) = 'YES' and tc_mma08 >=" + (Convert.ToDouble(LR) - 1) + " and tc_mma09 <= " + Convert.ToDouble(LR) + " and tc_mma10 = " + Convert.ToDouble(LB) + " and tc_mma11 >=" + Convert.ToDouble(LE) + " and tc_mma12 >=" + Convert.ToDouble(Convert.ToDouble(LT) + 0.5) + " and tc_mma04 >=" + (Convert.ToDouble(LA) - LAtmp) + " and tc_mma04 <= " + (Convert.ToDouble(LA) + LAtmp) + " and tc_mma05=" + Convert.ToSingle(ScrewDia) + " and tc_mma16=" + Convert.ToDouble(AWidth1) + " and tc_mma13 <= " + Convert.ToDouble(Convert.ToDouble(LC) + tmp) + " and tc_mma13 >=" + Convert.ToDouble(Convert.ToDouble(LC) - tmp) + " and tc_mma01 like '" + l_string + "%' order by abs(tc_mma04-" + LA + "),abs(tc_mma13-" + LC + "),substr(tc_mma01,0,1) desc,abs(tc_mma13-" + LC + "),tc_mma08,tc_mma11,tc_mma10,tc_mma24";
            //        }
            //        else
            //        {
            //            g_sql = "SELECT * FROM " + Adptable + " Where upper(tc_mma23) = 'YES' and tc_mma08 >=" + (Convert.ToDouble(LR) - 1) + " and tc_mma09 <=" + Convert.ToDouble(LR) + " and tc_mma10<=" + Convert.ToDouble(Convert.ToDouble(LB) + 0.1) + " and tc_mma10 >=" + Convert.ToDouble(LB) + " and tc_mma11>=" + Convert.ToDouble(LE) + " and tc_mma12 >=" + Convert.ToDouble(Convert.ToDouble(LT) + 0.5) + " and tc_mma04 >=" + (Convert.ToDouble(LA) - LAtmp) + " and tc_mma04 <= " + (Convert.ToDouble(LA) + LAtmp) + " and tc_mma05=" + Convert.ToDouble(ScrewDia) + " and tc_mma16=" + Convert.ToDouble(AWidth1) + " and tc_mma13<=" + Convert.ToDouble(Convert.ToDouble(LC) + tmp) + " and tc_mma13>=" + Convert.ToDouble(Convert.ToDouble(LC) - tmp) + " and tc_mma01 like '" + l_string + "%' order by abs(tc_mma04-" + LA + "),abs(tc_mma13-" + LC + "),substr(tc_mma01,0,1) desc,abs(tc_mma13-" + LC + "),tc_mma08,tc_mma11,tc_mma10,tc_mma24";
            //        }

            //        DataSet myDataReader_3 = class_nana_ds1.ORACLE_DS(g_sql);
            //        g_count = g_count + 1;

            //        if (myDataReader_3.Tables[0].Rows.Count > 0)
            //        {
            //            for (int i = 0; i < myDataReader_3.Tables[0].Rows.Count; i++)
            //            {
            //                if (rblAdpCount < l_adapter_list_count)
            //                {
            //                    if (tmp_newPartNo == null || tmp_newPartNo == "None")
            //                    {
            //                        tmp_newPartNo = Convert.ToString(myDataReader_3.Tables[0].Rows[i]["tc_mma01"]);

            //                        //換一體式連接板
            //                        if (Convert.ToString(ViewState["g_Reducer_One_piece"]) == "Y")
            //                        {
            //                            if (Convert.ToString(ViewState["g_Reducer_One_piece_used"]) == "Y")
            //                            {
            //                                //先關閉等生管通知
            //                                Fun_replace_one_piece(Convert.ToString(ViewState["Reducer_No"]), tmp_newPartNo);
            //                            }
            //                        }
            //                    }
            //                    //if (Convert.ToString(ViewState["g_Reducer_One_piece_chenged"]) == "Y")//已經換成一體式,不要再找其他連接板
            //                    //{
            //                    //    rbl_adapter.Items.Add(new ListItem(Convert.ToString(rbl_adapter.Items.Count + 1) + ". " + Convert.ToString(myDataReader_3.Tables[0].Rows[i]["tc_mma03"]) + " / " + Convert.ToString(ViewState["Adaper_No"]) + "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Stock&nbsp;&nbsp;&nbsp;" + get_stock_msg(Convert.ToString(ViewState["Adaper_No"])), Convert.ToString(ViewState["Adaper_No"])));
            //                    //    tmp = 100; //離開while 迴圈
            //                    //    break;
            //                    //}
            //                    //else
            //                    //{
            //                    //    if (rbl_adapter.Items.Count == 0) //最適配連接板
            //                    //    {
            //                    //        rbl_adapter.Items.Add(new ListItem(Convert.ToString(rbl_adapter.Items.Count + 1) + ". " + Convert.ToString(myDataReader_3.Tables[0].Rows[i]["tc_mma03"]) + " / " + Convert.ToString(myDataReader_3.Tables[0].Rows[i]["tc_mma01"]) + "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Stock&nbsp;&nbsp;&nbsp;" + get_stock_msg(Convert.ToString(myDataReader_3.Tables[0].Rows[i]["tc_mma01"])), Convert.ToString(myDataReader_3.Tables[0].Rows[i]["tc_mma01"])));

            //                    //        if (get_stock_msg(Convert.ToString(myDataReader_3.Tables[0].Rows[i]["tc_mma01"])) != "Re-Stocking")
            //                    //        {
            //                    //            //break;
            //                    //        }
            //                    //    }
            //                    //    else
            //                    //    {
            //                    //        //if (get_stock_msg(Convert.ToString(myDataReader_3.Tables[0].Rows[i]["tc_mma01"])) != "Re-Stocking") //有庫存才列入選項
            //                    //        //{
            //                    //        rbl_adapter.Items.Add(new ListItem(Convert.ToString(rbl_adapter.Items.Count + 1) + ". " + Convert.ToString(myDataReader_3.Tables[0].Rows[i]["tc_mma03"]) + " / " + Convert.ToString(myDataReader_3.Tables[0].Rows[i]["tc_mma01"]) + "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Stock&nbsp;&nbsp;&nbsp;" + get_stock_msg(Convert.ToString(myDataReader_3.Tables[0].Rows[i]["tc_mma01"])), Convert.ToString(myDataReader_3.Tables[0].Rows[i]["tc_mma01"])));
            //                    //        //}
            //                    //    }
            //                    //}
            //                }
            //                else
            //                {
            //                    tmp = 100; //離開while 迴圈
            //                }
            //            }

            //            if (tmp == 0)
            //            {
            //                tmp = 2.5;
            //            }
            //            else
            //            {
            //                tmp = tmp * 2;
            //            }
            //        }
            //        else
            //        {
            //            if (LAtmp == 0)
            //            {
            //                if (tmp == 0)
            //                {
            //                    tmp = 2.5;
            //                }
            //                else
            //                {
            //                    tmp = tmp * 2;

            //                    if (tmp > 40)
            //                    {
            //                        LAtmp = 0.1;
            //                        tmp = 40;
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                if (tmp == 0)
            //                {
            //                    tmp = 2.5;
            //                }
            //                else
            //                {
            //                    tmp = tmp * 2;
            //                }
            //            }
            //        }
            //    }
            //}

            ////if (rblAdpCount > 0)
            ////{
            ////    rbl_adapter.SelectedIndex = 0;
            ////    tmp_newPartNo = rbl_adapter.SelectedValue;
            ////}
            //return tmp_newPartNo;
        }
        #endregion
    }
}
