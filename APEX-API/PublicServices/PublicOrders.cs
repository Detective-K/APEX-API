using APEX_API.Services;
using APEX_API.TopprodModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace APEX_API.PublicServices
{
    public class PublicOrders
    {
        private readonly IServiceProvider _orderService = null;
        private readonly IServiceProvider _publicFunction = null;

        public void GBResult(dynamic OData, string type, string addbuy, string orderCode)
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
            string Ratio = Convert.ToString(OData["GearBox"]["Ratio"]);
            string Shaft = Convert.ToString(OData["GearBox"]["Shaft"]);
            string CustId = Convert.ToString(OData["custId"]);
            string ta_oeb005 = string.Empty;
            string AdapterData1, AdapterData2, AdapterData3, AdapterData4, AdapterData5;
            string AWidth1, AWidth2, ratio_x, file_ratio_x, R1No, PartNo, M3max, P_OuterDia, newrdeucer, l_AWidth1, suitadapter_c4;
            string C3, LR, LB, LE, LA, LC, ScrewDia = string.Empty, MT1N, MT1B, Mpower, MN1N, MN1B, MInertia, LT, FixPlate, FlangeWidth = string.Empty, Motor_Interface = string.Empty, Motor_Screw_orientation = string.Empty;
            string M3maxWeb, suitadapter, AdapterNx = string.Empty;
            string T, txtadaperNo, txtSUNGEAR;
            string LN = "0", LD, LZ, LA2 = "0", LD2 = "0", LZ2 = "0", LN2 = "0", LZ1 = "";
            string G_Reducer_One_piece = "N", G_Reducer_One_piece_chenged = "N";
            double Reducer_D7 = 0, Reducer_D9 = 0, Reducer_A1 = 0, Reducer_A2 = 0; //反鎖馬達用

            Double tc_shw10 = 0;
            string tc_shw13 = string.Empty;
            Double tc_shw08 = 0;
            Double tc_shw03 = 0;
            string l_suitadapter = string.Empty;

            DateTime g_begin, g_end;
            string adaperNo = string.Empty, DMpic, conjunction, adp7000, sFile, txtBushing, AdapterScrew, txtWasher, WasherT, AdapterPitch;
            string adpter_tc_mmaa_file = string.Empty, addbuy_error = string.Empty;

            string errMsg = string.Empty;
            List<TcOekFile> MortorInfo = _orderService.GetService<OrderService>().GetMotorInfoDetail(OData["Motor"]);
            List<Reducer1Order> ReducerInfo = _orderService.GetService<OrderService>().GetReducerInfo(OData["GearBox"]);
            List<Resg> ResgInfo = _orderService.GetService<OrderService>().GetResgInfo(OData["GearBox"]);
            List<TcOelFile> TcOelInfo = _orderService.GetService<OrderService>().GetTcOelFileInfo(OData["Motor"]);
            List<TcShwFile> TcShwInfo = new List<TcShwFile> { };
            List<OrderService.AdpDatas> _adpDatas = new List<OrderService.AdpDatas>();
            List<PublicFunctions.TcMmiFileInfo> _tcMmiFileInfo = new List<PublicFunctions.TcMmiFileInfo>();

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
                        ReducerInfo = ReducerInfo.Where(r => r.TcMmd16 == S).Count() > 0 ? ReducerInfo.Where(r => r.TcMmd16 == S || (r.TcMmd16 > S && "8,11,14,19,24,28,32,35,38,42,48,55,60".Contains(Convert.ToString(r.TcMmd16)) && r.TcMmd16 == r.TcMmd17)).ToList() : ReducerInfo.Where(r => r.TcMmd16 == Convert.ToDecimal(_publicFunction.GetService<PublicFunctions>().Get_NO(Convert.ToDouble(S))) || (r.TcMmd16 > S && "8,11,14,19,24,28,32,35,38,42,48,55,60".Contains(Convert.ToString(r.TcMmd16)) && r.TcMmd16 == r.TcMmd17)).ToList();
                    }
                }
                else
                {
                    if (S != 0)
                    {
                        ReducerInfo = ReducerInfo.Where(r => r.TcMmd16 == S).Count() > 0 ? ReducerInfo.Where(r => r.TcMmd16 == S).ToList() : ReducerInfo.Where(r => r.TcMmd16 == Convert.ToDecimal(_publicFunction.GetService<PublicFunctions>().Get_NO(Convert.ToDouble(S)))).ToList();
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
                G_Reducer_One_piece = Convert.ToString(ReducerInfo.FirstOrDefault().TcMmd45);
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


            if (Convert.ToString(type) == "5")
            {
                C3 = Convert.ToString(MortorInfo.FirstOrDefault().TcOek09).Replace(".000", "");

                LR = Convert.ToString(MortorInfo.FirstOrDefault().TcOek10).Replace(".000", "");

                LB = Convert.ToString(MortorInfo.FirstOrDefault().TcOek11).Replace(".000", "");

                LE = Convert.ToString(MortorInfo.FirstOrDefault().TcOek12).Replace(".000", "");

                LA = Convert.ToString(MortorInfo.FirstOrDefault().TcOek14).Replace(".000", "");

                LC = Convert.ToString(MortorInfo.FirstOrDefault().TcOek16).Replace(".000", "");
                //判斷正反鎖
                if (Convert.ToString(MortorInfo.FirstOrDefault().TcOek27) == "Y")  //Y:反鎖  N:正鎖
                {
                    Motor_Screw_orientation = "Y";
                    LZ1 = Convert.ToString(MortorInfo.FirstOrDefault().TcOek25);
                    ScrewDia = _publicFunction.GetService<PublicFunctions>().Get_C12(LZ1);
                }
                else
                {
                    Motor_Screw_orientation = "N";
                    ScrewDia = _publicFunction.GetService<PublicFunctions>().Get_M(Convert.ToDouble(MortorInfo.FirstOrDefault().TcOek15));   //20180130改用LZ去反查ScrewDia,不在用表格的ScrewDia
                }

                MT1N = Convert.ToString(MortorInfo.FirstOrDefault().TcOek05).Replace(".000", "");

                MT1B = Convert.ToString(MortorInfo.FirstOrDefault().TcOek04).Replace(".000", "");

                Mpower = Convert.ToString(MortorInfo.FirstOrDefault().TcOek03).Replace(".000", "");

                MN1N = Convert.ToString(MortorInfo.FirstOrDefault().TcOek07).Replace(".000", "");

                MN1B = Convert.ToString(MortorInfo.FirstOrDefault().TcOek06).Replace(".000", "");

                MInertia = Convert.ToString(MortorInfo.FirstOrDefault().TcOek08).Replace(".000", "");

                LT = Convert.ToString(MortorInfo.FirstOrDefault().TcOek13).Replace(".000", "");

                FixPlate = Convert.ToString(MortorInfo.FirstOrDefault().TcOek22);

                FlangeWidth = !String.IsNullOrEmpty(Convert.ToString(MortorInfo.FirstOrDefault().TcOek17).Trim()) ? Convert.ToString(MortorInfo.FirstOrDefault().TcOek17).Replace(".000", "") : "0";

                LZ = !String.IsNullOrEmpty(Convert.ToString(MortorInfo.FirstOrDefault().TcOek15).Trim()) ? Convert.ToString(MortorInfo.FirstOrDefault().TcOek15).Replace(".000", "") : _publicFunction.GetService<PublicFunctions>().Get_C12_NEW(LZ1);

                Motor_Interface = Convert.ToString(MortorInfo.FirstOrDefault().TcOek26) == "Y" ? "Y" : "N";

            }
            else if (Convert.ToString(type) == "4") //微型馬達
            {
                C3 = Convert.ToString(TcOelInfo.FirstOrDefault().TcOel09).Replace(".000", "");

                LR = Convert.ToString(TcOelInfo.FirstOrDefault().TcOel10).Replace(".000", "");

                LB = Convert.ToString(TcOelInfo.FirstOrDefault().TcOel11).Replace(".000", "");

                LE = Convert.ToString(TcOelInfo.FirstOrDefault().TcOel12).Replace(".000", "");

                LA = Convert.ToString(TcOelInfo.FirstOrDefault().TcOel14).Replace(".000", "");

                LC = Convert.ToString(TcOelInfo.FirstOrDefault().TcOel16).Replace(".000", "");

                MT1N = Convert.ToString(TcOelInfo.FirstOrDefault().TcOel05).Replace(".000", "");

                MT1B = Convert.ToString(TcOelInfo.FirstOrDefault().TcOel04).Replace(".000", "");

                Mpower = Convert.ToString(TcOelInfo.FirstOrDefault().TcOel03).Replace(".000", "");

                MN1N = Convert.ToString(TcOelInfo.FirstOrDefault().TcOel07).Replace(".000", ""); ;

                MN1B = Convert.ToString(TcOelInfo.FirstOrDefault().TcOel06).Replace(".000", "");

                MInertia = Convert.ToString(TcOelInfo.FirstOrDefault().TcOel08).Replace(".000", "");

                LT = Convert.ToString(TcOelInfo.FirstOrDefault().TcOel13).Replace(".000", "");

                LZ = Convert.ToString(TcOelInfo.FirstOrDefault().TcOel15).Replace(".000", "");

                LN = Convert.ToString(TcOelInfo.FirstOrDefault().TcOel17).Replace(".000", "");

                LD = Convert.ToString(TcOelInfo.FirstOrDefault().TcOel16).Replace(".000", "");

                if (!string.IsNullOrEmpty(Convert.ToString(TcOelInfo.FirstOrDefault().TcOel18)))
                {
                    LA2 = Convert.ToString(TcOelInfo.FirstOrDefault().TcOel18).Replace(".000", "");
                }

                if (!string.IsNullOrEmpty(Convert.ToString(TcOelInfo.FirstOrDefault().TcOel20)))
                {
                    LA2 = Convert.ToString(TcOelInfo.FirstOrDefault().TcOel20).Replace(".000", "");
                }

                if (!string.IsNullOrEmpty(Convert.ToString(TcOelInfo.FirstOrDefault().TcOel19)))
                {
                    LA2 = Convert.ToString(TcOelInfo.FirstOrDefault().TcOel19).Replace(".000", "");
                }

                if (!string.IsNullOrEmpty(Convert.ToString(TcOelInfo.FirstOrDefault().TcOel21)))
                {
                    LA2 = Convert.ToString(TcOelInfo.FirstOrDefault().TcOel21).Replace(".000", "");
                }
            }
            else
            {
                C3 = Convert.ToString(MortorInfo.FirstOrDefault().TcOek09).Replace(".000", "");

                LR = Convert.ToString(MortorInfo.FirstOrDefault().TcOek10).Replace(".000", "");

                LB = Convert.ToString(MortorInfo.FirstOrDefault().TcOek11).Replace(".000", "");

                LE = Convert.ToString(MortorInfo.FirstOrDefault().TcOek12).Replace(".000", "");

                LA = Convert.ToString(MortorInfo.FirstOrDefault().TcOek14).Replace(".000", "");

                LC = Convert.ToString(MortorInfo.FirstOrDefault().TcOek16).Replace(".000", "");
                //判斷正反鎖
                if (Convert.ToString(MortorInfo.FirstOrDefault().TcOek27) == "Y")  //Y:反鎖  N:正鎖
                {
                    Motor_Screw_orientation = "Y";
                    LZ1 = Convert.ToString(MortorInfo.FirstOrDefault().TcOek25);
                    ScrewDia = _publicFunction.GetService<PublicFunctions>().Get_C12(LZ1);
                }
                else
                {
                    Motor_Screw_orientation = "N";
                    ScrewDia = _publicFunction.GetService<PublicFunctions>().Get_M(Convert.ToDouble(MortorInfo.FirstOrDefault().TcOek15));   //20180130改用LZ去反查ScrewDia,不在用表格的ScrewDia
                }

                MT1N = Convert.ToString(MortorInfo.FirstOrDefault().TcOek05).Replace(".000", "");

                MT1B = Convert.ToString(MortorInfo.FirstOrDefault().TcOek04).Replace(".000", "");

                Mpower = Convert.ToString(MortorInfo.FirstOrDefault().TcOek03).Replace(".000", "");

                MN1N = Convert.ToString(MortorInfo.FirstOrDefault().TcOek07).Replace(".000", "");

                MN1B = Convert.ToString(MortorInfo.FirstOrDefault().TcOek06).Replace(".000", "");

                MInertia = Convert.ToString(MortorInfo.FirstOrDefault().TcOek08).Replace(".000", "");

                LT = Convert.ToString(MortorInfo.FirstOrDefault().TcOek13).Replace(".000", "");

                FixPlate = Convert.ToString(MortorInfo.FirstOrDefault().TcOek22);

                FlangeWidth = !String.IsNullOrEmpty(Convert.ToString(MortorInfo.FirstOrDefault().TcOek17).Trim()) ? Convert.ToString(MortorInfo.FirstOrDefault().TcOek17).Replace(".000", "") : "0";

                LZ = !String.IsNullOrEmpty(Convert.ToString(MortorInfo.FirstOrDefault().TcOek15).Trim()) ? Convert.ToString(MortorInfo.FirstOrDefault().TcOek15).Replace(".000", "") : _publicFunction.GetService<PublicFunctions>().Get_C12_NEW(LZ1);

                Motor_Interface = Convert.ToString(MortorInfo.FirstOrDefault().TcOek26) == "Y" ? "Y" : "N";
            }

            //換上對應的字母
            if (suitadapter.IndexOf("_") != -1)
            {
                if (Motor_Interface == "Y")
                {
                    l_suitadapter = "O" + suitadapter.Substring(1);//圓形
                }
                else
                {
                    l_suitadapter = "P" + suitadapter.Substring(1);//正方形
                }

                if (Motor_Screw_orientation == "Y")
                {
                    l_suitadapter = l_suitadapter.Substring(0, 4) + "4" + l_suitadapter.Substring(5);//反鎖
                }
                else
                {
                    l_suitadapter = l_suitadapter.Substring(0, 4) + "3" + l_suitadapter.Substring(5);//正鎖
                }

            }
            else
            {
                l_suitadapter = suitadapter;
            }

            TcShwInfo = _orderService.GetService<OrderService>().GetTcShwFileInfo(l_suitadapter);

            if (TcShwInfo.Count > 0)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(TcShwInfo.FirstOrDefault().TcShw10)))
                {
                    tc_shw10 = Convert.ToDouble(TcShwInfo.FirstOrDefault().TcShw10);
                }

                tc_shw13 = !string.IsNullOrEmpty(Convert.ToString(TcShwInfo.FirstOrDefault().TcShw13)) ? Convert.ToString(TcShwInfo.FirstOrDefault().TcShw13) : "";

                tc_shw08 = !string.IsNullOrEmpty(Convert.ToString(TcShwInfo.FirstOrDefault().TcShw08)) ? Convert.ToDouble(TcShwInfo.FirstOrDefault().TcShw08) : 0;

                tc_shw03 = !string.IsNullOrEmpty(Convert.ToString(TcShwInfo.FirstOrDefault().TcShw03)) ? Convert.ToDouble(TcShwInfo.FirstOrDefault().TcShw03) : 0;
            }

            if (Convert.ToDouble(LR) < tc_shw10 + _publicFunction.GetService<PublicFunctions>().Ceiling(Convert.ToDouble(LT), 0.5) + 0.5)
            {
                errMsg = "Error !";//Resources.Resource.msg020
            }

            if (Motor_Screw_orientation == "N")
            {
                if (Convert.ToDouble(LA) - Convert.ToDouble(_publicFunction.GetService<PublicFunctions>().Get_M(Convert.ToDouble(LZ))) - tc_shw08 < 1 && _publicFunction.GetService<PublicFunctions>().GetScrewT(_publicFunction.GetService<PublicFunctions>().Get_M(Convert.ToDouble(LZ))) > Convert.ToDouble(LT))
                {
                    //B1 +SCREW_DEP +0.5 > Motor.LR
                    if (tc_shw10 + _publicFunction.GetService<PublicFunctions>().GetScrewT(_publicFunction.GetService<PublicFunctions>().Get_M(Convert.ToDouble(LZ))) + 0.5 > Convert.ToDouble(LR))
                    {
                        errMsg = "Error !";//Resources.Resource.msg020
                    }
                }
            }

            if (Motor_Screw_orientation == "Y")//反鎖
            {
                if (Convert.ToDouble(LA) - _publicFunction.GetService<PublicFunctions>().Get_Screw_Max_Dia(Convert.ToDouble(_publicFunction.GetService<PublicFunctions>().Get_C12_NEW(LZ1))) - 2 < Reducer_D9)
                {
                    errMsg = "Error !";// Resources.Resource.msg021
                }
            }

            if (Motor_Screw_orientation == "N")
            {
                if (Convert.ToDouble(LA) - Convert.ToDouble(_publicFunction.GetService<PublicFunctions>().Get_M(Convert.ToDouble(LZ))) - Convert.ToDouble(LB) < 2)
                {
                    errMsg = "Error !";// Resources.Resource.msg022
                }
            }
            else
            {
                if (Math.Abs(Convert.ToDouble(LA) - Convert.ToDouble(_publicFunction.GetService<PublicFunctions>().Get_C12_NEW(LZ1)) - Convert.ToDouble(LB)) < 2)
                {
                    errMsg = "Error !";// Resources.Resource.msg022
                }
            }


            //================================================= 
            //標準品 adapter 
            //================================================= 
            g_begin = DateTime.Now;

            _adpDatas.Add(new OrderService.AdpDatas
            {
                TableName = AdapterData1,
                LBck = "LBstd",
                LR = LR,
                LB = LB,
                LE = LE,
                LT = LT,
                LA = LA,
                ScrewDia = ScrewDia,
                AWidth1 = AWidth1,
                LC = LC,
                LAtmp = "",
                Tmp = "",
                L_string = "",
                RblAdpCount = 0,
                G_Reducer_One_piece = G_Reducer_One_piece,
                G_Reducer_One_piece_used = "Y",
                Reducer_No = PartNo,
                Suitadapter_c4 = suitadapter_c4,
                Suitadapter = suitadapter,
                Brand = OData["Motor"]["Brand"],
                Spec = OData["Motor"]["Spec"],
                LN = LN,
                LZ = LZ,
                LN2 = LN2,
                LA2 = LA2,
                LZ2 = LZ2,
                Motor_Interface = Motor_Interface,
                Motor_Screw_orientation = Motor_Screw_orientation,
                Reducer_D9 = Reducer_D9,
                Reducer_D7 = Reducer_D7,
                R1No = R1No,
                Reducer_A1 = Reducer_A1,
                Reducer_A2 = Reducer_A2
            });

            if (Convert.ToString(type) == "1")
            {
                _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula(_adpDatas);
                adaperNo = _tcMmiFileInfo.FirstOrDefault().Tmp_newPartNo;
                PartNo = !string.IsNullOrEmpty(_tcMmiFileInfo.FirstOrDefault().PartNo) ? _tcMmiFileInfo.FirstOrDefault().PartNo : PartNo;
                G_Reducer_One_piece_chenged = _tcMmiFileInfo.FirstOrDefault().G_Reducer_One_piece_chenged;
            }
            if (Convert.ToString(type) == "2")
            {

                _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula_P2(_adpDatas, G_Reducer_One_piece_chenged);
            }

            if (Convert.ToString(type) == "3")
            {
                adaperNo = " ";
            }

            if (Convert.ToString(type) == "4")
            {
                _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula2(_adpDatas);
            }

            if (Convert.ToString(type) == "5")
            {
                _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula_type5(_adpDatas);
            }

            if (_tcMmiFileInfo.FirstOrDefault().Adaper_No == "None")
            {
                _adpDatas.FirstOrDefault().LBck = "LBmax";
                if (Convert.ToString(type) == "1")
                {
                    _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula(_adpDatas);  //LBmax=導正圓直徑加大
                }

                if (Convert.ToString(type) == "2")
                {
                    _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula_P2(_adpDatas, G_Reducer_One_piece_chenged);  //LBmax=導正圓直徑加大
                }

                if (Convert.ToString(type) == "4")
                {
                    _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula2(_adpDatas);  //LBmax=導正圓直徑加大
                }

                if (Convert.ToString(type) == "5")
                {
                    _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula_type5(_adpDatas);
                }
            }

            if (R1No == "R01" || R1No == "R02" || R1No == "R03" || R1No == "R04")
            {
                if (_tcMmiFileInfo.FirstOrDefault().Adaper_No == "None")  //P41
                {
                    AWidth1 = AWidth2;
                    _adpDatas.FirstOrDefault().LBck = "LBstd";

                    if (Convert.ToString(type) == "1")
                    {
                        _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula(_adpDatas);
                    }

                    if (Convert.ToString(type) == "2")
                    {
                        _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula_P2(_adpDatas, G_Reducer_One_piece_chenged);
                    }
                }

                if (_tcMmiFileInfo.FirstOrDefault().Adaper_No == "None")
                {
                    AWidth1 = AWidth2;
                    _adpDatas.FirstOrDefault().LBck = "LBmax";

                    if (Convert.ToString(type) == "1")
                    {
                        _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula(_adpDatas);
                    }

                    if (Convert.ToString(type) == "2")
                    {
                        _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula_P2(_adpDatas, G_Reducer_One_piece_chenged);
                    }
                }
            }
            //=================================================  
            // 8000系列如下
            //=================================================  
            if (_tcMmiFileInfo.FirstOrDefault().Adaper_No == "None")
            {
                AWidth1 = l_AWidth1;
                _adpDatas.FirstOrDefault().TableName = AdapterData2;
                _adpDatas.FirstOrDefault().LBck = "LBstd";
                if (Convert.ToString(type) == "1")
                {
                    _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula(_adpDatas);
                }

                if (Convert.ToString(type) == "2")
                {
                    _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula_P2(_adpDatas, G_Reducer_One_piece_chenged);
                }
            }

            if (_tcMmiFileInfo.FirstOrDefault().Adaper_No == "None")
            {
                AWidth1 = l_AWidth1;
                _adpDatas.FirstOrDefault().TableName = AdapterData2;
                _adpDatas.FirstOrDefault().LBck = "LBmax";
                if (Convert.ToString(type) == "1")
                {
                    _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula(_adpDatas);
                }

                if (Convert.ToString(type) == "2")
                {
                    _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula_P2(_adpDatas, G_Reducer_One_piece_chenged);
                }
            }

            if (R1No == "R01" || R1No == "R02" || R1No == "R03" || R1No == "R04")
            {
                if (_tcMmiFileInfo.FirstOrDefault().Adaper_No == "None")  //P41
                {
                    AWidth1 = AWidth2;
                    _adpDatas.FirstOrDefault().TableName = AdapterData2;
                    _adpDatas.FirstOrDefault().LBck = "LBstd";

                    if (Convert.ToString(type) == "1")
                    {
                        _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula(_adpDatas);
                    }

                    if (Convert.ToString(type) == "2")
                    {
                        _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula_P2(_adpDatas, G_Reducer_One_piece_chenged);
                    }
                }

                if (_tcMmiFileInfo.FirstOrDefault().Adaper_No == "None")
                {
                    AWidth1 = AWidth2;
                    _adpDatas.FirstOrDefault().TableName = AdapterData2;
                    _adpDatas.FirstOrDefault().LBck = "LBmax";
                    if (Convert.ToString(type) == "1")
                    {
                        _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula(_adpDatas);
                    }

                    if (Convert.ToString(type) == "2")
                    {
                        _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula_P2(_adpDatas, G_Reducer_One_piece_chenged);
                    }
                }
            }
            //================================================= 
            // 7000系列如下    需加固定版
            //================================================= 
            if (_tcMmiFileInfo.FirstOrDefault().Adaper_No == "None")
            {
                AWidth1 = l_AWidth1;
                _adpDatas.FirstOrDefault().TableName = AdapterData3;
                _adpDatas.FirstOrDefault().LBck = "LBstd";

                if (Convert.ToString(type) == "1")
                {
                    _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula(_adpDatas);
                }

                if (Convert.ToString(type) == "2")
                {
                    _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula_P2(_adpDatas, G_Reducer_One_piece_chenged);
                }
            }

            if (_tcMmiFileInfo.FirstOrDefault().Adaper_No == "None")
            {
                AWidth1 = l_AWidth1;
                _adpDatas.FirstOrDefault().TableName = AdapterData3;
                _adpDatas.FirstOrDefault().LBck = "LBmax";
                if (Convert.ToString(type) == "1")
                {
                    _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula(_adpDatas);
                }

                if (Convert.ToString(type) == "2")
                {
                    _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula_P2(_adpDatas, G_Reducer_One_piece_chenged);
                }
            }

            if (R1No == "R01" || R1No == "R02" || R1No == "R03" || R1No == "R04")
            {
                if (_tcMmiFileInfo.FirstOrDefault().Adaper_No == "None")  //P41
                {
                    AWidth1 = AWidth2;
                    _adpDatas.FirstOrDefault().TableName = AdapterData3;
                    _adpDatas.FirstOrDefault().LBck = "LBstd";
                    if (Convert.ToString(type) == "1")
                    {
                        _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula(_adpDatas);
                    }

                    if (Convert.ToString(type) == "2")
                    {
                        _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula_P2(_adpDatas, G_Reducer_One_piece_chenged);
                    }
                }

                if (_tcMmiFileInfo.FirstOrDefault().Adaper_No == "None")
                {
                    AWidth1 = AWidth2;
                    _adpDatas.FirstOrDefault().TableName = AdapterData3;
                    _adpDatas.FirstOrDefault().LBck = "LBmax";
                    if (Convert.ToString(type) == "1")
                    {
                        _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula(_adpDatas);
                    }

                    if (Convert.ToString(type) == "2")
                    {
                        _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula_P2(_adpDatas, G_Reducer_One_piece_chenged);
                    }
                }
            }

            //================================================= 
            // 9000系列如下    無庫存需全新設計
            //================================================= 

            if (_tcMmiFileInfo.FirstOrDefault().Adaper_No == "None")
            {
                AWidth1 = l_AWidth1;
                _adpDatas.FirstOrDefault().TableName = AdapterData4;
                _adpDatas.FirstOrDefault().LBck = "LBstd";

                if (Convert.ToString(type) == "1")
                {
                    _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula(_adpDatas);
                }

                if (Convert.ToString(type) == "2")
                {
                    _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula_P2(_adpDatas, G_Reducer_One_piece_chenged);
                }
            }

            if (_tcMmiFileInfo.FirstOrDefault().Adaper_No == "None")
            {
                AWidth1 = l_AWidth1;
                _adpDatas.FirstOrDefault().TableName = AdapterData4;
                _adpDatas.FirstOrDefault().LBck = "LBmax";

                if (Convert.ToString(type) == "1")
                {
                    _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula(_adpDatas);
                }

                if (Convert.ToString(type) == "2")
                {
                    _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula_P2(_adpDatas, G_Reducer_One_piece_chenged);
                }
            }


            if (R1No == "R01" || R1No == "R02" || R1No == "R03" || R1No == "R04")
            {
                if (_tcMmiFileInfo.FirstOrDefault().Adaper_No == "None")  //P41
                {
                    AWidth1 = AWidth2;
                    _adpDatas.FirstOrDefault().TableName = AdapterData4;
                    _adpDatas.FirstOrDefault().LBck = "LBstd";

                    if (Convert.ToString(type) == "1")
                    {
                        _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula(_adpDatas);
                    }

                    if (Convert.ToString(type) == "2")
                    {
                        _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula_P2(_adpDatas, G_Reducer_One_piece_chenged);
                    }
                }

                if (_tcMmiFileInfo.FirstOrDefault().Adaper_No == "None")
                {
                    AWidth1 = AWidth2;
                    _adpDatas.FirstOrDefault().TableName = AdapterData4;
                    _adpDatas.FirstOrDefault().LBck = "LBmax";

                    if (Convert.ToString(type) == "1")
                    {
                        _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula(_adpDatas);
                    }

                    if (Convert.ToString(type) == "2")
                    {
                        _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula_P2(_adpDatas, G_Reducer_One_piece_chenged);
                    }
                }
            }

            //================================================= 
            // 自動開發系列如下
            //=================================================

            adpter_tc_mmaa_file = "false";

            if (CustId != "D000482") //只有D00482測試===begin
            {
                if (_tcMmiFileInfo.FirstOrDefault().Adaper_No == "None")
                {
                    AWidth1 = l_AWidth1;
                    _adpDatas.FirstOrDefault().TableName = AdapterData5;
                    _adpDatas.FirstOrDefault().LBck = "LBstd";

                    if (Convert.ToString(type) == "1")
                    {
                        _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula(_adpDatas);
                    }

                    if (Convert.ToString(type) == "2")
                    {
                        _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula_P2(_adpDatas, G_Reducer_One_piece_chenged);
                    }

                    if (Convert.ToString(type) == "5")
                    {
                        _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula_type5(_adpDatas);
                    }

                    //代表在自動開發連接板暫存
                    if (_tcMmiFileInfo.FirstOrDefault().Adaper_No != "None")
                    {
                        adpter_tc_mmaa_file = "true";
                    }
                }

                if (_tcMmiFileInfo.FirstOrDefault().Adaper_No == "None")
                {
                    AWidth1 = l_AWidth1;
                    _adpDatas.FirstOrDefault().TableName = AdapterData5;
                    _adpDatas.FirstOrDefault().LBck = "LBmax";

                    if (Convert.ToString(type) == "1")
                    {
                        _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula(_adpDatas);
                    }

                    if (Convert.ToString(type) == "2")
                    {
                        _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula_P2(_adpDatas, G_Reducer_One_piece_chenged);
                    }

                    if (Convert.ToString(type) == "5")
                    {
                        _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula_type5(_adpDatas);
                    }

                    //代表在自動開發連接板暫存
                    if (_tcMmiFileInfo.FirstOrDefault().Adaper_No != "None")
                    {
                        adpter_tc_mmaa_file = "true";
                    }
                }

                if (R1No == "R01" || R1No == "R02" || R1No == "R03" || R1No == "R04") //先抓P0401 在抓P0403
                {
                    if (_tcMmiFileInfo.FirstOrDefault().Adaper_No == "None")  //P41
                    {
                        AWidth1 = AWidth2;
                        _adpDatas.FirstOrDefault().TableName = AdapterData5;
                        _adpDatas.FirstOrDefault().LBck = "LBstd";
                        if (Convert.ToString(type) == "1")
                        {
                            _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula(_adpDatas);
                        }

                        if (Convert.ToString(type) == "2")
                        {
                            _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula_P2(_adpDatas, G_Reducer_One_piece_chenged);
                        }

                        //代表在自動開發連接板暫存
                        if (_tcMmiFileInfo.FirstOrDefault().Adaper_No != "None")
                        {
                            adpter_tc_mmaa_file = "true";
                        }
                    }

                    if (_tcMmiFileInfo.FirstOrDefault().Adaper_No == "None")
                    {
                        AWidth1 = AWidth2;
                        _adpDatas.FirstOrDefault().TableName = AdapterData5;
                        _adpDatas.FirstOrDefault().LBck = "LBmax";
                        if (Convert.ToString(type) == "1")
                        {
                            _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula(_adpDatas);
                        }

                        if (Convert.ToString(type) == "2")
                        {
                            _tcMmiFileInfo = _publicFunction.GetService<PublicFunctions>().Formula_P2(_adpDatas, G_Reducer_One_piece_chenged);
                        }

                        //代表在自動開發連接板暫存
                        if (_tcMmiFileInfo.FirstOrDefault().Adaper_No != "None")
                        {
                            adpter_tc_mmaa_file = "true";
                        }
                    }
                }
            }
            //=======================================================================================  
            //adapter 訊息處理
            //包含 conjunction選配
            //=======================================================================================  
            addbuy_error = "false";

            if (_tcMmiFileInfo.FirstOrDefault().Adaper_No == "None")
            {
                //配不到連接板,將相關資訊寫入柱列
                //取文件名稱====================begin
                string l_docNumber;

                if (Convert.ToString(type) == "1" || Convert.ToString(type) == "5")
                {
                    if (GBType.Substring(0, 2) == "AD")
                    {
                        if (Ratio == "16" || Ratio == "21" || Ratio == "31" || Ratio == "61" || Ratio == "91")
                        {
                            file_ratio_x = "B2";
                        }
                    }
                    //2017040701 AB有S3 所以增加S3的檔名
                    if (Shaft == "S3")
                    {
                        l_docNumber = (GBType.Replace("(Low friction)", "")).Replace("M1", "").Replace("M2", "") + file_ratio_x + "_S3";
                    }
                    else
                    {
                        l_docNumber = (GBType.Replace("(Low friction)", "")).Replace("M1", "").Replace("M2", "") + file_ratio_x;
                    }
                }
                else if (Convert.ToString(type) == "2")
                {
                    if ("RD1,RD2,RD3,RD4".Contains(GBSeries))
                    {
                        l_docNumber = GBType.Replace("Ⅱ", "2").Substring(0, 4) + "-" + GBType.Replace("Ⅱ", "2").Substring(4) + "_" + ratio_x + "ST" + "_" + _publicFunction.GetService<PublicFunctions>().Get_NO(Convert.ToSingle(M3maxWeb));
                    }
                    else if (GBSeries == "RD5")
                    {
                        l_docNumber = GBType.Replace("Ⅱ", "2").Substring(0, 4) + "-" + GBType.Replace("Ⅱ", "2").Substring(4).Replace("_", "") + "_" + ratio_x + "ST" + "_" + _publicFunction.GetService<PublicFunctions>().Get_NO(Convert.ToSingle(M3maxWeb));
                    }
                    else if ("RE1,RD9,RE2,RE9,R40,RF2,RB3,RA9,RB2,RB4,RF4,RE4,RE3,RJ5,RJ9,RK5,RK9,R53,R54,RF1,RR1,RR2,RR3,RR4,RR5,RR6,RR8,RR9,RR7,RS4,RS3,RS2,RS1,RS5,RS6,RS7,R42".Contains(GBSeries))
                    {
                        if (("R40,RB3,R53,R54,RR1,RR3,RS5,RS6".Contains(GBSeries)) && ("S3,S4".Contains(GBSeries)))
                        {
                            l_docNumber = GBType + "_" + Shaft + "_" + ratio_x + "ST" + "_" + _publicFunction.GetService<PublicFunctions>().Get_NO(Convert.ToSingle(M3maxWeb));
                        }
                        else if ("RF2,RR2".Contains(R1No) || (("RJ5,RK5,RS4,RS2".Contains(GBSeries)) && (Convert.ToDouble(Ratio.Replace("*", "")) == 5.5 || Convert.ToDouble(Ratio.Replace("*", "")) == 11 || Convert.ToDouble(Ratio.Replace("*", "")) == 4 || Convert.ToDouble(Ratio.Replace("*", "")) == 8)))
                        {
                            l_docNumber = GBType + "_" + ratio_x + "ST_SBG" + "_" + _publicFunction.GetService<PublicFunctions>().Get_NO(Convert.ToSingle(M3maxWeb));
                        }
                        else
                        {
                            l_docNumber = GBType + "_" + ratio_x + "ST" + "_" + _publicFunction.GetService<PublicFunctions>().Get_NO(Convert.ToSingle(M3maxWeb));
                        }
                    }
                    else
                    {
                        l_docNumber = GBType.Replace("Ⅱ", "2").Substring(0, 3) + "-" + GBType.Replace("Ⅱ", "2").Substring(3) + "_" + ratio_x + "ST" + "_" + _publicFunction.GetService<PublicFunctions>().Get_NO(Convert.ToSingle(M3maxWeb));
                    }
                }
                else if (Convert.ToString(type) == "3")
                {
                    l_docNumber = GBType.Replace("(Low friction)", "") + Convert.ToString(ratio_x);
                }
                else if (Convert.ToString(type) == "4")
                {
                    l_docNumber = Convert.ToString(GBType) + Convert.ToString(ratio_x);
                }
                else
                {
                    l_docNumber = "UnKnown Type";
                }

                //取文件名稱====================end
                string l_LZ_temp = "0";
                string l_LZ1_temp = "";

                if (_adpDatas.FirstOrDefault().Motor_Screw_orientation == "Y")
                {
                    l_LZ_temp = "0";
                    l_LZ1_temp = Convert.ToString(LZ1);
                }
                else
                {
                    l_LZ_temp = LZ;
                    l_LZ1_temp = "";
                }
                if (tc_shw13 == "" || (tc_shw13 != "" && Convert.ToDouble(LB) >= Convert.ToDouble(tc_shw13)) || (tc_shw13 != "" && Convert.ToDouble(LB) == 0))
                {
                    List<TcMmlFile> TcMmlFileData = new List<TcMmlFile>();
                    TcMmlFileData.Add(
                        new TcMmlFile
                        {
                            TcMml02 = Convert.ToString(ReducerInfo.FirstOrDefault().TcMmd01),
                            TcMml03 = l_docNumber,
                            TcMml05 = Convert.ToString(OData["Motor"]["Brand"]),
                            TcMml06 = Convert.ToString(OData["Motor"]["Spec"]),
                            TcMml07 = Convert.ToDecimal(LA),
                            TcMml08 = Convert.ToDecimal(LB),
                            TcMml09 = Convert.ToDecimal(LC),
                            TcMml10 = Convert.ToDecimal(C3),
                            TcMml11 = Convert.ToDecimal(l_LZ_temp),
                            TcMml12 = Convert.ToDecimal(LR),
                            TcMml13 = Convert.ToDecimal(LT),
                            TcMml14 = Convert.ToDecimal(LE),
                            TcMml15 = Convert.ToDecimal(FlangeWidth),
                            TcMml16 = orderCode,
                            TcMml18 = CustId,
                            TcMml23 = Motor_Interface,
                            TcMml24 = Motor_Screw_orientation,
                            TcMml25 = suitadapter,
                            TcMml26 = l_LZ1_temp
                        });
                    var TcMmlFileInfo = _orderService.GetService<OrderService>().GetTcMmlFileInfo(TcMmlFileData);
                    if (TcMmlFileInfo.Count == 0)  //寫入tc_mml_file前判斷是否有同樣資料======begin
                    {
                        TcMmlFileData.FirstOrDefault().TcMml01 = _publicFunction.GetService<PublicFunctions>().GetTcMml01();
                        TcMmlFileData.FirstOrDefault().TcMml04 = "";
                        TcMmlFileData.FirstOrDefault().TcMml07 = Convert.ToString(OData["email"]);
                        TcMmlFileData.FirstOrDefault().TcMml19 = "0";
                        TcMmlFileData.FirstOrDefault().TcMml20 = "";
                        TcMmlFileData.FirstOrDefault().TcMml21 = "";
                        TcMmlFileData.FirstOrDefault().TcMml22 = "N";
                        _orderService.GetService<OrderService>().InsertTcMmlFile(TcMmlFileData);
                    }
                    else
                    {
                        errMsg = "Error !";//Resources.Resource.msg023
                    }

                    if (addbuy == "false")
                    {
                        errMsg = "Error !";//Resources.Resource.msg_err2
                    }
                    else
                    {
                        errMsg = "Error !";//Resources.Resource.msg_err2
                        addbuy_error = "true";
                    }
                }
            }
            else
            {
                if (G_Reducer_One_piece_chenged == "Y")
                {
                    txtadaperNo =  _tcMmiFileInfo.ToList().FirstOrDefault().G_Reducer_One_piece_old_Adapter_No;
                }
                else
                {
                    txtadaperNo = adaperNo;
                }

                if (Convert.ToString(adpter_tc_mmaa_file) == "false")// true 代表在自動開發連接板暫存
                {
                    g_sql = "SELECT * FROM tc_mma_file where tc_mma01 ='" + txtadaperNo + "'";
                }
                else
                {
                    g_sql = "SELECT tc_mmaa01 \"TC_MMA01\",tc_mmaa02 \"TC_MMA02\" ,tc_mmaa03 \"TC_MMA03\",tc_mmaa05 \"TC_MMA05\",tc_mmaa06 \"TC_MMA06\",tc_mmaa15 \"TC_MMA15\" ,tc_mmaa18 \"TC_MMA18\",tc_mmaa19 \"TC_MMA19\",tc_mmaa20 \"TC_MMA20\",tc_mmaa21 \"TC_MMA21\",tc_mmaa22 \"TC_MMA22\",tc_mmaa25 \"TC_MMA25\",tc_mmaa26 \"TC_MMA26\",tc_mmaa27 \"TC_MMA27\",tc_mmaa35 \"TC_MMA35\" FROM tc_mmaa_file WHERE tc_mmaa01 ='" + txtadaperNo + "'";
                }

                OracleDataReader myDataReader_3 = class_nana_ds1.ORACLE_RD(g_sql);

                if (myDataReader_3.Read())
                {
                    ViewState["Adaper_No"] = Convert.ToString(myDataReader_3["tc_mma01"]);

                    ViewState["Adaper_Spec"] = Convert.ToString(myDataReader_3["tc_mma03"]);

                    ViewState["Adaper_PicYN"] = Convert.ToString(myDataReader_3["tc_mma25"]);

                    ViewState["g_Adapter_Cus"] = Convert.ToString(myDataReader_3["tc_mma35"]);

                    DMpic = R1No + "_St" + ratio_x + "_tp" + Convert.ToString(Convert.ToInt16(myDataReader_3["tc_mma19"])) + ".gif";

                    conjunction = Convert.ToString(myDataReader_3["tc_mma22"]);

                    if (Convert.ToString(Convert.ToInt16(myDataReader_3["tc_mma18"])) == "3")
                    {
                        adp7000 = "[" + Convert.ToString(myDataReader_3["tc_mma21"]) + " - " + conjunction + "]";
                    }
                    else
                    {
                        adp7000 = "None";
                    }
                }

                ViewState["plate_1"] = null;

                ViewState["plate_2"] = null;

                //ViewState["plate_3"] = null;

                if (Convert.ToString(type) == "1" || Convert.ToString(type) == "2" || Convert.ToString(type) == "5")
                {
                    ViewState["class"] = Convert.ToString(Convert.ToInt16(myDataReader_3["tc_mma18"]));

                    ViewState["plate_1"] = Convert.ToString(myDataReader_3["tc_mma01"]); //料號

                    ViewState["plate_2"] = Convert.ToString(myDataReader_3["tc_mma03"]); //規格

                    //ViewState["plate_3"] = Convert.ToString(myDataReader_3["tc_mma02"]); //舊料號

                    AdapterScrew = Convert.ToString(myDataReader_3["tc_mma05"]).Replace(".000", "");

                    AdapterPitch = Convert.ToString(myDataReader_3["tc_mma06"]).Replace(".000", "");

                    ViewState["C11"] = Convert.ToSingle(myDataReader_3["tc_mma20"]);
                }

                if (Convert.ToString(type) == "4")
                {
                    ViewState["C10"] = Convert.ToSingle(myDataReader_3["tc_mma15"]);

                    ViewState["C21"] = Convert.ToSingle(myDataReader_3["tc_mma05"]);

                    ViewState["C22"] = Convert.ToSingle(myDataReader_3["tc_mma06"]);

                    AdapterPitch = Convert.ToString(myDataReader_3["tc_mma06"]).Replace(".000", "");

                }

                //換一體式連接板
                if (Convert.ToString(ViewState["g_Reducer_One_piece"]) == "Y")
                {
                    if (Convert.ToString(ViewState["g_Reducer_One_piece_used"]) == "Y")
                    {
                        //先關閉等生管通知
                        Fun_replace_one_piece(Convert.ToString(ViewState["Reducer_No"]), Convert.ToString(ViewState["Adaper_No"]));
                    }
                }
                //

                if (Convert.ToString(type) == "1" || Convert.ToString(type) == "5")
                {
                    if (GBType.Substring(0, 2) == "AD")
                    {
                        if (Ratio == "16" || Ratio == "21" || Ratio == "31" || Ratio == "61" || Ratio == "91")
                        {
                            file_ratio_x = "B2";
                        }
                    }

                    if (Convert.ToString(Convert.ToInt16(myDataReader_3["tc_mma18"])) == "3")
                    {
                        //sFile = (GBType.Replace("(Low friction)", "")).Replace("M1", "").Replace("M2", "") + file_ratio_x + "-" + Convert.ToString(myDataReader_3["adpter_really"]);
                        sFile = (GBType.Replace("(Low friction)", "")).Replace("M1", "").Replace("M2", "") + file_ratio_x + "-" + Convert.ToString(myDataReader_3["tc_mma21"]);
                    }
                    else
                    {
                        //2017040701 AB有S3 所以增加S3的檔名
                        if (Shaft == "S3")
                        {
                            //sFile = (GBType.Replace("(Low friction)", "")).Replace("M1", "").Replace("M2", "") + file_ratio_x + "_S3-" + Convert.ToString(myDataReader_3["tc_mma01"]);
                            sFile = (GBType.Replace("(Low friction)", "")).Replace("M1", "").Replace("M2", "") + file_ratio_x + "_S3-" + Convert.ToString(ViewState["plate_1"]);
                        }
                        else
                        {
                            //sFile = (GBType.Replace("(Low friction)", "")).Replace("M1", "").Replace("M2", "") + file_ratio_x + "-" + Convert.ToString(myDataReader_3["tc_mma01"]);
                            sFile = (GBType.Replace("(Low friction)", "")).Replace("M1", "").Replace("M2", "") + file_ratio_x + "-" + Convert.ToString(ViewState["plate_1"]);
                        }
                    }
                }

                if (Convert.ToString(type) == "2")
                {
                    if (this.lst_series.SelectedValue == "RD1" || this.lst_series.SelectedValue == "RD2" || this.lst_series.SelectedValue == "RD3" || this.lst_series.SelectedValue == "RD4")
                    {
                        sFile = GBType.Replace("Ⅱ", "2").Substring(0, 4) + "-" + GBType.Replace("Ⅱ", "2").Substring(4) + "_" + ratio_x + "ST" + "_" + get_NO(Convert.ToSingle(M3maxWeb)) + "-" + Convert.ToString(ViewState["plate_1"]);
                    }
                    else if (this.lst_series.SelectedValue == "RD5")
                    {
                        sFile = GBType.Replace("Ⅱ", "2").Substring(0, 4) + "-" + GBType.Replace("Ⅱ", "2").Substring(4).Replace("_", "") + "_" + ratio_x + "ST" + "_" + get_NO(Convert.ToSingle(M3maxWeb)) + "-" + Convert.ToString(ViewState["plate_1"]);
                    }
                    else if (this.lst_series.SelectedValue == "RE1" || this.lst_series.SelectedValue == "RD9" || this.lst_series.SelectedValue == "RE2" || this.lst_series.SelectedValue == "RE9" || this.lst_series.SelectedValue == "R40" || this.lst_series.SelectedValue == "RF2" || this.lst_series.SelectedValue == "RB3" || this.lst_series.SelectedValue == "RA9" || this.lst_series.SelectedValue == "RB2" || this.lst_series.SelectedValue == "RB4" || this.lst_series.SelectedValue == "RF4" || this.lst_series.SelectedValue == "RE4" || this.lst_series.SelectedValue == "RE3" || this.lst_series.SelectedValue == "RJ5" || this.lst_series.SelectedValue == "RJ9" || this.lst_series.SelectedValue == "RK5" || this.lst_series.SelectedValue == "RK9" || this.lst_series.SelectedValue == "R53" || this.lst_series.SelectedValue == "R54" || this.lst_series.SelectedValue == "RF1"
                                                                                                                                                                                                  || this.lst_series.SelectedValue == "RR1" || this.lst_series.SelectedValue == "RR2" || this.lst_series.SelectedValue == "RR3" || this.lst_series.SelectedValue == "RR4" || this.lst_series.SelectedValue == "RR5" || this.lst_series.SelectedValue == "RR6" || this.lst_series.SelectedValue == "RR8" || this.lst_series.SelectedValue == "RR9" || this.lst_series.SelectedValue == "RR7" || this.lst_series.SelectedValue == "RS4" || this.lst_series.SelectedValue == "RS3" || this.lst_series.SelectedValue == "RS2" || this.lst_series.SelectedValue == "RS1" || this.lst_series.SelectedValue == "RS5" || this.lst_series.SelectedValue == "RS6" || this.lst_series.SelectedValue == "RS7"
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          || this.lst_series.SelectedValue == "R42")
                    {
                        if ((this.lst_series.SelectedValue == "R40" || this.lst_series.SelectedValue == "RB3" || this.lst_series.SelectedValue == "R53" || this.lst_series.SelectedValue == "R54"
                          || this.lst_series.SelectedValue == "RR1" || this.lst_series.SelectedValue == "RR3" || this.lst_series.SelectedValue == "RS5" || this.lst_series.SelectedValue == "RS6") && (lst_Shaft.SelectedValue == "S3" || lst_Shaft.SelectedValue == "S4"))
                        {
                            sFile = GBType + "_" + lst_Shaft.SelectedValue + "_" + ratio_x + "ST" + "_" + get_NO(Convert.ToSingle(M3maxWeb)) + "-" + Convert.ToString(ViewState["plate_1"]);
                        }
                        else if (R1No == "RF2" || R1No == "RR2" || ((lst_series.SelectedValue == "RJ5" || lst_series.SelectedValue == "RK5" || lst_series.SelectedValue == "RS4" || lst_series.SelectedValue == "RS2") && (Convert.ToDouble(lst_ratio.SelectedValue.Replace("*", "")) == 5.5 || Convert.ToDouble(lst_ratio.SelectedValue.Replace("*", "")) == 11 || Convert.ToDouble(lst_ratio.SelectedValue.Replace("*", "")) == 4 || Convert.ToDouble(lst_ratio.SelectedValue.Replace("*", "")) == 8)))
                        {
                            sFile = GBType + "_" + ratio_x + "ST_SBG" + "_" + get_NO(Convert.ToSingle(M3maxWeb)) + "-" + Convert.ToString(ViewState["plate_1"]);
                        }
                        else
                        {
                            sFile = GBType + "_" + ratio_x + "ST" + "_" + get_NO(Convert.ToSingle(M3maxWeb)) + "-" + Convert.ToString(ViewState["plate_1"]);
                        }
                    }
                    else
                    {
                        sFile = GBType.Replace("Ⅱ", "2").Substring(0, 3) + "-" + GBType.Replace("Ⅱ", "2").Substring(3) + "_" + ratio_x + "ST" + "_" + get_NO(Convert.ToSingle(M3maxWeb)) + "-" + Convert.ToString(ViewState["plate_1"]);
                    }

                }

                if (Convert.ToString(type) == "3")
                {
                    sFile = GBType.Replace("(Low friction)", "") + Convert.ToString(ratio_x);
                }

                if (Convert.ToString(type) == "4")
                {
                    sFile = Convert.ToString(GBType) + Convert.ToString(ratio_x) + "-" + Convert.ToString(txtadaperNo);
                }

                ViewState["sFile"] = Convert.ToString(sFile);

                myDataReader_3.Close();
            }


        }
    }
}
