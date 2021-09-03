using APEX_API.Models;
using APEX_API.TopprodModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace APEX_API.Services
{
    public class OrderService
    {
        private readonly web2Context _web2Context;
        private readonly DataContext _DataContext;

        public OrderService(web2Context context, DataContext oracontext)
        {
            _web2Context = context;
            _DataContext = oracontext;
        }

        public List<Sale> CheckSalesMember(string feStr)
        {
            var feObject = JsonSerializer.Deserialize<Sale>(feStr);
            var MemberInfo = _web2Context.Sales.Where(x => x.SalesId == feObject.SalesId && x.Pwd == feObject.Pwd && x.ClaimLevel != 0);
            return MemberInfo.ToList();
        }

        public List<Cust> CheckCustsMember(string feStr)
        {
            var feObject = JsonSerializer.Deserialize<Cust>(feStr);
            var MemberInfo = _web2Context.Custs.Where(x => x.CustId == feObject.CustId && x.Pwd == feObject.Pwd);
            return MemberInfo.ToList();
        }

        public List<Cust> GetCustInfo(string CustId)
        {
            var MemberInfo = _web2Context.Custs.Where(C => C.CustId.Contains(CustId));
            return MemberInfo.ToList();
        }

        public object OrderList(string feStr)
        {
            var feObject = JsonSerializer.Deserialize<Cust>(feStr);
            var feOrder = JsonSerializer.Deserialize<Order>(feStr);
            List<Datas> myLists = new List<Datas>();
            var joinOrder2 = _web2Context.Orders.Join(_web2Context.Custs,
                o => o.CustId,
                c => c.CustId,
                (o, c) => new
                {
                    o.OrderId,
                    o.CustId,
                    o.OrderDate,
                    o.DelivWay,
                    o.DelivAddr,
                    o.DelivTel,
                    o.Attn,
                    o.Ostatus,
                    o.Pono,
                    o.Memo,
                    o.Currency,
                    c.Name
                })
                .GroupJoin(_web2Context.OrderDetails,
                oc => oc.OrderId,
                od => od.OrderId,
                (oc, od) => new
                {
                    ocs = oc,
                    ods = od
                })
               .SelectMany(
                     ocd => ocd.ods.DefaultIfEmpty(),
                    (x, y) => new { ocs = x.ocs, ods = y });
            var joinOrder = _web2Context.Orders.Join(_web2Context.Custs,
            o => o.CustId,
            c => c.CustId,
            (o, c) => new
            {
                o.OrderId,
                o.CustId,
                o.OrderDate,
                o.DelivWay,
                o.DelivAddr,
                o.DelivTel,
                o.Attn,
                o.Ostatus,
                o.Pono,
                o.Memo,
                o.Currency,
                c.Name
            })
           ;

            if (!string.IsNullOrEmpty(feObject.CustId))
            {
                joinOrder2 = joinOrder2.Where(ocd => ocd.ocs.CustId == feObject.CustId);
                joinOrder = joinOrder.Where(oc => oc.CustId == feObject.CustId);
            }
            if (!string.IsNullOrEmpty(feOrder.Ostatus))
            {
                joinOrder2 = joinOrder2.Where(ocd => ocd.ocs.Ostatus == feOrder.Ostatus);
                joinOrder = joinOrder.Where(oc => oc.Ostatus == feOrder.Ostatus);
            }

            joinOrder2 = joinOrder2.OrderByDescending(ocd => ocd.ocs.OrderDate);

            joinOrder = joinOrder.OrderByDescending(oc => oc.OrderDate);

            // GroupJoin Lambda
            //   var tt = _web2Context.Orders.GroupJoin(
            //      _web2Context.OrderDetails,
            //      customer => customer.OrderId,
            //      detail => detail.OrderId,
            //      (x, y) => new { Customer = x, Details = y })
            //.SelectMany(
            //      x => x.Details.DefaultIfEmpty(),
            //      (x, y) => new { customer = x.Customer, Details = y });
            // GroupJoin Linq
            //   var result = from person in _web2Context.Orders
            //                join detail in _web2Context.OrderDetails on person.OrderId equals detail.OrderId into Details
            //                from m in Details.DefaultIfEmpty()
            //                select new
            //                {
            //                    id = person.OrderId,
            //                    firstname = person.OrderNo,
            //                    lastname = person.OrderDate,
            //                    m
            //                };



            myLists.Add(new Datas { Data = joinOrder.ToList(), Data2 = joinOrder2.ToList() });
            return myLists;
        }

        // <summary>取基礎單價,GetBasePrice(客戶編號,料號ID,幣別 ,數量) 
        public Double GetBasePrice(string MB001, string MB002, string MB004, int MB003)
        {

            String temp_1 = "0";

            Double temp_2 = 0;

            Double temp_5 = 0;

            //P2代 和P1
            string[] P2_List = new string[] { "69", "D4", "66", "D2", "67", "D3", "65", "D1", "86", "D5", "E1", "E2", "D9", "E9", "21", "22", "23", "24", "64" };

            //P1代 不給折扣
            string[] P1_List = new string[] { "21", "22", "23", "24", "64" };

            List<ObkFile> obkData = _DataContext.ObkFiles.Where(obk => obk.Obk02 == MB001 && obk.Obk01 == MB002 && obk.Obk05 == MB004 && obk.Obkacti == "Y").ToList();
            List<ObkFile> obkData2 = _DataContext.ObkFiles.Where(obk => obk.Obk02 == "AE0002" && obk.Obk01 == MB002 && obk.Obk05 == MB004 && obk.Obkacti == "Y").ToList();


            if (MB002.Substring(0, 1) == "A" || MB002.Substring(0, 1) == "C")  //減速機
            {
                if (MB002.Substring(0, 1) == "C")
                {
                    MB002 = "A" + MB002.Substring(1, 10);
                }
                if (Array.IndexOf(P2_List, MB002.Substring(1, 2)) == -1) //P2代的價格已經包含大訂單折扣 , 所以不另外加
                {
                    //先判斷有無客戶的特別價
                    string tmpObk08 = obkData.Count > 0 ? obkData.SingleOrDefault().Obk08.ToString() : obkData2.SingleOrDefault().Obk08.ToString();

                    temp_1 = tmpObk08;

                    temp_2 = Convert.ToDouble(temp_1);

                    temp_5 = System.Math.Round(temp_2, 0, MidpointRounding.AwayFromZero);

                }
                else
                {
                    temp_5 = 0;
                }
            }
            else
            {  //非減速機

                if (obkData2.Count() > 0)
                {
                    temp_1 = obkData2.SingleOrDefault().Obk08.ToString();

                    temp_2 = Convert.ToDouble(temp_1);

                    temp_5 = temp_2;
                }
            }

            return temp_5;
        }

        //取單價,針對客戶的每個產品 getMB008(客戶ID,商品ID,幣別,數量)
        public Double GetSellingPrice(string MB001, string MB002, string MB004, int MB003)
        {
            String temp_1 = "0";

            Double temp_2 = 0;

            String temp_3 = "0";

            Double temp_4 = 0;

            Double temp_5 = 0;

            Boolean tmp_IsAgent = false;

            //先確定是代理商還是散客
            var Occ_FileData = _DataContext.OccFiles.Where(oc => oc.Occ01 == MB001);

            temp_3 = GetDiscountRate(MB001);
            if (Occ_FileData.Count() > 0)
            {
                if (("A0001,D0001").ToString().IndexOf(Occ_FileData.SingleOrDefault().Occ03) != -1)
                {
                    tmp_IsAgent = true;
                }
            }

            //先找是否在obk_file有資料,代表特殊價
            var Obk_FileData = _DataContext.ObkFiles.Where(Obk => Obk.Obk02 == MB001 && Obk.Obk01 == MB002 && Obk.Obk05 == MB004 && Obk.Obkacti == "Y");

            if (Obk_FileData.Count() > 0)
            {
                temp_1 = Obk_FileData.SingleOrDefault().Obk08.ToString();

                temp_2 = Convert.ToDouble(temp_1);

                temp_5 = temp_2;
            }
            else
            {
                //AT,ATB,AM 系列 不乘上商品價格
                string[] AT_AM_List = new string[] { "25", "26", "27", "28", "29", "30", "31", "32", "33", "57", "58", "59", "60", "61", "B6", "B7", "B8", "B9", "C1", "C2", "C3", "C4", "C5", "C7", "C8", "C8" };

                //P2代 找tc_pmr_file , PD PDR PL PLR 不打折
                //20190101 R40,RB3 不是找tc_pmr_file
                //string[] P2_List = new string[] { "69", "D4", "66", "D2", "67", "D3", "65", "D1", "86", "D5", "40", "B3", "E1", "E2", "D9", "E9" };
                string[] P2_List = new string[] { "69", "D4", "66", "D2", "67", "D3", "65", "D1", "86", "D5", "E1", "E2", "D9", "E9" };

                var ObkAE_FileData = _DataContext.ObkFiles.Where(Obk => Obk.Obk02 == "AE0002" && Obk.Obk01 == MB002 && Obk.Obk05 == MB004 && Obk.Obkacti == "Y");


                if (("A,C").ToString().Contains(MB002.Substring(0, 1)))  //減速機
                {
                    //AT,ATB,AM 系列 代理商不乘上商品價格,散客要.
                    if (Array.IndexOf(AT_AM_List, MB002.Substring(1, 2)) != -1)
                    {

                        if (ObkAE_FileData.Count() > 0)
                        {
                            temp_1 = ObkAE_FileData.SingleOrDefault().Obk08.ToString();

                            temp_2 = Convert.ToDouble(temp_1);

                            if (tmp_IsAgent)
                            {
                                temp_4 = 1;
                            }
                            else
                            {
                                temp_4 = Convert.ToDouble(temp_3) / 100;
                            }

                            temp_5 = (temp_2 * temp_4);

                            temp_5 = System.Math.Round(temp_5, 0, MidpointRounding.AwayFromZero);
                        }
                    }//P2代 找tc_pmr_file
                    else if (Array.IndexOf(P2_List, MB002.Substring(1, 2)) != -1)
                    {
                        string tmp_ima75 = "", tmp_ima76 = "", tmp_imabacklash = "";
                        //先找出料號(ima_file)的Model(ima75),stage(ima76),backlash(ima01最後1碼 0,1,X)
                        var Ima_FileData = _DataContext.ImaFiles.Where(Ima => Ima.Ima01 == MB002 && Ima.Imaacti == "Y");

                        if (Ima_FileData.Count() > 0)
                        {
                            tmp_ima75 = Ima_FileData.SingleOrDefault().Ima75.ToString();
                            tmp_ima76 = Ima_FileData.SingleOrDefault().Ima76.ToString();
                            tmp_imabacklash = Ima_FileData.SingleOrDefault().Ima01.ToString().Substring(11, 1);

                            //先找是否有在tc_pmr_file 有客戶的標價  //20181003
                            var TcPmr_FileData = _DataContext.TcPmrFiles.Where(TP => TP.TcPmr08 == MB001 && TP.TcPmr01 == tmp_ima75 && TP.TcPmr02 == Convert.ToInt32(tmp_ima76) && TP.TcPmr021 == MB004 && TP.TcPmr022 == tmp_imabacklash && TP.TcPmr03 <= MB003 && TP.TcPmr04 >= MB003);

                            if (TcPmr_FileData.Count() > 0)
                            {
                                temp_1 = TcPmr_FileData.SingleOrDefault().TcPmr05.ToString();

                                temp_2 = Convert.ToDouble(temp_1);

                                temp_5 = System.Math.Round(temp_2, 0, MidpointRounding.AwayFromZero);
                            }
                            else
                            {

                                var TcPmrAny_FileData = _DataContext.TcPmrFiles.Where(TP => TP.TcPmr08 == "any" && TP.TcPmr01 == tmp_ima75 && TP.TcPmr02 == Convert.ToInt32(tmp_ima76) && TP.TcPmr021 == MB004 && TP.TcPmr022 == tmp_imabacklash && TP.TcPmr03 <= MB003 && TP.TcPmr04 >= MB003);


                                if (TcPmrAny_FileData.Count() > 0)
                                {
                                    temp_1 = TcPmrAny_FileData.SingleOrDefault().TcPmr05.ToString();

                                    temp_2 = Convert.ToDouble(temp_1);

                                    //BAK001 APEX KOREA
                                    //BAJ002 APEX JAPAN
                                    //BAC001 上海
                                    //BAC015 深圳
                                    //BAC016 重慶
                                    //BAC017 北京
                                    //BAC019 廈門
                                    //PII價格代降20% ， 2016/3/17起生效
                                    //69,D4,66,D2,67,D3,65,D1,86,D5
                                    //PD PDR PL PLR不包含在降價系列
                                    //2019/1/1 有新價取消折扣
                                    string[] P001_List = new string[] { "E1", "E2", "D9", "E9" };

                                    if (Array.IndexOf(P001_List, MB002.Substring(1, 2)) == -1)
                                    {
                                        string[] MB001_List = new string[] { "BAK001", "BAJ002", "BAC001", "BAC001-1", "BAC015", "BAC016", "BAC017", "BAC019", "BAC019-1" };

                                        if (Array.IndexOf(MB001_List, MB001) != -1)
                                        {
                                            //temp_2 = temp_2 * 0.8;
                                            temp_2 = temp_2 * 1;
                                        }

                                        //大匠AA0003 大研AA0003-1  明震AA0005  降10%  3/18
                                        //2019/1/1 有新價取消折扣
                                        string[] MB001_List2 = new string[] { "AA0003", "AA0003-1", "AA0005" };

                                        if (Array.IndexOf(MB001_List2, MB001) != -1)
                                        {
                                            //temp_2 = temp_2 * 0.9;
                                            temp_2 = temp_2 * 1;
                                        }
                                    }

                                    temp_2 = System.Math.Round(temp_2, 0, MidpointRounding.AwayFromZero);

                                    if (tmp_IsAgent)
                                    {
                                        temp_4 = 1;
                                    }
                                    else
                                    {
                                        temp_4 = Convert.ToDouble(temp_3) / 100;
                                    }

                                    temp_5 = (temp_2 * temp_4);

                                    temp_5 = System.Math.Round(temp_5, 0, MidpointRounding.AwayFromZero);

                                }
                                else
                                {
                                    temp_5 = 0; //20190122 防止連線未關,在最後在return
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Obk_FileData.Count() > 0)
                        {

                            temp_1 = Obk_FileData.SingleOrDefault().Obk08.ToString();

                            temp_2 = Convert.ToDouble(temp_1);

                            temp_2 = System.Math.Round(temp_2, 0, MidpointRounding.AwayFromZero);

                            temp_4 = Convert.ToDouble(temp_3) / 100;

                            temp_5 = (temp_2 * temp_4);

                            temp_5 = System.Math.Round(temp_5, 0, MidpointRounding.AwayFromZero);
                        }
                    }

                }
                else
                {  //非減速機

                    if (Obk_FileData.Count() > 0)
                    {

                        temp_1 = Obk_FileData.SingleOrDefault().Obk08.ToString();

                        temp_2 = Convert.ToDouble(temp_1);

                        if (tmp_IsAgent)
                        {
                            temp_4 = 1;
                        }
                        else
                        {
                            temp_4 = Convert.ToDouble(temp_3) / 100;
                        }

                        temp_5 = (temp_2 * temp_4);

                        temp_5 = System.Math.Round(temp_5, 0, MidpointRounding.AwayFromZero);
                    }
                }

            }

            return temp_5;
        }


        /// <summary>取客戶的折扣率</summary>
        /// <param name="occ01">客戶編號</param>
        /// <returns></returns>
        public String GetDiscountRate(string occ01)
        {

            String temp_1 = "";

            var Occ_FileData = _DataContext.OccFiles.Where(oc => oc.Occ01 == occ01);

            if (Occ_FileData.Count() > 0)
            {
                if (string.IsNullOrEmpty(Convert.ToString(Occ_FileData.SingleOrDefault().Occud07)) || Convert.ToString(Occ_FileData.SingleOrDefault().Occud07) == "0")
                {
                    temp_1 = "200";
                }
                else
                {
                    temp_1 = Occ_FileData.SingleOrDefault().Occud07.ToString();
                }
            }

            return temp_1;
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
                    if (LubData == "1" || LubData == "2")
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
                    if (LubData == "1" || LubData == "2")
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
                    if (LubData == "1")
                    {
                        FinalCharge = System.Math.Round((DiscountPrice * 1.05), 0, MidpointRounding.AwayFromZero);
                    }
                    else if (LubData == "2" || LubData == "3")
                    {
                        //加收10%並且4捨五入
                        FinalCharge = System.Math.Round((DiscountPrice * 1.1), 0, MidpointRounding.AwayFromZero);
                    }
                }
            }
            else
            {
                if (LubData == "0")
                {
                    //加收10%並且4捨五入
                    FinalCharge = System.Math.Round((DiscountPrice * 1.1), 0, MidpointRounding.AwayFromZero);
                }
            }

            return FinalCharge;
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

        public string GetOrderID(Order Value)
        {
            //string TempOD = Utf8Json.JsonSerializer.Deserialize<dynamic>(feStr.ToString())["SaleInfo"].ToString() != "[]" ? "APEX" : Utf8Json.JsonSerializer.Deserialize<dynamic>(Utf8Json.JsonSerializer.Deserialize<dynamic>(feStr.ToString())["CustInfo"])[0]["custId"];
            string TempOD = !string.IsNullOrEmpty(Value.EmployId) ? "APEX" : Value.CustId;
            int TempCount = _web2Context.Orders.Where(
                x => x.OrderId.Contains(TempOD + Convert.ToString(Convert.ToInt32(DateTime.Now.Year) - 1911).Substring(1, 2) + Convert.ToString(Convert.ToInt32(DateTime.Now.Month) + 100).Substring(1, 2))
                )
                .ToList()
                .Count()
                ;
            TempOD += Convert.ToString(Convert.ToSingle(DateTime.Now.Year) - 1911).Substring(1, 2) + Convert.ToString(Convert.ToSingle(DateTime.Now.Month) + 100).Substring(1, 2) + Convert.ToString(TempCount + 1001).Substring(1, 3);

            return TempOD;
        }

        public void InsertOrder(Order Value, string TempOD)
        {
            Order Insert = new Order
            {
                OrderId = TempOD,
                CustId = Value.CustId,
                Deliver = Value.Deliver,
                Pono = Value.Pono,
                DelivAddr = Value.DelivAddr,
                Currency = Value.Currency,
                DelivAddr2 = Value.DelivAddr2,
                DelivWay = Value.DelivWay,
                Attn = Value.Attn,
                DelivTel = Value.DelivTel,
                OrderDate = Convert.ToDateTime(Value.OrderDate),
                DelivDate = Convert.ToDateTime(Value.DelivDate),
                Ostatus = Value.Ostatus,
                Remail = Value.Remail
            };
            _web2Context.Orders.Add(Insert);
            _web2Context.SaveChanges();
        }

        public void UpdateOrderList(dynamic tempOrder, dynamic tempOrderDetail)
        {
            if (tempOrder.Count > 0)
            {
                Order _order = new Order
                {
                    OrderId = Convert.ToString(tempOrder["OrderId"]),
                    Pono = Convert.ToString(tempOrder["Pono"])
                };
                var update = _web2Context.Orders.Where(x => x.OrderId == _order.OrderId);
            }

            if (tempOrderDetail.Count > 0)
            {
                OrderDetail _orderDetail = new OrderDetail
                {
                    OrderId = Convert.ToString(tempOrderDetail["OrderId"]),
                    OrderDetailId = Convert.ToInt32(tempOrderDetail["OrderDetailId"]),
                    Qty = Convert.ToInt32(tempOrderDetail["Qty"]),
                    Memo = Convert.ToString(tempOrderDetail["Memo"]),
                    SubTot = Convert.ToDouble(tempOrderDetail["SubTot"])
                };
                var update = _web2Context.OrderDetails.Where(x => x.OrderId == _orderDetail.OrderId && x.OrderDetailId == _orderDetail.OrderDetailId);
                if (update.Count() > 0)
                {
                    update.SingleOrDefault().Qty = _orderDetail.Qty;
                    update.SingleOrDefault().Memo = _orderDetail.Memo;
                    update.SingleOrDefault().SubTot = _orderDetail.SubTot;
                    _web2Context.SaveChanges();
                }
            }

        }
        public OrderDetail ProductInformation(dynamic Orderdetail)
        {


            OrderDetail _orderdetail = new OrderDetail();

            return _orderdetail;
        }
        private class Datas
        {
            public object Data { get; set; }
            public object Data2 { get; set; }
        }

    }
}
