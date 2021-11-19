using APEX_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utf8Json;
//using Jil;
using System.Text.Json;
using System.Text.Json.Serialization;
using APEX_API.Services;
using Microsoft.AspNetCore.Cors;
using APEX_API.PublicServices;
using APEX_API.TopprodModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APEX_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OrderController : ControllerBase
    {
        private readonly web2Context _web2Context;
        private readonly DataContext _DataContext;
        private readonly OrderService _orderService;
        private readonly PublicFunctions _publicFunction;
        private readonly PublicOrders _publicOrder;
        private readonly IWebHostEnvironment _hostEnvironment;


        public OrderController(web2Context context, DataContext oracontext, OrderService orderService, PublicFunctions publicFunctions, PublicOrders publicOrders, IWebHostEnvironment environment)
        {
            _web2Context = context;
            _DataContext = oracontext;
            _orderService = orderService;
            _publicFunction = publicFunctions;
            _publicOrder = publicOrders;
            _hostEnvironment = environment;
        }

        // GET: api/<OrderController>
        [Route("")]
        [Route("Home")]
        [Route("[controller]/[action]")]
        public string Index()
        {
            return Convert.ToString("yes");
        }

        [HttpGet]
        public ActionResult Get(string jsonStr)
        {
            //var cd = _web2Context.Sales.Where(x => x.SalesId == "D00482").Select(x => x.Pwd).FirstOrDefault();
            //var jsonObject = JSON.DeserializeDynamic(jsonStr);
            //string username = jsonObject.username;
            //string password = jsonObject.password;
            //int MemberCount  = _web2Context.Sales.Where(x => x.SalesId == username && x.Pwd == password && x.ClaimLevel !=0).Count();
            var ks = _web2Context.Sales.Where(x => x.SalesId == "D00482").Select(x => new { x.ClaimLevel, x.Pwd, x.SalesId }).ToList();
            var ls = Jil.JSON.Deserialize<dynamic>(Utf8Json.JsonSerializer.ToJsonString(ks));
            //if (MemberCount > 0)
            //{
            //    return Ok(new { code = 200, message = "登入成功"});
            //}
            return BadRequest(new { code = 400, message = "登入失敗，帳號或密碼為空" });
        }

        [HttpGet("[action]")]
        [EnableCors("CorsPolicy")]
        public ActionResult GearBoxInit(string feStr)
        {
            if (!string.IsNullOrEmpty(feStr))
            {
                dynamic OData = Utf8Json.JsonSerializer.Deserialize<dynamic>(feStr.ToString());
                var MortorInfo = _orderService.GetMotorInfo(Convert.ToString(OData["isSale"]));
                var ModelInfo = !string.IsNullOrEmpty(Convert.ToString(OData["tcOek01"])) ? _orderService.GetModelInfo(OData) : "";
                var GearBoxInfo = _orderService.GetGearBoxInfo(OData);
                return Ok(new { code = 200, MortorInfo = MortorInfo, ModelInfo = ModelInfo, GearBoxInfo = GearBoxInfo });
            }

            return BadRequest(new { code = 400, message = "Error Request" });
        }

        [HttpGet("[action]")]
        [EnableCors("CorsPolicy")]
        public ActionResult GearBoxDetail(string feStr)
        {
            if (!string.IsNullOrEmpty(feStr))
            {
                dynamic OData = Utf8Json.JsonSerializer.Deserialize<dynamic>(feStr.ToString());
                List<TcOekFile> MortorInfo = _orderService.GetMotorInfoDetail(OData);
                List<Reducer1Order> ReducerInfo = new List<Reducer1Order> { };
                List<Reducer1Order> RatioInfo = new List<Reducer1Order> { };
                List<Reducer1Order> BacklashShaft = new List<Reducer1Order> { };

                ReducerInfo = _orderService.GetReducer(OData, Convert.ToDecimal(MortorInfo.FirstOrDefault().TcOek05), Convert.ToDecimal(MortorInfo.FirstOrDefault().TcOek04), Convert.ToDecimal(MortorInfo.FirstOrDefault().TcOek08), Convert.ToDecimal(MortorInfo.FirstOrDefault().TcOek09), "", "", "");
                RatioInfo = _orderService.GetReducer(OData, Convert.ToDecimal(MortorInfo.FirstOrDefault().TcOek05), Convert.ToDecimal(MortorInfo.FirstOrDefault().TcOek04), Convert.ToDecimal(MortorInfo.FirstOrDefault().TcOek08), Convert.ToDecimal(MortorInfo.FirstOrDefault().TcOek09), "Ratio", (!string.IsNullOrEmpty(Convert.ToString(OData["GBModel"])) ? Convert.ToString(OData["GBModel"]) : ReducerInfo.FirstOrDefault().TcMmd03), MortorInfo.FirstOrDefault().TcOek27);
                BacklashShaft = _orderService.GetReducerSB(OData, Convert.ToDecimal(MortorInfo.FirstOrDefault().TcOek09), (!string.IsNullOrEmpty(Convert.ToString(OData["GBModel"])) ? Convert.ToString(OData["GBModel"]) : ReducerInfo.FirstOrDefault().TcMmd03), (!string.IsNullOrEmpty(Convert.ToString(OData["Ratio"])) ? Convert.ToDecimal(OData["Ratio"]) : Convert.ToDecimal(RatioInfo.FirstOrDefault().TcMmd04)));
                return Ok(new { code = 200, ReducerInfo = ReducerInfo, RatioInfo = RatioInfo, BacklashShaft = BacklashShaft });
            }

            return BadRequest(new { code = 400, message = "Error Request" });
        }

        [HttpGet("[action]")]
        [EnableCors("CorsPolicy")]
        public ActionResult GearBoxReSult(string feStr)
        {
            if (!string.IsNullOrEmpty(feStr))
            {
                dynamic OData = Utf8Json.JsonSerializer.Deserialize<dynamic>(feStr.ToString());
                string R14Groups = "R14,R26,R28,R30,R32,R57,R58,R59,R60,RB6,RB8,RC1,RC3,RC7,RC8,RC9,RC5";
                string R65Groups = "R65,R66,R67,R69,R86,RD1,RD2,RD3,RD4,RD5,RE1,RD9,RE2,RE9,R40,RF2,RB3,RA9,RB2,RB4,RF4,RE3,RE4,RJ9,RJ5,RK9,RK5,R53,R54,RF1,RR1,RR2,RR3,RR4,RR5,RR6,RR7,RR8,RR9,RS3,RS4,RS1,RS2,RS5,RS6,RS7,R42";//tmp_type 3,
                string R25Groups = "R25";
                string RG4Groups = "RG4,RG5";
                string Cprod = string.Empty, orderCode = string.Empty, reducerNO = string.Empty, tmp_file = string.Empty;
                string ReJsonData = string.Empty;
                string tmp_flag = string.Empty;
                string bomList = string.Empty;
                Dictionary<string, string> ResultInfo = new();

                if (R14Groups.Contains(Convert.ToString(OData["GearBox"]["GBSeries"])))
                {
                    ResultInfo = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(_publicOrder.GBResult(OData, "3", "false", ""));
                    tmp_flag = "3";
                }
                else if (R25Groups.Contains(Convert.ToString(OData["GearBox"]["GBSeries"])))
                {
                    ResultInfo = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(_publicOrder.GBResult(OData, "4", "false", ""));
                    tmp_flag = "4";
                }
                else if (R65Groups.Contains(Convert.ToString(OData["GearBox"]["GBSeries"])))
                {
                    ResultInfo = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(_publicOrder.GBResult(OData, "2", "false", ""));
                    tmp_flag = "2";
                }
                else if (RG4Groups.Contains(Convert.ToString(OData["GearBox"]["GBSeries"])))
                {
                    ResultInfo = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(_publicOrder.GBResult(OData, "5", "false", ""));
                    tmp_flag = "5";
                }
                else
                {
                    ResultInfo = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(_publicOrder.GBResult(OData, "1", "false", ""));
                    tmp_flag = "1";
                }

                //------------------------------------判斷 orderCode 
                if (Convert.ToString(OData["GearBox"]["Ratio"]).IndexOf(".") >= 0)
                {
                    String temp1 = Convert.ToString(Convert.ToSingle(Convert.ToString(OData["GearBox"]["Ratio"]).Replace("*", "")) + 10000);

                    if (temp1.Length > 5)
                    {
                        Cprod = Convert.ToString(OData["GearBox"]["GBModel"]) + " - " + temp1.Substring(temp1.Length - 5);
                    }
                    else
                    {
                        Cprod = Convert.ToString(OData["GearBox"]["GBModel"]) + " - " + temp1;
                    }
                }
                else
                {
                    String temp1 = Convert.ToString(Convert.ToSingle(OData["GearBox"]["Ratio"].Replace("*", "")) + 10000);

                    if (Convert.ToSingle(Convert.ToSingle(OData["GearBox"]["Ratio"]).Replace("*", "")) < 1000)
                    {
                        Cprod = Convert.ToString(OData["GearBox"]["GBModel"]) + " - " + temp1.Substring(temp1.Length - 3);
                    }
                    else
                    {
                        Cprod = Convert.ToString(OData["GearBox"]["GBModel"]) + " - " + temp1.Substring(temp1.Length - 4);
                    }
                }

                if (!string.IsNullOrEmpty(Convert.ToString(OData["GearBox"]["Shaft"])))
                {
                    if (Convert.ToString(OData["GearBox"]["Shaft"]) == "S1")
                    {
                        reducerNO = Convert.ToString(ResultInfo["PartNo"]) + "1";

                        Cprod = Cprod + " - " + Convert.ToString(OData["GearBox"]["Shaft"]);   //S1
                    }
                    else if (Convert.ToString(OData["GearBox"]["Shaft"]) == "S2")
                    {
                        reducerNO = Convert.ToString(ResultInfo["PartNo"]) + "2";

                        Cprod = Cprod + " - " + Convert.ToString(OData["GearBox"]["Shaft"]);   //S2
                    }
                    else if (Convert.ToString(OData["GearBox"]["Shaft"]) == "S3")
                    {
                        reducerNO = Convert.ToString(ResultInfo["PartNo"]) + "3";

                        Cprod = Cprod + " - " + Convert.ToString(OData["GearBox"]["Shaft"]);   //S3
                    }
                    else if (Convert.ToString(OData["GearBox"]["Shaft"]) == "S4")
                    {
                        reducerNO = Convert.ToString(ResultInfo["PartNo"]) + "4";

                        Cprod = Cprod + " - " + Convert.ToString(OData["GearBox"]["Shaft"]);   //S4
                    }
                    else
                    {
                        reducerNO = Convert.ToString(ResultInfo["PartNo"]) + "X";
                    }
                }
                else
                {
                    reducerNO = Convert.ToString(ResultInfo["PartNo"]) + "X";
                }

                if (!string.IsNullOrEmpty(Convert.ToString(OData["GearBox"]["Backlash"])))
                {
                    if (Convert.ToString(OData["GearBox"]["Backlash"]) == "P0")
                    {
                        reducerNO = reducerNO + "0";

                        Cprod = Cprod + " - " + Convert.ToString(OData["GearBox"]["Backlash"]);   //P0
                    }

                    if (Convert.ToString(OData["GearBox"]["Backlash"]) == "P1")
                    {
                        reducerNO = reducerNO + "1";

                        Cprod = Cprod + " - " + Convert.ToString(OData["GearBox"]["Backlash"]);   //P1
                    }

                    if (Convert.ToString(OData["GearBox"]["Backlash"]) == "P2")
                    {
                        reducerNO = reducerNO + "2";

                        Cprod = Cprod + " - " + Convert.ToString(OData["GearBox"]["Backlash"]);   //P2
                    }
                }
                else
                {
                    reducerNO = reducerNO + "X";
                }

                orderCode = Cprod + " / " + Convert.ToString(OData["Motor"]["Brand"]) + " " + Convert.ToString(OData["Motor"]["Spec"]);
                tmp_file = _hostEnvironment.WebRootPath + "/image/prod/" + Convert.ToString(ResultInfo["R1No"]) + ".png";
                tmp_file = System.IO.File.Exists(tmp_file) ? tmp_file : _hostEnvironment.WebRootPath + "image/ComingSoon.gif";

                if (tmp_flag == "3")  //ADS
                {
                    //減速機
                    lbl_x32.Text = Resources.Resource.x32 + "<BR><font color=red>(Gearbox only)</font>";
                    //lbl_x33.Text = Convert.ToString(ViewState["Reducer_Spec"]) + " / " + reducerNO.Replace("R", "A");
                    lbl_x33.Text = Convert.ToString(ViewState["Reducer_Spec"]) + " / " + Apex_class.Fun_Str_Replace(reducerNO, "R", "A");
                    lbl_Gearbox_Stock.Text = l_Gearbox_stock + "<sup><font color=red>(2)</font></sup><BR><font color=red>(Gearbox only)</font>";

                    //馬達連接板
                    tr_x34.Visible = false;
                    lbl_x34.Text = "";
                    lbl_x35.Text = "";

                    //固定板
                    tr_x37.Visible = false;
                    lbl_x37.Text = "";
                    lbl_x38.Text = "";

                    //軸襯套
                    tr_x39.Visible = false;
                    lbl_x39.Text = "";
                    lbl_x40.Text = "";

                    //內六角螺絲
                    tr_x41.Visible = false;
                    lbl_x41.Text = "";
                    lbl_x42.Text = "";

                    //墊圈
                    tr_x43.Visible = false;
                    lbl_x43.Text = "";
                    lbl_x44.Text = "";

                    tr_x45.Visible = false;
                    //SUNGEAR
                    lbl_x45.Text = "";
                    lbl_x46.Text = "";

                    tr_x47.Visible = false;
                    //螺絲
                    lbl_x47.Text = "";
                    lbl_x48.Text = "";

                }
                else if (tmp_flag == "4") //AM
                {
                    //減速機
                    lbl_x32.Text = Resources.Resource.x32 + "<BR><font color=red>(Gearbox only)</font>";
                    //lbl_x33.Text = Convert.ToString(ViewState["Reducer_Spec"]) + " / " + newrdeucer.Replace("R", "A");
                    lbl_x33.Text = Convert.ToString(ViewState["Reducer_Spec"]) + " / " + Apex_class.Fun_Str_Replace(newrdeucer, "R", "A");
                    lbl_Gearbox_Stock.Text = l_Gearbox_stock + "<sup><font color=red>(2)</font></sup><BR><font color=red>(Gearbox only)</font>";

                    //馬達連接板
                    tr_x34.Visible = true;
                    lbl_x34.Text = Resources.Resource.x34;
                    lbl_x35.Text = Convert.ToString(txtadaperNo).Replace("P", "S");
                    lbl_adaper_stock.Text = get_stock_msg(txtadaperNo);
                    //其它順位連接板
                    //if (string.IsNullOrEmpty(adapterNx) == false)
                    //{
                    //    lbl_x35.Text = lbl_x35.Text + "<br>" + Resources.Resource.oth_adapter + "：" + adapterNx;
                    //}

                    //固定板
                    tr_x37.Visible = false;
                    lbl_x37.Text = "";
                    lbl_x38.Text = "";

                    //軸襯套
                    tr_x39.Visible = false;
                    lbl_x39.Text = "";
                    lbl_x40.Text = "";

                    //內六角螺絲
                    tr_x41.Visible = false;
                    lbl_x41.Text = "";
                    lbl_x42.Text = "";

                    //墊圈
                    tr_x43.Visible = false;
                    lbl_x43.Text = "";
                    lbl_x44.Text = "";

                    tr_x45.Visible = true;
                    //SUNGEAR
                    lbl_x45.Text = Resources.Resource.x45;
                    lbl_x46.Text = txtSUNGEAR.Replace("P", "S");

                    tr_x47.Visible = true;
                    //螺絲
                    lbl_x47.Text = Resources.Resource.x47;
                    lbl_x48.Text = txtScrew;

                }
                else   //  AB P2
                {
                    //減速機
                    lbl_x32.Text = Resources.Resource.x32 + "<BR><font color=red>(Gearbox only)</font>";
                    //lbl_x33.Text = Cprod + " / " + reducerNO.Replace("R", "A");
                    lbl_x33.Text = Cprod + " / " + Apex_class.Fun_Str_Replace(reducerNO, "R", "A");
                    //New 1-Piece Design, combining 
                    //Gearbox rear-cover and motor adapter in one, 
                    //Fully compatible to the separated version. 
                    lbl_Gearbox_Stock.Text = l_Gearbox_stock + "<sup><font color=red>(2)</font></sup><BR><font color=red>(Gearbox only)</font>";

                    //馬達連接板
                    tr_x34.Visible = true;
                    lbl_x34.Text = Resources.Resource.x34;
                    if (Convert.ToString(ViewState["plate_1"]).Length >= 5)
                    {
                        if (Convert.ToString(ViewState["plate_1"]).Substring(0, 5) == "P0405")
                        {
                            lbl_x35.Text = Convert.ToString(ViewState["plate_2"]) + " / " + Convert.ToString(ViewState["plate_1"]);
                            lbl_adaper_stock.Text = get_stock_msg(Convert.ToString(ViewState["plate_1"]));
                        }
                        else
                        {
                            lbl_x35.Text = Convert.ToString(ViewState["plate_2"]) + " / " + Convert.ToString(ViewState["plate_1"]).Replace("P", "S").Replace("O", "U");
                            lbl_adaper_stock.Text = get_stock_msg(Convert.ToString(ViewState["plate_1"]));
                        }
                    }
                    else
                    {
                        lbl_x35.Text = Convert.ToString(ViewState["plate_2"]) + " / " + Convert.ToString(ViewState["plate_1"]).Replace("P", "S").Replace("O", "U");
                        lbl_adaper_stock.Text = get_stock_msg(Convert.ToString(ViewState["plate_1"]));
                    }

                    //固定板
                    tr_x37.Visible = true;
                    lbl_x37.Text = Resources.Resource.x37;

                    if (adp7000 != "None")
                    {
                        if (tmp_flag != "3")
                        {
                            //lbl_x38.Text = Convert.ToString(Session["plate_1"]) + " = " + adp7000;
                            lbl_x38.Text = Convert.ToString(ViewState["plate_1"]) + " = " + adp7000;
                        }
                    }

                    FixPlate = FixPlate.Trim();

                    if (FixPlate != "")
                    {
                        lbl_x38.Text = FixPlate;
                        lbl_fixplate_stock.Text = get_stock_msg(FixPlate);
                    }
                    else
                    {
                        lbl_x38.Text = "（No need）";
                        lbl_fixplate_stock.Text = "";
                    }

                    //軸襯套
                    tr_x39.Visible = true;
                    lbl_x39.Text = Resources.Resource.x39;

                    if (txtBushing == "無需軸襯套" || txtBushing == " ")
                    {
                        lbl_x40.Text = "（No need）";
                        lbl_bushing_stock.Text = "";
                    }
                    else if (txtBushing == "Bushing not available, please contact APEX sales!!")
                    {
                        lbl_x40.Text = txtBushing;
                        lbl_bushing_stock.Text = "";
                    }
                    else
                    {
                        //消除中文 20141106
                        if (txtBushing.IndexOf("(") >= 0)
                        {
                            lbl_x40.Text = txtBushing.Substring(0, txtBushing.IndexOf("("));
                            lbl_bushing_stock.Text = get_stock_msg(txtBushing.Substring(0, 11));
                            if (Convert.ToString(ViewState["Bushing_Spec"]).Length > 0)
                            {
                                lbl_x40.Text = Convert.ToString(ViewState["Bushing_Spec"]).Substring(0, Convert.ToString(ViewState["Bushing_Spec"]).IndexOf("(")) + " / " + Convert.ToString(ViewState["Bushing_No"]);
                            }
                        }
                        else
                        {
                            if (txtBushing.IndexOf("舊") >= 0)
                            {
                                lbl_x40.Text = txtBushing.Substring(0, txtBushing.IndexOf("舊"));
                                lbl_bushing_stock.Text = get_stock_msg(txtBushing.Substring(0, 11));
                                lbl_x40.Text = Convert.ToString(ViewState["Bushing_Spec"]).Substring(0, Convert.ToString(ViewState["Bushing_Spec"]).IndexOf("舊")) + " / " + Convert.ToString(ViewState["Bushing_No"]);
                            }
                            else
                            {
                                lbl_x40.Text = txtBushing.Replace("舊", "Old Part Number");
                                lbl_bushing_stock.Text = get_stock_msg(txtBushing.Substring(0, 11));
                                lbl_x40.Text = Convert.ToString(ViewState["Bushing_Spec"]).Replace("舊", "Old Part Number") + " / " + Convert.ToString(ViewState["Bushing_No"]);
                            }
                        }
                    }

                    //內六角螺絲Screw
                    tr_x41.Visible = true;
                    lbl_x41.Text = Resources.Resource.x41;

                    //消除中文 20141106
                    if (txtScrew_conj == "Without Screws")
                    {
                        lbl_x42.Text = "（Without Screws）";
                    }
                    else
                    {
                        if (txtScrew_conj.IndexOf("(") >= 0)
                        {
                            lbl_x42.Text = txtScrew_conj.Substring(0, txtScrew_conj.IndexOf("("));
                            lbl_x42.Text = Convert.ToString(ViewState["Screw_Spec"]).Substring(0, Convert.ToString(ViewState["Screw_Spec"]).IndexOf("(")) + " / " + Convert.ToString(ViewState["Screw_No"]);
                        }
                        else
                        {
                            if (txtScrew_conj.IndexOf("舊") >= 0)
                            {
                                lbl_x42.Text = txtScrew_conj.Substring(0, txtScrew_conj.IndexOf("舊"));
                                lbl_x42.Text = Convert.ToString(ViewState["Screw_Spec"]).Substring(0, Convert.ToString(ViewState["Screw_Spec"]).IndexOf("舊")) + " / " + Convert.ToString(ViewState["Screw_No"]);
                            }
                            else
                            {
                                lbl_x42.Text = txtScrew_conj.Replace("舊", "Old Part Number").Replace("全牙", " full thread");
                                lbl_x42.Text = Convert.ToString(ViewState["Screw_Spec"]).Replace("舊", "Old Part Number").Replace("全牙", " full thread") + " / " + Convert.ToString(ViewState["Screw_No"]);
                            }
                        }
                    }

                    //彈簧墊圈Washer
                    tr_x43.Visible = true;
                    lbl_x43.Text = Resources.Resource.x43;

                    //消除中文 20141106
                    if (txtWasher == "Without Washer")
                    {
                        lbl_x44.Text = "（Without Washer）";
                    }
                    else
                    {
                        if (txtWasher.IndexOf("(") >= 0)
                        {
                            lbl_x44.Text = txtWasher.Substring(0, txtWasher.IndexOf("("));
                            lbl_x44.Text = Convert.ToString(ViewState["Washer_Spec"]).Substring(0, Convert.ToString(ViewState["Washer_Spec"]).IndexOf("(")) + " / " + Convert.ToString(ViewState["Washer_No"]);
                        }
                        else
                        {
                            if (txtWasher.IndexOf("舊") >= 0)
                            {
                                lbl_x44.Text = txtWasher.Substring(0, txtWasher.IndexOf("舊"));
                                lbl_x44.Text = Convert.ToString(ViewState["Washer_Spec"]).Substring(0, Convert.ToString(ViewState["Washer_Spec"]).IndexOf("舊")) + " / " + Convert.ToString(ViewState["Washer_No"]);
                            }
                            else
                            {
                                lbl_x44.Text = txtWasher.Replace("舊", "Old Part Number").Replace("染黑", "BLACK OXIDIXING");
                                lbl_x44.Text = Convert.ToString(ViewState["Washer_Spec"]).Replace("舊", "Old Part Number").Replace("染黑", "BLACK OXIDIXING") + " / " + Convert.ToString(ViewState["Washer_No"]);
                            }
                        }
                    }

                    tr_x45.Visible = false;
                    //SUNGEAR
                    lbl_x45.Text = "";
                    lbl_x46.Text = "";

                    tr_x47.Visible = false;
                    //螺絲
                    lbl_x47.Text = "";
                    lbl_x48.Text = "";
                }



                //return Ok(new { code = 200, ReducerInfo = ReducerInfo, RatioInfo = RatioInfo, BacklashShaft = BacklashShaft });

                ReJsonData = "{\"PicPath\" :\"" + tmp_file + "\",\"orderCode\" :\"" + orderCode + "\",\"sFile\" :\"" + Convert.ToString(ResultInfo["sFile"]) + "\" ,\"class\" :\"" + Convert.ToString(ResultInfo["class"]) + "\"}";
                return Ok(new { code = 200, Message = "" });
            }

            return BadRequest(new { code = 400, message = "Error Request" });
        }

        [HttpGet("[action]")]
        [EnableCors("CorsPolicy")]
        public ActionResult CustSearchInfo(string CustId)
        {
            var joinList = _orderService.GetCustInfo(CustId);
            return Ok(new { code = 200, CustInfo = joinList });
        }


        [HttpGet("[action]")]
        [EnableCors("CorsPolicy")]
        public ActionResult ProductPrice(string feStr)
        {
            Double BasePrice = 0;
            Double SellingPrice = 0;
            Double Discount = 0;
            Double DiscountPrice = 0;
            Double FinalCharge = 0;
            if (!string.IsNullOrEmpty(feStr))
            {
                dynamic OData = Utf8Json.JsonSerializer.Deserialize<dynamic>(feStr.ToString());
                switch (Convert.ToString(OData["OrderType"]))
                {
                    case "Rack":
                        BasePrice = _orderService.GetRackBasePrice(Convert.ToString(OData["CustId"]), Convert.ToString(OData["PartNo"]), Convert.ToString(OData["Currency"]), Convert.ToInt32(OData["Qty"]));
                        Discount = _orderService.GetRackLargeDiscount(Convert.ToString(OData["Spec"]), Convert.ToInt16(OData["Qty"]), Convert.ToString(OData["CustId"]));
                        DiscountPrice = System.Math.Round(BasePrice * (1 - Discount), 0, MidpointRounding.AwayFromZero);
                        FinalCharge = DiscountPrice;
                        break;
                    case "Pinion":
                        BasePrice = _orderService.GetRackBasePrice(Convert.ToString(OData["CustId"]), Convert.ToString(OData["PartNo"]), Convert.ToString(OData["Currency"]), Convert.ToInt32(OData["Qty"]));
                        Discount = _orderService.GetPinionLargeDiscount(Convert.ToString(OData["Spec"]), Convert.ToInt16(OData["Qty"]), Convert.ToString(OData["CustId"]));
                        DiscountPrice = System.Math.Round(BasePrice * (1 - Discount), 0, MidpointRounding.AwayFromZero);
                        FinalCharge = DiscountPrice;
                        break;
                    default:
                        BasePrice = _orderService.GetBasePrice(Convert.ToString(OData["CustId"]), Convert.ToString(OData["PartNo"]), Convert.ToString(OData["Currency"]), Convert.ToInt32(OData["Qty"]));
                        Discount = _publicFunction.GetDiscount(Convert.ToString(OData["Currency"]), BasePrice, Convert.ToInt16(OData["Qty"]), Convert.ToString(OData["PartNo"]));
                        SellingPrice = _orderService.GetSellingPrice(Convert.ToString(OData["CustId"]), Convert.ToString(OData["PartNo"]), Convert.ToString(OData["Currency"]), Convert.ToInt32(OData["Qty"]));
                        DiscountPrice = System.Math.Round(SellingPrice * (1 - Discount), 0, MidpointRounding.AwayFromZero);
                        FinalCharge = DiscountPrice;
                        if (("A,C").ToString().IndexOf(Convert.ToString(OData["PartNo"]).Substring(0, 1)) != -1)
                        {
                            double ChangeOilPrice = _publicFunction.GetChangeOilPrice(Convert.ToString(OData["PartNo"]), DiscountPrice, Convert.ToString(OData["Lubrication"]), Convert.ToString(OData["CustId"]), Convert.ToString(OData["Currency"]), Convert.ToString(OData["Spec"]));
                            if (ChangeOilPrice != 0)
                            {
                                FinalCharge = ChangeOilPrice;
                            }
                            if (!string.IsNullOrEmpty(Convert.ToString(OData["AdapterCus"])))
                            {
                                if (("G4,G5").ToString().IndexOf(Convert.ToString(OData["PartNo"]).Substring(0, 1)) == -1)
                                {
                                    if (Convert.ToString(OData["AdapterCus"]).Substring(0, 1) == "O")
                                    {
                                        FinalCharge = 0;//20190214 O品號要另外報價
                                    }
                                }
                            }
                        }
                        else
                        {
                            DiscountPrice = 0;
                        }

                        break;
                }
                return Ok(new { code = 200, Discount = Discount, FinalCharge = FinalCharge });
            }
            return BadRequest(new { code = 400, message = "Error Request" });
        }

        [HttpGet("[action]")]
        [EnableCors("CorsPolicy")]
        public ActionResult ApexDiscount(string feStr)
        {
            if (!string.IsNullOrEmpty(feStr))
            {
                dynamic OData = Utf8Json.JsonSerializer.Deserialize<dynamic>(feStr.ToString());
                double Discount = _publicFunction.GetDiscount(Convert.ToString(OData["Currency"]), Convert.ToString(OData["UnitPrice"]), Convert.ToInt16(OData["Qty"]), Convert.ToString(OData["PartNo"]));
                return Ok(new { code = 200, Discount = Discount });
            }
            return BadRequest(new { code = 400, message = "Error Request" });
        }


        [HttpGet("[action]")]
        [EnableCors("CorsPolicy")]
        public string OrderList(string feStr)
        {
            var joinList = _orderService.OrderList(feStr);
            return System.Text.Json.JsonSerializer.Serialize(joinList);
            //return JsonSerializer.Serialize(joinList);
        }

        // POST api/<OrderController>
        [HttpPost("[action]")]
        [EnableCors("CorsPolicy")]
        public ActionResult OrderList([FromBody] JsonElement feStr)
        {
            //Utf8Json.JsonSerializer.Deserialize<dynamic>(feStr.ToString())["SaleInfo"].ToString() == "[]"
            if (!string.IsNullOrEmpty(feStr.ToString()))
            {
                Order Value = Utf8Json.JsonSerializer.Deserialize<Order>(feStr.ToString());
                if (Value.CustId == "BAI060")
                {
                    return BadRequest(new { code = 400, message = "Add Error" });
                }
                string TempOD = _orderService.GetOrderID(Value);
                _orderService.InsertOrder(Value, TempOD);

                return Ok(new { code = 200, message = "Save Success" });
            }
            return BadRequest(new { code = 400, message = "Error Request" });
        }

        // PUT api/<OrderController>/5
        [HttpPut("OrderList")]
        [EnableCors("CorsPolicy")]
        public ActionResult OrderListPut([FromBody] JsonElement feStr)
        {
            if (!string.IsNullOrEmpty(feStr.ToString()))
            {
                dynamic OData = Utf8Json.JsonSerializer.Deserialize<dynamic>(feStr.ToString())["Order"];
                dynamic OdData = Utf8Json.JsonSerializer.Deserialize<dynamic>(feStr.ToString())["OrderDetail"];
                _orderService.UpdateOrderList(Utf8Json.JsonSerializer.Deserialize<dynamic>(feStr.ToString())["Order"], Utf8Json.JsonSerializer.Deserialize<dynamic>(feStr.ToString())["OrderDetail"]);
                return Ok(new { code = 200, message = "Save Success" });
            }
            return BadRequest(new { code = 400, message = "Error Request" });
        }


        // DELETE api/<OrderController>/5
        [HttpDelete("OrderList")]
        public ActionResult OrderListDelete([FromBody] JsonElement feStr)
        {

            if (!string.IsNullOrEmpty(feStr.ToString()))
            {
                dynamic OdData = Utf8Json.JsonSerializer.Deserialize<dynamic>(feStr.ToString())["OrderDetail"];
                _orderService.DeleteOrderList(Utf8Json.JsonSerializer.Deserialize<dynamic>(feStr.ToString())["OrderDetail"]);
                return Ok(new { code = 200, message = "Delete Success" });
            }
            return BadRequest(new { code = 400, message = "Error Request" });
        }

        // POST api/<OrderController>
        [HttpPost("Login")]
        [EnableCors("CorsPolicy")]
        public ActionResult Post([FromBody] JsonElement feStr)
        {
            if (!string.IsNullOrEmpty(feStr.ToString()))
            {
                List<Cust> CustInfo = _orderService.CheckCustsMember(feStr.ToString());
                List<Sale> SaleInfo = _orderService.CheckSalesMember(feStr.ToString());
                if (CustInfo.Count == 0) CustInfo.Add(new Cust { CustId = "" });
                if (SaleInfo.Count == 0) SaleInfo.Add(new Sale { SalesId = "" });

                if ((CustInfo.Count > 0) || (SaleInfo.Count > 0))
                {
                    return Ok(new { code = 200, message = "Login successful", CustInfo = CustInfo, SaleInfo = SaleInfo });
                }
            }
            return BadRequest(new { code = 400, message = "Unable to login" });
        }

        public class SaveReportDetailInput
        {
            public string Username { set; get; }
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return Convert.ToString(id);
        }

        [HttpGet("[action]")]
        [EnableCors("CorsPolicy")]
        public void Cd()
        {
            Dictionary<string, Type> tableTypeDict = new Dictionary<string, Type>()
{
                 { "TcMmgFiles", typeof( TcMmgFile)},
                 { "TcOekFiles", typeof( TcOekFile)}
                };
            //IEnumerable<dynamic> cz;

            List<PublicFunctions.TcMmiFileInfo> TcMmiFileInfo = new List<PublicFunctions.TcMmiFileInfo>() { new PublicFunctions.TcMmiFileInfo { } };
            //var cz = _DataContext.TcOekFiles.AsQueryable().Where(m => m.TcOek03 == null);
            var cz = _DataContext.AdapClass1s.AsQueryable().Where(m => m.TcMma29 == null);
            int i = 1;
            string l_tc_mml01 = DateTime.Now.ToString("yyMMdd");

            // l_sql = "select tc_mml01 from tc_mml_file Where rownum = 1 and tc_mml01 like '" + l_tc_mml01 + "%' order by tc_mml01 desc";


            //var tmp = _DataContext.TcMmlFiles.AsEnumerable()
            //                                 .Select(Tm => new { RowNum = i++, Tm.TcMml01 })
            //                                 .Where(Tm => Tm.RowNum == 1 && EF.Functions.Like(Tm.TcMml01, l_tc_mml01 + "%"))
            //                                 .OrderByDescending(Tm => Tm.TcMml01)
            //                                 ;

            List<string> aa = new();
            aa.Add("bb");
            aa.Add("cc");
            List<TcMmbFile> TM = new List<TcMmbFile> { new TcMmbFile { } };
            TM.FirstOrDefault().TcMmb01 = "bb,cc";
            var ac = TM.FirstOrDefault().TcMmb01.Split(",");
            var tmp = _DataContext.TcMmlFiles.Select(Tm => new { Tm.TcMml01 })
                                            .Where(Tm => EF.Functions.Like(Tm.TcMml01, "211109" + "%"))
                                            .OrderByDescending(Tm => Tm.TcMml01)
                                            ;

            var json = @"
                 {
                   ""Max"": 9487,
 
                   ""Min"": ""One Piece Comic - 934""
 
                        }
                    ";
            using (JsonDocument document = JsonDocument.Parse(json))
            {
                var sumOfAllTemperatures = 0;

            }

            var jsonString = @"{ ""id"":""48e86841-f62c-42c9-ae20-b54ba8c35d6d"",""Max"": ""9487""}";
            var cs = "bad";
            var jsonString1 = $@"{{ ""TcMmf08"": ""{cs}"",
                                    ""Max"": ""9487""}}";
            var book = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString1);

            var i1 = 100;
            var i2 = 99;
            string s1 = @$"{i1}\{i2}";

            var ck = _hostEnvironment.ContentRootPath;
            var aka = _hostEnvironment.WebRootPath;


        }



    }

}
