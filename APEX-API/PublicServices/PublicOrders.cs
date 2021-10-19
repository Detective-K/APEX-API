using APEX_API.Services;
using APEX_API.TopprodModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APEX_API.PublicServices
{
    public class PublicOrders
    {
        private readonly OrderService _orderService;
        private readonly PublicFunctions _publicFunction;
        public PublicOrders(OrderService orderService, PublicFunctions publicFunctions)
        {
            _orderService = orderService;
            _publicFunction = publicFunctions;
        }
        public void GBResult(dynamic OData, string type, string addbuy)
        {
            Decimal S = 0;
            string R65Groups = "R65,R66,R67,R69,R86,RD1,RD2,RD3,RD4,RD5,RE1,RD9,RE2,RE9,R40,RF2,RB3,RA9,RB2,RB4,RF4,RE3,RE4,RJ9,RJ5,RK9,RK5,R53,R54,RF1,RR1,RR2,RR3,RR4,RR5,RR6,RR7,RR8,RR9,RS3,RS4,RS1,RS2,RS5,RS6,RS7,R42";
            string AdpR01Groups = "R01,R02,R03,R04";
            string AdpR14Groups = "R15,R16,R19,R20";
            string AdpR25Groups = "R25";
            string AdpR65Groups = "R65,R66,R67,R69,R86,RD1,RD2,RD3,RD4,RD5,RE1,RD9,RE2,RE9";
            string AdpRG4Grooups = "RG4,RG5";
            string GBSeries = Convert.ToString(OData["GearBox"]["GBSeries"]);
            string GBType = Convert.ToString(OData["GearBox"]["GBModel"]);
            string ta_oeb005 = string.Empty;
            string AdapterData1, AdapterData2, AdapterData3, AdapterData4, AdapterData5;
            string AWidth1, AWidth2, ratio_x, file_ratio_x, R1No, PartNo, M3max, P_OuterDia, newrdeucer, l_AWidth1, suitadapter_c4;
            string M3maxWeb, suitadapter;
            string T, txtadaperNo, txtSUNGEAR;
            double Reducer_D7, Reducer_D9, Reducer_A1, Reducer_A2; //反鎖馬達用
            List<TcOekFile> MortorInfo = _orderService.GetMotorInfoDetail(OData["Motor"]);
            List<Reducer1Order> ReducerInfo = _orderService.GetReducerInfo(OData["GearBox"]);
            List<Resg> ResgInfo = _orderService.GetResgInfo(OData["GearBox"]);
            List<TcOelFile> TcOelInfo = _orderService.GetTcOelFileInfo(OData["Motor"]);
            if (Convert.ToString(type) == "2")
            {
                if (MortorInfo.Count() > 0)
                {
                    S = Convert.ToDecimal(MortorInfo.FirstOrDefault().TcOek09);
                }
            }

            if (Convert.ToString(type) == "1")
            {
                //20180807 AFX220 AXFR220 只有日本和業務可下可以下 , 全世界都關
                if ((Convert.ToString(OData["custId"]) == "BAJ002" || Convert.ToString(OData["isSale"]) == "Y") && ("R19A,R20A".Contains(GBSeries)))
                {
                    ReducerInfo = ReducerInfo.Where(r => ("R19A,R19B,R20A").Contains(r.TcMmd01.Substring(0, 4))).ToList();
                }
                else if ("R19,R20".Contains(Convert.ToString(OData["GearBox"]["GBSeries"])))
                {
                    ReducerInfo = ReducerInfo.Where(r => !("R19A,R19B,R20A").Contains(r.TcMmd01.Substring(0, 4))).ToList();
                }
            }
            if (Convert.ToString(type) == "2")
            {
                if ("RA9,RB2".Contains(GBSeries))
                {
                    ReducerInfo = ReducerInfo.Where(r => EF.Functions.Like(r.TcMmd01, "RA9%") || EF.Functions.Like(r.TcMmd01, "RB2%")).ToList();
                }
                else if ("R40,RF2".Contains(GBSeries))
                {
                    ReducerInfo = ReducerInfo.Where(r => EF.Functions.Like(r.TcMmd01, "R40%") || EF.Functions.Like(r.TcMmd01, "RF2%")).ToList();
                }
                else if ("RB4,RF4,RE3,RE4".Contains(GBSeries))
                {
                    ReducerInfo = ReducerInfo.Where(r => EF.Functions.Like(r.TcMmd01, "RB4%") || EF.Functions.Like(r.TcMmd01, "RF4%") || EF.Functions.Like(r.TcMmd01, "RE3%") || EF.Functions.Like(r.TcMmd01, "RE4%")).ToList();
                }
                else if ("R54,RF1".Contains(GBSeries))
                {
                    ReducerInfo = ReducerInfo.Where(r => EF.Functions.Like(r.TcMmd01, "R54%") || EF.Functions.Like(r.TcMmd01, "RF1%")).ToList();
                }
                else if ("RR4,RR5".Contains(GBSeries))
                {
                    ReducerInfo = ReducerInfo.Where(r => EF.Functions.Like(r.TcMmd01, "RR4%") || EF.Functions.Like(r.TcMmd01, "RR5%")).ToList();
                }
                else if ("RR1,RR2".Contains(GBSeries))
                {
                    ReducerInfo = ReducerInfo.Where(r => EF.Functions.Like(r.TcMmd01, "RR1%") || EF.Functions.Like(r.TcMmd01, "RR2%")).ToList();
                }
                else if ("RR6,RR8,RR7,RR9".Contains(GBSeries))
                {
                    ReducerInfo = ReducerInfo.Where(r => EF.Functions.Like(r.TcMmd01, "RR6%") || EF.Functions.Like(r.TcMmd01, "RR8%") || EF.Functions.Like(r.TcMmd01, "RR7%") || EF.Functions.Like(r.TcMmd01, "RR9%")).ToList();
                }
                else if ("RS6,RS7".Contains(GBSeries))
                {
                    ReducerInfo = ReducerInfo.Where(r => EF.Functions.Like(r.TcMmd01, "RS6%") || EF.Functions.Like(r.TcMmd01, "RS7%")).ToList();
                }

                if (Convert.ToString(OData["Range"]) == "2" && R65Groups.Contains(GBSeries))
                {
                    if (S != 0)
                    {
                        ReducerInfo = ReducerInfo.Where(r => r.TcMmd16 == S).Count() > 0 ? ReducerInfo.Where(r => r.TcMmd16 == S || (r.TcMmd16 > S && "8,11,14,19,24,28,32,35,38,42,48,55,60".Contains(Convert.ToString(r.TcMmd16)) && r.TcMmd16 == r.TcMmd17)).ToList() : ReducerInfo.Where(r => r.TcMmd16 == Convert.ToDecimal(_publicFunction.get_NO(Convert.ToDouble(S))) || (r.TcMmd16 > S && "8,11,14,19,24,28,32,35,38,42,48,55,60".Contains(Convert.ToString(r.TcMmd16)) && r.TcMmd16 == r.TcMmd17)).ToList();
                    }
                }
                else
                {
                    if (S != 0)
                    {
                        ReducerInfo = ReducerInfo.Where(r => r.TcMmd16 == S).Count() > 0 ? ReducerInfo.Where(r => r.TcMmd16 == S).ToList() : ReducerInfo.Where(r => r.TcMmd16 == Convert.ToDecimal(_publicFunction.get_NO(Convert.ToDouble(S)))).ToList();
                    }
                }
                if (ReducerInfo.Count() > 0)
                {
                    if ("R650,R660,R670,R690,R860".Contains(ReducerInfo.FirstOrDefault().TcMmd01.Substring(0, 4)))
                    {
                        ReducerInfo = ReducerInfo.Where(r => EF.Functions.Like(r.TcMmd01, "R__4%")).ToList();
                        ta_oeb005 = Convert.ToString(ReducerInfo.FirstOrDefault().TcMmd01).Substring(0, 4);
                    }
                }
            }

            if (Convert.ToString(type) != "4")
            {
                l_AWidth1 = Convert.ToString(ReducerInfo.FirstOrDefault().TcMmd20).Replace(".000", "");
                AWidth1 = l_AWidth1;

                AWidth2 = Convert.ToString(ReducerInfo.FirstOrDefault().TcMmd21).Replace(".000", "");

                ratio_x = Convert.ToString(ReducerInfo.FirstOrDefault().TcMmd24).Replace(".000", "");

                file_ratio_x = Convert.ToString(ReducerInfo.FirstOrDefault().TcMmd24).Replace(".000", "");

                suitadapter_c4 = Convert.ToString(ReducerInfo.FirstOrDefault().TcMmd29).Replace(".000", "");

                R1No = Convert.ToString(ReducerInfo.FirstOrDefault().TcMmd01).Substring(0, 3);

                PartNo = Convert.ToString(ReducerInfo.FirstOrDefault().TcMmd01);

                M3max = Convert.ToString(ReducerInfo.FirstOrDefault().TcMmd16).Replace(".000", "");

                M3maxWeb = Convert.ToString(ReducerInfo.FirstOrDefault().TcMmd17).Replace(".000", "");

                P_OuterDia = Convert.ToString(ReducerInfo.FirstOrDefault().TcMmd18).Replace(".000", "");

                newrdeucer = Convert.ToString(ReducerInfo.FirstOrDefault().TcMmd01);

                suitadapter = Convert.ToString(ReducerInfo.FirstOrDefault().TcMmd28);

                Reducer_D7 = Convert.ToString(ReducerInfo.FirstOrDefault().TcMmd51) != "" ? Convert.ToDouble(ReducerInfo.FirstOrDefault().TcMmd51) : 0;
                Reducer_D9 = Convert.ToString(ReducerInfo.FirstOrDefault().TcMmd52) != "" ? Convert.ToDouble(ReducerInfo.FirstOrDefault().TcMmd52) : 0;
                Reducer_A1 = Convert.ToString(ReducerInfo.FirstOrDefault().TcMmd53) != "" ? Convert.ToDouble(ReducerInfo.FirstOrDefault().TcMmd53) : 0;
                Reducer_A2 = Convert.ToString(ReducerInfo.FirstOrDefault().TcMmd54) != "" ? Convert.ToDouble(ReducerInfo.FirstOrDefault().TcMmd54) : 0;

            }
            else
            {
                l_AWidth1 = Convert.ToString(ResgInfo.FirstOrDefault().TcMmd20).Replace(".000", "");
                AWidth1 = l_AWidth1;

                AWidth2 = Convert.ToString(ResgInfo.FirstOrDefault().TcMmd21).Replace(".000", "");

                ratio_x = Convert.ToString(ResgInfo.FirstOrDefault().TcMmd24).Replace(".000", "");

                file_ratio_x = Convert.ToString(ResgInfo.FirstOrDefault().TcMmd24).Replace(".000", "");

                suitadapter_c4 = Convert.ToString(ResgInfo.FirstOrDefault().TcMmd29).Replace(".000", "");

                R1No = Convert.ToString(ResgInfo.FirstOrDefault().TcMmd01).Substring(0, 3);

                PartNo = Convert.ToString(ResgInfo.FirstOrDefault().TcMmd01);

                M3max = Convert.ToString(ResgInfo.FirstOrDefault().TcMmd16).Replace(".000", "");

                M3maxWeb = Convert.ToString(ResgInfo.FirstOrDefault().TcMmd17).Replace(".000", "");

                P_OuterDia = Convert.ToString(ResgInfo.FirstOrDefault().TcMmd18).Replace(".000", "");

                newrdeucer = Convert.ToString(ResgInfo.FirstOrDefault().TcMmd01);

                suitadapter = Convert.ToString(ResgInfo.FirstOrDefault().TcMmd28);
                //Resg 無以下四個欄位但原本的確有寫
               // Reducer_D7 = Convert.ToString(ResgInfo.FirstOrDefault().TcMmd51) != "" ? Convert.ToDouble(ResgInfo.FirstOrDefault().TcMmd51) : 0; 
               // Reducer_D9 = Convert.ToString(ResgInfo.FirstOrDefault().TcMmd52) != "" ? Convert.ToDouble(ResgInfo.FirstOrDefault().TcMmd52) : 0;
                //Reducer_A1 = Convert.ToString(ResgInfo.FirstOrDefault().TcMmd53) != "" ? Convert.ToDouble(ResgInfo.FirstOrDefault().TcMmd53) : 0;
               // Reducer_A2 = Convert.ToString(ResgInfo.FirstOrDefault().TcMmd54) != "" ? Convert.ToDouble(ResgInfo.FirstOrDefault().TcMmd54) : 0;
                T = Convert.ToString(ResgInfo.FirstOrDefault().TcMmg04);
            }

            if (AdpR01Groups.Contains(R1No))
            {
                AdapterData1 = "Adap_class1";

                AdapterData2 = "Adap_classEx2";

                AdapterData3 = "Adap_classEx3";

                AdapterData4 = "Adap_classEx4";

                AdapterData5 = "Adap_classEx5";
            }
            else if (AdpR14Groups.Contains(R1No))
            {
                if (("AN023,AN023M1,AN023B,AN023BM1,ANR023,ANR023M1,ANR023B,ANR023BM1,AFX060,AFX060M1,AFXR060,AFXR060M1,AFXR042,AFXR042M1,AFXR042,AFXR042M1".Contains(GBType)) && (Convert.ToSingle(ratio_x) == 2))
                {
                    AdapterData1 = "Adap_class1";

                    AdapterData2 = "Adap_classEx2";

                    AdapterData3 = "Adap_classEx3";

                    AdapterData4 = "Adap_classEx4";

                    AdapterData5 = "Adap_classEx5";
                }
                else
                {
                    if (("AFXR042,AFX042".Contains(GBType)) && (Convert.ToSingle(ratio_x) == 1))
                    {
                        AdapterData1 = "Adap_class1";

                        AdapterData2 = "Adap_classEx2";

                        AdapterData3 = "Adap_classEx3";

                        AdapterData4 = "Adap_classEx4";

                        AdapterData5 = "Adap_classEx5";
                    }
                    else
                    {
                        AdapterData1 = "Adap_type1_class1";

                        AdapterData2 = "Adap_type1_classEx2";

                        AdapterData3 = "Adap_type1_classEx3";

                        AdapterData4 = "Adap_type1_classEx4";

                        AdapterData5 = "Adap_type1_classEx5";
                    }
                }
            }
            else if (AdpR25Groups.Contains(R1No))
            {

                AdapterData1 = "Adap_dir1_class1";

                AdapterData2 = "Adap_dir1_class1";

                AdapterData3 = "Adap_dir1_class1";

                AdapterData4 = "Adap_dir1_class1";

                AdapterData5 = "Adap_dir1_class1";
            }
            else if (AdpR65Groups.Contains(R1No))
            {
                AdapterData1 = "Adap_type1_class0";

                AdapterData2 = "Adap_type1_class0";

                AdapterData3 = "Adap_type1_class0";

                AdapterData4 = "Adap_type1_class0";

                AdapterData5 = "Adap_type1_class5";
            }
            else if (AdpRG4Grooups.Contains(R1No))
            {
                AdapterData1 = "Adap_type5_class0";

                AdapterData2 = "Adap_type5_class0";

                AdapterData3 = "Adap_type5_class0";

                AdapterData4 = "Adap_type5_class0";

                AdapterData5 = "Adap_type5_class5";
            }
            else
            {
                AdapterData1 = "Adap_type1_class1";

                AdapterData2 = "Adap_type1_classEx2";

                AdapterData3 = "Adap_type1_classEx3";

                AdapterData4 = "Adap_type1_classEx4";

                AdapterData5 = "Adap_type1_classEx5";
            }

        }
    }
}
