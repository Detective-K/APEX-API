using APEX_API.Models;
using APEX_API.PublicServices;
using APEX_API.TopprodModels;
using Microsoft.EntityFrameworkCore;
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
        private readonly PublicFunctions _publicFunction;

        public OrderService(web2Context context, DataContext oracontext, PublicFunctions publicFunctions)
        {
            _web2Context = context;
            _DataContext = oracontext;
            _publicFunction = publicFunctions;
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

        public List<TcOekFile> GetMotorInfo(string isSale)
        {
            var MotorInfo = _DataContext.TcOekFiles.AsQueryable();
            if (isSale == "Y")
            {
                MotorInfo = MotorInfo.Where(m => ("YES,EIP").ToUpper().Contains(m.TcOek21.ToUpper())).Select(m => new TcOekFile { TcOek01 = m.TcOek01 }).Distinct();
            }
            else
            {
                MotorInfo = MotorInfo.Where(m => m.TcOek21.ToUpper().Contains("YES")).Select(m => new TcOekFile { TcOek01 = m.TcOek01 }).Distinct();
            }
            MotorInfo = MotorInfo.OrderBy(m => m.TcOek01);
            return MotorInfo.ToList();
        }

        public List<TcOekFile> GetMotorInfoDetail(dynamic OData)
        {
            string _tcOek01 = Convert.ToString(OData["Brand"]);
            string _tcOek02 = Convert.ToString(OData["Spec"]);
            var MotorInfo = _DataContext.TcOekFiles.AsQueryable();
            if (!string.IsNullOrEmpty(_tcOek01))
            {
                MotorInfo = MotorInfo.Where(m => m.TcOek01 == _tcOek01);
            }
            if (!string.IsNullOrEmpty(_tcOek02))
            {
                MotorInfo = MotorInfo.Where(m => m.TcOek02 == _tcOek02);
            }
            return MotorInfo.ToList();
        }

        public List<Reducer1Order> GetReducerInfo(dynamic OData)
        {
            string _tc_mmd03 = Convert.ToString(OData["GBModel"]);
            decimal _tc_mmd04 = Convert.ToString(OData["Ratio"]) == "" ? 0 : Convert.ToDecimal(OData["Ratio"]);
            var ReducerInfo = _DataContext.Reducer1Orders.AsQueryable();
            if (!string.IsNullOrEmpty(_tc_mmd03))
            {
                ReducerInfo = ReducerInfo.Where(r => r.TcMmd03 == _tc_mmd03);
            }
            if (_tc_mmd04 != 0)
            {
                ReducerInfo = ReducerInfo.Where(r => r.TcMmd04 == _tc_mmd04);
            }
            return ReducerInfo.ToList();
        }
        public List<Resg> GetResgInfo(dynamic OData)
        {
            string _tc_mmd01 = Convert.ToString(OData["Ratio"]);
            var ResgInfo = _DataContext.Resgs.AsQueryable();
            if (!string.IsNullOrEmpty(_tc_mmd01))
            {
                ResgInfo = ResgInfo.Where(r => r.TcMmd01 == _tc_mmd01);
            }
            return ResgInfo.ToList();
        }

        public List<TcOelFile> GetTcOelFileInfo(dynamic OData)
        {
            string _tcOel01 = Convert.ToString(OData["Brand"]);
            string _tcOel02 = Convert.ToString(OData["Spec"]);
            var TcOelFileInfo = _DataContext.TcOelFiles.AsQueryable();
            if (!string.IsNullOrEmpty(_tcOel01))
            {
                TcOelFileInfo = TcOelFileInfo.Where(m => m.TcOel01 == _tcOel01);
            }
            if (!string.IsNullOrEmpty(_tcOel02))
            {
                TcOelFileInfo = TcOelFileInfo.Where(m => m.TcOel02 == _tcOel02);
            }
            return TcOelFileInfo.ToList();
        }


        public List<TcShwFile> GetTcShwFileInfo(string suitAdapter)
        {
            var TcShwFileInfo = _DataContext.TcShwFiles.AsQueryable();
            if (!string.IsNullOrEmpty(suitAdapter))
            {
                TcShwFileInfo = TcShwFileInfo.Where(tS => tS.TcShw01 == suitAdapter);
            }
            return TcShwFileInfo.ToList();
        }

        public List<TcMmiFile> GetTcMmiFileInfo(string _tcMmi04)
        {
            var TcMmiFileInfo = _DataContext.TcMmiFiles.AsQueryable();
            if (!string.IsNullOrEmpty(_tcMmi04))
            {
                TcMmiFileInfo = TcMmiFileInfo.Where(tM => tM.TcMmi05 == "Y" && tM.TcMmi04 == _tcMmi04);
            }
            return TcMmiFileInfo.ToList();
        }

        // Gearbox Model & Ratio
        public List<Reducer1Order> GetReducer(dynamic OData, Decimal T1N, Decimal T1B, Decimal Inertia, Decimal S, string item, string _TcMmd03, string MotorScrewOrientation)
        {
            string _TcMmd01 = Convert.ToString(OData["GBSeries"]);
            Decimal _InertiaApp = Convert.ToString(OData["InertiaApp"]) == "" ? 0 : Convert.ToDecimal(OData["InertiaApp"]);
            string R14Groups = "R14,R26,R28,R30,R32,R57,R58,R59,R60,RB6,RB8,RC1,RC3,RC7,RC8,RC9,RC5";//tmp_type 1,
            string R21Groups = "R21,R22,R23,R24,R64,R27,R29,R31,R33,R61,RB7,RB9,RC2,RC4,RC6";//tmp_type 2,
            string R65Groups = "R65,R66,R67,R69,R86,RD1,RD2,RD3,RD4,RD5,RE1,RD9,RE2,RE9,R40,RF2,RB3,RA9,RB2,RB4,RF4,RE3,RE4,RJ9,RJ5,RK9,RK5,R53,R54,RF1,RR1,RR2,RR3,RR4,RR5,RR6,RR7,RR8,RR9,RS3,RS4,RS1,RS2,RS5,RS6,RS7,R42";//tmp_type 3,
            string R25Groups = "R25";//tmp_type 5,
            string RedSpStr = "R27, R29, R31, R33, R61,RB7, RB9,RC2,RC4,RC6";//tmp_type 4,
            IQueryable<Reducer1Order> ReducerInfo = Enumerable.Empty<Reducer1Order>().AsQueryable();
            var Reducer1Info = _DataContext.Reducer1Orders.AsQueryable();
            var Reducer2Info = _DataContext.Reducer2Orders.AsQueryable();
            var Reducer3Info = _DataContext.Reducer3Orders.AsQueryable();

            switch (Convert.ToString(OData["Range"]))
            {
                case "1":
                    if ((R14Groups).Contains(_TcMmd01))
                    {
                        Reducer1Info = Reducer1Info.Where(r1 => EF.Functions.Like(r1.TcMmd01, _TcMmd01 + "%"));
                        if (T1N != 0)
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => r1.TcMmd07 >= T1N);
                        }
                        if (T1B != 0)
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => r1.TcMmd05 >= T1B);
                        }
                        if (!string.IsNullOrEmpty(_TcMmd03))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => r1.TcMmd03 == _TcMmd03);
                        }
                        ReducerInfo = Reducer1Info;
                    }
                    else if ((R21Groups).Contains(_TcMmd01))
                    {
                        if (item != "Ratio")
                        {
                            Reducer3Info = Reducer3Info.Where(r3 => EF.Functions.Like(r3.TcMmd01, _TcMmd01 + "%"));
                            Reducer2Info = Reducer2Info.Where(r2 => EF.Functions.Like(r2.TcMmd01, _TcMmd01 + "%"));
                            if (S != 0)
                            {
                                Reducer3Info = Reducer3Info.Where(r3 => r3.TcMmd16 >= S && r3.TcMmd16 <= S * Convert.ToDecimal(2.6));
                                Reducer2Info = Reducer2Info.Where(r2 => r2.TcMmd16 >= S && r2.TcMmd16 <= S * Convert.ToDecimal(2.6));
                            }
                            if (RedSpStr.Contains(_TcMmd01))
                            {
                                Reducer3Info = Reducer3Info.Where(r3 => (r3.TcMmd24 == 1 && (r3.TcMmd16 - S >= Convert.ToDecimal(1.3) || r3.TcMmd16 - S == 0) || r3.TcMmd24 != 1));
                                Reducer2Info = Reducer2Info.Where(r2 => (r2.TcMmd24 == 1 && (r2.TcMmd16 - S >= Convert.ToDecimal(1.3) || r2.TcMmd16 - S == 0) || r2.TcMmd24 != 1));
                            }
                            Reducer2Info = Reducer2Info.Where(r2 => !Reducer3Info.Select(r3 => r3.Re).Contains(r2.Re));
                            ReducerInfo = Reducer3Info
                                         .Select(r3 => new Reducer1Order { TcMmd03 = r3.TcMmd03 }).Distinct()
                                         .Union(Reducer2Info.Select(r2 => new Reducer1Order { TcMmd03 = r2.TcMmd03 }).Distinct());
                        }
                        else
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => EF.Functions.Like(r1.TcMmd01, _TcMmd01 + "%"));
                            if (S != 0)
                            {
                                Reducer1Info = Reducer1Info.Where(r1 => r1.TcMmd16 >= S && r1.TcMmd16 <= S * Convert.ToDecimal(2.6));
                            }
                            if (!string.IsNullOrEmpty(_TcMmd03))
                            {
                                Reducer1Info = Reducer1Info.Where(r1 => r1.TcMmd03 == _TcMmd03);
                            }
                            if (RedSpStr.Contains(_TcMmd01))
                            {
                                Reducer1Info = Reducer1Info.Where(r1 => (r1.TcMmd24 == 1 && (r1.TcMmd16 - S >= Convert.ToDecimal(1.3) || r1.TcMmd16 - S == 0) || r1.TcMmd24 != 1));
                            }

                            ReducerInfo = Reducer1Info;
                        }

                    }
                    else if ((R65Groups).Contains(_TcMmd01))
                    {
                        if ("R65,R66,R67,R69,R86".Contains(_TcMmd01))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => EF.Functions.Like(r1.TcMmd01, _TcMmd01 + "%") && !EF.Functions.Like(r1.TcMmd01, _TcMmd01 + "0%"));
                        }
                        else if ("R40,RF2".Contains(_TcMmd01))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => ("R40,RF2").Contains(r1.TcMmd01.Substring(0, 3)));
                        }
                        else if ("RR1,RR2".Contains(_TcMmd01))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => ("RR1,RR2").Contains(r1.TcMmd01.Substring(0, 3)));
                        }
                        else if ("RA9,RB2".Contains(_TcMmd01))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => ("RA9,RB2").Contains(r1.TcMmd01.Substring(0, 3)));
                        }
                        else if ("RR4,RR5".Contains(_TcMmd01))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => ("RR4,RR5").Contains(r1.TcMmd01.Substring(0, 3)));
                        }
                        else if ("RB4,RF4,RE3,RE4".Contains(_TcMmd01))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => ("RB4,RF4,RE3,RE4").Contains(r1.TcMmd01.Substring(0, 3)));
                        }
                        else if ("RR6,RR8,RR7,RR9".Contains(_TcMmd01))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => ("RR6,RR8,RR7,RR9").Contains(r1.TcMmd01.Substring(0, 3)));
                        }
                        else if ("R54,RF1".Contains(_TcMmd01))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => ("R54,RF1").Contains(r1.TcMmd01.Substring(0, 3)));
                        }
                        else if ("RS6,RS7".Contains(_TcMmd01))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => ("RS6,RS7").Contains(r1.TcMmd01.Substring(0, 3)));
                        }
                        else
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => EF.Functions.Like(r1.TcMmd01, _TcMmd01 + "%"));
                        }
                        if (!string.IsNullOrEmpty(_TcMmd03))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => r1.TcMmd03 == _TcMmd03);
                        }

                        if (S != 0)
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => r1.TcMmd16 == S).Count() > 0 ? Reducer1Info.Where(r1 => r1.TcMmd16 == S) : Reducer1Info.Where(r1 => r1.TcMmd16 == Convert.ToDecimal(_publicFunction.Get_NO(Convert.ToDouble(S))));
                        }

                        ReducerInfo = Reducer1Info;
                    }
                    else
                    {
                        if ((Convert.ToString(OData["custId"]) == "BAJ002" || Convert.ToString(OData["isSale"]) == "Y") && (("R19A,R20A").Contains(_TcMmd01)))
                        {
                            Reducer3Info = Reducer3Info.Where(r3 => ("R19A,R19B,R20A").Contains(r3.TcMmd01.Substring(0, 4)));
                            Reducer2Info = Reducer2Info.Where(r2 => ("R19A,R19B,R20A").Contains(r2.TcMmd01.Substring(0, 4)));
                            Reducer3Info = Reducer3Info.Where(r3 => EF.Functions.Like(r3.TcMmd01, _TcMmd01.Replace("A", "") + "%"));
                            Reducer2Info = Reducer2Info.Where(r2 => EF.Functions.Like(r2.TcMmd01, _TcMmd01.Replace("A", "") + "%"));
                        }
                        else if (("R19,R20").Contains(_TcMmd01))
                        {
                            Reducer3Info = Reducer3Info.Where(r3 => !("R19A,R19B,R20A").Contains(r3.TcMmd01.Substring(0, 4)));
                            Reducer2Info = Reducer2Info.Where(r2 => !("R19A,R19B,R20A").Contains(r2.TcMmd01.Substring(0, 4)));
                            Reducer3Info = Reducer3Info.Where(r3 => EF.Functions.Like(r3.TcMmd01, _TcMmd01 + "%"));
                            Reducer2Info = Reducer2Info.Where(r2 => EF.Functions.Like(r2.TcMmd01, _TcMmd01 + "%"));
                        }
                        else
                        {
                            Reducer3Info = Reducer3Info.Where(r3 => EF.Functions.Like(r3.TcMmd01, _TcMmd01 + "%"));
                            Reducer2Info = Reducer2Info.Where(r2 => EF.Functions.Like(r2.TcMmd01, _TcMmd01 + "%"));
                        }

                        if (S != 0)
                        {
                            Reducer3Info = Reducer3Info.Where(r3 => r3.TcMmd16 >= S && r3.TcMmd16 <= S * Convert.ToDecimal(2.6) && ((r3.TcMmd16 - S >= Convert.ToDecimal(1.3)) || (r3.TcMmd16 - S == Convert.ToDecimal(0))));
                            Reducer2Info = Reducer2Info.Where(r2 => r2.TcMmd16 >= S && r2.TcMmd16 <= S * Convert.ToDecimal(2.6) && ((r2.TcMmd16 - S >= Convert.ToDecimal(1.3)) || (r2.TcMmd16 - S == Convert.ToDecimal(0))));
                        }
                        if (!string.IsNullOrEmpty(_TcMmd03))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => r1.TcMmd03 == _TcMmd03);
                        }

                        Reducer2Info = Reducer2Info.Where(r2 => !Reducer3Info.Select(r3 => r3.Re).Contains(r2.Re));
                        ReducerInfo = Reducer3Info
                                     .Select(r3 => new Reducer1Order { TcMmd03 = r3.TcMmd03, TcMmd04 = r3.TcMmd04 }).Distinct()
                                     .Union(Reducer2Info.Select(r2 => new Reducer1Order { TcMmd03 = r2.TcMmd03, TcMmd04 = r2.TcMmd04 }).Distinct());
                    }
                    break;
                case "2":
                    if ((R14Groups).Contains(_TcMmd01))
                    {
                        Reducer1Info = Reducer1Info.Where(r1 => EF.Functions.Like(r1.TcMmd01, _TcMmd01 + "%"));
                        if (T1N != 0)
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => r1.TcMmd07 >= T1N);
                        }
                        if (T1B != 0)
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => r1.TcMmd05 >= T1B);
                        }
                        if (!string.IsNullOrEmpty(_TcMmd03))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => r1.TcMmd03 == _TcMmd03);
                        }
                        ReducerInfo = Reducer1Info;
                    }
                    else if ((R21Groups).Contains(_TcMmd01))
                    {
                        Reducer1Info = Reducer1Info.Where(r1 => EF.Functions.Like(r1.TcMmd01, _TcMmd01 + "%"));
                        if (S != 0)
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => r1.TcMmd16 >= S);
                        }
                        if (!string.IsNullOrEmpty(_TcMmd03))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => r1.TcMmd03 == _TcMmd03);
                        }
                        if (RedSpStr.Contains(_TcMmd01))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => (r1.TcMmd24 == 1 && (r1.TcMmd16 - S >= Convert.ToDecimal(1.3) || r1.TcMmd16 - S == 0) || r1.TcMmd24 != 1));
                        }

                        ReducerInfo = Reducer1Info;

                    }
                    else if ((R65Groups).Contains(_TcMmd01))
                    {
                        if ("R65,R66,R67,R69,R86".Contains(_TcMmd01))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => EF.Functions.Like(r1.TcMmd01, _TcMmd01 + "%") && !EF.Functions.Like(r1.TcMmd01, _TcMmd01 + "0%"));
                        }
                        else if ("R40,RF2".Contains(_TcMmd01))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => ("R40,RF2").Contains(r1.TcMmd01.Substring(0, 3)));
                        }
                        else if ("RR1,RR2".Contains(_TcMmd01))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => ("RR1,RR2").Contains(r1.TcMmd01.Substring(0, 3)));
                        }
                        else if ("RA9,RB2".Contains(_TcMmd01))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => ("RA9,RB2").Contains(r1.TcMmd01.Substring(0, 3)));
                        }
                        else if ("RR4,RR5".Contains(_TcMmd01))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => ("RR4,RR5").Contains(r1.TcMmd01.Substring(0, 3)));
                        }
                        else if ("RB4,RF4,RE3,RE4".Contains(_TcMmd01))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => ("RB4,RF4,RE3,RE4").Contains(r1.TcMmd01.Substring(0, 3)));
                        }
                        else if ("RR6,RR8,RR7,RR9".Contains(_TcMmd01))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => ("RR6,RR8,RR7,RR9").Contains(r1.TcMmd01.Substring(0, 3)));
                        }
                        else if ("R54,RF1".Contains(_TcMmd01))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => ("R54,RF1").Contains(r1.TcMmd01.Substring(0, 3)));
                        }
                        else if ("RS6,RS7".Contains(_TcMmd01))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => ("RS6,RS7").Contains(r1.TcMmd01.Substring(0, 3)));
                        }
                        else
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => EF.Functions.Like(r1.TcMmd01, _TcMmd01 + "%"));
                        }
                        if (!string.IsNullOrEmpty(_TcMmd03))
                        {
                            Reducer1Info = Reducer1Info.Where(r => r.TcMmd03 == _TcMmd03);
                        }

                        if (S != 0)
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => r1.TcMmd16 == S).Count() > 0 ? Reducer1Info.Where(r1 => r1.TcMmd16 == S || (r1.TcMmd16 > S && "8,11,14,19,24,28,32,35,38,42,48,55,60".Contains(Convert.ToString(r1.TcMmd16)))) : Reducer1Info.Where(r1 => r1.TcMmd16 == Convert.ToDecimal(_publicFunction.Get_NO(Convert.ToDouble(S))) || (r1.TcMmd16 > S && "8,11,14,19,24,28,32,35,38,42,48,55,60".Contains(Convert.ToString(r1.TcMmd16))));
                        }

                        ReducerInfo = Reducer1Info;
                    }
                    else
                    {
                        if ((Convert.ToString(OData["custId"]) == "BAJ002" || Convert.ToString(OData["isSale"]) == "Y") && (("R19A,R20A").Contains(_TcMmd01)))
                        {
                            Reducer3Info = Reducer3Info.Where(r3 => ("R19A,R19B,R20A").Contains(r3.TcMmd01.Substring(0, 4)));
                            Reducer2Info = Reducer2Info.Where(r2 => ("R19A,R19B,R20A").Contains(r2.TcMmd01.Substring(0, 4)));
                            Reducer3Info = Reducer3Info.Where(r3 => EF.Functions.Like(r3.TcMmd01, _TcMmd01.Replace("A", "") + "%"));
                            Reducer2Info = Reducer2Info.Where(r2 => EF.Functions.Like(r2.TcMmd01, _TcMmd01.Replace("A", "") + "%"));
                        }
                        else if (("R19,R20").Contains(_TcMmd01))
                        {
                            Reducer3Info = Reducer3Info.Where(r3 => !("R19A,R19B,R20A").Contains(r3.TcMmd01.Substring(0, 4)));
                            Reducer2Info = Reducer2Info.Where(r2 => !("R19A,R19B,R20A").Contains(r2.TcMmd01.Substring(0, 4)));
                            Reducer3Info = Reducer3Info.Where(r3 => EF.Functions.Like(r3.TcMmd01, _TcMmd01 + "%"));
                            Reducer2Info = Reducer2Info.Where(r2 => EF.Functions.Like(r2.TcMmd01, _TcMmd01 + "%"));
                        }
                        else
                        {
                            Reducer3Info = Reducer3Info.Where(r3 => EF.Functions.Like(r3.TcMmd01, _TcMmd01 + "%"));
                            Reducer2Info = Reducer2Info.Where(r2 => EF.Functions.Like(r2.TcMmd01, _TcMmd01 + "%"));
                        }

                        if (S != 0)
                        {
                            Reducer3Info = Reducer3Info.Where(r3 => r3.TcMmd16 >= S && ((r3.TcMmd16 - S >= Convert.ToDecimal(1.3)) || (r3.TcMmd16 - S == Convert.ToDecimal(0))));
                            Reducer2Info = Reducer2Info.Where(r2 => r2.TcMmd16 >= S && ((r2.TcMmd16 - S >= Convert.ToDecimal(1.3)) || (r2.TcMmd16 - S == Convert.ToDecimal(0))));
                        }
                        if (!string.IsNullOrEmpty(_TcMmd03))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => r1.TcMmd03 == _TcMmd03);
                        }

                        Reducer2Info = Reducer2Info.Where(r2 => !Reducer3Info.Select(r3 => r3.Re).Contains(r2.Re));
                        ReducerInfo = Reducer3Info
                                     .Select(r3 => new Reducer1Order { TcMmd03 = r3.TcMmd03, TcMmd04 = r3.TcMmd04 }).Distinct()
                                     .Union(Reducer2Info.Select(r2 => new Reducer1Order { TcMmd03 = r2.TcMmd03, TcMmd04 = r2.TcMmd04 }).Distinct());
                    }
                    break;
                default:
                    if ((R14Groups).Contains(_TcMmd01) && MotorScrewOrientation != "Y")
                    {
                        Reducer1Info = Reducer1Info.Where(r1 => EF.Functions.Like(r1.TcMmd01, _TcMmd01 + "%"));
                        if (T1N != 0)
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => r1.TcMmd07 >= T1N && (r1.TcMmd27 * Convert.ToDecimal(0.5) <= T1N));
                        }
                        if (T1B != 0)
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => r1.TcMmd05 >= T1B);
                        }
                        if (!string.IsNullOrEmpty(_TcMmd03))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => r1.TcMmd03 == _TcMmd03);
                        }
                        if (_InertiaApp != 0)
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => (_InertiaApp / (r1.TcMmd04 * r1.TcMmd04) / Inertia) <= 4);
                        }
                        ReducerInfo = Reducer1Info;
                    }
                    else if ((R21Groups).Contains(_TcMmd01) && MotorScrewOrientation != "Y")
                    {
                        Reducer3Info = Reducer3Info.Where(r3 => EF.Functions.Like(r3.TcMmd01, _TcMmd01 + "%"));
                        Reducer2Info = Reducer2Info.Where(r2 => EF.Functions.Like(r2.TcMmd01, _TcMmd01 + "%"));
                        if (T1N != 0)
                        {
                            Reducer3Info = Reducer3Info.Where(r3 => r3.TcMmd07 >= T1N && (r3.TcMmd27 * Convert.ToDecimal(0.5) <= T1N));
                            Reducer2Info = Reducer2Info.Where(r2 => r2.TcMmd07 >= T1N && (r2.TcMmd27 * Convert.ToDecimal(0.5) <= T1N));
                        }
                        if (T1B != 0)
                        {
                            Reducer3Info = Reducer3Info.Where(r3 => r3.TcMmd05 >= T1B);
                            Reducer2Info = Reducer2Info.Where(r2 => r2.TcMmd05 >= T1B);
                        }
                        if (S != 0)
                        {
                            Reducer3Info = Reducer3Info.Where(r3 => r3.TcMmd16 >= S && r3.TcMmd16 <= S * Convert.ToDecimal(2.6));
                            Reducer2Info = Reducer2Info.Where(r2 => r2.TcMmd16 >= S && r2.TcMmd16 <= S * Convert.ToDecimal(2.6));
                        }
                        if (RedSpStr.Contains(_TcMmd01))
                        {
                            Reducer3Info = Reducer3Info.Where(r3 => (r3.TcMmd24 == 1 && (r3.TcMmd16 - S >= Convert.ToDecimal(1.3) || r3.TcMmd16 - S == 0) || r3.TcMmd24 != 1));
                            Reducer2Info = Reducer2Info.Where(r2 => (r2.TcMmd24 == 1 && (r2.TcMmd16 - S >= Convert.ToDecimal(1.3) || r2.TcMmd16 - S == 0) || r2.TcMmd24 != 1));
                        }
                        if (_InertiaApp != 0)
                        {
                            Reducer3Info = Reducer3Info.Where(r3 => (_InertiaApp / (r3.TcMmd04 * r3.TcMmd04) / Inertia) <= 4);
                            Reducer2Info = Reducer2Info.Where(r2 => (_InertiaApp / (r2.TcMmd04 * r2.TcMmd04) / Inertia) <= 4);
                        }
                        if (!string.IsNullOrEmpty(_TcMmd03))
                        {
                            Reducer3Info = Reducer3Info.Where(r3 => r3.TcMmd03 == _TcMmd03);
                            Reducer2Info = Reducer2Info.Where(r2 => r2.TcMmd03 == _TcMmd03);
                        }
                        Reducer2Info = Reducer2Info.Where(r2 => !Reducer3Info.Select(r3 => r3.Re).Contains(r2.Re));
                        ReducerInfo = Reducer3Info
                                     .Select(r3 => new Reducer1Order { TcMmd03 = r3.TcMmd03 }).Distinct()
                                     .Union(Reducer2Info.Select(r2 => new Reducer1Order { TcMmd03 = r2.TcMmd03 }).Distinct());
                    }
                    else if ((R65Groups).Contains(_TcMmd01) && MotorScrewOrientation != "Y")
                    {
                        Reducer1Info = Reducer1Info.Where(r1 => r1.TcMmd35 == "N");
                        if (T1N != 0)
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => r1.TcMmd07 >= T1N && (r1.TcMmd27 * Convert.ToDecimal(0.5) <= T1N));
                        }
                        if (T1B != 0)
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => r1.TcMmd05 >= T1B);
                        }
                        if (!string.IsNullOrEmpty(_TcMmd03))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => r1.TcMmd03 == _TcMmd03);
                        }

                        if (_InertiaApp != 0)
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => (_InertiaApp / (r1.TcMmd04 * r1.TcMmd04) / Inertia) <= 4);
                        }
                        if ("R65,R66,R67,R69,R86".Contains(_TcMmd01))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => EF.Functions.Like(r1.TcMmd01, _TcMmd01 + "%") && !EF.Functions.Like(r1.TcMmd01, _TcMmd01 + "0%"));
                        }
                        else if ("R40,RF2".Contains(_TcMmd01))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => ("R40,RF2").Contains(r1.TcMmd01.Substring(0, 3)));
                        }
                        else if ("RR1,RR2".Contains(_TcMmd01))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => ("RR1,RR2").Contains(r1.TcMmd01.Substring(0, 3)));
                        }
                        else if ("RA9,RB2".Contains(_TcMmd01))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => ("RA9,RB2").Contains(r1.TcMmd01.Substring(0, 3)));
                        }
                        else if ("RR4,RR5".Contains(_TcMmd01))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => ("RR4,RR5").Contains(r1.TcMmd01.Substring(0, 3)));
                        }
                        else if ("RB4,RF4,RE3,RE4".Contains(_TcMmd01))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => ("RB4,RF4,RE3,RE4").Contains(r1.TcMmd01.Substring(0, 3)));
                        }
                        else if ("RR6,RR8,RR7,RR9".Contains(_TcMmd01))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => ("RR6,RR8,RR7,RR9").Contains(r1.TcMmd01.Substring(0, 3)));
                        }
                        else if ("R54,RF1".Contains(_TcMmd01))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => ("R54,RF1").Contains(r1.TcMmd01.Substring(0, 3)));
                        }
                        else if ("RS6,RS7".Contains(_TcMmd01))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => ("RS6,RS7").Contains(r1.TcMmd01.Substring(0, 3)));
                        }
                        else
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => EF.Functions.Like(r1.TcMmd01, _TcMmd01 + "%"));
                        }
                        if (S != 0)
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => r1.TcMmd16 == S).Count() > 0 ? Reducer1Info.Where(r1 => r1.TcMmd16 == S) : Reducer1Info.Where(r1 => r1.TcMmd16 == Convert.ToDecimal(_publicFunction.Get_NO(Convert.ToDouble(S))));
                        }
                        ReducerInfo = Reducer1Info;
                    }
                    else if ((R25Groups).Contains(_TcMmd01) && MotorScrewOrientation != "Y")
                    {
                        Reducer1Info = Reducer1Info.Where(r1 => EF.Functions.Like(r1.TcMmd01, _TcMmd01 + "%"));
                        if (T1N != 0)
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => r1.TcMmd07 >= T1N);
                        }
                        if (T1B != 0)
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => r1.TcMmd05 >= T1B);
                        }
                        if (S != 0)
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => r1.TcMmd16 >= S);
                        }
                        if (!string.IsNullOrEmpty(_TcMmd03))
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => r1.TcMmd03 == _TcMmd03);
                        }
                        if (_InertiaApp != 0)
                        {
                            Reducer1Info = Reducer1Info.Where(r1 => (_InertiaApp / (r1.TcMmd04 * r1.TcMmd04) / Inertia) <= 4);
                        }
                        ReducerInfo = Reducer1Info;
                    }
                    else if (MotorScrewOrientation != "Y")
                    {
                        if ((Convert.ToString(OData["custId"]) == "BAJ002" || Convert.ToString(OData["isSale"]) == "Y") && (("R19A,R20A").Contains(_TcMmd01)))
                        {
                            Reducer3Info = Reducer3Info.Where(r3 => ("R19A,R19B,R20A").Contains(r3.TcMmd01.Substring(0, 4)));
                            Reducer2Info = Reducer2Info.Where(r2 => ("R19A,R19B,R20A").Contains(r2.TcMmd01.Substring(0, 4)));
                            Reducer3Info = Reducer3Info.Where(r3 => EF.Functions.Like(r3.TcMmd01, _TcMmd01.Replace("A", "") + "%"));
                            Reducer2Info = Reducer2Info.Where(r2 => EF.Functions.Like(r2.TcMmd01, _TcMmd01.Replace("A", "") + "%"));
                        }
                        else if (("R19,R20").Contains(_TcMmd01))
                        {
                            Reducer3Info = Reducer3Info.Where(r3 => !("R19A,R19B,R20A").Contains(r3.TcMmd01.Substring(0, 4)));
                            Reducer2Info = Reducer2Info.Where(r2 => !("R19A,R19B,R20A").Contains(r2.TcMmd01.Substring(0, 4)));
                            Reducer3Info = Reducer3Info.Where(r3 => EF.Functions.Like(r3.TcMmd01, _TcMmd01 + "%"));
                            Reducer2Info = Reducer2Info.Where(r2 => EF.Functions.Like(r2.TcMmd01, _TcMmd01 + "%"));
                        }
                        else
                        {
                            Reducer3Info = Reducer3Info.Where(r3 => EF.Functions.Like(r3.TcMmd01, _TcMmd01 + "%"));
                            Reducer2Info = Reducer2Info.Where(r2 => EF.Functions.Like(r2.TcMmd01, _TcMmd01 + "%"));
                        }

                        if (T1N != 0)
                        {
                            Reducer3Info = Reducer3Info.Where(r3 => r3.TcMmd07 >= T1N && (r3.TcMmd27 * Convert.ToDecimal(0.5) <= T1N));
                            Reducer2Info = Reducer2Info.Where(r2 => r2.TcMmd07 >= T1N && (r2.TcMmd27 * Convert.ToDecimal(0.5) <= T1N));
                        }
                        if (T1B != 0)
                        {
                            Reducer3Info = Reducer3Info.Where(r3 => r3.TcMmd05 >= T1B);
                            Reducer2Info = Reducer2Info.Where(r2 => r2.TcMmd05 >= T1B);
                        }
                        if (S != 0)
                        {
                            Reducer3Info = Reducer3Info.Where(r3 => r3.TcMmd16 >= S && r3.TcMmd16 <= S * Convert.ToDecimal(2.6) && ((r3.TcMmd16 - S >= Convert.ToDecimal(1.3)) || (r3.TcMmd16 - S == Convert.ToDecimal(0))));
                            Reducer2Info = Reducer2Info.Where(r2 => r2.TcMmd16 >= S && r2.TcMmd16 <= S * Convert.ToDecimal(2.6) && ((r2.TcMmd16 - S >= Convert.ToDecimal(1.3)) || (r2.TcMmd16 - S == Convert.ToDecimal(0))));
                        }
                        if (!string.IsNullOrEmpty(_TcMmd03))
                        {
                            Reducer3Info = Reducer3Info.Where(r3 => r3.TcMmd03 == _TcMmd03);
                            Reducer2Info = Reducer2Info.Where(r2 => r2.TcMmd03 == _TcMmd03);
                        }

                        if (_InertiaApp != 0)
                        {
                            Reducer3Info = Reducer3Info.Where(r3 => (_InertiaApp / (r3.TcMmd04 * r3.TcMmd04) / Inertia) <= 4);
                            Reducer2Info = Reducer2Info.Where(r2 => (_InertiaApp / (r2.TcMmd04 * r2.TcMmd04) / Inertia) <= 4);
                        }
                        Reducer2Info = Reducer2Info.Where(r2 => !Reducer3Info.Select(r3 => r3.Re).Contains(r2.Re));
                        ReducerInfo = Reducer3Info
                                     .Select(r3 => new Reducer1Order { TcMmd03 = r3.TcMmd03, TcMmd04 = r3.TcMmd04 }).Distinct()
                                     .Union(Reducer2Info.Select(r2 => new Reducer1Order { TcMmd03 = r2.TcMmd03, TcMmd04 = r2.TcMmd04 }).Distinct());
                    }
                    break;
            }
            switch (item)
            {
                case "Ratio":
                    ReducerInfo = ReducerInfo.Select(r => new Reducer1Order { TcMmd04 = r.TcMmd04 }).Distinct();
                    ReducerInfo = ReducerInfo.OrderBy(r => r.TcMmd04);
                    break;
                default://Gearbox Model
                    ReducerInfo = ReducerInfo.Select(r => new Reducer1Order { TcMmd03 = r.TcMmd03 }).Distinct();
                    ReducerInfo = ReducerInfo.OrderBy(r => r.TcMmd03);
                    break;
            }
            return ReducerInfo.ToList();
        }

        //Backlash & Shaft 
        public List<Reducer1Order> GetReducerSB(dynamic OData, Decimal S, string _TcMmd03, Decimal _TcMmd04)
        {
            string _TcMmd01 = Convert.ToString(OData["GBSeries"]);
            string R65Groups = "R65,R66,R67,R69,R86,RD1,RD2,RD3,RD4,RD5,RE1,RD9,RE2,RE9,R40,RF2,RB3,RA9,RB2,RB4,RF4,RE3,RE4,RJ9,RJ5,RK9,RK5,R53,R54,RF1,RR1,RR2,RR3,RR4,RR5,RR6,RR7,RR8,RR9,RS3,RS4,RS1,RS2,RS5,RS6,RS7,R42";//tmp_type 3,
            string RR1Groups = "RR1,RR2,RR3,RR4,RR5,RR6,RR7,RR8,RR9,RD9,RE9,RD1,RD2,RD3,RD4,RD5,RB3,RA9,RB2,RB4,RF4,RE3,RE4,RK9,RK5,RJ9,RJ5,R53,R54,RF1,RS1,RS2,RS3,RS4,RS5,RS6,RS7,R42";
            //string R27Groups = "R27,R29,R31,R33,R61,RB7,RB9,RC2,RC4,RC6,R26,R28,R30,R32,R57,R58,R59,R60,RB6,RB8,RC1,RC3,RC7,RC8,RC9,RC5";
            var ReducerInfo = _DataContext.Reducer1Orders.AsQueryable();

            if (Convert.ToString(OData["Range"]) == "2" && R65Groups.Contains(_TcMmd01))
            {
                if ("R65,R66,R67,R69,R86".Contains(_TcMmd01))
                {
                    ReducerInfo = ReducerInfo.Where(r1 => EF.Functions.Like(r1.TcMmd01, _TcMmd01 + "%") && !EF.Functions.Like(r1.TcMmd01, _TcMmd01 + "0%"));
                }
                else if ("R40,RF2".Contains(_TcMmd01))
                {
                    ReducerInfo = ReducerInfo.Where(r1 => ("R40,RF2").Contains(r1.TcMmd01.Substring(0, 3)));
                }
                else if ("RR1,RR2".Contains(_TcMmd01))
                {
                    ReducerInfo = ReducerInfo.Where(r1 => ("RR1,RR2").Contains(r1.TcMmd01.Substring(0, 3)));
                }
                else if ("RA9,RB2".Contains(_TcMmd01))
                {
                    ReducerInfo = ReducerInfo.Where(r1 => ("RA9,RB2").Contains(r1.TcMmd01.Substring(0, 3)));
                }
                else if ("RR4,RR5".Contains(_TcMmd01))
                {
                    ReducerInfo = ReducerInfo.Where(r1 => ("RR4,RR5").Contains(r1.TcMmd01.Substring(0, 3)));
                }
                else if ("RB4,RF4,RE3,RE4".Contains(_TcMmd01))
                {
                    ReducerInfo = ReducerInfo.Where(r1 => ("RB4,RF4,RE3,RE4").Contains(r1.TcMmd01.Substring(0, 3)));
                }
                else if ("RR6,RR8,RR7,RR9".Contains(_TcMmd01))
                {
                    ReducerInfo = ReducerInfo.Where(r1 => ("RR6,RR8,RR7,RR9").Contains(r1.TcMmd01.Substring(0, 3)));
                }
                else if ("R54,RF1".Contains(_TcMmd01))
                {
                    ReducerInfo = ReducerInfo.Where(r1 => ("R54,RF1").Contains(r1.TcMmd01.Substring(0, 3)));
                }
                else if ("RS6,RS7".Contains(_TcMmd01))
                {
                    ReducerInfo = ReducerInfo.Where(r1 => ("RS6,RS7").Contains(r1.TcMmd01.Substring(0, 3)));
                }
                else
                {
                    ReducerInfo = ReducerInfo.Where(r1 => EF.Functions.Like(r1.TcMmd01, _TcMmd01 + "%"));
                }
                if (!string.IsNullOrEmpty(_TcMmd03))
                {
                    ReducerInfo = ReducerInfo.Where(r => r.TcMmd03 == _TcMmd03);
                }
                if (!string.IsNullOrEmpty(Convert.ToString(_TcMmd04)))
                {
                    ReducerInfo = ReducerInfo.Where(r => r.TcMmd04 == Convert.ToDecimal(Convert.ToString(_TcMmd04).Replace("*", "")));
                }

                if (S != 0)
                {
                    ReducerInfo = ReducerInfo.Where(r1 => r1.TcMmd16 == S).Count() > 0 ? ReducerInfo.Where(r1 => r1.TcMmd16 == S || (r1.TcMmd16 > S && "8,11,14,19,24,28,32,35,38,42,48,55,60".Contains(Convert.ToString(r1.TcMmd16)) && r1.TcMmd16 == r1.TcMmd17)) : ReducerInfo.Where(r1 => r1.TcMmd16 == Convert.ToDecimal(_publicFunction.Get_NO(Convert.ToDouble(S))) || (r1.TcMmd16 > S && "8,11,14,19,24,28,32,35,38,42,48,55,60".Contains(Convert.ToString(r1.TcMmd16)) && r1.TcMmd16 == r1.TcMmd17));
                }
            }
            else
            {
                if (_TcMmd01 != "R25")
                {
                    if ("R65,R66,R67,R69,R86".Contains(_TcMmd01))
                    {
                        ReducerInfo = ReducerInfo.Where(r => EF.Functions.Like(r.TcMmd01, _TcMmd01 + "4%"));
                        if (S != 0)
                        {
                            ReducerInfo = ReducerInfo.Where(r => r.TcMmd16 == S);
                        }
                    }
                    else if ("R40,RF2".Contains(_TcMmd01))
                    {
                        ReducerInfo = ReducerInfo.Where(r1 => ("R40,RF2").Contains(r1.TcMmd01.Substring(0, 3)));
                        if (S != 0)
                        {
                            ReducerInfo = ReducerInfo.Where(r => r.TcMmd16 == S);
                        }
                    }
                    else if (RR1Groups.Contains(_TcMmd01))
                    {
                        ReducerInfo = ReducerInfo.Where(r1 => EF.Functions.Like(r1.TcMmd01, _TcMmd01 + "%"));
                        if (S != 0)
                        {
                            ReducerInfo = ReducerInfo.Where(r => r.TcMmd16 == S);
                        }
                    }
                    else
                    {
                        ReducerInfo = ReducerInfo.Where(r1 => EF.Functions.Like(r1.TcMmd01, _TcMmd01 + "%"));
                    }

                    if (!string.IsNullOrEmpty(_TcMmd03))
                    {
                        ReducerInfo = ReducerInfo.Where(r => r.TcMmd03 == _TcMmd03);
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(_TcMmd04)))
                    {
                        ReducerInfo = ReducerInfo.Where(r => r.TcMmd04 == Convert.ToDecimal(Convert.ToString(_TcMmd04).Replace("*", "")));
                    }
                }
            }
            ReducerInfo = ReducerInfo.Select(r => new Reducer1Order { TcMmd22 = r.TcMmd22, TcMmd23 = r.TcMmd23 }).Distinct();

            return ReducerInfo.ToList();
        }

        public List<TcOekFile> GetModelInfo(dynamic OData)
        {
            var ModelInfo = _DataContext.TcOekFiles.AsQueryable();
            string _isSale = Convert.ToString(OData["isSale"]);
            string _tcOek01 = Convert.ToString(OData["tcOek01"]);
            if (_isSale == "Y")
            {
                ModelInfo = ModelInfo.Where(m => ("YES,EIP").ToUpper().Contains(m.TcOek21.ToUpper()) && m.TcOek01 == _tcOek01)
                           .Select(m => new TcOekFile { TcOek02 = m.TcOek02, TcOek21 = m.TcOek21, TcOek27 = m.TcOek27 })
                           .Distinct();
            }
            else
            {
                ModelInfo = ModelInfo.Where(m => m.TcOek21.ToUpper().Contains("YES") && m.TcOek01 == _tcOek01)
                           .Select(m => new TcOekFile { TcOek02 = m.TcOek02, TcOek21 = m.TcOek21, TcOek27 = m.TcOek27 })
                           .Distinct();
            }
            ModelInfo = ModelInfo.OrderBy(m => m.TcOek02);
            return ModelInfo.ToList();
        }

        public List<TcMmeFile> GetGearBoxInfo(dynamic OData)
        {
            var GBInfo = _DataContext.TcMmeFiles.AsQueryable();
            string _isSale = Convert.ToString(OData["isSale"]);
            string _custId = Convert.ToString(OData["custId"]);
            if (_isSale == "Y")
            {
                GBInfo = GBInfo
                    .Where(GB => ("R01,R02,R03,R04,R08,R09,R10,R11,R12,R13,R14,R15,R16,R19,R20,R21,R22,R23,R24,R26,R27,R28,R29, R30,R31, R32,R33, R57, R58, R59, R60, R61, R86,R69,R65,R66,R67,RB6,RB7, RB9,  RC4, RC1, RC3, RB8,RC2, RC5, RC6,  RC7, RC8, RC9,RD1,RD2,RD3,RD4,RD5,RE1,RD9,RE2,RE9,R40,RF2,RB3,RA9,RB4,RJ9,RJ5,RK9,RK5,R53,R54,RR1,RR2,RR3,RR4,RR5,RR6,RR7,RR8,RR9,RS1,RS2,RS3,RS4,RS5,RS6,RS7").Contains(GB.TcMme01) && GB.TcMme04 == "Y")
                    .Select(GB => new TcMmeFile { TcMme01 = GB.TcMme01, TcMme02 = GB.TcMme02, TcMme06 = GB.TcMme06 })
                    .Union(GBInfo.Where(GB => ("R10,R11,R21,R22,R23,R24,R64").Contains(GB.TcMme01)).Select(GB => new TcMmeFile { TcMme01 = GB.TcMme01, TcMme02 = GB.TcMme02, TcMme06 = GB.TcMme06 }))
                    .Union(GBInfo.Where(GB => ("RG4,RG5").Contains(GB.TcMme01)).Select(GB => new TcMmeFile { TcMme01 = GB.TcMme01, TcMme02 = GB.TcMme02, TcMme06 = GB.TcMme06 }))
                    .Union(GBInfo.Where(GB => GB.TcMme01.Contains("R42")).Select(GB => new TcMmeFile { TcMme01 = GB.TcMme01, TcMme02 = GB.TcMme02, TcMme06 = GB.TcMme06 }))
                ;
            }
            else
            {
                if (("AA0005,BAC016,BAC017,BAC019,BAC019-1,BAJ002").Contains(_custId))
                {
                    GBInfo = GBInfo.Where(GB => ("R01,R02,R03,R04,R08,R09,R10,R11,R12,R13,R14,R15,R16,R19,R20,R21,R22,R23,R24,R26,R27,R28,R29,R30,R31,R32,R33,R57,R58,R59,R60,R61,R64,R86,R69,R65,R66,R67,RB6,RB7,RB9,RC4,RC1,RC3,RB8,RC2,RC5,RC6,RC7,RC8,RC9,RD1,RD2,RD3,RD4,RD5,RE1,RD9,RE2,RE9,R40,RF2,RB3,RA9,RB4,RJ9,RJ5,RK9,RK5,R53,R54,RG4,RG5,R42").Contains(GB.TcMme01));
                }
                else if (("BAK001,BAC001,AA0003,AA0003-1,BAC015,BAA001,BAS002").Contains(_custId))
                {
                    GBInfo = GBInfo.Where(GB => ("R01,R02,R03,R04,R08,R09,R10,R11,R12,R13,R14,R15,R16,R19,R20,R21,R22,R23,R24,R26,R27,R28,R29,R30,R31,R32,R33,R57,R58,R59,R60,R61,R64,R86,R69,R65,R66,R67,RB6,RB7,RB9,RC4,RC1,RC3,RB8,RC2,RC5,RC6,RC7,RC8,RC9,RD1,RD2,RD3,RD4,RD5,RE1,RD9,RE2,RE9,R40,RF2,RB3,RA9,RB4,RJ9,RJ5,RK9,RK5,R53,R54,RG4,RG5,R42").Contains(GB.TcMme01));
                }
                else if (_custId == "BAC001-1")
                {
                    GBInfo = GBInfo.Where(GB => ("RR1,RR2,RR3,RR4,RR5,RR6,RR7,RR8,RR9,RS1,RS2,RS3,RS4,RS5,RS6,RS7").Contains(GB.TcMme01));
                }
                else if (_custId == "BAJ003")
                {
                    GBInfo = GBInfo.Where(GB => (GB.TcMme02.Contains("AFX")) || (GB.TcMme01 == "R86"));
                }
                else if (_custId == "BAJ002")
                {
                    GBInfo = GBInfo.Where(GB => ("R01,R02,R03,R04,R08,R09,R10,R11,R12,R13,R14,R15,R16,R19,R20,R21,R22,R23,R24,R26,R27,R28,R29, R30,R31, R32,R33, R57, R58, R59, R60, R61, R86,R69,R65,R66,R67,RB6,RB7, RB9,  RC4, RC1, RC3, RB8,RC2, RC5, RC6,  RC7, RC8, RC9,RD1,RD2,RD3,RD4,RD5,RE1,RD9,RE2,RE9,R40,RF2,RB3,RA9,RB4,RJ9,RJ5,RK9,RK5,R53,R54,RG4,RG5,R42").Contains(GB.TcMme01));
                }
                else if (("BAF001,BAI010,BAI035,BAM002,BAP001,BAS001,BAS003,BAT001,BAT002,BAC023,BAU001,BAG025,BAI006").Contains(_custId))
                {
                    GBInfo = GBInfo.Where(GB => ("R01,R02,R03,R04,R08,R09,R10,R11,R12,R13,R14,R15,R16,R19,R20,R21,R22,R23,R24,R26,R27,R28,R29, R30,R31, R32,R33, R57, R58, R59, R60, R61, R86,R69,R65,R66,R67,RB6,RB7, RB9,  RC4, RC1, RC3, RB8,RC2, RC5, RC6,  RC7, RC8, RC9,RD1,RD2,RD3,RD4,RD5,RE1,RD9,RE2,RE9,R40,RF2,RB3,RA9,RB4,RJ9,RJ5,RK9,RK5,R53,R54,RG4,RG5,R42").Contains(GB.TcMme01));
                }
                else
                {
                    GBInfo = GBInfo.Where(GB => ("R01, R02, R03, R04, R08, R09, R10, R11, R12, R13, R14, R15, R16, R19, R20, R21, R22, R23, R24, R26, R27, R28, R29, R30, R31, R32, R33, R57, R58, R59, R60, R61, R86, R69, R65, R66, R67, RB6, RB7, RB9, RC4, RC1, RC3, RB8, RC2, RC5, RC6, RC7, RC8, RC9, RD1, RD2, RD3, RD4, RD5, RE1, RD9, RE2, RE9, R40, RF2, RB3, RA9, RB4, RJ9, RJ5, RK9, RK5, R53, R54, RG4, RG5, R42").Contains(GB.TcMme01));
                }
                GBInfo = GBInfo.Where(GB => GB.TcMme04 == "Y");
                GBInfo = GBInfo.Select(GB => new TcMmeFile { TcMme01 = GB.TcMme01, TcMme02 = GB.TcMme02, TcMme06 = GB.TcMme06 });
                GBInfo = GBInfo.OrderBy(m => m.TcMme06);
            }
            return GBInfo.ToList();
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

        //取基礎單價,get_Rack_base_price(客戶編號,料號ID,幣別 ,數量) , 用於判斷大訂單折扣
        //依客戶的幣別判斷USD NTD RMB EURO > AE0002  , 除了以下兩個特別
        //Iran > BAI006
        //Turkey > BAT002
        public Double GetRackBasePrice(string MB001, string MB002, string MB004, int MB003)
        {
            String temp_1 = "0";

            String temp_occud07 = "0";

            Double temp_2 = 0;

            Double temp_5 = 0;

            List<string> ProjectPrice = new List<string> { "BAC001", "BAC001-1", "BAC015", "BAC016", "BAC017", "BAC019", "BAC019-1" };

            //特別的客戶
            string[] MB001_List = new string[] { "BAI006", "BAT002" };

            //除了特別客戶其它用AE0002
            if (Array.IndexOf(MB001_List, MB001) != -1)
            {
                List<ObkFile> Obk_FileData = _DataContext.ObkFiles.Where(obk => obk.Obk01 == MB002 && obk.Obk02 == MB001 && obk.Obk05 == MB004).ToList();
                temp_1 = Obk_FileData.Count() > 0 ? Obk_FileData.SingleOrDefault().Obk08.ToString() : "0";
            }
            else
            {
                List<ObkFile> Obk_FileData = _DataContext.ObkFiles.Where(obk => obk.Obk01 == MB002 && obk.Obk02 == "AE0002" && obk.Obk05 == MB004).ToList();
                temp_1 = Obk_FileData.Count() > 0 ? Obk_FileData.SingleOrDefault().Obk08.ToString() : "0";
            }


            List<OccFile> Occ_FileData = _DataContext.OccFiles.Where(occ => occ.Occ01 == MB001).ToList();

            if (Occ_FileData.Count() > 0)
            {
                if (("A0001,D0001").ToString().Contains(Occ_FileData.SingleOrDefault().Occ03.ToString()))
                {
                    temp_occud07 = "100";
                }
                else
                {
                    temp_occud07 = Occ_FileData.SingleOrDefault().Occud07.ToString();
                }
            }
            else
            {
                temp_occud07 = "0";
            }


            temp_2 = Convert.ToDouble(temp_1) * (Convert.ToDouble(temp_occud07) / 100);

            temp_5 = System.Math.Round(temp_2, 0, MidpointRounding.AwayFromZero);

            //專案價
            if (ProjectPrice.Contains(MB001))
            {
                List<TcShfFile> Tcsh_FileData = _DataContext.TcShfFiles.Where(tc => tc.TcShf01 == MB002).ToList();

                if (Tcsh_FileData.Count() > 0)
                {
                    switch (Tcsh_FileData.SingleOrDefault().TcShf02.ToString())
                    {
                        case "0206R100C10":
                            temp_5 = 320;
                            break;
                        case "026MR100C10":
                            temp_5 = 285;
                            break;
                        case "036MR100C10":
                            temp_5 = 420;
                            break;
                        case "026CR100C10":
                            temp_5 = 225;
                            break;
                        case "0208R100C10":
                            temp_5 = 150;
                            break;
                        case "0210R100C10":
                            temp_5 = 220;
                            break;
                        default:
                            break;
                    };
                }
            }
            return temp_5;
        }
        /// <summary>20200708 R&P 大訂單折扣,get_Rack_Large_Order_Discount_20200708(OrderCode,數量,客戶)
        public Double GetRackLargeDiscount(string p_Ordercode, Int16 p_Qty, string p_Cust_No)
        {
            double l_Discount = 0;
            double l_Rack_Length = 0;
            string l_Rack_Quality = "";
            double l_Rack_Mn = 0;

            List<TcShgFile> TcShg_FileData = _DataContext.TcShgFiles.Where(tcShg => tcShg.TcShg10 == "APEX" && tcShg.TcShg01 == p_Ordercode).ToList();

            if (TcShg_FileData.Count() > 0)
            {
                l_Rack_Length = Convert.ToDouble(TcShg_FileData.SingleOrDefault().TcShg11);
                l_Rack_Quality = Convert.ToString(TcShg_FileData.SingleOrDefault().TcShg02);
                l_Rack_Mn = Convert.ToDouble(TcShg_FileData.SingleOrDefault().TcShg03);

                //Q6,Q6M,Q6C,Mn=1====================Begin
                if (l_Rack_Quality == "6" || l_Rack_Quality == "6M" || l_Rack_Quality == "6C")
                {
                    if (l_Rack_Mn == 1)
                    {
                        if (l_Rack_Length == 500)
                        {
                            if (p_Qty >= 108 && p_Qty < 208)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 208 && p_Qty < 390)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 390 && p_Qty < 780)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 780 && p_Qty < 1560)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 1560)
                            {
                                l_Discount = 0.1;
                            }

                        }
                        else if (l_Rack_Length == 1000)
                        {
                            if (p_Qty >= 54 && p_Qty < 104)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 104 && p_Qty < 195)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 195 && p_Qty < 390)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 390 && p_Qty < 780)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 780)
                            {
                                l_Discount = 0.1;
                            }
                        }
                        else if (l_Rack_Length > 1000)
                        {
                            if (p_Qty >= 54 && p_Qty < 96)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 96 && p_Qty < 195)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 195 && p_Qty < 390)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 390 && p_Qty < 585)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 585)
                            {
                                l_Discount = 0.1;
                            }
                        }
                    }
                }
                //Q6,Q6M,Q6C,Mn=1====================End

                //Q8,Q10,Mn=1.5====================Begin
                if (l_Rack_Quality == "8" || l_Rack_Quality == "10")
                {
                    if (l_Rack_Mn == 1.5)
                    {
                        if (l_Rack_Length == 500)
                        {
                            if (p_Qty >= 96 && p_Qty < 154)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 154 && p_Qty < 312)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 312 && p_Qty < 648)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 648 && p_Qty < 1296)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 1296)
                            {
                                l_Discount = 0.1;
                            }

                        }
                        else if (l_Rack_Length == 1000)
                        {
                            if (p_Qty >= 48 && p_Qty < 77)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 77 && p_Qty < 156)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 156 && p_Qty < 324)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 324 && p_Qty < 648)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 648)
                            {
                                l_Discount = 0.1;
                            }
                        }
                        else if (l_Rack_Length > 1000)
                        {
                            if (p_Qty >= 48 && p_Qty < 88)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 88 && p_Qty < 156)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 156 && p_Qty < 312)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 312 && p_Qty < 468)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 468)
                            {
                                l_Discount = 0.1;
                            }
                        }
                    }
                }
                //Q8,Q10,Mn=1.5====================End

                //Q6,Q6M,Q6C,Mn=1.5====================Begin 
                if (l_Rack_Quality == "6" || l_Rack_Quality == "6M" || l_Rack_Quality == "6C")
                {
                    if (l_Rack_Mn == 1.5)
                    {
                        if (l_Rack_Length == 500)
                        {
                            if (p_Qty >= 70 && p_Qty < 140)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 140 && p_Qty < 264)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 264 && p_Qty < 528)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 528 && p_Qty < 1056)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 1056)
                            {
                                l_Discount = 0.1;
                            }

                        }
                        else if (l_Rack_Length == 1000)
                        {
                            if (p_Qty >= 35 && p_Qty < 70)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 70 && p_Qty < 132)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 132 && p_Qty < 264)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 264 && p_Qty < 528)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 528)
                            {
                                l_Discount = 0.1;
                            }
                        }
                        else if (l_Rack_Length > 1000)
                        {
                            if (p_Qty >= 35 && p_Qty < 70)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 70 && p_Qty < 132)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 132 && p_Qty < 264)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 264 && p_Qty < 396)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 396)
                            {
                                l_Discount = 0.1;
                            }
                        }
                    }
                }
                //Q6,Q6M,Q6C,Mn=1.5====================End

                //Q8H,Mn=2====================Begin            
                if (l_Rack_Quality == "8H")
                {
                    if (l_Rack_Mn == 2)
                    {
                        if (l_Rack_Length == 500)
                        {
                            if (p_Qty >= 48 && p_Qty < 96)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 96 && p_Qty < 180)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 180 && p_Qty < 360)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 360 && p_Qty < 720)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 720)
                            {
                                l_Discount = 0.1;
                            }

                        }
                        else if (l_Rack_Length == 1000)
                        {
                            if (p_Qty >= 24 && p_Qty < 48)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 48 && p_Qty < 90)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 90 && p_Qty < 180)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 180 && p_Qty < 360)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 360)
                            {
                                l_Discount = 0.1;
                            }
                        }
                        else if (l_Rack_Length > 1000)
                        {
                            if (p_Qty >= 24 && p_Qty < 48)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 48 && p_Qty < 90)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 90 && p_Qty < 180)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 180 && p_Qty < 270)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 270)
                            {
                                l_Discount = 0.1;
                            }
                        }
                    }
                }
                //Q8H,Mn=2====================End

                //Q5H,Q5,Q6,Q6M,Q6C,Mn=2====================Begin            
                if (l_Rack_Quality == "5H" || l_Rack_Quality == "5" || l_Rack_Quality == "6" || l_Rack_Quality == "6M" || l_Rack_Quality == "6C")
                {
                    if (l_Rack_Mn == 2)
                    {
                        if (l_Rack_Length == 500)
                        {
                            if (p_Qty >= 60 && p_Qty < 96)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 96 && p_Qty < 200)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 200 && p_Qty < 400)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 400 && p_Qty < 800)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 800)
                            {
                                l_Discount = 0.1;
                            }

                        }
                        else if (l_Rack_Length == 1000)
                        {
                            if (p_Qty >= 30 && p_Qty < 48)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 48 && p_Qty < 100)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 100 && p_Qty < 200)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 200 && p_Qty < 400)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 400)
                            {
                                l_Discount = 0.1;
                            }
                        }
                        else if (l_Rack_Length > 1000)
                        {
                            if (p_Qty >= 30 && p_Qty < 48)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 48 && p_Qty < 100)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 100 && p_Qty < 200)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 200 && p_Qty < 300)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 300)
                            {
                                l_Discount = 0.1;
                            }
                        }
                    }
                }
                //Q5H,Q5,Q6,Q6M,Q6C,Mn=2====================End


                //Q8,Q10,Mn=2====================Begin            
                if (l_Rack_Quality == "8" || l_Rack_Quality == "10")
                {
                    if (l_Rack_Mn == 2)
                    {
                        if (l_Rack_Length == 500)
                        {
                            if (p_Qty >= 48 && p_Qty < 80)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 80 && p_Qty < 180)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 180 && p_Qty < 360)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 360 && p_Qty < 720)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 720)
                            {
                                l_Discount = 0.1;
                            }

                        }
                        else if (l_Rack_Length == 1000)
                        {
                            if (p_Qty >= 24 && p_Qty < 40)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 40 && p_Qty < 90)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 90 && p_Qty < 180)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 180 && p_Qty < 360)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 360)
                            {
                                l_Discount = 0.1;
                            }
                        }
                        else if (l_Rack_Length > 1000)
                        {
                            if (p_Qty >= 24 && p_Qty < 48)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 48 && p_Qty < 90)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 90 && p_Qty < 180)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 180 && p_Qty < 270)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 270)
                            {
                                l_Discount = 0.1;
                            }
                        }
                    }
                }
                //Q8,Q10,Mn=2====================End

                //Q5,Q6,Q6M,Q6C,Mn=2.5====================Begin            
                if (l_Rack_Quality == "5" || l_Rack_Quality == "6" || l_Rack_Quality == "6M" || l_Rack_Quality == "6C")
                {
                    if (l_Rack_Mn == 2.5)
                    {
                        if (l_Rack_Length == 500)
                        {
                            if (p_Qty >= 40 && p_Qty < 70)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 70 && p_Qty < 128)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 128 && p_Qty < 256)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 256 && p_Qty < 512)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 512)
                            {
                                l_Discount = 0.1;
                            }

                        }
                        else if (l_Rack_Length == 1000)
                        {
                            if (p_Qty >= 20 && p_Qty < 35)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 35 && p_Qty < 64)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 64 && p_Qty < 128)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 128 && p_Qty < 256)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 256)
                            {
                                l_Discount = 0.1;
                            }
                        }
                        else if (l_Rack_Length > 1000)
                        {
                            if (p_Qty >= 20 && p_Qty < 35)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 35 && p_Qty < 64)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 64 && p_Qty < 128)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 128 && p_Qty < 192)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 192)
                            {
                                l_Discount = 0.1;
                            }
                        }
                    }
                }
                //Q5,Q6,Q6M,Q6C,Mn=2.5====================End

                //Q8,Q10,Mn=2.5====================Begin            
                if (l_Rack_Quality == "8" || l_Rack_Quality == "10")
                {
                    if (l_Rack_Mn == 2.5)
                    {
                        if (l_Rack_Length == 500)
                        {
                            if (p_Qty >= 40 && p_Qty < 70)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 70 && p_Qty < 128)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 128 && p_Qty < 256)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 256 && p_Qty < 512)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 512)
                            {
                                l_Discount = 0.1;
                            }

                        }
                        else if (l_Rack_Length == 1000)
                        {
                            if (p_Qty >= 20 && p_Qty < 35)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 35 && p_Qty < 64)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 64 && p_Qty < 128)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 128 && p_Qty < 256)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 256)
                            {
                                l_Discount = 0.1;
                            }
                        }
                        else if (l_Rack_Length > 1000)
                        {
                            if (p_Qty >= 20 && p_Qty < 35)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 35 && p_Qty < 64)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 64 && p_Qty < 128)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 128 && p_Qty < 192)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 192)
                            {
                                l_Discount = 0.1;
                            }
                        }
                    }
                }
                //Q8,Q10,Mn=2.5====================End

                //Q8H,Mn=3====================Begin            
                if (l_Rack_Quality == "8H")
                {
                    if (l_Rack_Mn == 3)
                    {
                        if (l_Rack_Length == 500)
                        {
                            if (p_Qty >= 40 && p_Qty < 70)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 70 && p_Qty < 128)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 128 && p_Qty < 256)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 256 && p_Qty < 512)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 512)
                            {
                                l_Discount = 0.1;
                            }

                        }
                        else if (l_Rack_Length == 1000)
                        {
                            if (p_Qty >= 20 && p_Qty < 35)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 35 && p_Qty < 64)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 64 && p_Qty < 128)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 128 && p_Qty < 256)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 256)
                            {
                                l_Discount = 0.1;
                            }
                        }
                        else if (l_Rack_Length > 1000)
                        {
                            if (p_Qty >= 20 && p_Qty < 35)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 35 && p_Qty < 64)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 64 && p_Qty < 128)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 128 && p_Qty < 192)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 192)
                            {
                                l_Discount = 0.1;
                            }
                        }
                    }
                }
                //Q8H,Mn=3====================End

                //Q5H,Q5,Q5+,Q6,Q6M,Q6C,Mn=3====================Begin            
                if (l_Rack_Quality == "5H" || l_Rack_Quality == "5" || l_Rack_Quality == "5+" || l_Rack_Quality == "6" || l_Rack_Quality == "6M" || l_Rack_Quality == "6C")
                {
                    if (l_Rack_Mn == 3)
                    {
                        if (l_Rack_Length == 500)
                        {
                            if (p_Qty >= 40 && p_Qty < 70)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 70 && p_Qty < 128)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 128 && p_Qty < 256)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 256 && p_Qty < 512)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 512)
                            {
                                l_Discount = 0.1;
                            }

                        }
                        else if (l_Rack_Length == 1000)
                        {
                            if (p_Qty >= 20 && p_Qty < 35)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 35 && p_Qty < 64)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 64 && p_Qty < 128)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 128 && p_Qty < 256)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 256)
                            {
                                l_Discount = 0.1;
                            }
                        }
                        else if (l_Rack_Length > 1000)
                        {
                            if (p_Qty >= 20 && p_Qty < 35)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 35 && p_Qty < 64)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 64 && p_Qty < 128)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 128 && p_Qty < 192)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 192)
                            {
                                l_Discount = 0.1;
                            }
                        }
                    }
                }
                //Q5H,Q5,Q5+,Q6,Q6M,Q6C,Mn=3====================End

                //Q8,Q10,Mn=3====================Begin            
                if (l_Rack_Quality == "8" || l_Rack_Quality == "10")
                {
                    if (l_Rack_Mn == 3)
                    {
                        if (l_Rack_Length == 500)
                        {
                            if (p_Qty >= 40 && p_Qty < 70)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 70 && p_Qty < 128)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 128 && p_Qty < 256)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 256 && p_Qty < 512)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 512)
                            {
                                l_Discount = 0.1;
                            }

                        }
                        else if (l_Rack_Length == 1000)
                        {
                            if (p_Qty >= 20 && p_Qty < 35)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 35 && p_Qty < 64)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 64 && p_Qty < 128)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 128 && p_Qty < 256)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 256)
                            {
                                l_Discount = 0.1;
                            }
                        }
                        else if (l_Rack_Length > 1000)
                        {
                            if (p_Qty >= 20 && p_Qty < 35)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 35 && p_Qty < 64)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 64 && p_Qty < 128)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 128 && p_Qty < 192)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 192)
                            {
                                l_Discount = 0.1;
                            }
                        }
                    }
                }
                //Q8,Q10,Mn=3====================End

                //Q8H,Mn=4====================Begin
                if (l_Rack_Quality == "8H")
                {
                    if (l_Rack_Mn == 4)
                    {
                        if (l_Rack_Length == 500)
                        {
                            if (p_Qty >= 40 && p_Qty < 84)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 84 && p_Qty < 168)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 168 && p_Qty < 336)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 336 && p_Qty < 504)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 504)
                            {
                                l_Discount = 0.1;
                            }

                        }
                        else if (l_Rack_Length == 1000)
                        {
                            if (p_Qty >= 20 && p_Qty < 42)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 42 && p_Qty < 84)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 84 && p_Qty < 168)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 168 && p_Qty < 252)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 252)
                            {
                                l_Discount = 0.1;
                            }
                        }
                        else if (l_Rack_Length > 1000)
                        {
                            if (p_Qty >= 20 && p_Qty < 42)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 42 && p_Qty < 84)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 84 && p_Qty < 126)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 126 && p_Qty < 168)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 168)
                            {
                                l_Discount = 0.1;
                            }
                        }
                    }
                }
                //Q8H,Mn=4====================End

                //Q5H,Q5,Q5+,Q6,Q6M,Q6C,Mn=4====================Begin
                if (l_Rack_Quality == "5H" || l_Rack_Quality == "5" || l_Rack_Quality == "5+" || l_Rack_Quality == "6" || l_Rack_Quality == "6M" || l_Rack_Quality == "6C")
                {
                    if (l_Rack_Mn == 4)
                    {
                        if (l_Rack_Length == 500)
                        {
                            if (p_Qty >= 40 && p_Qty < 84)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 84 && p_Qty < 168)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 168 && p_Qty < 336)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 336 && p_Qty < 504)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 504)
                            {
                                l_Discount = 0.1;
                            }

                        }
                        else if (l_Rack_Length == 1000)
                        {
                            if (p_Qty >= 20 && p_Qty < 42)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 42 && p_Qty < 84)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 84 && p_Qty < 168)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 168 && p_Qty < 252)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 252)
                            {
                                l_Discount = 0.1;
                            }
                        }
                        else if (l_Rack_Length > 1000)
                        {
                            if (p_Qty >= 20 && p_Qty < 42)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 42 && p_Qty < 84)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 84 && p_Qty < 126)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 126 && p_Qty < 168)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 168)
                            {
                                l_Discount = 0.1;
                            }
                        }
                    }
                }
                //Q5H,Q5,Q5+,Q6,Q6M,Q6C,Mn=4====================End

                //Q8,Q10,Mn=4====================Begin
                if (l_Rack_Quality == "8" || l_Rack_Quality == "10")
                {
                    if (l_Rack_Mn == 4)
                    {
                        if (l_Rack_Length == 500)
                        {
                            if (p_Qty >= 40 && p_Qty < 72)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 72 && p_Qty < 144)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 144 && p_Qty < 288)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 288 && p_Qty < 432)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 432)
                            {
                                l_Discount = 0.1;
                            }

                        }
                        else if (l_Rack_Length == 1000)
                        {
                            if (p_Qty >= 20 && p_Qty < 36)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 36 && p_Qty < 72)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 72 && p_Qty < 144)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 144 && p_Qty < 216)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 216)
                            {
                                l_Discount = 0.1;
                            }
                        }
                        else if (l_Rack_Length > 1000)
                        {
                            if (p_Qty >= 20 && p_Qty < 36)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 36 && p_Qty < 72)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 72 && p_Qty < 108)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 108 && p_Qty < 144)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 144)
                            {
                                l_Discount = 0.1;
                            }
                        }
                    }
                }
                //Q8,Q10,Mn=4====================End

                //Q4,Q5,Q5+,Q6,Q6M,Mn=5====================Begin
                if (l_Rack_Quality == "4" || l_Rack_Quality == "5" || l_Rack_Quality == "5+" || l_Rack_Quality == "6" || l_Rack_Quality == "6M")
                {
                    if (l_Rack_Mn == 5)
                    {
                        if (l_Rack_Length == 500)
                        {
                            if (p_Qty >= 30 && p_Qty < 60)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 60 && p_Qty < 120)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 120 && p_Qty < 240)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 240 && p_Qty < 360)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 360)
                            {
                                l_Discount = 0.1;
                            }

                        }
                        else if (l_Rack_Length == 1000)
                        {
                            if (p_Qty >= 15 && p_Qty < 30)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 30 && p_Qty < 60)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 60 && p_Qty < 120)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 120 && p_Qty < 180)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 180)
                            {
                                l_Discount = 0.1;
                            }
                        }
                        else if (l_Rack_Length > 1000)
                        {
                            if (p_Qty >= 15 && p_Qty < 30)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 30 && p_Qty < 60)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 60 && p_Qty < 90)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 90 && p_Qty < 120)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 120)
                            {
                                l_Discount = 0.1;
                            }
                        }
                    }
                }
                //Q4,Q5,Q5+,Q6,Q6M,Mn=5====================End

                //Q5H,Mn=5====================Begin
                if (l_Rack_Quality == "5H")
                {
                    if (l_Rack_Mn == 5)
                    {
                        if (l_Rack_Length == 500)
                        {
                            if (p_Qty >= 24 && p_Qty < 50)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 50 && p_Qty < 100)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 100 && p_Qty < 200)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 200 && p_Qty < 300)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 300)
                            {
                                l_Discount = 0.1;
                            }

                        }
                        else if (l_Rack_Length == 1000)
                        {
                            if (p_Qty >= 12 && p_Qty < 25)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 25 && p_Qty < 50)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 50 && p_Qty < 100)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 100 && p_Qty < 150)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 150)
                            {
                                l_Discount = 0.1;
                            }
                        }
                        else if (l_Rack_Length > 1000)
                        {
                            if (p_Qty >= 12 && p_Qty < 25)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 25 && p_Qty < 50)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 50 && p_Qty < 75)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 75 && p_Qty < 100)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 100)
                            {
                                l_Discount = 0.1;
                            }
                        }
                    }
                }
                //Q5H,Mn=5====================End

                //Q8,Q10,Mn=5====================Begin
                if (l_Rack_Quality == "8" || l_Rack_Quality == "10")
                {
                    if (l_Rack_Mn == 5)
                    {
                        if (l_Rack_Length == 500)
                        {
                            if (p_Qty >= 30 && p_Qty < 60)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 60 && p_Qty < 120)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 120 && p_Qty < 240)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 240 && p_Qty < 360)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 360)
                            {
                                l_Discount = 0.1;
                            }

                        }
                        else if (l_Rack_Length == 1000)
                        {
                            if (p_Qty >= 15 && p_Qty < 30)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 30 && p_Qty < 60)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 60 && p_Qty < 120)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 120 && p_Qty < 180)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 180)
                            {
                                l_Discount = 0.1;
                            }
                        }
                        else if (l_Rack_Length > 1000)
                        {
                            if (p_Qty >= 15 && p_Qty < 30)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 30 && p_Qty < 60)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 60 && p_Qty < 90)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 90 && p_Qty < 120)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 120)
                            {
                                l_Discount = 0.1;
                            }
                        }
                    }
                }
                //Q8,Q10,Mn=5====================End

                //Q5H,Mn=6====================Begin
                if (l_Rack_Quality == "5H")
                {
                    if (l_Rack_Mn == 6)
                    {
                        if (l_Rack_Length == 500)
                        {
                            if (p_Qty >= 18 && p_Qty < 40)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 40 && p_Qty < 80)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 80 && p_Qty < 160)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 160 && p_Qty < 240)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 240)
                            {
                                l_Discount = 0.1;
                            }

                        }
                        else if (l_Rack_Length == 1000)
                        {
                            if (p_Qty >= 9 && p_Qty < 20)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 20 && p_Qty < 40)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 40 && p_Qty < 80)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 80 && p_Qty < 120)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 120)
                            {
                                l_Discount = 0.1;
                            }
                        }
                        else if (l_Rack_Length > 1000)
                        {
                            if (p_Qty >= 9 && p_Qty < 20)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 20 && p_Qty < 40)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 40 && p_Qty < 60)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 60 && p_Qty < 80)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 80)
                            {
                                l_Discount = 0.1;
                            }
                        }
                    }
                }
                //Q5H,Mn=6====================End

                //Q4,Q5,Q5+,Q6,Q6M,Mn=6====================Begin
                if (l_Rack_Quality == "4" || l_Rack_Quality == "5" || l_Rack_Quality == "5+" || l_Rack_Quality == "6" || l_Rack_Quality == "6M")
                {
                    if (l_Rack_Mn == 6)
                    {
                        if (l_Rack_Length == 500)
                        {
                            if (p_Qty >= 24 && p_Qty < 50)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 50 && p_Qty < 100)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 100 && p_Qty < 200)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 200 && p_Qty < 300)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 300)
                            {
                                l_Discount = 0.1;
                            }

                        }
                        else if (l_Rack_Length == 1000)
                        {
                            if (p_Qty >= 12 && p_Qty < 25)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 25 && p_Qty < 50)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 50 && p_Qty < 100)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 100 && p_Qty < 150)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 150)
                            {
                                l_Discount = 0.1;
                            }
                        }
                        else if (l_Rack_Length > 1000)
                        {
                            if (p_Qty >= 12 && p_Qty < 25)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 25 && p_Qty < 50)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 50 && p_Qty < 75)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 75 && p_Qty < 100)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 100)
                            {
                                l_Discount = 0.1;
                            }
                        }
                    }
                }
                //Q4,Q5,Q5+,Q6,Q6M,Mn=6====================End

                //Q8,Q10,Mn=6====================Begin
                if (l_Rack_Quality == "8" || l_Rack_Quality == "10")
                {
                    if (l_Rack_Mn == 6)
                    {
                        if (l_Rack_Length == 500)
                        {
                            if (p_Qty >= 24 && p_Qty < 50)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 50 && p_Qty < 100)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 100 && p_Qty < 200)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 200 && p_Qty < 300)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 300)
                            {
                                l_Discount = 0.1;
                            }

                        }
                        else if (l_Rack_Length == 1000)
                        {
                            if (p_Qty >= 12 && p_Qty < 25)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 25 && p_Qty < 50)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 50 && p_Qty < 100)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 100 && p_Qty < 150)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 150)
                            {
                                l_Discount = 0.1;
                            }
                        }
                        else if (l_Rack_Length > 1000)
                        {
                            if (p_Qty >= 12 && p_Qty < 25)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 25 && p_Qty < 50)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 50 && p_Qty < 75)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 75 && p_Qty < 100)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 100)
                            {
                                l_Discount = 0.1;
                            }
                        }
                    }
                }
                //Q8,Q10,Mn=6====================End

                //Q4,Q5H,Q5,Q6,Q6M,Mn=8====================Begin
                if (l_Rack_Quality == "4" || l_Rack_Quality == "5H" || l_Rack_Quality == "5" || l_Rack_Quality == "6" || l_Rack_Quality == "6M")
                {
                    if (l_Rack_Mn == 8)
                    {
                        if (l_Rack_Length == 500)
                        {
                            if (p_Qty >= 36 && p_Qty < 72)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 72 && p_Qty < 108)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 108 && p_Qty < 144)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 144)
                            {
                                l_Discount = 0.1;
                            }

                        }
                        else if (l_Rack_Length == 1000)
                        {
                            if (p_Qty >= 18 && p_Qty < 36)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 36 && p_Qty < 54)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 54 && p_Qty < 72)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 72)
                            {
                                l_Discount = 0.1;
                            }
                        }
                        else if (l_Rack_Length > 1000)
                        {
                            if (p_Qty >= 18 && p_Qty < 27)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 27 && p_Qty < 36)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 36)
                            {
                                l_Discount = 0.07;
                            }
                        }
                    }
                }
                //Q4,Q5H,Q5,Q6,Q6M,Mn=8====================End

                //Q8,Q10,Mn=8====================Begin
                if (l_Rack_Quality == "8" || l_Rack_Quality == "10")
                {
                    if (l_Rack_Mn == 8)
                    {
                        if (l_Rack_Length == 500)
                        {
                            if (p_Qty >= 36 && p_Qty < 72)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 72 && p_Qty < 108)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 108 && p_Qty < 144)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 144)
                            {
                                l_Discount = 0.1;
                            }

                        }
                        else if (l_Rack_Length == 1000)
                        {
                            if (p_Qty >= 18 && p_Qty < 36)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 36 && p_Qty < 54)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 54 && p_Qty < 72)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 72)
                            {
                                l_Discount = 0.1;
                            }
                        }
                        else if (l_Rack_Length > 1000)
                        {
                            if (p_Qty >= 18 && p_Qty < 27)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 27 && p_Qty < 36)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 36)
                            {
                                l_Discount = 0.07;
                            }
                        }
                    }
                }
                //Q8,Q10,Mn=8====================End


                //Q4,Q5H,Q5,Q6,Q6M,Mn=10====================Begin
                if (l_Rack_Quality == "4" || l_Rack_Quality == "5H" || l_Rack_Quality == "5" || l_Rack_Quality == "6" || l_Rack_Quality == "6M")
                {
                    if (l_Rack_Mn == 10)
                    {
                        if (l_Rack_Length == 500)
                        {
                            if (p_Qty >= 30 && p_Qty < 60)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 60 && p_Qty < 90)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 90 && p_Qty < 120)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 120)
                            {
                                l_Discount = 0.1;
                            }

                        }
                        else if (l_Rack_Length == 1000)
                        {
                            if (p_Qty >= 15 && p_Qty < 30)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 30 && p_Qty < 45)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 45 && p_Qty < 60)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 60)
                            {
                                l_Discount = 0.1;
                            }
                        }
                        else if (l_Rack_Length > 1000)
                        {
                            if (p_Qty >= 12 && p_Qty < 18)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 18 && p_Qty < 24)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 24)
                            {
                                l_Discount = 0.07;
                            }
                        }
                    }
                }
                //Q4,Q5H,Q5,Q6,Q6M,Mn=10====================End

                //Q8,Q10,Mn=10====================Begin
                if (l_Rack_Quality == "8" || l_Rack_Quality == "10")
                {
                    if (l_Rack_Mn == 10)
                    {
                        if (l_Rack_Length == 500)
                        {
                            if (p_Qty >= 30 && p_Qty < 60)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 60 && p_Qty < 90)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 90 && p_Qty < 120)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 120)
                            {
                                l_Discount = 0.1;
                            }

                        }
                        else if (l_Rack_Length == 1000)
                        {
                            if (p_Qty >= 15 && p_Qty < 30)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 30 && p_Qty < 45)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 45 && p_Qty < 60)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 60)
                            {
                                l_Discount = 0.1;
                            }
                        }
                        else if (l_Rack_Length > 1000)
                        {
                            if (p_Qty >= 12 && p_Qty < 18)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 18 && p_Qty < 24)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 24)
                            {
                                l_Discount = 0.07;
                            }
                        }
                    }
                }
                //Q8,Q10,Mn=10====================End

                //Q4,Q5,Q6,Q8,Q10,Mn=12====================Begin
                if (l_Rack_Quality == "4" || l_Rack_Quality == "5" || l_Rack_Quality == "6" || l_Rack_Quality == "8" || l_Rack_Quality == "10")
                {
                    if (l_Rack_Mn == 12)
                    {
                        if (l_Rack_Length == 500)
                        {
                            if (p_Qty >= 16 && p_Qty < 32)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 32 && p_Qty < 48)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 48 && p_Qty < 64)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 64)
                            {
                                l_Discount = 0.1;
                            }

                        }
                        else if (l_Rack_Length == 1000)
                        {
                            if (p_Qty >= 8 && p_Qty < 16)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 16 && p_Qty < 24)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 24 && p_Qty < 32)
                            {
                                l_Discount = 0.07;
                            }
                            else if (p_Qty >= 32)
                            {
                                l_Discount = 0.1;
                            }
                        }
                        else if (l_Rack_Length > 1000)
                        {
                            if (p_Qty >= 8 && p_Qty < 12)
                            {
                                l_Discount = 0.03;
                            }
                            else if (p_Qty >= 12 && p_Qty < 16)
                            {
                                l_Discount = 0.05;
                            }
                            else if (p_Qty >= 16)
                            {
                                l_Discount = 0.07;
                            }
                        }
                    }
                }
                //Q4,Q5,Q6,Q8,Q10,Mn=12====================End

            }
            else
            {
                l_Discount = 0;
            }

            //特別的客戶
            string[] No_Discount_List = new string[] { "0206R100C10", "026MR100C10", "036MR100C10", "026CR100C10", "0208R100C10", "0210R100C10" };

            string[] No_Discount_Cust_List = new string[] { "BAC001", "BAC001-1", "BAC015", "BAC016", "BAC017", "BAC019", "BAC019-1" };
            //除了特別客戶其它用AE0002
            if ((Array.IndexOf(No_Discount_List, p_Ordercode) != -1) && (Array.IndexOf(No_Discount_Cust_List, p_Cust_No) != -1))
            {
                l_Discount = 0;
            }

            return l_Discount;
        }

        /// <summary>20200708 R&P 大訂單折扣,get_Pinion_Large_Order_Discount_20200708(OrderCode,數量,客戶)
        public Double GetPinionLargeDiscount(string p_Ordercode, Int16 p_Qty, string p_Cust_No)
        {
            double l_Discount = 0;
            string l_Pinion_Type = "";
            double l_Pinion_Mn = 0;

            List<TcShhFile> TcShh_FileData = _DataContext.TcShhFiles.Where(tcShh => tcShh.TcShh08 == "APEX" && tcShh.TcShh01 == p_Ordercode).ToList();

            if (TcShh_FileData.Count() > 0)
            {
                l_Pinion_Type = Convert.ToString(TcShh_FileData.SingleOrDefault().TcShh02);
                l_Pinion_Mn = Convert.ToDouble(TcShh_FileData.SingleOrDefault().TcShh05);
            }

            if (l_Pinion_Type == "A" || l_Pinion_Type == "B" || l_Pinion_Type == "C" || l_Pinion_Type == "D")
            {
                if (l_Pinion_Mn == 1 || l_Pinion_Mn == 1.5 || l_Pinion_Mn == 2 || l_Pinion_Mn == 2.5 || l_Pinion_Mn == 3)
                {
                    if (p_Qty >= 11 && p_Qty <= 30)
                    {
                        l_Discount = 0.03;
                    }
                    else if (p_Qty >= 31 && p_Qty <= 40)
                    {
                        l_Discount = 0.05;
                    }
                    else if (p_Qty >= 41 && p_Qty <= 50)
                    {
                        l_Discount = 0.07;
                    }
                    else if (p_Qty >= 51)
                    {
                        l_Discount = 0.1;
                    }
                }
                else if (l_Pinion_Mn == 4 || l_Pinion_Mn == 5 || l_Pinion_Mn == 6)
                {
                    if (p_Qty >= 11 && p_Qty <= 20)
                    {
                        l_Discount = 0.03;
                    }
                    else if (p_Qty >= 21 && p_Qty <= 30)
                    {
                        l_Discount = 0.05;
                    }
                    else if (p_Qty >= 31 && p_Qty <= 40)
                    {
                        l_Discount = 0.07;
                    }
                    else if (p_Qty >= 41)
                    {
                        l_Discount = 0.1;
                    }
                }
                else if (l_Pinion_Mn == 8 || l_Pinion_Mn == 10 || l_Pinion_Mn == 12)
                {
                    if (p_Qty >= 5 && p_Qty <= 10)
                    {
                        l_Discount = 0.03;
                    }
                    else if (p_Qty >= 11 && p_Qty <= 20)
                    {
                        l_Discount = 0.05;
                    }
                    else if (p_Qty >= 21 && p_Qty <= 30)
                    {
                        l_Discount = 0.07;
                    }
                    else if (p_Qty >= 31)
                    {
                        l_Discount = 0.1;
                    }
                }
            }


            if (l_Pinion_Type == "E" || l_Pinion_Type == "F" || l_Pinion_Type == "G" || l_Pinion_Type == "H")
            {
                if (l_Pinion_Mn == 1 || l_Pinion_Mn == 1.5 || l_Pinion_Mn == 2 || l_Pinion_Mn == 2.5 || l_Pinion_Mn == 3)
                {
                    if (p_Qty >= 31 && p_Qty <= 50)
                    {
                        l_Discount = 0.03;
                    }
                    else if (p_Qty >= 51 && p_Qty <= 70)
                    {
                        l_Discount = 0.05;
                    }
                    else if (p_Qty >= 71 && p_Qty <= 100)
                    {
                        l_Discount = 0.07;
                    }
                    else if (p_Qty >= 101)
                    {
                        l_Discount = 0.1;
                    }
                }
                else if (l_Pinion_Mn == 4 || l_Pinion_Mn == 5 || l_Pinion_Mn == 6)
                {
                    if (p_Qty >= 21 && p_Qty <= 40)
                    {
                        l_Discount = 0.03;
                    }
                    else if (p_Qty >= 41 && p_Qty <= 60)
                    {
                        l_Discount = 0.05;
                    }
                    else if (p_Qty >= 61 && p_Qty <= 70)
                    {
                        l_Discount = 0.07;
                    }
                    else if (p_Qty >= 71)
                    {
                        l_Discount = 0.1;
                    }
                }
                else if (l_Pinion_Mn == 8 || l_Pinion_Mn == 10 || l_Pinion_Mn == 12)
                {
                    if (p_Qty >= 21 && p_Qty <= 30)
                    {
                        l_Discount = 0.03;
                    }
                    else if (p_Qty >= 31 && p_Qty <= 40)
                    {
                        l_Discount = 0.05;
                    }
                    else if (p_Qty >= 41 && p_Qty <= 50)
                    {
                        l_Discount = 0.07;
                    }
                    else if (p_Qty >= 51)
                    {
                        l_Discount = 0.1;
                    }
                }
            }

            return l_Discount;
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
            List<OccFile> Occ_FileData = _DataContext.OccFiles.Where(oc => oc.Occ01 == MB001).ToList();

            temp_3 = GetDiscountRate(MB001);
            if (Occ_FileData.Count() > 0)
            {
                if (("A0001,D0001").ToString().IndexOf(Occ_FileData.SingleOrDefault().Occ03) != -1)
                {
                    tmp_IsAgent = true;
                }
            }

            //先找是否在obk_file有資料,代表特殊價
            List<ObkFile> Obk_FileData = _DataContext.ObkFiles.Where(Obk => Obk.Obk02 == MB001 && Obk.Obk01 == MB002 && Obk.Obk05 == MB004 && Obk.Obkacti == "Y").ToList();

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

            List<OccFile> Occ_FileData = _DataContext.OccFiles.Where(oc => oc.Occ01 == occ01).ToList();

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


        public IEnumerable<dynamic> GetAdapInfo(List<AdpDatas> _datas, string item)
        {
            if (_datas.FirstOrDefault().LBck == "LBstd")
            {
                switch (_datas.FirstOrDefault().TableName)
                {
                    case "Adap_class1":
                        var AdapClass1sInfo = _DataContext.AdapClass1s.AsQueryable();
                        if ("Formula,Formula_P2".Contains(item))
                        {
                            AdapClass1sInfo = AdapClass1sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                                      && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                                       && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia)
                                                                       );
                            switch (item)
                            {
                                case "Formula":
                                    AdapClass1sInfo = AdapClass1sInfo.Where(Adp => Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5))
                                                                          && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                          && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                          && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + "%")
                                                                          )
                                                                         .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                         .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                         .ThenBy(Adp => Adp.TcMma08)
                                                                         .ThenBy(Adp => Adp.TcMma11)
                                                                         .ThenBy(Adp => Adp.TcMma10)
                                                                         .ThenBy(Adp => Adp.TcMma24)
                                                                         ;
                                    break;
                                case "Formula_P2":
                                    AdapClass1sInfo = AdapClass1sInfo.Where(Adp => (Adp.TcMma08 + Convert.ToDecimal(_datas.FirstOrDefault().Suitadapter_c4)) >= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5))
                                                                            && Adp.TcMma13 >= _publicFunction.GRound((Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)), 0) && Adp.TcMma13 <= _publicFunction.GRound((Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp)), 0)
                                                                             && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                                            )
                                                                             .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                                            .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                            .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                                            .ThenBy(Adp => Adp.TcMma13)
                                                                            .ThenBy(Adp => Adp.TcMma08)
                                                                            .ThenBy(Adp => Adp.TcMma11)
                                                                            .ThenBy(Adp => Adp.TcMma10)
                                                                            .ThenBy(Adp => Adp.TcMma24)
                                                                            ;
                                    break;
                            }
                        }
                        else if (item == "formula2")
                        {
                            AdapClass1sInfo = AdapClass1sInfo.Where(Adp => (Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) / 2 || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) * 2)
                                                                  && Adp.TcMma08 >= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB)
                                                                   && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= Convert.ToDecimal(_datas.FirstOrDefault().LT) && Adp.TcMma04 == Convert.ToDecimal(_datas.FirstOrDefault().LA)
                                                                   && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().LZ) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                   && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                    )
                                                                    .OrderBy(Adp => Adp.TcMma13)
                                                                    .ThenBy(Adp => Adp.TcMma08)
                                                                    .ThenBy(Adp => Adp.TcMma11)
                                                                    .ThenBy(Adp => Adp.TcMma10)
                                                                    .ThenBy(Adp => Adp.TcMma24)
                                                                    ;

                        }
                        else if (item == "Formula_type5")
                        {
                            AdapClass1sInfo = AdapClass1sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                        && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5)) && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                        && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                        && Adp.TcMma26 == _datas.FirstOrDefault().Motor_Interface && Adp.TcMma27 == _datas.FirstOrDefault().Motor_Screw_orientation
                                                        && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                        )
                                                        .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                        .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                        .ThenBy(Adp => Adp.TcMma08)
                                                        .ThenBy(Adp => Adp.TcMma11)
                                                        .ThenBy(Adp => Adp.TcMma10)
                                                        .ThenBy(Adp => Adp.TcMma24)
                                                        ;

                            if (_datas.FirstOrDefault().Motor_Screw_orientation == "N") //正鎖
                            {
                                AdapClass1sInfo = AdapClass1sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)));
                            }
                            else
                            {
                                AdapClass1sInfo = AdapClass1sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) + Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia));
                            }
                        }
                        return AdapClass1sInfo;
                    case "Adap_classEx2":
                        var AdapClassex2sInfo = _DataContext.AdapClassex2s.AsQueryable();
                        if ("Formula,Formula_P2".Contains(item))
                        {
                            AdapClassex2sInfo = AdapClassex2sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                                      && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                                       && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia)
                                                                       );
                            switch (item)
                            {
                                case "Formula":
                                    AdapClassex2sInfo = AdapClassex2sInfo.Where(Adp => Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5))
                                                                          && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                          && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                          && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + "%")
                                                                          )
                                                                         .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                         .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                         .ThenBy(Adp => Adp.TcMma08)
                                                                         .ThenBy(Adp => Adp.TcMma11)
                                                                         .ThenBy(Adp => Adp.TcMma10)
                                                                         .ThenBy(Adp => Adp.TcMma24)
                                                                         ;
                                    break;
                                case "Formula_P2":
                                    AdapClassex2sInfo = AdapClassex2sInfo.Where(Adp => (Adp.TcMma08 + Convert.ToDecimal(_datas.FirstOrDefault().Suitadapter_c4)) >= Convert.ToDecimal(_datas.FirstOrDefault().LR)


                                                                            && Adp.TcMma13 >= _publicFunction.GRound((Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)), 0) && Adp.TcMma13 <= _publicFunction.GRound((Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp)), 0)
                                                                             && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                                            )
                                                                             .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                                            .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                            .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                                            .ThenBy(Adp => Adp.TcMma13)
                                                                            .ThenBy(Adp => Adp.TcMma08)
                                                                            .ThenBy(Adp => Adp.TcMma11)
                                                                            .ThenBy(Adp => Adp.TcMma10)
                                                                            .ThenBy(Adp => Adp.TcMma24)
                                                                            ;
                                    break;
                            }
                        }
                        else if (item == "formula2")
                        {
                            AdapClassex2sInfo = AdapClassex2sInfo.Where(Adp => (Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) / 2 || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) * 2)
                                                                  && Adp.TcMma08 >= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB)
                                                                   && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= Convert.ToDecimal(_datas.FirstOrDefault().LT) && Adp.TcMma04 == Convert.ToDecimal(_datas.FirstOrDefault().LA)
                                                                   && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().LZ) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                   && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))

                                                                    )
                                                                    .OrderBy(Adp => Adp.TcMma13)
                                                                    .ThenBy(Adp => Adp.TcMma08)
                                                                    .ThenBy(Adp => Adp.TcMma11)
                                                                    .ThenBy(Adp => Adp.TcMma10)
                                                                    .ThenBy(Adp => Adp.TcMma24)
                                                                    ;

                        }
                        else if (item == "Formula_type5")
                        {
                            AdapClassex2sInfo = AdapClassex2sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                        && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5)) && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                        && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                        && Adp.TcMma26 == _datas.FirstOrDefault().Motor_Interface && Adp.TcMma27 == _datas.FirstOrDefault().Motor_Screw_orientation
                                                        && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                        )
                                                        .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                        .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                        .ThenBy(Adp => Adp.TcMma08)
                                                        .ThenBy(Adp => Adp.TcMma11)
                                                        .ThenBy(Adp => Adp.TcMma10)
                                                        .ThenBy(Adp => Adp.TcMma24)
                                                        ;

                            if (_datas.FirstOrDefault().Motor_Screw_orientation == "N") //正鎖
                            {
                                AdapClassex2sInfo = AdapClassex2sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)));
                            }
                            else
                            {
                                AdapClassex2sInfo = AdapClassex2sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) + Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia));
                            }
                        }
                        return AdapClassex2sInfo;
                    case "Adap_classEx3":
                        var AdapClassex3sInfo = _DataContext.AdapClassex3s.AsQueryable();
                        if ("Formula,Formula_P2".Contains(item))
                        {
                            AdapClassex3sInfo = AdapClassex3sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                                      && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                                       && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia)
                                                                       );
                            switch (item)
                            {
                                case "Formula":
                                    AdapClassex3sInfo = AdapClassex3sInfo.Where(Adp => Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5))
                                                                          && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                          && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                          && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + "%")
                                                                          )
                                                                         .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                         .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                         .ThenBy(Adp => Adp.TcMma08)
                                                                         .ThenBy(Adp => Adp.TcMma11)
                                                                         .ThenBy(Adp => Adp.TcMma10)
                                                                         .ThenBy(Adp => Adp.TcMma24)
                                                                         ;
                                    break;
                                case "Formula_P2":
                                    AdapClassex3sInfo = AdapClassex3sInfo.Where(Adp => (Adp.TcMma08 + Convert.ToDecimal(_datas.FirstOrDefault().Suitadapter_c4)) >= Convert.ToDecimal(_datas.FirstOrDefault().LR)


                                                                            && Adp.TcMma13 >= _publicFunction.GRound((Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)), 0) && Adp.TcMma13 <= _publicFunction.GRound((Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp)), 0)
                                                                             && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                                            )
                                                                             .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                                            .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                            .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                                            .ThenBy(Adp => Adp.TcMma13)
                                                                            .ThenBy(Adp => Adp.TcMma08)
                                                                            .ThenBy(Adp => Adp.TcMma11)
                                                                            .ThenBy(Adp => Adp.TcMma10)
                                                                            .ThenBy(Adp => Adp.TcMma24)
                                                                            ;
                                    break;
                            }
                        }
                        else if (item == "formula2")
                        {
                            AdapClassex3sInfo = AdapClassex3sInfo.Where(Adp => (Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) / 2 || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) * 2)
                                                                  && Adp.TcMma08 >= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB)
                                                                   && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= Convert.ToDecimal(_datas.FirstOrDefault().LT) && Adp.TcMma04 == Convert.ToDecimal(_datas.FirstOrDefault().LA)
                                                                   && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().LZ) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                   && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))

                                                                    )
                                                                    .OrderBy(Adp => Adp.TcMma13)
                                                                    .ThenBy(Adp => Adp.TcMma08)
                                                                    .ThenBy(Adp => Adp.TcMma11)
                                                                    .ThenBy(Adp => Adp.TcMma10)
                                                                    .ThenBy(Adp => Adp.TcMma24)
                                                                    ;

                        }
                        else if (item == "Formula_type5")
                        {
                            AdapClassex3sInfo = AdapClassex3sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                        && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5)) && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                        && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                        && Adp.TcMma26 == _datas.FirstOrDefault().Motor_Interface && Adp.TcMma27 == _datas.FirstOrDefault().Motor_Screw_orientation
                                                        && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                        )
                                                        .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                        .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                        .ThenBy(Adp => Adp.TcMma08)
                                                        .ThenBy(Adp => Adp.TcMma11)
                                                        .ThenBy(Adp => Adp.TcMma10)
                                                        .ThenBy(Adp => Adp.TcMma24)
                                                        ;

                            if (_datas.FirstOrDefault().Motor_Screw_orientation == "N") //正鎖
                            {
                                AdapClassex3sInfo = AdapClassex3sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)));
                            }
                            else
                            {
                                AdapClassex3sInfo = AdapClassex3sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) + Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia));
                            }
                        }
                        return AdapClassex3sInfo;
                    case "Adap_classEx4":
                        var AdapClassex4sInfo = _DataContext.AdapClassex4s.AsQueryable();
                        if ("Formula,Formula_P2".Contains(item))
                        {
                            AdapClassex4sInfo = AdapClassex4sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                                      && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                                       && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia)
                                                                       );
                            switch (item)
                            {
                                case "Formula":
                                    AdapClassex4sInfo = AdapClassex4sInfo.Where(Adp => Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5))
                                                                          && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                          && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                          && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + "%")
                                                                          )
                                                                         .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                         .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                         .ThenBy(Adp => Adp.TcMma08)
                                                                         .ThenBy(Adp => Adp.TcMma11)
                                                                         .ThenBy(Adp => Adp.TcMma10)
                                                                         .ThenBy(Adp => Adp.TcMma24)
                                                                         ;
                                    break;
                                case "Formula_P2":
                                    AdapClassex4sInfo = AdapClassex4sInfo.Where(Adp => (Adp.TcMma08 + Convert.ToDecimal(_datas.FirstOrDefault().Suitadapter_c4)) >= Convert.ToDecimal(_datas.FirstOrDefault().LR)


                                                                            && Adp.TcMma13 >= _publicFunction.GRound((Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)), 0) && Adp.TcMma13 <= _publicFunction.GRound((Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp)), 0)
                                                                             && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                                            )
                                                                             .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                                            .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                            .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                                            .ThenBy(Adp => Adp.TcMma13)
                                                                            .ThenBy(Adp => Adp.TcMma08)
                                                                            .ThenBy(Adp => Adp.TcMma11)
                                                                            .ThenBy(Adp => Adp.TcMma10)
                                                                            .ThenBy(Adp => Adp.TcMma24)
                                                                            ;
                                    break;
                            }
                        }
                        else if (item == "formula2")
                        {
                            AdapClassex4sInfo = AdapClassex4sInfo.Where(Adp => (Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) / 2 || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) * 2)
                                                                  && Adp.TcMma08 >= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB)
                                                                   && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= Convert.ToDecimal(_datas.FirstOrDefault().LT) && Adp.TcMma04 == Convert.ToDecimal(_datas.FirstOrDefault().LA)
                                                                   && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().LZ) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                   && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))

                                                                    )
                                                                    .OrderBy(Adp => Adp.TcMma13)
                                                                    .ThenBy(Adp => Adp.TcMma08)
                                                                    .ThenBy(Adp => Adp.TcMma11)
                                                                    .ThenBy(Adp => Adp.TcMma10)
                                                                    .ThenBy(Adp => Adp.TcMma24)
                                                                    ;

                        }
                        else if (item == "Formula_type5")
                        {
                            AdapClassex4sInfo = AdapClassex4sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                        && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5)) && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                        && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                        && Adp.TcMma26 == _datas.FirstOrDefault().Motor_Interface && Adp.TcMma27 == _datas.FirstOrDefault().Motor_Screw_orientation
                                                        && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                        )
                                                        .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                        .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                        .ThenBy(Adp => Adp.TcMma08)
                                                        .ThenBy(Adp => Adp.TcMma11)
                                                        .ThenBy(Adp => Adp.TcMma10)
                                                        .ThenBy(Adp => Adp.TcMma24)
                                                        ;

                            if (_datas.FirstOrDefault().Motor_Screw_orientation == "N") //正鎖
                            {
                                AdapClassex4sInfo = AdapClassex4sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)));
                            }
                            else
                            {
                                AdapClassex4sInfo = AdapClassex4sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) + Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia));
                            }
                        }
                        return AdapClassex4sInfo;
                    case "Adap_type1_class0":
                        var AdapType1Class0sInfo = _DataContext.AdapType1Class0s.AsQueryable();
                        if ("Formula,Formula_P2".Contains(item))
                        {
                            AdapType1Class0sInfo = AdapType1Class0sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                                      && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                                       && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia)
                                                                       );
                            switch (item)
                            {
                                case "Formula":
                                    AdapType1Class0sInfo = AdapType1Class0sInfo.Where(Adp => Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5))
                                                                          && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                          && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                          && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + "%")
                                                                          )
                                                                         .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                         .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                         .ThenBy(Adp => Adp.TcMma08)
                                                                         .ThenBy(Adp => Adp.TcMma11)
                                                                         .ThenBy(Adp => Adp.TcMma10)
                                                                         .ThenBy(Adp => Adp.TcMma24)
                                                                         ;
                                    break;
                                case "Formula_P2":
                                    AdapType1Class0sInfo = AdapType1Class0sInfo.Where(Adp => (Adp.TcMma08 + Convert.ToDecimal(_datas.FirstOrDefault().Suitadapter_c4)) >= Convert.ToDecimal(_datas.FirstOrDefault().LR)


                                                                            && Adp.TcMma13 >= _publicFunction.GRound((Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)), 0) && Adp.TcMma13 <= _publicFunction.GRound((Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp)), 0)
                                                                             && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                                            )
                                                                             .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                                            .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                            .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                                            .ThenBy(Adp => Adp.TcMma13)
                                                                            .ThenBy(Adp => Adp.TcMma08)
                                                                            .ThenBy(Adp => Adp.TcMma11)
                                                                            .ThenBy(Adp => Adp.TcMma10)
                                                                            .ThenBy(Adp => Adp.TcMma24)
                                                                            ;
                                    break;
                            }
                        }
                        else if (item == "formula2")
                        {
                            AdapType1Class0sInfo = AdapType1Class0sInfo.Where(Adp => (Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) / 2 || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) * 2)
                                                                  && Adp.TcMma08 >= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB)
                                                                   && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= Convert.ToDecimal(_datas.FirstOrDefault().LT) && Adp.TcMma04 == Convert.ToDecimal(_datas.FirstOrDefault().LA)
                                                                   && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().LZ) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                   && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))

                                                                    )
                                                                    .OrderBy(Adp => Adp.TcMma13)
                                                                    .ThenBy(Adp => Adp.TcMma08)
                                                                    .ThenBy(Adp => Adp.TcMma11)
                                                                    .ThenBy(Adp => Adp.TcMma10)
                                                                    .ThenBy(Adp => Adp.TcMma24)
                                                                    ;

                        }
                        else if (item == "Formula_type5")
                        {
                            AdapType1Class0sInfo = AdapType1Class0sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                        && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5)) && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                        && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                        && Adp.TcMma26 == _datas.FirstOrDefault().Motor_Interface && Adp.TcMma27 == _datas.FirstOrDefault().Motor_Screw_orientation
                                                        && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                        )
                                                        .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                        .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                        .ThenBy(Adp => Adp.TcMma08)
                                                        .ThenBy(Adp => Adp.TcMma11)
                                                        .ThenBy(Adp => Adp.TcMma10)
                                                        .ThenBy(Adp => Adp.TcMma24)
                                                        ;

                            if (_datas.FirstOrDefault().Motor_Screw_orientation == "N") //正鎖
                            {
                                AdapType1Class0sInfo = AdapType1Class0sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)));
                            }
                            else
                            {
                                AdapType1Class0sInfo = AdapType1Class0sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) + Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia));
                            }
                        }
                        return AdapType1Class0sInfo;
                    case "Adap_type1_class1":
                        var AdapType1Class1sInfo = _DataContext.AdapType1Class1s.AsQueryable();
                        if ("Formula,Formula_P2".Contains(item))
                        {
                            AdapType1Class1sInfo = AdapType1Class1sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                                      && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                                       && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia)
                                                                       );
                            switch (item)
                            {
                                case "Formula":
                                    AdapType1Class1sInfo = AdapType1Class1sInfo.Where(Adp => Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5))
                                                                          && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                          && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                          && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + "%")
                                                                          )
                                                                         .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                         .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                         .ThenBy(Adp => Adp.TcMma08)
                                                                         .ThenBy(Adp => Adp.TcMma11)
                                                                         .ThenBy(Adp => Adp.TcMma10)
                                                                         .ThenBy(Adp => Adp.TcMma24)
                                                                         ;
                                    break;
                                case "Formula_P2":
                                    AdapType1Class1sInfo = AdapType1Class1sInfo.Where(Adp => (Adp.TcMma08 + Convert.ToDecimal(_datas.FirstOrDefault().Suitadapter_c4)) >= Convert.ToDecimal(_datas.FirstOrDefault().LR)


                                                                            && Adp.TcMma13 >= _publicFunction.GRound((Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)), 0) && Adp.TcMma13 <= _publicFunction.GRound((Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp)), 0)
                                                                             && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                                            )
                                                                             .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                                            .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                            .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                                            .ThenBy(Adp => Adp.TcMma13)
                                                                            .ThenBy(Adp => Adp.TcMma08)
                                                                            .ThenBy(Adp => Adp.TcMma11)
                                                                            .ThenBy(Adp => Adp.TcMma10)
                                                                            .ThenBy(Adp => Adp.TcMma24)
                                                                            ;
                                    break;
                            }
                        }
                        else if (item == "formula2")
                        {
                            AdapType1Class1sInfo = AdapType1Class1sInfo.Where(Adp => (Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) / 2 || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) * 2)
                                                                  && Adp.TcMma08 >= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB)
                                                                   && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= Convert.ToDecimal(_datas.FirstOrDefault().LT) && Adp.TcMma04 == Convert.ToDecimal(_datas.FirstOrDefault().LA)
                                                                   && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().LZ) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                   && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))

                                                                    )
                                                                    .OrderBy(Adp => Adp.TcMma13)
                                                                    .ThenBy(Adp => Adp.TcMma08)
                                                                    .ThenBy(Adp => Adp.TcMma11)
                                                                    .ThenBy(Adp => Adp.TcMma10)
                                                                    .ThenBy(Adp => Adp.TcMma24)
                                                                    ;

                        }
                        else if (item == "Formula_type5")
                        {
                            AdapType1Class1sInfo = AdapType1Class1sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                        && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5)) && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                        && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                        && Adp.TcMma26 == _datas.FirstOrDefault().Motor_Interface && Adp.TcMma27 == _datas.FirstOrDefault().Motor_Screw_orientation
                                                        && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                        )
                                                        .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                        .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                        .ThenBy(Adp => Adp.TcMma08)
                                                        .ThenBy(Adp => Adp.TcMma11)
                                                        .ThenBy(Adp => Adp.TcMma10)
                                                        .ThenBy(Adp => Adp.TcMma24)
                                                        ;

                            if (_datas.FirstOrDefault().Motor_Screw_orientation == "N") //正鎖
                            {
                                AdapType1Class1sInfo = AdapType1Class1sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)));
                            }
                            else
                            {
                                AdapType1Class1sInfo = AdapType1Class1sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) + Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia));
                            }
                        }
                        return AdapType1Class1sInfo;
                    case "Adap_type1_class5":
                        var AdapType1Class5sInfo = _DataContext.AdapType1Class5s.AsQueryable();
                        if ("Formula,Formula_P2".Contains(item))
                        {
                            AdapType1Class5sInfo = AdapType1Class5sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                                      && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                                       && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia)
                                                                       );
                            switch (item)
                            {
                                case "Formula":
                                    AdapType1Class5sInfo = AdapType1Class5sInfo.Where(Adp => Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5))
                                                                          && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                          && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                          && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + "%")
                                                                          )
                                                                         .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                         .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                         .ThenBy(Adp => Adp.TcMma08)
                                                                         .ThenBy(Adp => Adp.TcMma11)
                                                                         .ThenBy(Adp => Adp.TcMma10)
                                                                         .ThenBy(Adp => Adp.TcMma24)
                                                                         ;
                                    break;
                                case "Formula_P2":
                                    AdapType1Class5sInfo = AdapType1Class5sInfo.Where(Adp => (Adp.TcMma08 + Convert.ToDecimal(_datas.FirstOrDefault().Suitadapter_c4)) >= Convert.ToDecimal(_datas.FirstOrDefault().LR)


                                                                            && Adp.TcMma13 >= _publicFunction.GRound((Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)), 0) && Adp.TcMma13 <= _publicFunction.GRound((Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp)), 0)
                                                                             && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                                            )
                                                                             .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                                            .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                            .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                                            .ThenBy(Adp => Adp.TcMma13)
                                                                            .ThenBy(Adp => Adp.TcMma08)
                                                                            .ThenBy(Adp => Adp.TcMma11)
                                                                            .ThenBy(Adp => Adp.TcMma10)
                                                                            .ThenBy(Adp => Adp.TcMma24)
                                                                            ;
                                    break;
                            }
                        }
                        else if (item == "formula2")
                        {
                            AdapType1Class5sInfo = AdapType1Class5sInfo.Where(Adp => (Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) / 2 || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) * 2)
                                                                  && Adp.TcMma08 >= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB)
                                                                   && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= Convert.ToDecimal(_datas.FirstOrDefault().LT) && Adp.TcMma04 == Convert.ToDecimal(_datas.FirstOrDefault().LA)
                                                                   && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().LZ) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                   && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))

                                                                    )
                                                                    .OrderBy(Adp => Adp.TcMma13)
                                                                    .ThenBy(Adp => Adp.TcMma08)
                                                                    .ThenBy(Adp => Adp.TcMma11)
                                                                    .ThenBy(Adp => Adp.TcMma10)
                                                                    .ThenBy(Adp => Adp.TcMma24)
                                                                    ;

                        }
                        else if (item == "Formula_type5")
                        {
                            AdapType1Class5sInfo = AdapType1Class5sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                        && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5)) && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                        && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                        && Adp.TcMma26 == _datas.FirstOrDefault().Motor_Interface && Adp.TcMma27 == _datas.FirstOrDefault().Motor_Screw_orientation
                                                        && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                        )
                                                        .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                        .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                        .ThenBy(Adp => Adp.TcMma08)
                                                        .ThenBy(Adp => Adp.TcMma11)
                                                        .ThenBy(Adp => Adp.TcMma10)
                                                        .ThenBy(Adp => Adp.TcMma24)
                                                        ;

                            if (_datas.FirstOrDefault().Motor_Screw_orientation == "N") //正鎖
                            {
                                AdapType1Class5sInfo = AdapType1Class5sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)));
                            }
                            else
                            {
                                AdapType1Class5sInfo = AdapType1Class5sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) + Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia));
                            }
                        }
                        return AdapType1Class5sInfo;
                    case "Adap_type5_class0":
                        var AdapType5Class0sInfo = _DataContext.AdapType5Class0s.AsQueryable();
                        if ("Formula,Formula_P2".Contains(item))
                        {
                            AdapType5Class0sInfo = AdapType5Class0sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                                      && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                                       && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia)
                                                                       );
                            switch (item)
                            {
                                case "Formula":
                                    AdapType5Class0sInfo = AdapType5Class0sInfo.Where(Adp => Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5))
                                                                          && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                          && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                          && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + "%")
                                                                          )
                                                                         .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                         .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                         .ThenBy(Adp => Adp.TcMma08)
                                                                         .ThenBy(Adp => Adp.TcMma11)
                                                                         .ThenBy(Adp => Adp.TcMma10)
                                                                         .ThenBy(Adp => Adp.TcMma24)
                                                                         ;
                                    break;
                                case "Formula_P2":
                                    AdapType5Class0sInfo = AdapType5Class0sInfo.Where(Adp => (Adp.TcMma08 + Convert.ToDecimal(_datas.FirstOrDefault().Suitadapter_c4)) >= Convert.ToDecimal(_datas.FirstOrDefault().LR)


                                                                            && Adp.TcMma13 >= _publicFunction.GRound((Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)), 0) && Adp.TcMma13 <= _publicFunction.GRound((Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp)), 0)
                                                                             && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                                            )
                                                                             .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                                            .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                            .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                                            .ThenBy(Adp => Adp.TcMma13)
                                                                            .ThenBy(Adp => Adp.TcMma08)
                                                                            .ThenBy(Adp => Adp.TcMma11)
                                                                            .ThenBy(Adp => Adp.TcMma10)
                                                                            .ThenBy(Adp => Adp.TcMma24)
                                                                            ;
                                    break;
                            }
                        }
                        else if (item == "formula2")
                        {
                            AdapType5Class0sInfo = AdapType5Class0sInfo.Where(Adp => (Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) / 2 || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) * 2)
                                                                  && Adp.TcMma08 >= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB)
                                                                   && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= Convert.ToDecimal(_datas.FirstOrDefault().LT) && Adp.TcMma04 == Convert.ToDecimal(_datas.FirstOrDefault().LA)
                                                                   && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().LZ) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                   && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))

                                                                    )
                                                                    .OrderBy(Adp => Adp.TcMma13)
                                                                    .ThenBy(Adp => Adp.TcMma08)
                                                                    .ThenBy(Adp => Adp.TcMma11)
                                                                    .ThenBy(Adp => Adp.TcMma10)
                                                                    .ThenBy(Adp => Adp.TcMma24)
                                                                    ;

                        }
                        else if (item == "Formula_type5")
                        {
                            AdapType5Class0sInfo = AdapType5Class0sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                        && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5)) && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                        && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                        && Adp.TcMma26 == _datas.FirstOrDefault().Motor_Interface && Adp.TcMma27 == _datas.FirstOrDefault().Motor_Screw_orientation
                                                        && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                        )
                                                        .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                        .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                        .ThenBy(Adp => Adp.TcMma08)
                                                        .ThenBy(Adp => Adp.TcMma11)
                                                        .ThenBy(Adp => Adp.TcMma10)
                                                        .ThenBy(Adp => Adp.TcMma24)
                                                        ;

                            if (_datas.FirstOrDefault().Motor_Screw_orientation == "N") //正鎖
                            {
                                AdapType5Class0sInfo = AdapType5Class0sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)));
                            }
                            else
                            {
                                AdapType5Class0sInfo = AdapType5Class0sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) + Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia));
                            }
                        }
                        return AdapType5Class0sInfo;
                    case "Adap_type5_class5":
                        var AdapType5Class5sInfo = _DataContext.AdapType5Class5s.AsQueryable();
                        if ("Formula,Formula_P2".Contains(item))
                        {
                            AdapType5Class5sInfo = AdapType5Class5sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                                      && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                                       && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia)
                                                                       );
                            switch (item)
                            {
                                case "Formula":
                                    AdapType5Class5sInfo = AdapType5Class5sInfo.Where(Adp => Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5))
                                                                          && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                          && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                          && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + "%")
                                                                          )
                                                                         .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                         .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                         .ThenBy(Adp => Adp.TcMma08)
                                                                         .ThenBy(Adp => Adp.TcMma11)
                                                                         .ThenBy(Adp => Adp.TcMma10)
                                                                         .ThenBy(Adp => Adp.TcMma24)
                                                                         ;
                                    break;
                                case "Formula_P2":
                                    AdapType5Class5sInfo = AdapType5Class5sInfo.Where(Adp => (Adp.TcMma08 + Convert.ToDecimal(_datas.FirstOrDefault().Suitadapter_c4)) >= Convert.ToDecimal(_datas.FirstOrDefault().LR)


                                                                            && Adp.TcMma13 >= _publicFunction.GRound((Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)), 0) && Adp.TcMma13 <= _publicFunction.GRound((Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp)), 0)
                                                                             && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                                            )
                                                                             .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                                            .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                            .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                                            .ThenBy(Adp => Adp.TcMma13)
                                                                            .ThenBy(Adp => Adp.TcMma08)
                                                                            .ThenBy(Adp => Adp.TcMma11)
                                                                            .ThenBy(Adp => Adp.TcMma10)
                                                                            .ThenBy(Adp => Adp.TcMma24)
                                                                            ;
                                    break;
                            }
                        }
                        else if (item == "formula2")
                        {
                            AdapType5Class5sInfo = AdapType5Class5sInfo.Where(Adp => (Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) / 2 || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) * 2)
                                                                  && Adp.TcMma08 >= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB)
                                                                   && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= Convert.ToDecimal(_datas.FirstOrDefault().LT) && Adp.TcMma04 == Convert.ToDecimal(_datas.FirstOrDefault().LA)
                                                                   && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().LZ) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                   && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))

                                                                    )
                                                                    .OrderBy(Adp => Adp.TcMma13)
                                                                    .ThenBy(Adp => Adp.TcMma08)
                                                                    .ThenBy(Adp => Adp.TcMma11)
                                                                    .ThenBy(Adp => Adp.TcMma10)
                                                                    .ThenBy(Adp => Adp.TcMma24)
                                                                    ;

                        }
                        else if (item == "Formula_type5")
                        {
                            AdapType5Class5sInfo = AdapType5Class5sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                        && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5)) && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                        && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                        && Adp.TcMma26 == _datas.FirstOrDefault().Motor_Interface && Adp.TcMma27 == _datas.FirstOrDefault().Motor_Screw_orientation
                                                        && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                        )
                                                        .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                        .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                        .ThenBy(Adp => Adp.TcMma08)
                                                        .ThenBy(Adp => Adp.TcMma11)
                                                        .ThenBy(Adp => Adp.TcMma10)
                                                        .ThenBy(Adp => Adp.TcMma24)
                                                        ;

                            if (_datas.FirstOrDefault().Motor_Screw_orientation == "N") //正鎖
                            {
                                AdapType5Class5sInfo = AdapType5Class5sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)));
                            }
                            else
                            {
                                AdapType5Class5sInfo = AdapType5Class5sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) + Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia));
                            }
                        }
                        return AdapType5Class5sInfo;
                    case "Adap_type1_classEx2":
                        var AdapType1Classex2sInfo = _DataContext.AdapType1Classex2s.AsQueryable();
                        if ("Formula,Formula_P2".Contains(item))
                        {
                            AdapType1Classex2sInfo = AdapType1Classex2sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                                      && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                                       && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia)
                                                                       );
                            switch (item)
                            {
                                case "Formula":
                                    AdapType1Classex2sInfo = AdapType1Classex2sInfo.Where(Adp => Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5))
                                                                          && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                          && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                          && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + "%")
                                                                          )
                                                                         .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                         .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                         .ThenBy(Adp => Adp.TcMma08)
                                                                         .ThenBy(Adp => Adp.TcMma11)
                                                                         .ThenBy(Adp => Adp.TcMma10)
                                                                         .ThenBy(Adp => Adp.TcMma24)
                                                                         ;
                                    break;
                                case "Formula_P2":
                                    AdapType1Classex2sInfo = AdapType1Classex2sInfo.Where(Adp => (Adp.TcMma08 + Convert.ToDecimal(_datas.FirstOrDefault().Suitadapter_c4)) >= Convert.ToDecimal(_datas.FirstOrDefault().LR)


                                                                            && Adp.TcMma13 >= _publicFunction.GRound((Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)), 0) && Adp.TcMma13 <= _publicFunction.GRound((Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp)), 0)
                                                                             && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                                            )
                                                                             .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                                            .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                            .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                                            .ThenBy(Adp => Adp.TcMma13)
                                                                            .ThenBy(Adp => Adp.TcMma08)
                                                                            .ThenBy(Adp => Adp.TcMma11)
                                                                            .ThenBy(Adp => Adp.TcMma10)
                                                                            .ThenBy(Adp => Adp.TcMma24)
                                                                            ;
                                    break;
                            }
                        }
                        else if (item == "formula2")
                        {
                            AdapType1Classex2sInfo = AdapType1Classex2sInfo.Where(Adp => (Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) / 2 || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) * 2)
                                                                  && Adp.TcMma08 >= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB)
                                                                   && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= Convert.ToDecimal(_datas.FirstOrDefault().LT) && Adp.TcMma04 == Convert.ToDecimal(_datas.FirstOrDefault().LA)
                                                                   && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().LZ) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                   && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))

                                                                    )
                                                                    .OrderBy(Adp => Adp.TcMma13)
                                                                    .ThenBy(Adp => Adp.TcMma08)
                                                                    .ThenBy(Adp => Adp.TcMma11)
                                                                    .ThenBy(Adp => Adp.TcMma10)
                                                                    .ThenBy(Adp => Adp.TcMma24)
                                                                    ;

                        }
                        else if (item == "Formula_type5")
                        {
                            AdapType1Classex2sInfo = AdapType1Classex2sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                        && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5)) && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                        && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                        && Adp.TcMma26 == _datas.FirstOrDefault().Motor_Interface && Adp.TcMma27 == _datas.FirstOrDefault().Motor_Screw_orientation
                                                        && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                        )
                                                        .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                        .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                        .ThenBy(Adp => Adp.TcMma08)
                                                        .ThenBy(Adp => Adp.TcMma11)
                                                        .ThenBy(Adp => Adp.TcMma10)
                                                        .ThenBy(Adp => Adp.TcMma24)
                                                        ;

                            if (_datas.FirstOrDefault().Motor_Screw_orientation == "N") //正鎖
                            {
                                AdapType1Classex2sInfo = AdapType1Classex2sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)));
                            }
                            else
                            {
                                AdapType1Classex2sInfo = AdapType1Classex2sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) + Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia));
                            }
                        }
                        return AdapType1Classex2sInfo;
                    case "Adap_type1_classEx3":
                        var AdapType1Classex3sInfo = _DataContext.AdapType1Classex3s.AsQueryable();
                        if ("Formula,Formula_P2".Contains(item))
                        {
                            AdapType1Classex3sInfo = AdapType1Classex3sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                                      && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                                       && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia)
                                                                       );
                            switch (item)
                            {
                                case "Formula":
                                    AdapType1Classex3sInfo = AdapType1Classex3sInfo.Where(Adp => Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5))
                                                                          && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                          && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                          && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + "%")
                                                                          )
                                                                         .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                         .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                         .ThenBy(Adp => Adp.TcMma08)
                                                                         .ThenBy(Adp => Adp.TcMma11)
                                                                         .ThenBy(Adp => Adp.TcMma10)
                                                                         .ThenBy(Adp => Adp.TcMma24)
                                                                         ;
                                    break;
                                case "Formula_P2":
                                    AdapType1Classex3sInfo = AdapType1Classex3sInfo.Where(Adp => (Adp.TcMma08 + Convert.ToDecimal(_datas.FirstOrDefault().Suitadapter_c4)) >= Convert.ToDecimal(_datas.FirstOrDefault().LR)


                                                                            && Adp.TcMma13 >= _publicFunction.GRound((Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)), 0) && Adp.TcMma13 <= _publicFunction.GRound((Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp)), 0)
                                                                             && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                                            )
                                                                             .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                                            .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                            .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                                            .ThenBy(Adp => Adp.TcMma13)
                                                                            .ThenBy(Adp => Adp.TcMma08)
                                                                            .ThenBy(Adp => Adp.TcMma11)
                                                                            .ThenBy(Adp => Adp.TcMma10)
                                                                            .ThenBy(Adp => Adp.TcMma24)
                                                                            ;
                                    break;
                            }
                        }
                        else if (item == "formula2")
                        {
                            AdapType1Classex3sInfo = AdapType1Classex3sInfo.Where(Adp => (Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) / 2 || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) * 2)
                                                                  && Adp.TcMma08 >= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB)
                                                                   && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= Convert.ToDecimal(_datas.FirstOrDefault().LT) && Adp.TcMma04 == Convert.ToDecimal(_datas.FirstOrDefault().LA)
                                                                   && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().LZ) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                   && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))

                                                                    )
                                                                    .OrderBy(Adp => Adp.TcMma13)
                                                                    .ThenBy(Adp => Adp.TcMma08)
                                                                    .ThenBy(Adp => Adp.TcMma11)
                                                                    .ThenBy(Adp => Adp.TcMma10)
                                                                    .ThenBy(Adp => Adp.TcMma24)
                                                                    ;

                        }
                        else if (item == "Formula_type5")
                        {
                            AdapType1Classex3sInfo = AdapType1Classex3sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                        && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5)) && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                        && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                        && Adp.TcMma26 == _datas.FirstOrDefault().Motor_Interface && Adp.TcMma27 == _datas.FirstOrDefault().Motor_Screw_orientation
                                                        && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                        )
                                                        .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                        .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                        .ThenBy(Adp => Adp.TcMma08)
                                                        .ThenBy(Adp => Adp.TcMma11)
                                                        .ThenBy(Adp => Adp.TcMma10)
                                                        .ThenBy(Adp => Adp.TcMma24)
                                                        ;

                            if (_datas.FirstOrDefault().Motor_Screw_orientation == "N") //正鎖
                            {
                                AdapType1Classex3sInfo = AdapType1Classex3sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)));
                            }
                            else
                            {
                                AdapType1Classex3sInfo = AdapType1Classex3sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) + Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia));
                            }
                        }
                        return AdapType1Classex3sInfo;
                    case "Adap_type1_classEx4":
                        var AdapType1Classex4sInfo = _DataContext.AdapType1Classex4s.AsQueryable();
                        if ("Formula,Formula_P2".Contains(item))
                        {
                            AdapType1Classex4sInfo = AdapType1Classex4sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                                      && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                                       && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia)
                                                                       );
                            switch (item)
                            {
                                case "Formula":
                                    AdapType1Classex4sInfo = AdapType1Classex4sInfo.Where(Adp => Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5))
                                                                          && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                          && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                          && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + "%")
                                                                          )
                                                                         .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                         .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                         .ThenBy(Adp => Adp.TcMma08)
                                                                         .ThenBy(Adp => Adp.TcMma11)
                                                                         .ThenBy(Adp => Adp.TcMma10)
                                                                         .ThenBy(Adp => Adp.TcMma24)
                                                                         ;
                                    break;
                                case "Formula_P2":
                                    AdapType1Classex4sInfo = AdapType1Classex4sInfo.Where(Adp => (Adp.TcMma08 + Convert.ToDecimal(_datas.FirstOrDefault().Suitadapter_c4)) >= Convert.ToDecimal(_datas.FirstOrDefault().LR)


                                                                            && Adp.TcMma13 >= _publicFunction.GRound((Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)), 0) && Adp.TcMma13 <= _publicFunction.GRound((Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp)), 0)
                                                                             && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                                            )
                                                                             .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                                            .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                            .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                                            .ThenBy(Adp => Adp.TcMma13)
                                                                            .ThenBy(Adp => Adp.TcMma08)
                                                                            .ThenBy(Adp => Adp.TcMma11)
                                                                            .ThenBy(Adp => Adp.TcMma10)
                                                                            .ThenBy(Adp => Adp.TcMma24)
                                                                            ;
                                    break;
                            }
                        }
                        else if (item == "formula2")
                        {
                            AdapType1Classex4sInfo = AdapType1Classex4sInfo.Where(Adp => (Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) / 2 || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) * 2)
                                                                  && Adp.TcMma08 >= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB)
                                                                   && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= Convert.ToDecimal(_datas.FirstOrDefault().LT) && Adp.TcMma04 == Convert.ToDecimal(_datas.FirstOrDefault().LA)
                                                                   && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().LZ) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                   && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))

                                                                    )
                                                                    .OrderBy(Adp => Adp.TcMma13)
                                                                    .ThenBy(Adp => Adp.TcMma08)
                                                                    .ThenBy(Adp => Adp.TcMma11)
                                                                    .ThenBy(Adp => Adp.TcMma10)
                                                                    .ThenBy(Adp => Adp.TcMma24)
                                                                    ;
                        }
                        else if (item == "Formula_type5")
                        {
                            AdapType1Classex4sInfo = AdapType1Classex4sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                        && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5)) && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                        && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                        && Adp.TcMma26 == _datas.FirstOrDefault().Motor_Interface && Adp.TcMma27 == _datas.FirstOrDefault().Motor_Screw_orientation
                                                        && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                        )
                                                        .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                        .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                        .ThenBy(Adp => Adp.TcMma08)
                                                        .ThenBy(Adp => Adp.TcMma11)
                                                        .ThenBy(Adp => Adp.TcMma10)
                                                        .ThenBy(Adp => Adp.TcMma24)
                                                        ;

                            if (_datas.FirstOrDefault().Motor_Screw_orientation == "N") //正鎖
                            {
                                AdapType1Classex4sInfo = AdapType1Classex4sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)));
                            }
                            else
                            {
                                AdapType1Classex4sInfo = AdapType1Classex4sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) + Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia));
                            }
                        }
                        return AdapType1Classex4sInfo;
                    case "Adap_type1_classEx5":
                        var AdapType1Classex5sInfo = _DataContext.AdapType1Classex5s.AsQueryable();
                        if ("Formula,Formula_P2".Contains(item))
                        {
                            AdapType1Classex5sInfo = AdapType1Classex5sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                                      && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                                       && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia)
                                                                       );
                            switch (item)
                            {
                                case "Formula":
                                    AdapType1Classex5sInfo = AdapType1Classex5sInfo.Where(Adp => Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5))
                                                                          && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                          && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                          && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + "%")
                                                                          )
                                                                         .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                         .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                         .ThenBy(Adp => Adp.TcMma08)
                                                                         .ThenBy(Adp => Adp.TcMma11)
                                                                         .ThenBy(Adp => Adp.TcMma10)
                                                                         .ThenBy(Adp => Adp.TcMma24)
                                                                         ;
                                    break;
                                case "Formula_P2":
                                    AdapType1Classex5sInfo = AdapType1Classex5sInfo.Where(Adp => (Adp.TcMma08 + Convert.ToDecimal(_datas.FirstOrDefault().Suitadapter_c4)) >= Convert.ToDecimal(_datas.FirstOrDefault().LR)


                                                                            && Adp.TcMma13 >= _publicFunction.GRound((Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)), 0) && Adp.TcMma13 <= _publicFunction.GRound((Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp)), 0)
                                                                             && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                                            )
                                                                             .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                                            .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                            .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                                            .ThenBy(Adp => Adp.TcMma13)
                                                                            .ThenBy(Adp => Adp.TcMma08)
                                                                            .ThenBy(Adp => Adp.TcMma11)
                                                                            .ThenBy(Adp => Adp.TcMma10)
                                                                            .ThenBy(Adp => Adp.TcMma24)
                                                                            ;
                                    break;
                            }
                        }
                        else if (item == "formula2")
                        {
                            AdapType1Classex5sInfo = AdapType1Classex5sInfo.Where(Adp => (Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) / 2 || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) * 2)
                                                                  && Adp.TcMma08 >= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB)
                                                                   && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= Convert.ToDecimal(_datas.FirstOrDefault().LT) && Adp.TcMma04 == Convert.ToDecimal(_datas.FirstOrDefault().LA)
                                                                   && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().LZ) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                   && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))

                                                                    )
                                                                    .OrderBy(Adp => Adp.TcMma13)
                                                                    .ThenBy(Adp => Adp.TcMma08)
                                                                    .ThenBy(Adp => Adp.TcMma11)
                                                                    .ThenBy(Adp => Adp.TcMma10)
                                                                    .ThenBy(Adp => Adp.TcMma24)
                                                                    ;

                        }
                        else if (item == "Formula_type5")
                        {
                            AdapType1Classex5sInfo = AdapType1Classex5sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                        && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5)) && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                        && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                        && Adp.TcMma26 == _datas.FirstOrDefault().Motor_Interface && Adp.TcMma27 == _datas.FirstOrDefault().Motor_Screw_orientation
                                                        && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                        )
                                                        .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                        .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                        .ThenBy(Adp => Adp.TcMma08)
                                                        .ThenBy(Adp => Adp.TcMma11)
                                                        .ThenBy(Adp => Adp.TcMma10)
                                                        .ThenBy(Adp => Adp.TcMma24)
                                                        ;

                            if (_datas.FirstOrDefault().Motor_Screw_orientation == "N") //正鎖
                            {
                                AdapType1Classex5sInfo = AdapType1Classex5sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)));
                            }
                            else
                            {
                                AdapType1Classex5sInfo = AdapType1Classex5sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) + Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia));
                            }
                        }
                        return AdapType1Classex5sInfo;
                    case "Adap_dir1_class1":
                        var AdapDir1Class1sInfo = _DataContext.AdapDir1Class1s.AsQueryable();
                        if ("Formula,Formula_P2".Contains(item))
                        {
                            AdapDir1Class1sInfo = AdapDir1Class1sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                                      && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                                       && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia)
                                                                       );
                            switch (item)
                            {
                                case "Formula":
                                    AdapDir1Class1sInfo = AdapDir1Class1sInfo.Where(Adp => Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5))
                                                                          && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                          && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                          && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + "%")
                                                                          )
                                                                         .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                         .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                         .ThenBy(Adp => Adp.TcMma08)
                                                                         .ThenBy(Adp => Adp.TcMma11)
                                                                         .ThenBy(Adp => Adp.TcMma10)
                                                                         .ThenBy(Adp => Adp.TcMma24)
                                                                         ;
                                    break;
                                case "Formula_P2":
                                    AdapDir1Class1sInfo = AdapDir1Class1sInfo.Where(Adp => (Adp.TcMma08 + Convert.ToDecimal(_datas.FirstOrDefault().Suitadapter_c4)) >= Convert.ToDecimal(_datas.FirstOrDefault().LR)


                                                                            && Adp.TcMma13 >= _publicFunction.GRound((Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)), 0) && Adp.TcMma13 <= _publicFunction.GRound((Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp)), 0)
                                                                             && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                                            )
                                                                             .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                                            .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                                            .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                                            .ThenBy(Adp => Adp.TcMma13)
                                                                            .ThenBy(Adp => Adp.TcMma08)
                                                                            .ThenBy(Adp => Adp.TcMma11)
                                                                            .ThenBy(Adp => Adp.TcMma10)
                                                                            .ThenBy(Adp => Adp.TcMma24)
                                                                            ;
                                    break;
                            }
                        }
                        else if (item == "formula2")
                        {
                            AdapDir1Class1sInfo = AdapDir1Class1sInfo.Where(Adp => (Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) / 2 || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN) * 2)
                                                                  && Adp.TcMma08 >= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB)
                                                                   && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= Convert.ToDecimal(_datas.FirstOrDefault().LT) && Adp.TcMma04 == Convert.ToDecimal(_datas.FirstOrDefault().LA)
                                                                   && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().LZ) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                   && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))

                                                                    )
                                                                    .OrderBy(Adp => Adp.TcMma13)
                                                                    .ThenBy(Adp => Adp.TcMma08)
                                                                    .ThenBy(Adp => Adp.TcMma11)
                                                                    .ThenBy(Adp => Adp.TcMma10)
                                                                    .ThenBy(Adp => Adp.TcMma24)
                                                                    ;

                        }
                        else if (item == "Formula_type5")
                        {
                            AdapDir1Class1sInfo = AdapDir1Class1sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 == Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                        && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5)) && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                        && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                        && Adp.TcMma26 == _datas.FirstOrDefault().Motor_Interface && Adp.TcMma27 == _datas.FirstOrDefault().Motor_Screw_orientation
                                                        && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                        )
                                                        .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                        .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                        .ThenBy(Adp => Adp.TcMma08)
                                                        .ThenBy(Adp => Adp.TcMma11)
                                                        .ThenBy(Adp => Adp.TcMma10)
                                                        .ThenBy(Adp => Adp.TcMma24)
                                                        ;

                            if (_datas.FirstOrDefault().Motor_Screw_orientation == "N") //正鎖
                            {
                                AdapDir1Class1sInfo = AdapDir1Class1sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)));
                            }
                            else
                            {
                                AdapDir1Class1sInfo = AdapDir1Class1sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) + Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia));
                            }
                        }
                        return AdapDir1Class1sInfo;
                    default:
                        return null;
                }
            }
            else
            {
                switch (_datas.FirstOrDefault().TableName)
                {
                    case "Adap_class1":
                        var AdapClass1sInfo = _DataContext.AdapClass1s.AsQueryable();
                        if ("Formula,Formula_P2".Contains(item))
                        {
                            AdapClass1sInfo = AdapClass1sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 <= (Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)) && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5))
                                                                && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                                && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                 && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                  && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + "%")
                                                                )
                                                         .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 + Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                         .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                         .ThenBy(Adp => Adp.TcMma08)
                                                         .ThenBy(Adp => Adp.TcMma11)
                                                         .ThenBy(Adp => Adp.TcMma10)
                                                         .ThenBy(Adp => Adp.TcMma24);
                        }
                        else if (item == "formula2")
                        {
                            AdapClass1sInfo = AdapClass1sInfo.Where(Adp => (Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) / 2 || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) * 2)
                                                                  && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma10 <= Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)
                                                                   && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= Convert.ToDecimal(_datas.FirstOrDefault().LT) && Adp.TcMma04 == Convert.ToDecimal(_datas.FirstOrDefault().LA2)
                                                                   && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().LZ2) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                   && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                    )
                                                                    .OrderBy(Adp => Adp.TcMma13)
                                                                    .ThenBy(Adp => Adp.TcMma08)
                                                                    .ThenBy(Adp => Adp.TcMma11)
                                                                    .ThenBy(Adp => Adp.TcMma10)
                                                                    .ThenBy(Adp => Adp.TcMma24)
                                                                    ;

                        }
                        else if (item == "Formula_type5")
                        {
                            AdapClass1sInfo = AdapClass1sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR)
                                                        && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma10 <= Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)
                                                        && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                        && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5)) && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                        && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                        && Adp.TcMma26 == _datas.FirstOrDefault().Motor_Interface && Adp.TcMma27 == _datas.FirstOrDefault().Motor_Screw_orientation
                                                        && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                        )
                                                        .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                        .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                        .ThenBy(Adp => Adp.TcMma08)
                                                        .ThenBy(Adp => Adp.TcMma11)
                                                        .ThenBy(Adp => Adp.TcMma10)
                                                        .ThenBy(Adp => Adp.TcMma24)
                                                        ;

                            if (_datas.FirstOrDefault().Motor_Screw_orientation == "N") //正鎖
                            {
                                AdapClass1sInfo = AdapClass1sInfo.Where(Adp => Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)));
                            }
                            else
                            {
                                AdapClass1sInfo = AdapClass1sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) + Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia));
                            }
                        }
                        return AdapClass1sInfo;
                    case "Adap_classEx2":
                        var AdapClassex2sInfo = _DataContext.AdapClassex2s.AsQueryable();
                        if ("Formula,Formula_P2".Contains(item))
                        {
                            AdapClassex2sInfo = AdapClassex2sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 <= (Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)) && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5))
                                                                && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                                && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                 && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                  && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + "%")
                                                                )
                                                         .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 + Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                         .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                         .ThenBy(Adp => Adp.TcMma08)
                                                         .ThenBy(Adp => Adp.TcMma11)
                                                         .ThenBy(Adp => Adp.TcMma10)
                                                         .ThenBy(Adp => Adp.TcMma24);
                        }
                        else if (item == "formula2")
                        {
                            AdapClassex2sInfo = AdapClassex2sInfo.Where(Adp => (Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) / 2 || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) * 2)
                                                                  && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma10 <= Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)
                                                                   && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= Convert.ToDecimal(_datas.FirstOrDefault().LT) && Adp.TcMma04 == Convert.ToDecimal(_datas.FirstOrDefault().LA2)
                                                                   && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().LZ2) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                   && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                    )
                                                                    .OrderBy(Adp => Adp.TcMma13)
                                                                    .ThenBy(Adp => Adp.TcMma08)
                                                                    .ThenBy(Adp => Adp.TcMma11)
                                                                    .ThenBy(Adp => Adp.TcMma10)
                                                                    .ThenBy(Adp => Adp.TcMma24)
                                                                    ;

                        }
                        else if (item == "Formula_type5")
                        {
                            AdapClassex2sInfo = AdapClassex2sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR)
                                                        && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma10 <= Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)
                                                        && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                        && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5)) && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                        && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                        && Adp.TcMma26 == _datas.FirstOrDefault().Motor_Interface && Adp.TcMma27 == _datas.FirstOrDefault().Motor_Screw_orientation
                                                        && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                        )
                                                        .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                        .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                        .ThenBy(Adp => Adp.TcMma08)
                                                        .ThenBy(Adp => Adp.TcMma11)
                                                        .ThenBy(Adp => Adp.TcMma10)
                                                        .ThenBy(Adp => Adp.TcMma24)
                                                        ;

                            if (_datas.FirstOrDefault().Motor_Screw_orientation == "N") //正鎖
                            {
                                AdapClassex2sInfo = AdapClassex2sInfo.Where(Adp => Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)));
                            }
                            else
                            {
                                AdapClassex2sInfo = AdapClassex2sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) + Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia));
                            }
                        }
                        return AdapClassex2sInfo;
                    case "Adap_classEx3":
                        var AdapClassex3sInfo = _DataContext.AdapClassex3s.AsQueryable();
                        if ("Formula,Formula_P2".Contains(item))
                        {
                            AdapClassex3sInfo = AdapClassex3sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 <= (Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)) && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5))
                                                                && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                                && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                 && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                  && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + "%")
                                                                )
                                                         .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 + Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                         .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                         .ThenBy(Adp => Adp.TcMma08)
                                                         .ThenBy(Adp => Adp.TcMma11)
                                                         .ThenBy(Adp => Adp.TcMma10)
                                                         .ThenBy(Adp => Adp.TcMma24);
                        }
                        else if (item == "formula2")
                        {
                            AdapClassex3sInfo = AdapClassex3sInfo.Where(Adp => (Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) / 2 || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) * 2)
                                                                  && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma10 <= Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)
                                                                   && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= Convert.ToDecimal(_datas.FirstOrDefault().LT) && Adp.TcMma04 == Convert.ToDecimal(_datas.FirstOrDefault().LA2)
                                                                   && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().LZ2) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                   && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                    )
                                                                    .OrderBy(Adp => Adp.TcMma13)
                                                                    .ThenBy(Adp => Adp.TcMma08)
                                                                    .ThenBy(Adp => Adp.TcMma11)
                                                                    .ThenBy(Adp => Adp.TcMma10)
                                                                    .ThenBy(Adp => Adp.TcMma24)
                                                                    ;

                        }
                        else if (item == "Formula_type5")
                        {
                            AdapClassex3sInfo = AdapClassex3sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR)
                                                        && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma10 <= Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)
                                                        && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                        && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5)) && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                        && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                        && Adp.TcMma26 == _datas.FirstOrDefault().Motor_Interface && Adp.TcMma27 == _datas.FirstOrDefault().Motor_Screw_orientation
                                                        && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                        )
                                                        .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                        .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                        .ThenBy(Adp => Adp.TcMma08)
                                                        .ThenBy(Adp => Adp.TcMma11)
                                                        .ThenBy(Adp => Adp.TcMma10)
                                                        .ThenBy(Adp => Adp.TcMma24)
                                                        ;

                            if (_datas.FirstOrDefault().Motor_Screw_orientation == "N") //正鎖
                            {
                                AdapClassex3sInfo = AdapClassex3sInfo.Where(Adp => Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)));
                            }
                            else
                            {
                                AdapClassex3sInfo = AdapClassex3sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) + Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia));
                            }
                        }
                        return AdapClassex3sInfo;
                    case "Adap_classEx4":
                        var AdapClassex4sInfo = _DataContext.AdapClassex4s.AsQueryable();
                        if ("Formula,Formula_P2".Contains(item))
                        {
                            AdapClassex4sInfo = AdapClassex4sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 <= (Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)) && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5))
                                                                && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                                && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                 && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                  && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + "%")
                                                                )
                                                         .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 + Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                         .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                         .ThenBy(Adp => Adp.TcMma08)
                                                         .ThenBy(Adp => Adp.TcMma11)
                                                         .ThenBy(Adp => Adp.TcMma10)
                                                         .ThenBy(Adp => Adp.TcMma24);
                        }
                        else if (item == "formula2")
                        {
                            AdapClassex4sInfo = AdapClassex4sInfo.Where(Adp => (Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) / 2 || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) * 2)
                                                                  && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma10 <= Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)
                                                                   && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= Convert.ToDecimal(_datas.FirstOrDefault().LT) && Adp.TcMma04 == Convert.ToDecimal(_datas.FirstOrDefault().LA2)
                                                                   && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().LZ2) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                   && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                    )
                                                                    .OrderBy(Adp => Adp.TcMma13)
                                                                    .ThenBy(Adp => Adp.TcMma08)
                                                                    .ThenBy(Adp => Adp.TcMma11)
                                                                    .ThenBy(Adp => Adp.TcMma10)
                                                                    .ThenBy(Adp => Adp.TcMma24)
                                                                    ;

                        }
                        else if (item == "Formula_type5")
                        {
                            AdapClassex4sInfo = AdapClassex4sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR)
                                                        && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma10 <= Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)
                                                        && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                        && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5)) && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                        && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                        && Adp.TcMma26 == _datas.FirstOrDefault().Motor_Interface && Adp.TcMma27 == _datas.FirstOrDefault().Motor_Screw_orientation
                                                        && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                        )
                                                        .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                        .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                        .ThenBy(Adp => Adp.TcMma08)
                                                        .ThenBy(Adp => Adp.TcMma11)
                                                        .ThenBy(Adp => Adp.TcMma10)
                                                        .ThenBy(Adp => Adp.TcMma24)
                                                        ;

                            if (_datas.FirstOrDefault().Motor_Screw_orientation == "N") //正鎖
                            {
                                AdapClassex4sInfo = AdapClassex4sInfo.Where(Adp => Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)));
                            }
                            else
                            {
                                AdapClassex4sInfo = AdapClassex4sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) + Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia));
                            }
                        }
                        return AdapClassex4sInfo;
                    case "Adap_type1_class0":
                        var AdapType1Class0sInfo = _DataContext.AdapType1Class0s.AsQueryable();
                        if ("Formula,Formula_P2".Contains(item))
                        {
                            AdapType1Class0sInfo = AdapType1Class0sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 <= (Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)) && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5))
                                                                && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                                && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                 && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                  && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + "%")
                                                                )
                                                         .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 + Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                         .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                         .ThenBy(Adp => Adp.TcMma08)
                                                         .ThenBy(Adp => Adp.TcMma11)
                                                         .ThenBy(Adp => Adp.TcMma10)
                                                         .ThenBy(Adp => Adp.TcMma24);
                        }
                        else if (item == "formula2")
                        {
                            AdapType1Class0sInfo = AdapType1Class0sInfo.Where(Adp => (Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) / 2 || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) * 2)
                                                                  && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma10 <= Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)
                                                                   && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= Convert.ToDecimal(_datas.FirstOrDefault().LT) && Adp.TcMma04 == Convert.ToDecimal(_datas.FirstOrDefault().LA2)
                                                                   && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().LZ2) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                   && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                    )
                                                                    .OrderBy(Adp => Adp.TcMma13)
                                                                    .ThenBy(Adp => Adp.TcMma08)
                                                                    .ThenBy(Adp => Adp.TcMma11)
                                                                    .ThenBy(Adp => Adp.TcMma10)
                                                                    .ThenBy(Adp => Adp.TcMma24)
                                                                    ;

                        }
                        else if (item == "Formula_type5")
                        {
                            AdapType1Class0sInfo = AdapType1Class0sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR)
                                                        && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma10 <= Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)
                                                        && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                        && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5)) && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                        && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                        && Adp.TcMma26 == _datas.FirstOrDefault().Motor_Interface && Adp.TcMma27 == _datas.FirstOrDefault().Motor_Screw_orientation
                                                        && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                        )
                                                        .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                        .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                        .ThenBy(Adp => Adp.TcMma08)
                                                        .ThenBy(Adp => Adp.TcMma11)
                                                        .ThenBy(Adp => Adp.TcMma10)
                                                        .ThenBy(Adp => Adp.TcMma24)
                                                        ;

                            if (_datas.FirstOrDefault().Motor_Screw_orientation == "N") //正鎖
                            {
                                AdapType1Class0sInfo = AdapType1Class0sInfo.Where(Adp => Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)));
                            }
                            else
                            {
                                AdapType1Class0sInfo = AdapType1Class0sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) + Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia));
                            }
                        }
                        return AdapType1Class0sInfo;
                    case "Adap_type1_class1":
                        var AdapType1Class1sInfo = _DataContext.AdapType1Class1s.AsQueryable();
                        if ("Formula,Formula_P2".Contains(item))
                        {
                            AdapType1Class1sInfo = AdapType1Class1sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 <= (Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)) && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5))
                                                                && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                                && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                 && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                  && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + "%")
                                                                )
                                                         .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 + Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                         .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                         .ThenBy(Adp => Adp.TcMma08)
                                                         .ThenBy(Adp => Adp.TcMma11)
                                                         .ThenBy(Adp => Adp.TcMma10)
                                                         .ThenBy(Adp => Adp.TcMma24);
                        }
                        else if (item == "formula2")
                        {
                            AdapType1Class1sInfo = AdapType1Class1sInfo.Where(Adp => (Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) / 2 || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) * 2)
                                                                  && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma10 <= Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)
                                                                   && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= Convert.ToDecimal(_datas.FirstOrDefault().LT) && Adp.TcMma04 == Convert.ToDecimal(_datas.FirstOrDefault().LA2)
                                                                   && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().LZ2) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                   && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                    )
                                                                    .OrderBy(Adp => Adp.TcMma13)
                                                                    .ThenBy(Adp => Adp.TcMma08)
                                                                    .ThenBy(Adp => Adp.TcMma11)
                                                                    .ThenBy(Adp => Adp.TcMma10)
                                                                    .ThenBy(Adp => Adp.TcMma24)
                                                                    ;

                        }
                        else if (item == "Formula_type5")
                        {
                            AdapType1Class1sInfo = AdapType1Class1sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR)
                                                        && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma10 <= Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)
                                                        && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                        && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5)) && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                        && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                        && Adp.TcMma26 == _datas.FirstOrDefault().Motor_Interface && Adp.TcMma27 == _datas.FirstOrDefault().Motor_Screw_orientation
                                                        && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                        )
                                                        .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                        .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                        .ThenBy(Adp => Adp.TcMma08)
                                                        .ThenBy(Adp => Adp.TcMma11)
                                                        .ThenBy(Adp => Adp.TcMma10)
                                                        .ThenBy(Adp => Adp.TcMma24)
                                                        ;

                            if (_datas.FirstOrDefault().Motor_Screw_orientation == "N") //正鎖
                            {
                                AdapType1Class1sInfo = AdapType1Class1sInfo.Where(Adp => Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)));
                            }
                            else
                            {
                                AdapType1Class1sInfo = AdapType1Class1sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) + Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia));
                            }
                        }
                        return AdapType1Class1sInfo;
                    case "Adap_type1_class5":
                        var AdapType1Class5sInfo = _DataContext.AdapType1Class5s.AsQueryable();
                        if ("Formula,Formula_P2".Contains(item))
                        {
                            AdapType1Class5sInfo = AdapType1Class5sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 <= (Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)) && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5))
                                                                && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                                && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                 && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                  && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + "%")
                                                                )
                                                         .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 + Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                         .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                         .ThenBy(Adp => Adp.TcMma08)
                                                         .ThenBy(Adp => Adp.TcMma11)
                                                         .ThenBy(Adp => Adp.TcMma10)
                                                         .ThenBy(Adp => Adp.TcMma24);
                        }
                        else if (item == "formula2")
                        {
                            AdapType1Class5sInfo = AdapType1Class5sInfo.Where(Adp => (Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) / 2 || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) * 2)
                                                                  && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma10 <= Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)
                                                                   && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= Convert.ToDecimal(_datas.FirstOrDefault().LT) && Adp.TcMma04 == Convert.ToDecimal(_datas.FirstOrDefault().LA2)
                                                                   && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().LZ2) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                   && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                    )
                                                                    .OrderBy(Adp => Adp.TcMma13)
                                                                    .ThenBy(Adp => Adp.TcMma08)
                                                                    .ThenBy(Adp => Adp.TcMma11)
                                                                    .ThenBy(Adp => Adp.TcMma10)
                                                                    .ThenBy(Adp => Adp.TcMma24)
                                                                    ;

                        }
                        else if (item == "Formula_type5")
                        {
                            AdapType1Class5sInfo = AdapType1Class5sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR)
                                                        && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma10 <= Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)
                                                        && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                        && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5)) && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                        && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                        && Adp.TcMma26 == _datas.FirstOrDefault().Motor_Interface && Adp.TcMma27 == _datas.FirstOrDefault().Motor_Screw_orientation
                                                        && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                        )
                                                        .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                        .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                        .ThenBy(Adp => Adp.TcMma08)
                                                        .ThenBy(Adp => Adp.TcMma11)
                                                        .ThenBy(Adp => Adp.TcMma10)
                                                        .ThenBy(Adp => Adp.TcMma24)
                                                        ;

                            if (_datas.FirstOrDefault().Motor_Screw_orientation == "N") //正鎖
                            {
                                AdapType1Class5sInfo = AdapType1Class5sInfo.Where(Adp => Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)));
                            }
                            else
                            {
                                AdapType1Class5sInfo = AdapType1Class5sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) + Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia));
                            }
                        }
                        return AdapType1Class5sInfo;
                    case "Adap_type5_class0":
                        var AdapType5Class0sInfo = _DataContext.AdapType5Class0s.AsQueryable();
                        if ("Formula,Formula_P2".Contains(item))
                        {
                            AdapType5Class0sInfo = AdapType5Class0sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 <= (Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)) && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5))
                                                                && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                                && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                 && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                  && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + "%")
                                                                )
                                                         .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 + Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                         .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                         .ThenBy(Adp => Adp.TcMma08)
                                                         .ThenBy(Adp => Adp.TcMma11)
                                                         .ThenBy(Adp => Adp.TcMma10)
                                                         .ThenBy(Adp => Adp.TcMma24);
                        }
                        else if (item == "formula2")
                        {
                            AdapType5Class0sInfo = AdapType5Class0sInfo.Where(Adp => (Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) / 2 || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) * 2)
                                                                  && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma10 <= Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)
                                                                   && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= Convert.ToDecimal(_datas.FirstOrDefault().LT) && Adp.TcMma04 == Convert.ToDecimal(_datas.FirstOrDefault().LA2)
                                                                   && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().LZ2) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                   && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                    )
                                                                    .OrderBy(Adp => Adp.TcMma13)
                                                                    .ThenBy(Adp => Adp.TcMma08)
                                                                    .ThenBy(Adp => Adp.TcMma11)
                                                                    .ThenBy(Adp => Adp.TcMma10)
                                                                    .ThenBy(Adp => Adp.TcMma24)
                                                                    ;

                        }
                        else if (item == "Formula_type5")
                        {
                            AdapType5Class0sInfo = AdapType5Class0sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR)
                                                        && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma10 <= Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)
                                                        && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                        && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5)) && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                        && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                        && Adp.TcMma26 == _datas.FirstOrDefault().Motor_Interface && Adp.TcMma27 == _datas.FirstOrDefault().Motor_Screw_orientation
                                                        && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                        )
                                                        .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                        .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                        .ThenBy(Adp => Adp.TcMma08)
                                                        .ThenBy(Adp => Adp.TcMma11)
                                                        .ThenBy(Adp => Adp.TcMma10)
                                                        .ThenBy(Adp => Adp.TcMma24)
                                                        ;

                            if (_datas.FirstOrDefault().Motor_Screw_orientation == "N") //正鎖
                            {
                                AdapType5Class0sInfo = AdapType5Class0sInfo.Where(Adp => Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)));
                            }
                            else
                            {
                                AdapType5Class0sInfo = AdapType5Class0sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) + Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia));
                            }
                        }
                        return AdapType5Class0sInfo;
                    case "Adap_type5_class5":
                        var AdapType5Class5sInfo = _DataContext.AdapType5Class5s.AsQueryable();
                        if ("Formula,Formula_P2".Contains(item))
                        {
                            AdapType5Class5sInfo = AdapType5Class5sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 <= (Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)) && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5))
                                                                && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                                && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                 && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                  && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + "%")
                                                                )
                                                         .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 + Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                         .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                         .ThenBy(Adp => Adp.TcMma08)
                                                         .ThenBy(Adp => Adp.TcMma11)
                                                         .ThenBy(Adp => Adp.TcMma10)
                                                         .ThenBy(Adp => Adp.TcMma24);
                        }
                        else if (item == "formula2")
                        {
                            AdapType5Class5sInfo = AdapType5Class5sInfo.Where(Adp => (Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) / 2 || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) * 2)
                                                                  && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma10 <= Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)
                                                                   && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= Convert.ToDecimal(_datas.FirstOrDefault().LT) && Adp.TcMma04 == Convert.ToDecimal(_datas.FirstOrDefault().LA2)
                                                                   && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().LZ2) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                   && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                    )
                                                                    .OrderBy(Adp => Adp.TcMma13)
                                                                    .ThenBy(Adp => Adp.TcMma08)
                                                                    .ThenBy(Adp => Adp.TcMma11)
                                                                    .ThenBy(Adp => Adp.TcMma10)
                                                                    .ThenBy(Adp => Adp.TcMma24)
                                                                    ;

                        }
                        else if (item == "Formula_type5")
                        {
                            AdapType5Class5sInfo = AdapType5Class5sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR)
                                                        && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma10 <= Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)
                                                        && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                        && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5)) && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                        && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                        && Adp.TcMma26 == _datas.FirstOrDefault().Motor_Interface && Adp.TcMma27 == _datas.FirstOrDefault().Motor_Screw_orientation
                                                        && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                        )
                                                        .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                        .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                        .ThenBy(Adp => Adp.TcMma08)
                                                        .ThenBy(Adp => Adp.TcMma11)
                                                        .ThenBy(Adp => Adp.TcMma10)
                                                        .ThenBy(Adp => Adp.TcMma24)
                                                        ;

                            if (_datas.FirstOrDefault().Motor_Screw_orientation == "N") //正鎖
                            {
                                AdapType5Class5sInfo = AdapType5Class5sInfo.Where(Adp => Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)));
                            }
                            else
                            {
                                AdapType5Class5sInfo = AdapType5Class5sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) + Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia));
                            }
                        }
                        return AdapType5Class5sInfo;
                    case "Adap_type1_classEx2":
                        var AdapType1Classex2sInfo = _DataContext.AdapType1Classex2s.AsQueryable();
                        if ("Formula,Formula_P2".Contains(item))
                        {
                            AdapType1Classex2sInfo = AdapType1Classex2sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 <= (Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)) && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5))
                                                                && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                                && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                 && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                  && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + "%")
                                                                )
                                                         .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 + Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                         .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                         .ThenBy(Adp => Adp.TcMma08)
                                                         .ThenBy(Adp => Adp.TcMma11)
                                                         .ThenBy(Adp => Adp.TcMma10)
                                                         .ThenBy(Adp => Adp.TcMma24);
                        }
                        else if (item == "formula2")
                        {
                            AdapType1Classex2sInfo = AdapType1Classex2sInfo.Where(Adp => (Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) / 2 || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) * 2)
                                                                  && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma10 <= Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)
                                                                   && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= Convert.ToDecimal(_datas.FirstOrDefault().LT) && Adp.TcMma04 == Convert.ToDecimal(_datas.FirstOrDefault().LA2)
                                                                   && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().LZ2) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                   && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                    )
                                                                    .OrderBy(Adp => Adp.TcMma13)
                                                                    .ThenBy(Adp => Adp.TcMma08)
                                                                    .ThenBy(Adp => Adp.TcMma11)
                                                                    .ThenBy(Adp => Adp.TcMma10)
                                                                    .ThenBy(Adp => Adp.TcMma24)
                                                                    ;
                        }
                        else if (item == "Formula_type5")
                        {
                            AdapType1Classex2sInfo = AdapType1Classex2sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR)
                                                        && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma10 <= Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)
                                                        && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                        && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5)) && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                        && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                        && Adp.TcMma26 == _datas.FirstOrDefault().Motor_Interface && Adp.TcMma27 == _datas.FirstOrDefault().Motor_Screw_orientation
                                                        && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                        )
                                                        .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                        .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                        .ThenBy(Adp => Adp.TcMma08)
                                                        .ThenBy(Adp => Adp.TcMma11)
                                                        .ThenBy(Adp => Adp.TcMma10)
                                                        .ThenBy(Adp => Adp.TcMma24)
                                                        ;

                            if (_datas.FirstOrDefault().Motor_Screw_orientation == "N") //正鎖
                            {
                                AdapType1Classex2sInfo = AdapType1Classex2sInfo.Where(Adp => Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)));
                            }
                            else
                            {
                                AdapType1Classex2sInfo = AdapType1Classex2sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) + Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia));
                            }
                        }
                        return AdapType1Classex2sInfo;
                    case "Adap_type1_classEx3":
                        var AdapType1Classex3sInfo = _DataContext.AdapType1Classex3s.AsQueryable();
                        if ("Formula,Formula_P2".Contains(item))
                        {
                            AdapType1Classex3sInfo = AdapType1Classex3sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 <= (Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)) && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5))
                                                                && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                                && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                 && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                  && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + "%")
                                                                )
                                                         .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 + Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                         .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                         .ThenBy(Adp => Adp.TcMma08)
                                                         .ThenBy(Adp => Adp.TcMma11)
                                                         .ThenBy(Adp => Adp.TcMma10)
                                                         .ThenBy(Adp => Adp.TcMma24);
                        }
                        else if (item == "formula2")
                        {
                            AdapType1Classex3sInfo = AdapType1Classex3sInfo.Where(Adp => (Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) / 2 || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) * 2)
                                                                  && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma10 <= Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)
                                                                   && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= Convert.ToDecimal(_datas.FirstOrDefault().LT) && Adp.TcMma04 == Convert.ToDecimal(_datas.FirstOrDefault().LA2)
                                                                   && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().LZ2) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                   && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                    )
                                                                    .OrderBy(Adp => Adp.TcMma13)
                                                                    .ThenBy(Adp => Adp.TcMma08)
                                                                    .ThenBy(Adp => Adp.TcMma11)
                                                                    .ThenBy(Adp => Adp.TcMma10)
                                                                    .ThenBy(Adp => Adp.TcMma24)
                                                                    ;

                        }
                        else if (item == "Formula_type5")
                        {
                            AdapType1Classex3sInfo = AdapType1Classex3sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR)
                                                        && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma10 <= Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)
                                                        && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                        && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5)) && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                        && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                        && Adp.TcMma26 == _datas.FirstOrDefault().Motor_Interface && Adp.TcMma27 == _datas.FirstOrDefault().Motor_Screw_orientation
                                                        && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                        )
                                                        .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                        .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                        .ThenBy(Adp => Adp.TcMma08)
                                                        .ThenBy(Adp => Adp.TcMma11)
                                                        .ThenBy(Adp => Adp.TcMma10)
                                                        .ThenBy(Adp => Adp.TcMma24)
                                                        ;

                            if (_datas.FirstOrDefault().Motor_Screw_orientation == "N") //正鎖
                            {
                                AdapType1Classex3sInfo = AdapType1Classex3sInfo.Where(Adp => Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)));
                            }
                            else
                            {
                                AdapType1Classex3sInfo = AdapType1Classex3sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) + Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia));
                            }
                        }
                        return AdapType1Classex3sInfo;
                    case "Adap_type1_classEx4":
                        var AdapType1Classex4sInfo = _DataContext.AdapType1Classex4s.AsQueryable();
                        if ("Formula,Formula_P2".Contains(item))
                        {
                            AdapType1Classex4sInfo = AdapType1Classex4sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 <= (Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)) && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5))
                                                                && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                                && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                 && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                  && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + "%")
                                                                )
                                                         .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 + Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                         .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                         .ThenBy(Adp => Adp.TcMma08)
                                                         .ThenBy(Adp => Adp.TcMma11)
                                                         .ThenBy(Adp => Adp.TcMma10)
                                                         .ThenBy(Adp => Adp.TcMma24);
                        }
                        else if (item == "formula2")
                        {
                            AdapType1Classex4sInfo = AdapType1Classex4sInfo.Where(Adp => (Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) / 2 || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) * 2)
                                                                  && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma10 <= Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)
                                                                   && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= Convert.ToDecimal(_datas.FirstOrDefault().LT) && Adp.TcMma04 == Convert.ToDecimal(_datas.FirstOrDefault().LA2)
                                                                   && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().LZ2) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                   && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                    )
                                                                    .OrderBy(Adp => Adp.TcMma13)
                                                                    .ThenBy(Adp => Adp.TcMma08)
                                                                    .ThenBy(Adp => Adp.TcMma11)
                                                                    .ThenBy(Adp => Adp.TcMma10)
                                                                    .ThenBy(Adp => Adp.TcMma24)
                                                                    ;

                        }
                        else if (item == "Formula_type5")
                        {
                            AdapType1Classex4sInfo = AdapType1Classex4sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR)
                                                        && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma10 <= Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)
                                                        && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                        && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5)) && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                        && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                        && Adp.TcMma26 == _datas.FirstOrDefault().Motor_Interface && Adp.TcMma27 == _datas.FirstOrDefault().Motor_Screw_orientation
                                                        && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                        )
                                                        .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                        .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                        .ThenBy(Adp => Adp.TcMma08)
                                                        .ThenBy(Adp => Adp.TcMma11)
                                                        .ThenBy(Adp => Adp.TcMma10)
                                                        .ThenBy(Adp => Adp.TcMma24)
                                                        ;

                            if (_datas.FirstOrDefault().Motor_Screw_orientation == "N") //正鎖
                            {
                                AdapType1Classex4sInfo = AdapType1Classex4sInfo.Where(Adp => Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)));
                            }
                            else
                            {
                                AdapType1Classex4sInfo = AdapType1Classex4sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) + Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia));
                            }
                        }
                        return AdapType1Classex4sInfo;
                    case "Adap_type1_classEx5":
                        var AdapType1Classex5sInfo = _DataContext.AdapType1Classex5s.AsQueryable();
                        if ("Formula,Formula_P2".Contains(item))
                        {
                            AdapType1Classex5sInfo = AdapType1Classex5sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 <= (Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)) && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5))
                                                                && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                                && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                 && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                  && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + "%")
                                                                )
                                                         .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 + Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                         .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                         .ThenBy(Adp => Adp.TcMma08)
                                                         .ThenBy(Adp => Adp.TcMma11)
                                                         .ThenBy(Adp => Adp.TcMma10)
                                                         .ThenBy(Adp => Adp.TcMma24);
                        }
                        else if (item == "formula2")
                        {
                            AdapType1Classex5sInfo = AdapType1Classex5sInfo.Where(Adp => (Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) / 2 || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) * 2)
                                                                  && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma10 <= Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)
                                                                   && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= Convert.ToDecimal(_datas.FirstOrDefault().LT) && Adp.TcMma04 == Convert.ToDecimal(_datas.FirstOrDefault().LA2)
                                                                   && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().LZ2) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                   && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                    )
                                                                    .OrderBy(Adp => Adp.TcMma13)
                                                                    .ThenBy(Adp => Adp.TcMma08)
                                                                    .ThenBy(Adp => Adp.TcMma11)
                                                                    .ThenBy(Adp => Adp.TcMma10)
                                                                    .ThenBy(Adp => Adp.TcMma24)
                                                                    ;

                        }
                        else if (item == "Formula_type5")
                        {
                            AdapType1Classex5sInfo = AdapType1Classex5sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR)
                                                        && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma10 <= Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)
                                                        && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                        && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5)) && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                        && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                        && Adp.TcMma26 == _datas.FirstOrDefault().Motor_Interface && Adp.TcMma27 == _datas.FirstOrDefault().Motor_Screw_orientation
                                                        && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                        )
                                                        .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                        .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                        .ThenBy(Adp => Adp.TcMma08)
                                                        .ThenBy(Adp => Adp.TcMma11)
                                                        .ThenBy(Adp => Adp.TcMma10)
                                                        .ThenBy(Adp => Adp.TcMma24)
                                                        ;

                            if (_datas.FirstOrDefault().Motor_Screw_orientation == "N") //正鎖
                            {
                                AdapType1Classex5sInfo = AdapType1Classex5sInfo.Where(Adp => Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)));
                            }
                            else
                            {
                                AdapType1Classex5sInfo = AdapType1Classex5sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) + Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia));
                            }
                        }
                        return AdapType1Classex5sInfo;
                    case "Adap_dir1_class1":
                        var AdapDir1Class1sInfo = _DataContext.AdapDir1Class1s.AsQueryable();
                        if ("Formula,Formula_P2".Contains(item))
                        {
                            AdapDir1Class1sInfo = AdapDir1Class1sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 <= (Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)) && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5))
                                                                && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                                && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                 && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                  && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + "%")
                                                                )
                                                         .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 + Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                         .ThenByDescending(Adp => Adp.TcMma01.Substring(0, 1))
                                                         .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                         .ThenBy(Adp => Adp.TcMma08)
                                                         .ThenBy(Adp => Adp.TcMma11)
                                                         .ThenBy(Adp => Adp.TcMma10)
                                                         .ThenBy(Adp => Adp.TcMma24);
                        }
                        else if (item == "formula2")
                        {
                            AdapDir1Class1sInfo = AdapDir1Class1sInfo.Where(Adp => (Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) / 2 || Adp.TcMma07 == Convert.ToDecimal(_datas.FirstOrDefault().LN2) * 2)
                                                                  && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR) && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma10 <= Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)
                                                                   && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE) && Adp.TcMma12 >= Convert.ToDecimal(_datas.FirstOrDefault().LT) && Adp.TcMma04 == Convert.ToDecimal(_datas.FirstOrDefault().LA2)
                                                                   && Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().LZ2) && Adp.TcMma16 == Convert.ToDecimal(_datas.FirstOrDefault().AWidth1)
                                                                   && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                                    )
                                                                    .OrderBy(Adp => Adp.TcMma13)
                                                                    .ThenBy(Adp => Adp.TcMma08)
                                                                    .ThenBy(Adp => Adp.TcMma11)
                                                                    .ThenBy(Adp => Adp.TcMma10)
                                                                    .ThenBy(Adp => Adp.TcMma24)
                                                                    ;

                        }
                        else if (item == "Formula_type5")
                        {
                            AdapDir1Class1sInfo = AdapDir1Class1sInfo.Where(Adp => Adp.TcMma23.ToUpper() == "YES" && Adp.TcMma08 >= (Convert.ToDecimal(_datas.FirstOrDefault().LR) - 1) && Adp.TcMma09 <= Convert.ToDecimal(_datas.FirstOrDefault().LR)
                                                        && Adp.TcMma10 >= Convert.ToDecimal(_datas.FirstOrDefault().LB) && Adp.TcMma10 <= Convert.ToDecimal(_datas.FirstOrDefault().LB) + Convert.ToDecimal(0.1)
                                                        && Adp.TcMma11 >= Convert.ToDecimal(_datas.FirstOrDefault().LE)
                                                        && Adp.TcMma12 >= (Convert.ToDecimal(_datas.FirstOrDefault().LT) + Convert.ToDecimal(0.5)) && Adp.TcMma04 >= (Convert.ToDecimal(_datas.FirstOrDefault().LA) - Convert.ToDecimal(_datas.FirstOrDefault().LAtmp))
                                                        && Adp.TcMma13 >= (Convert.ToDecimal(_datas.FirstOrDefault().LC) - Convert.ToDecimal(_datas.FirstOrDefault().Tmp)) && Adp.TcMma13 <= (Convert.ToDecimal(_datas.FirstOrDefault().LC) + Convert.ToDecimal(_datas.FirstOrDefault().Tmp))
                                                        && Adp.TcMma26 == _datas.FirstOrDefault().Motor_Interface && Adp.TcMma27 == _datas.FirstOrDefault().Motor_Screw_orientation
                                                        && EF.Functions.Like(Adp.TcMma01, _datas.FirstOrDefault().L_string + _datas.FirstOrDefault().Suitadapter.Substring(1) + "%")
                                                        )
                                                        .OrderBy(Adp => Math.Abs((double)(Adp.TcMma04 - Convert.ToDecimal(_datas.FirstOrDefault().LA))))
                                                        .ThenBy(Adp => Math.Abs((double)(Adp.TcMma13 - Convert.ToDecimal(_datas.FirstOrDefault().LC))))
                                                        .ThenBy(Adp => Adp.TcMma08)
                                                        .ThenBy(Adp => Adp.TcMma11)
                                                        .ThenBy(Adp => Adp.TcMma10)
                                                        .ThenBy(Adp => Adp.TcMma24)
                                                        ;

                            if (_datas.FirstOrDefault().Motor_Screw_orientation == "N") //正鎖
                            {
                                AdapDir1Class1sInfo = AdapDir1Class1sInfo.Where(Adp => Adp.TcMma05 == Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia) && Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)));
                            }
                            else
                            {
                                AdapDir1Class1sInfo = AdapDir1Class1sInfo.Where(Adp => Adp.TcMma04 <= (Convert.ToDecimal(_datas.FirstOrDefault().LA) + Convert.ToDecimal(_datas.FirstOrDefault().LAtmp)) + Convert.ToDecimal(_datas.FirstOrDefault().ScrewDia));
                            }
                        }
                        return AdapDir1Class1sInfo;
                    default:
                        return null;
                }
            }

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
            //if (tempOrder.Count > 0)
            //{
            //    Order _order = new Order
            //    {
            //        OrderId = Convert.ToString(tempOrder["OrderId"]),
            //        Pono = Convert.ToString(tempOrder["Pono"])
            //    };
            //    var update = _web2Context.Orders.Where(x => x.OrderId == _order.OrderId);
            //}

            if (tempOrderDetail.Count > 0)
            {
                OrderDetail _orderDetail = new OrderDetail
                {
                    OrderId = Convert.ToString(tempOrderDetail["OrderId"]),
                    OrderDetailId = Convert.ToInt32(tempOrderDetail["OrderDetailId"]),
                    Qty = Convert.ToInt32(tempOrderDetail["Qty"]),
                    Memo = Convert.ToString(tempOrderDetail["Memo"]),
                    SubTot = Convert.ToDouble(tempOrderDetail["SubTot"]),
                    Customize = Convert.ToString(tempOrderDetail["Customize"])
                };
                var update = _web2Context.OrderDetails.Where(x => x.OrderId == _orderDetail.OrderId && x.OrderDetailId == _orderDetail.OrderDetailId);
                if (update.Count() > 0)
                {
                    update.SingleOrDefault().Qty = _orderDetail.Qty;
                    update.SingleOrDefault().Memo = _orderDetail.Memo;
                    update.SingleOrDefault().SubTot = _orderDetail.SubTot;
                    update.SingleOrDefault().Customize = _orderDetail.Customize;
                    _web2Context.SaveChanges();
                }
            }

        }

        public void DeleteOrderList(dynamic tempOrderDetail)
        {

            if (tempOrderDetail.Count > 0)
            {
                OrderDetail _orderDetail = new OrderDetail
                {
                    OrderId = Convert.ToString(tempOrderDetail["OrderId"]),
                    OrderDetailId = Convert.ToInt32(tempOrderDetail["OrderDetailId"]),
                };
                var deleteData = _web2Context.OrderDetails.Where(x => x.OrderId == _orderDetail.OrderId && x.OrderDetailId == _orderDetail.OrderDetailId);
                if (deleteData.Count() > 0)
                {
                    _web2Context.OrderDetails.Remove(deleteData.SingleOrDefault());
                    _web2Context.SaveChanges();
                }
            }

        }

        private class Datas
        {
            public object Data { get; set; }
            public object Data2 { get; set; }
        }

        public class AdpDatas
        {
            public string TableName { get; set; }
            public string LBck { get; set; }
            public string LR { get; set; }
            public string LB { get; set; }
            public string LE { get; set; }
            public string LT { get; set; }
            public string LA { get; set; }
            public string ScrewDia { get; set; }
            public string AWidth1 { get; set; }
            public string LC { get; set; }
            public string LAtmp { get; set; }
            public string Tmp { get; set; }
            public string L_string { get; set; }
            public int RblAdpCount { get; set; }
            public string G_Reducer_One_piece { get; set; }
            public string G_Reducer_One_piece_used { get; set; }
            public string Reducer_No { get; set; }
            public string Suitadapter_c4 { get; set; }
            public string Suitadapter { get; set; }
            public string Brand { get; set; }
            public string Spec { get; set; }
            public string LN { get; set; }
            public string LZ { get; set; }
            public string LN2 { get; set; }
            public string LA2 { get; set; }
            public string LZ2 { get; set; }
            public string Motor_Interface { get; set; }
            public string Motor_Screw_orientation { get; set; }
            public double Reducer_D9 { get; set; }
            public double Reducer_D7 { get; set; }
            public string R1No { get; set; }
            public double Reducer_A1 { get; set; }
            public double Reducer_A2 { get; set; }
        }

    }
}
