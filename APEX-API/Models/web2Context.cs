using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace APEX_API.Models
{
    public partial class web2Context : DbContext
    {
        public web2Context()
        {
        }

        public web2Context(DbContextOptions<web2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<AcCode> AcCodes { get; set; }
        public virtual DbSet<AcCodeBig> AcCodeBigs { get; set; }
        public virtual DbSet<AgentsMember> AgentsMembers { get; set; }
        public virtual DbSet<ApexCodeTable> ApexCodeTables { get; set; }
        public virtual DbSet<ApexProductList> ApexProductLists { get; set; }
        public virtual DbSet<ApexProductListDetail> ApexProductListDetails { get; set; }
        public virtual DbSet<ApexSlider> ApexSliders { get; set; }
        public virtual DbSet<ApexVideoList> ApexVideoLists { get; set; }
        public virtual DbSet<ComplaintsApexF> ComplaintsApexFs { get; set; }
        public virtual DbSet<ComplaintsApexFR> ComplaintsApexFRs { get; set; }
        public virtual DbSet<ComplaintsApexH> ComplaintsApexHs { get; set; }
        public virtual DbSet<ComplaintsApexHAcCode> ComplaintsApexHAcCodes { get; set; }
        public virtual DbSet<ComplaintsApexHCommunication> ComplaintsApexHCommunications { get; set; }
        public virtual DbSet<ComplaintsApexHInternal> ComplaintsApexHInternals { get; set; }
        public virtual DbSet<ComplaintsApexHReminderLog> ComplaintsApexHReminderLogs { get; set; }
        public virtual DbSet<ComplaintsApexHSn> ComplaintsApexHSns { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Cust> Custs { get; set; }
        public virtual DbSet<ExiExhibition> ExiExhibitions { get; set; }
        public virtual DbSet<ExiNews> ExiNews { get; set; }
        public virtual DbSet<ImportImage> ImportImages { get; set; }
        public virtual DbSet<InputMotor> InputMotors { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderBom> OrderBoms { get; set; }
        public virtual DbSet<OrderBom2> OrderBom2s { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderDetail2> OrderDetail2s { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Paymentdetail> Paymentdetails { get; set; }
        public virtual DbSet<RpQa> RpQas { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<SupCad> SupCads { get; set; }
        public virtual DbSet<WebOrderCusUpload> WebOrderCusUploads { get; set; }
        public virtual DbSet<WebOrderCusUploadDetail> WebOrderCusUploadDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=web2;Trusted_Connection=True;User ID=asfako;Password=12345");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_BIN");

            modelBuilder.Entity<AcCode>(entity =>
            {
                entity.HasKey(e => new { e.Product, e.Code });

                entity.ToTable("AC_code");

                entity.Property(e => e.Product)
                    .HasMaxLength(1)
                    .HasColumnName("product");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .HasColumnName("code");

                entity.Property(e => e.CodeDescC)
                    .HasMaxLength(500)
                    .HasColumnName("code_desc_c");

                entity.Property(e => e.CodeDescE)
                    .HasMaxLength(500)
                    .HasColumnName("code_desc_e");
            });

            modelBuilder.Entity<AcCodeBig>(entity =>
            {
                entity.HasKey(e => new { e.Product, e.Code });

                entity.ToTable("AC_code_big");

                entity.Property(e => e.Product)
                    .HasMaxLength(1)
                    .HasColumnName("product");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .HasColumnName("code");

                entity.Property(e => e.CodeDescC)
                    .HasMaxLength(500)
                    .HasColumnName("code_desc_c");

                entity.Property(e => e.CodeDescE)
                    .HasMaxLength(500)
                    .HasColumnName("code_desc_e");
            });

            modelBuilder.Entity<AgentsMember>(entity =>
            {
                entity.HasKey(e => e.MemberId)
                    .HasName("PK_Agnets_Memeber");

                entity.ToTable("Agents_Member");

                entity.Property(e => e.MemberId)
                    .HasMaxLength(50)
                    .HasColumnName("Member_ID");

                entity.Property(e => e.AgentId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Agent_ID");

                entity.Property(e => e.MemberAddress)
                    .HasMaxLength(250)
                    .HasColumnName("Member_Address");

                entity.Property(e => e.MemberLevel)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Member_Level");

                entity.Property(e => e.MemberMail)
                    .HasMaxLength(50)
                    .HasColumnName("Member_Mail");

                entity.Property(e => e.MemberName)
                    .HasMaxLength(50)
                    .HasColumnName("Member_Name");

                entity.Property(e => e.MemberPwd)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Member_PWD");

                entity.Property(e => e.MemberStatus)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Member_Status");

                entity.Property(e => e.MemberTel)
                    .HasMaxLength(50)
                    .HasColumnName("Member_TEL");
            });

            modelBuilder.Entity<ApexCodeTable>(entity =>
            {
                entity.HasKey(e => e.ApexCodeId)
                    .HasName("PK__APEX_Code_Table__0D7A0286");

                entity.ToTable("APEX_Code_Table");

                entity.Property(e => e.ApexCodeId)
                    .HasColumnName("apex_code_id")
                    .HasComment("流水號");

                entity.Property(e => e.ApexCodeKindId)
                    .HasMaxLength(200)
                    .HasColumnName("apex_code_kind_id")
                    .HasComment("種類編號");

                entity.Property(e => e.ApexCodeName)
                    .HasMaxLength(200)
                    .HasColumnName("apex_code_name")
                    .HasComment("名稱");

                entity.Property(e => e.ApexCodeSn)
                    .HasMaxLength(200)
                    .HasColumnName("apex_code_sn")
                    .HasComment("編號");

                entity.Property(e => e.ApexCodeUnitId)
                    .HasMaxLength(200)
                    .HasColumnName("apex_code_unit_id")
                    .HasComment("系統單元編號");
            });

            modelBuilder.Entity<ApexProductList>(entity =>
            {
                entity.ToTable("APEX_ProductList");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasComment("流水號");

                entity.Property(e => e.ImageName)
                    .HasMaxLength(200)
                    .HasComment("圖片名稱");

                entity.Property(e => e.Language)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength(true)
                    .HasComment("語系");

                entity.Property(e => e.Overview)
                    .HasColumnType("ntext")
                    .HasComment("產品概要");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasComment("產品名稱");

                entity.Property(e => e.ProductNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasComment("產品編號");

                entity.Property(e => e.Size)
                    .HasColumnType("ntext")
                    .HasComment("產品型號");
            });

            modelBuilder.Entity<ApexProductListDetail>(entity =>
            {
                entity.ToTable("APEX_ProductList_Detail");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasComment("流水號");

                entity.Property(e => e.EndPage)
                    .HasMaxLength(3)
                    .HasComment("結束頁面");

                entity.Property(e => e.Item)
                    .HasMaxLength(20)
                    .HasComment("產品解說項目");

                entity.Property(e => e.Language)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength(true)
                    .HasComment("語系");

                entity.Property(e => e.PdfName)
                    .HasMaxLength(100)
                    .HasComment("型錄檔名");

                entity.Property(e => e.ProductKind)
                    .HasMaxLength(20)
                    .HasComment("產品種類");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasComment("產品名稱");

                entity.Property(e => e.ProductNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasComment("產品編號");

                entity.Property(e => e.StartPage)
                    .HasMaxLength(3)
                    .HasComment("開始頁面");
            });

            modelBuilder.Entity<ApexSlider>(entity =>
            {
                entity.ToTable("APEX_Slider");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasComment("流水號");

                entity.Property(e => e.BannerId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Banner_ID")
                    .HasComment("項目編碼");

                entity.Property(e => e.ContentDetail)
                    .HasMaxLength(500)
                    .HasColumnName("Content_Detail")
                    .HasComment("細節");

                entity.Property(e => e.ContentMain)
                    .HasMaxLength(500)
                    .HasColumnName("Content_Main")
                    .HasComment("內容");

                entity.Property(e => e.ContentMore)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("Content_More")
                    .HasDefaultValueSql("('#')")
                    .HasComment("連結");

                entity.Property(e => e.ContentTitle)
                    .HasMaxLength(500)
                    .HasColumnName("Content_Title")
                    .HasComment("標題");

                entity.Property(e => e.ImageName)
                    .HasMaxLength(200)
                    .HasColumnName("Image_Name")
                    .HasComment("圖片名稱");

                entity.Property(e => e.SliderLanguage)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("Slider_Language")
                    .IsFixedLength(true)
                    .HasComment("語言代碼");

                entity.Property(e => e.SliderOrder)
                    .HasColumnName("Slider_Order")
                    .HasComment("排序");

                entity.Property(e => e.WebSitePage)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("使用頁面");
            });

            modelBuilder.Entity<ApexVideoList>(entity =>
            {
                entity.ToTable("APEX_VideoList");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasComment("流水號");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(200)
                    .HasComment("影片縮圖");

                entity.Property(e => e.ProjectName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("專案名稱");

                entity.Property(e => e.VideoLength)
                    .HasMaxLength(20)
                    .HasComment("片長");

                entity.Property(e => e.VideoUrl)
                    .HasMaxLength(200)
                    .HasComment("影片連結");

                entity.Property(e => e.ViedoHead)
                    .HasMaxLength(2000)
                    .HasComment("影片標題內容");
            });

            modelBuilder.Entity<ComplaintsApexF>(entity =>
            {
                entity.ToTable("complaints_apex_f");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ComplaintsNum)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasColumnName("complaints_num");

                entity.Property(e => e.Filename)
                    .HasMaxLength(200)
                    .HasColumnName("filename");

                entity.Property(e => e.Item).HasColumnName("item");
            });

            modelBuilder.Entity<ComplaintsApexFR>(entity =>
            {
                entity.HasKey(e => new { e.ComplaintsNum, e.Item });

                entity.ToTable("complaints_apex_f_r");

                entity.Property(e => e.ComplaintsNum)
                    .HasMaxLength(16)
                    .HasColumnName("complaints_num");

                entity.Property(e => e.Item).HasColumnName("item");

                entity.Property(e => e.Filename)
                    .HasMaxLength(200)
                    .HasColumnName("filename");

                entity.Property(e => e.InternalFlag)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("Internal_Flag")
                    .HasDefaultValueSql("(N'N')")
                    .HasComment("上傳種類");

                entity.Property(e => e.SalesId)
                    .HasMaxLength(50)
                    .HasColumnName("SalesID");
            });

            modelBuilder.Entity<ComplaintsApexH>(entity =>
            {
                entity.HasKey(e => e.ComplaintsNum);

                entity.ToTable("complaints_apex_h");

                entity.Property(e => e.ComplaintsNum)
                    .HasMaxLength(16)
                    .HasColumnName("complaints_num");

                entity.Property(e => e.AcCode)
                    .HasMaxLength(10)
                    .HasColumnName("ac_code");

                entity.Property(e => e.AcCodeBig)
                    .HasMaxLength(10)
                    .HasColumnName("ac_code_big");

                entity.Property(e => e.ApexCodeSn)
                    .HasMaxLength(50)
                    .HasColumnName("apex_code_sn")
                    .HasComment("WebClaim_Conclusion_001對應代碼");

                entity.Property(e => e.AttachmentPartsBushing)
                    .HasMaxLength(1)
                    .HasColumnName("Attachment_Parts_Bushing");

                entity.Property(e => e.AttachmentPartsKey)
                    .HasMaxLength(1)
                    .HasColumnName("Attachment_Parts_Key");

                entity.Property(e => e.AttachmentPartsManual)
                    .HasMaxLength(1)
                    .HasColumnName("Attachment_Parts_Manual");

                entity.Property(e => e.AttachmentPartsPlug)
                    .HasMaxLength(1)
                    .HasColumnName("Attachment_Parts_Plug");

                entity.Property(e => e.AttachmentPartsScrews)
                    .HasMaxLength(1)
                    .HasColumnName("Attachment_Parts_Screws");

                entity.Property(e => e.AttachmentPartsSetCollar)
                    .HasMaxLength(1)
                    .HasColumnName("Attachment_Parts_Set_Collar");

                entity.Property(e => e.CloseDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Close_date");

                entity.Property(e => e.ComfirmDate)
                    .HasColumnType("datetime")
                    .HasColumnName("comfirm_date");

                entity.Property(e => e.Conclusion).HasMaxLength(500);

                entity.Property(e => e.ConclusionConfirm)
                    .HasMaxLength(500)
                    .HasColumnName("Conclusion_Confirm")
                    .HasComment("追蹤矯正及預防措施執行結案確認");

                entity.Property(e => e.Correction)
                    .HasMaxLength(500)
                    .HasComment("臨時矯正措施");

                entity.Property(e => e.CorrectionPreventive)
                    .HasMaxLength(500)
                    .HasColumnName("Correction_Preventive")
                    .HasComment("矯正及預防措施");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("create_date");

                entity.Property(e => e.Defect)
                    .HasMaxLength(500)
                    .HasComment("不良原因");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .HasColumnName("description");

                entity.Property(e => e.DriveType)
                    .HasMaxLength(100)
                    .HasColumnName("Drive_Type");

                entity.Property(e => e.EfgpprocessNo)
                    .HasMaxLength(50)
                    .HasColumnName("EFGPProcessNo")
                    .HasComment("EasyFlow流程序號");

                entity.Property(e => e.F2aB).HasMaxLength(100);

                entity.Property(e => e.F2rB).HasMaxLength(100);

                entity.Property(e => e.Machinery)
                    .HasMaxLength(100)
                    .HasColumnName("machinery");

                entity.Property(e => e.ModeNo)
                    .HasMaxLength(200)
                    .HasColumnName("mode_no");

                entity.Property(e => e.NewsCustomerFlg)
                    .HasColumnName("News_Customer_flg")
                    .HasComment("Customer新訊息註記");

                entity.Property(e => e.NewsFlg)
                    .HasMaxLength(1)
                    .HasColumnName("News_flg");

                entity.Property(e => e.OperationalModeS1)
                    .HasMaxLength(1)
                    .HasColumnName("Operational_Mode_S1");

                entity.Property(e => e.OperationalModeS5)
                    .HasMaxLength(1)
                    .HasColumnName("Operational_Mode_S5");

                entity.Property(e => e.ProblemAnalysis)
                    .HasMaxLength(500)
                    .HasColumnName("Problem_Analysis")
                    .HasComment("問題原因與影響層面分析");

                entity.Property(e => e.Ptype)
                    .HasMaxLength(1)
                    .HasColumnName("ptype");

                entity.Property(e => e.Reason2)
                    .HasMaxLength(1000)
                    .HasComment("GearBox以外的Reason");

                entity.Property(e => e.ReciveDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Recive_date")
                    .HasComment("收到日期");

                entity.Property(e => e.Reply).HasMaxLength(1000);

                entity.Property(e => e.Responsibility)
                    .HasMaxLength(500)
                    .HasComment("責任歸屬");

                entity.Property(e => e.ReturnDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Return_date");

                entity.Property(e => e.ReturnFlg)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("Return_flg")
                    .HasDefaultValueSql("(N'N')");

                entity.Property(e => e.SalesProgress)
                    .HasMaxLength(500)
                    .HasColumnName("Sales_Progress")
                    .HasComment("營業部處理進度");

                entity.Property(e => e.ShippingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Shipping_Date");

                entity.Property(e => e.SnNum)
                    .HasMaxLength(100)
                    .HasColumnName("sn_num");

                entity.Property(e => e.State)
                    .HasMaxLength(1)
                    .HasColumnName("state");

                entity.Property(e => e.T2max).HasMaxLength(100);

                entity.Property(e => e.T2n)
                    .HasMaxLength(100)
                    .HasColumnName("T2N");

                entity.Property(e => e.Users)
                    .HasMaxLength(50)
                    .HasColumnName("users");
            });

            modelBuilder.Entity<ComplaintsApexHAcCode>(entity =>
            {
                entity.ToTable("complaints_apex_h_AC_code");

                entity.Property(e => e.ComplaintsApexHAcCodeId).HasColumnName("complaints_apex_h_AC_code_id");

                entity.Property(e => e.AcCode)
                    .HasMaxLength(10)
                    .HasColumnName("ac_code");

                entity.Property(e => e.AcCodeBig)
                    .HasMaxLength(10)
                    .HasColumnName("ac_code_big");

                entity.Property(e => e.CodeDescE).HasColumnName("code_desc_e");

                entity.Property(e => e.ComplaintsNum)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasColumnName("complaints_num");
            });

            modelBuilder.Entity<ComplaintsApexHCommunication>(entity =>
            {
                entity.HasKey(e => e.CommunicationId)
                    .HasName("PK__complaints_apex___7F2BE32F");

                entity.ToTable("complaints_apex_h_Communication");

                entity.Property(e => e.CommunicationId)
                    .HasColumnName("Communication_id")
                    .HasComment("流水號");

                entity.Property(e => e.ComplaintsNum)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasColumnName("complaints_num")
                    .HasComment("客訴單號");

                entity.Property(e => e.InternalFlag)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("Internal_Flag")
                    .HasComment("是否為內部");

                entity.Property(e => e.InternalQaItem)
                    .HasMaxLength(50)
                    .HasColumnName("Internal_QA_Item")
                    .HasComment("內部回覆項目");

                entity.Property(e => e.Message)
                    .HasMaxLength(2000)
                    .HasComment("回覆內容");

                entity.Property(e => e.MotionIp)
                    .HasMaxLength(50)
                    .HasColumnName("MotionIP")
                    .HasComment("使用者IP");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("發言人姓名");

                entity.Property(e => e.ReciveDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Recive_date")
                    .HasComment("收到日期");

                entity.Property(e => e.Replydate)
                    .HasColumnType("datetime")
                    .HasColumnName("replydate")
                    .HasComment("回覆時間");

                entity.Property(e => e.UploadItem)
                    .HasColumnName("Upload_item")
                    .HasComment("檔案項目");

                entity.Property(e => e.Users)
                    .HasMaxLength(50)
                    .HasColumnName("users")
                    .HasComment("使用者ID");
            });

            modelBuilder.Entity<ComplaintsApexHInternal>(entity =>
            {
                entity.ToTable("complaints_apex_h_internal");

                entity.Property(e => e.ComplaintsApexHInternalId)
                    .HasColumnName("complaints_apex_h_internal_id")
                    .HasComment("流水號");

                entity.Property(e => e.ComplaintsNum)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasColumnName("complaints_num")
                    .HasComment("客訴單號");

                entity.Property(e => e.EmployeeCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("employee_code")
                    .HasComment("員工編號");

                entity.Property(e => e.InternalReply)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .HasColumnName("internal_reply")
                    .HasComment("內部回覆內容");

                entity.Property(e => e.Replydate)
                    .HasColumnType("datetime")
                    .HasColumnName("replydate")
                    .HasComment("回覆時間");
            });

            modelBuilder.Entity<ComplaintsApexHReminderLog>(entity =>
            {
                entity.HasKey(e => e.ReminderLogId)
                    .HasName("PK__complaints_apex___5BE2A6F2");

                entity.ToTable("complaints_apex_h_ReminderLog");

                entity.Property(e => e.ReminderLogId)
                    .HasColumnName("ReminderLog_id")
                    .HasComment("流水號");

                entity.Property(e => e.ComplaintsNum)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasColumnName("complaints_num")
                    .HasComment("客訴單號");

                entity.Property(e => e.EmployeeCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("employee_code")
                    .HasComment("發信員工編號");

                entity.Property(e => e.Localtime)
                    .HasColumnType("datetime")
                    .HasComment("寄件時間");

                entity.Property(e => e.Recipient)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasComment("收件者");
            });

            modelBuilder.Entity<ComplaintsApexHSn>(entity =>
            {
                entity.ToTable("complaints_apex_h_sn");

                entity.Property(e => e.ComplaintsApexHSnId).HasColumnName("complaints_apex_h_sn_id");

                entity.Property(e => e.BackFlag)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("Back_flag")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ComplaintsNum)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasColumnName("complaints_num");

                entity.Property(e => e.ModeNo)
                    .HasMaxLength(200)
                    .HasColumnName("mode_no");

                entity.Property(e => e.ShippingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Shipping_Date");

                entity.Property(e => e.SnNum)
                    .HasMaxLength(100)
                    .HasColumnName("sn_num");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Country");

                entity.Property(e => e.Country1)
                    .HasMaxLength(50)
                    .HasColumnName("Country");

                entity.Property(e => e.CountryCode).HasMaxLength(10);

                entity.Property(e => e.Currency).HasMaxLength(4);
            });

            modelBuilder.Entity<Cust>(entity =>
            {
                entity.ToTable("Cust");

                entity.Property(e => e.CustId)
                    .HasMaxLength(10)
                    .HasColumnName("CustID");

                entity.Property(e => e.Addr).HasMaxLength(255);

                entity.Property(e => e.Addr2).HasMaxLength(255);

                entity.Property(e => e.Addr3).HasMaxLength(255);

                entity.Property(e => e.Ceo)
                    .HasMaxLength(50)
                    .HasColumnName("CEO");

                entity.Property(e => e.ClaimLevel)
                    .HasColumnName("Claim_Level")
                    .HasDefaultValueSql("((4))")
                    .HasComment("客訴系統權限等級");

                entity.Property(e => e.CountryCode).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Deliver).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.EmployId)
                    .HasMaxLength(5)
                    .HasColumnName("EmployID");

                entity.Property(e => e.Fax).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(150);

                entity.Property(e => e.Group).HasMaxLength(20);

                entity.Property(e => e.Invoice).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PayBank).HasMaxLength(50);

                entity.Property(e => e.PayNo)
                    .HasMaxLength(50)
                    .HasColumnName("PayNO");

                entity.Property(e => e.Paymant).HasMaxLength(50);

                entity.Property(e => e.Pwd)
                    .HasMaxLength(50)
                    .HasColumnName("PWD");

                entity.Property(e => e.Tel1).HasMaxLength(50);

                entity.Property(e => e.Tel2).HasMaxLength(50);

                entity.Property(e => e.Tel3).HasMaxLength(50);

                entity.Property(e => e.WebSite).HasMaxLength(50);
            });

            modelBuilder.Entity<ExiExhibition>(entity =>
            {
                entity.HasKey(e => e.ExiId);

                entity.ToTable("Exi_EXHIBITIONS");

                entity.Property(e => e.ExiId).HasColumnName("Exi_ID");

                entity.Property(e => e.BeginDate).HasColumnType("datetime");

                entity.Property(e => e.ExiShowBooth)
                    .HasMaxLength(200)
                    .HasColumnName("Exi_Show_Booth");

                entity.Property(e => e.ExiShowDate)
                    .HasMaxLength(100)
                    .HasColumnName("Exi_Show_Date");

                entity.Property(e => e.ExiShowLocation)
                    .HasMaxLength(200)
                    .HasColumnName("Exi_Show_Location");

                entity.Property(e => e.ExiShowTitle)
                    .HasMaxLength(200)
                    .HasColumnName("Exi_Show_Title");

                entity.Property(e => e.ExiTitle)
                    .HasMaxLength(50)
                    .HasColumnName("Exi_Title");

                entity.Property(e => e.RpLanguage)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("RP_Language")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<ExiNews>(entity =>
            {
                entity.ToTable("Exi_NEWS");

                entity.Property(e => e.ExiNewsId)
                    .HasColumnName("Exi_NEWS_ID")
                    .HasComment("流水號");

                entity.Property(e => e.ExiNewsContent)
                    .HasMaxLength(2000)
                    .HasColumnName("Exi_NEWS_Content")
                    .HasComment("內容");

                entity.Property(e => e.ExiNewsLanguage)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("Exi_NEWS_Language")
                    .IsFixedLength(true)
                    .HasComment("語系");

                entity.Property(e => e.ExiNewsLink)
                    .HasMaxLength(50)
                    .HasColumnName("Exi_NEWS_Link")
                    .HasComment("連結內容");

                entity.Property(e => e.ExiNewsLinkUrl)
                    .HasMaxLength(200)
                    .HasColumnName("Exi_NEWS_Link_URL")
                    .HasComment("連結位置");

                entity.Property(e => e.ExiNewsOrder)
                    .HasColumnName("Exi_NEWS_Order")
                    .HasComment("排序");

                entity.Property(e => e.ExiNewsPic2Url)
                    .HasMaxLength(200)
                    .HasColumnName("Exi_NEWS_PIC2_URL")
                    .HasComment("次要圖片位置");

                entity.Property(e => e.ExiNewsPicContent)
                    .HasMaxLength(100)
                    .HasColumnName("Exi_NEWS_PIC_Content");

                entity.Property(e => e.ExiNewsPicUrl)
                    .HasMaxLength(200)
                    .HasColumnName("Exi_NEWS_PIC_URL")
                    .HasComment("圖片位置");

                entity.Property(e => e.ExiNewsTitle)
                    .HasMaxLength(50)
                    .HasColumnName("Exi_NEWS_Title")
                    .HasComment("標題");
            });

            modelBuilder.Entity<ImportImage>(entity =>
            {
                entity.ToTable("ImportImage");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FolderName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("folderName");

                entity.Property(e => e.Photo).IsRequired();

                entity.Property(e => e.ProjectName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InputMotor>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Input_Motor");

                entity.Property(e => e.CustId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("CustID");

                entity.Property(e => e.La)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("LA");

                entity.Property(e => e.Lb)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("LB");

                entity.Property(e => e.Lc)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("LC");

                entity.Property(e => e.Le)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("LE");

                entity.Property(e => e.Lg)
                    .HasMaxLength(50)
                    .HasColumnName("LG");

                entity.Property(e => e.Lr)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("LR");

                entity.Property(e => e.Lt)
                    .HasMaxLength(50)
                    .HasColumnName("LT");

                entity.Property(e => e.Lz)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("LZ");

                entity.Property(e => e.MotorId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("MotorID");

                entity.Property(e => e.OrderDetailId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("OrderDetailID");

                entity.Property(e => e.OrderId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("OrderID");

                entity.Property(e => e.PartNo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.S)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(16)
                    .HasColumnName("OrderID");

                entity.Property(e => e.AccountNo)
                    .HasMaxLength(50)
                    .HasColumnName("AccountNO");

                entity.Property(e => e.Attn).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Currency).HasMaxLength(50);

                entity.Property(e => e.CustId)
                    .HasMaxLength(50)
                    .HasColumnName("CustID");

                entity.Property(e => e.CustPo)
                    .HasMaxLength(20)
                    .HasColumnName("CustPO");

                entity.Property(e => e.DelivAddr).HasMaxLength(150);

                entity.Property(e => e.DelivAddr2).HasMaxLength(100);

                entity.Property(e => e.DelivDate).HasColumnType("datetime");

                entity.Property(e => e.DelivTel).HasMaxLength(50);

                entity.Property(e => e.DelivWay).HasMaxLength(50);

                entity.Property(e => e.Deliver).HasMaxLength(50);

                entity.Property(e => e.EmployId)
                    .HasMaxLength(50)
                    .HasColumnName("EmployID");

                entity.Property(e => e.InvAddr).HasMaxLength(150);

                entity.Property(e => e.Mailcode)
                    .HasMaxLength(1)
                    .HasColumnName("mailcode");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.OrderNo).HasMaxLength(50);

                entity.Property(e => e.Ostatus).HasMaxLength(10);

                entity.Property(e => e.PayTerm).HasMaxLength(100);

                entity.Property(e => e.Pono)
                    .HasMaxLength(50)
                    .HasColumnName("PONO");

                entity.Property(e => e.QuotId)
                    .HasMaxLength(10)
                    .HasColumnName("QuotID");

                entity.Property(e => e.Rate).HasMaxLength(5);

                entity.Property(e => e.Remail)
                    .HasMaxLength(100)
                    .HasColumnName("remail");

                entity.Property(e => e.SalesDate).HasColumnType("datetime");

                entity.Property(e => e.TaxType).HasMaxLength(50);
            });

            modelBuilder.Entity<OrderBom>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("OrderBom");

                entity.Property(e => e.Detail).HasMaxLength(50);

                entity.Property(e => e.MotoName).HasMaxLength(50);

                entity.Property(e => e.OrderId)
                    .HasMaxLength(16)
                    .HasColumnName("OrderID");

                entity.Property(e => e.PartNo).HasMaxLength(50);

                entity.Property(e => e.Qty).HasMaxLength(50);

                entity.Property(e => e.Spec).HasMaxLength(50);
            });

            modelBuilder.Entity<OrderBom2>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("OrderBom2");

                entity.Property(e => e.Detail).HasMaxLength(50);

                entity.Property(e => e.MotoName).HasMaxLength(50);

                entity.Property(e => e.OrderId)
                    .HasMaxLength(16)
                    .HasColumnName("OrderID");

                entity.Property(e => e.PartNo).HasMaxLength(50);

                entity.Property(e => e.Qty).HasMaxLength(50);

                entity.Property(e => e.Spec).HasMaxLength(50);
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");

                entity.Property(e => e.OrderDetailId)
                    .ValueGeneratedNever()
                    .HasColumnName("OrderDetailID");

                entity.Property(e => e.AdapterCus)
                    .HasMaxLength(15)
                    .HasColumnName("adapter_cus")
                    .IsFixedLength(true);

                entity.Property(e => e.Bas0011).HasColumnName("BAS001_1");

                entity.Property(e => e.Bas0012)
                    .HasMaxLength(1)
                    .HasColumnName("BAS001_2");

                entity.Property(e => e.CkpartNo)
                    .HasMaxLength(50)
                    .HasColumnName("CKPartNo");

                entity.Property(e => e.Ckqty).HasColumnName("CKQty");

                entity.Property(e => e.Creater).HasMaxLength(12);

                entity.Property(e => e.Customize).HasMaxLength(500);

                entity.Property(e => e.DeliveDate).HasColumnType("datetime");

                entity.Property(e => e.Discount)
                    .HasMaxLength(50)
                    .HasColumnName("discount");

                entity.Property(e => e.InertiaApp)
                    .HasMaxLength(20)
                    .HasColumnName("inertia_app");

                entity.Property(e => e.InertiaAppWarranty)
                    .HasMaxLength(10)
                    .HasColumnName("inertia_app_Warranty");

                entity.Property(e => e.IsWarranty)
                    .HasMaxLength(50)
                    .HasColumnName("is_Warranty");

                entity.Property(e => e.IsWarrantyO)
                    .HasMaxLength(50)
                    .HasColumnName("is_Warranty_o");

                entity.Property(e => e.Lubrication).HasMaxLength(50);

                entity.Property(e => e.LubricationT1)
                    .HasMaxLength(50)
                    .HasColumnName("Lubrication_t1");

                entity.Property(e => e.M1).HasMaxLength(1);

                entity.Property(e => e.Memo).HasMaxLength(255);

                entity.Property(e => e.MemoM1)
                    .HasMaxLength(100)
                    .HasColumnName("Memo_M1");

                entity.Property(e => e.MotoId).HasColumnName("MotoID");

                entity.Property(e => e.MotoName).HasMaxLength(50);

                entity.Property(e => e.Mtmaker).HasMaxLength(50);

                entity.Property(e => e.Od001)
                    .HasMaxLength(100)
                    .HasColumnName("OD001");

                entity.Property(e => e.Od002)
                    .HasMaxLength(100)
                    .HasColumnName("OD002");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(16)
                    .HasColumnName("OrderID");

                entity.Property(e => e.OrderType).HasMaxLength(50);

                entity.Property(e => e.P2).HasMaxLength(1);

                entity.Property(e => e.PartNo).HasMaxLength(50);

                entity.Property(e => e.Spec).HasMaxLength(50);

                entity.Property(e => e.TaOeb005)
                    .HasMaxLength(50)
                    .HasColumnName("ta_oeb005");

                entity.Property(e => e.Unit).HasMaxLength(10);

                entity.Property(e => e.UseToDeltarobot)
                    .HasMaxLength(50)
                    .HasColumnName("use_to_deltarobot");
            });

            modelBuilder.Entity<OrderDetail2>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("OrderDetail2");

                entity.Property(e => e.Bas0011).HasColumnName("BAS001_1");

                entity.Property(e => e.Bas0012)
                    .HasMaxLength(1)
                    .HasColumnName("BAS001_2");

                entity.Property(e => e.CkpartNo)
                    .HasMaxLength(50)
                    .HasColumnName("CKPartNo");

                entity.Property(e => e.Ckqty).HasColumnName("CKQty");

                entity.Property(e => e.Creater).HasMaxLength(12);

                entity.Property(e => e.Discount)
                    .HasMaxLength(50)
                    .HasColumnName("discount");

                entity.Property(e => e.IsWarranty)
                    .HasMaxLength(50)
                    .HasColumnName("is_Warranty");

                entity.Property(e => e.IsWarrantyO)
                    .HasMaxLength(50)
                    .HasColumnName("is_Warranty_o");

                entity.Property(e => e.Lubrication).HasMaxLength(50);

                entity.Property(e => e.LubricationT1)
                    .HasMaxLength(50)
                    .HasColumnName("Lubrication_t1");

                entity.Property(e => e.M1).HasMaxLength(1);

                entity.Property(e => e.Memo).HasMaxLength(255);

                entity.Property(e => e.MemoM1)
                    .HasMaxLength(100)
                    .HasColumnName("Memo_M1");

                entity.Property(e => e.MotoId).HasColumnName("MotoID");

                entity.Property(e => e.MotoName).HasMaxLength(50);

                entity.Property(e => e.Mtmaker).HasMaxLength(50);

                entity.Property(e => e.Od001)
                    .HasMaxLength(100)
                    .HasColumnName("OD001");

                entity.Property(e => e.Od002)
                    .HasMaxLength(100)
                    .HasColumnName("OD002");

                entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(16)
                    .HasColumnName("OrderID");

                entity.Property(e => e.OrderType).HasMaxLength(50);

                entity.Property(e => e.P2).HasMaxLength(1);

                entity.Property(e => e.PartNo).HasMaxLength(50);

                entity.Property(e => e.Spec).HasMaxLength(50);

                entity.Property(e => e.TaOeb005)
                    .HasMaxLength(50)
                    .HasColumnName("ta_oeb005");

                entity.Property(e => e.Unit).HasMaxLength(10);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.識別碼);

                entity.ToTable("payment");

                entity.Property(e => e.Pay01)
                    .HasMaxLength(50)
                    .HasColumnName("pay01");

                entity.Property(e => e.Pay02)
                    .HasMaxLength(50)
                    .HasColumnName("pay02");

                entity.Property(e => e.Pay03)
                    .HasMaxLength(50)
                    .HasColumnName("pay03");

                entity.Property(e => e.Pay04)
                    .HasMaxLength(50)
                    .HasColumnName("pay04");

                entity.Property(e => e.Pay05)
                    .HasMaxLength(50)
                    .HasColumnName("pay05");

                entity.Property(e => e.Pay06)
                    .HasMaxLength(50)
                    .HasColumnName("pay06");

                entity.Property(e => e.Pay07)
                    .HasMaxLength(50)
                    .HasColumnName("pay07");

                entity.Property(e => e.Pay08)
                    .HasMaxLength(50)
                    .HasColumnName("pay08");

                entity.Property(e => e.Pay09)
                    .HasMaxLength(50)
                    .HasColumnName("pay09");

                entity.Property(e => e.Pay10)
                    .HasMaxLength(50)
                    .HasColumnName("pay10");

                entity.Property(e => e.Pay11)
                    .HasMaxLength(50)
                    .HasColumnName("pay11");

                entity.Property(e => e.Pay12)
                    .HasMaxLength(50)
                    .HasColumnName("pay12");

                entity.Property(e => e.Pay13)
                    .HasMaxLength(50)
                    .HasColumnName("pay13");

                entity.Property(e => e.Pay14)
                    .HasMaxLength(50)
                    .HasColumnName("pay14");

                entity.Property(e => e.Pay15)
                    .HasMaxLength(50)
                    .HasColumnName("pay15");

                entity.Property(e => e.Pay16)
                    .HasMaxLength(50)
                    .HasColumnName("pay16");

                entity.Property(e => e.Pay17)
                    .HasMaxLength(50)
                    .HasColumnName("pay17");

                entity.Property(e => e.Pay18)
                    .HasMaxLength(255)
                    .HasColumnName("pay18");

                entity.Property(e => e.Pay19)
                    .HasMaxLength(50)
                    .HasColumnName("pay19");

                entity.Property(e => e.Pay20)
                    .HasMaxLength(50)
                    .HasColumnName("pay20");
            });

            modelBuilder.Entity<Paymentdetail>(entity =>
            {
                entity.HasKey(e => e.識別碼);

                entity.ToTable("paymentdetail");

                entity.Property(e => e.Pd01)
                    .HasMaxLength(50)
                    .HasColumnName("pd01");

                entity.Property(e => e.Pd02)
                    .HasMaxLength(50)
                    .HasColumnName("pd02");

                entity.Property(e => e.Pd03)
                    .HasMaxLength(50)
                    .HasColumnName("pd03");

                entity.Property(e => e.Pd04)
                    .HasMaxLength(50)
                    .HasColumnName("pd04");

                entity.Property(e => e.Pd05)
                    .HasMaxLength(50)
                    .HasColumnName("pd05");

                entity.Property(e => e.Pd06)
                    .HasMaxLength(50)
                    .HasColumnName("pd06");

                entity.Property(e => e.Pd07)
                    .HasMaxLength(50)
                    .HasColumnName("pd07");

                entity.Property(e => e.Pd08)
                    .HasMaxLength(50)
                    .HasColumnName("pd08");

                entity.Property(e => e.Pd09)
                    .HasMaxLength(50)
                    .HasColumnName("pd09");

                entity.Property(e => e.Pd10)
                    .HasMaxLength(50)
                    .HasColumnName("pd10");

                entity.Property(e => e.Pd11)
                    .HasMaxLength(50)
                    .HasColumnName("pd11");

                entity.Property(e => e.Pd12).HasColumnName("pd12");

                entity.Property(e => e.Pd13).HasColumnName("pd13");

                entity.Property(e => e.Pd14).HasColumnName("pd14");

                entity.Property(e => e.Pd15)
                    .HasMaxLength(50)
                    .HasColumnName("pd15");

                entity.Property(e => e.Pd16)
                    .HasMaxLength(50)
                    .HasColumnName("pd16");

                entity.Property(e => e.Pd17)
                    .HasMaxLength(50)
                    .HasColumnName("pd17");

                entity.Property(e => e.Pd18)
                    .HasMaxLength(50)
                    .HasColumnName("pd18");

                entity.Property(e => e.Pd19)
                    .HasMaxLength(50)
                    .HasColumnName("pd19");

                entity.Property(e => e.Pd20)
                    .HasMaxLength(50)
                    .HasColumnName("pd20");
            });

            modelBuilder.Entity<RpQa>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("RP_QA");

                entity.Property(e => e.RpA)
                    .HasMaxLength(2000)
                    .HasColumnName("RP_A");

                entity.Property(e => e.RpId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RP_ID");

                entity.Property(e => e.RpLanguage)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("RP_Language")
                    .IsFixedLength(true);

                entity.Property(e => e.RpQ)
                    .HasMaxLength(500)
                    .HasColumnName("RP_Q");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.SalesId);

                entity.Property(e => e.SalesId)
                    .HasMaxLength(50)
                    .HasColumnName("SalesID");

                entity.Property(e => e.ClaimLevel)
                    .HasColumnName("Claim_Level")
                    .HasComment("客訴系統權限等級");

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.EmployeeCnName)
                    .HasMaxLength(50)
                    .HasColumnName("Employee_CnName")
                    .HasComment("員工姓名");

                entity.Property(e => e.ExceptionLoginIp)
                    .HasMaxLength(50)
                    .HasColumnName("ExceptionLoginIP")
                    .HasComment("內部人員IP登入");

                entity.Property(e => e.Pwd)
                    .HasMaxLength(50)
                    .HasColumnName("PWD");

                entity.Property(e => e.Receiver)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<SupCad>(entity =>
            {
                entity.ToTable("Sup_Cad");

                entity.Property(e => e.SupCadId).HasColumnName("Sup_Cad_ID");

                entity.Property(e => e.SupCadDlName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Sup_Cad_Dl_Name");

                entity.Property(e => e.SupCadDlType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Sup_Cad_Dl_Type");

                entity.Property(e => e.SupCadSeries)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Sup_Cad_Series");

                entity.Property(e => e.SupCadSeriesTitle)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Sup_Cad_Series_Title");
            });

            modelBuilder.Entity<WebOrderCusUpload>(entity =>
            {
                entity.ToTable("WebOrder_Cus_Upload");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasComment("流水號");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("create_date")
                    .HasComment("開單時間");

                entity.Property(e => e.CreateIp)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("CreateIP")
                    .HasComment("開單IP");

                entity.Property(e => e.CusUploadNum)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("Cus_Upload_Num")
                    .HasComment("單號");

                entity.Property(e => e.CustId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("CustID")
                    .HasComment("代理商帳號");

                entity.Property(e => e.SalesId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("SalesID")
                    .HasComment("開單帳號");
            });

            modelBuilder.Entity<WebOrderCusUploadDetail>(entity =>
            {
                entity.ToTable("WebOrder_Cus_Upload_Detail");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasComment("流水號");

                entity.Property(e => e.ComfirmId)
                    .HasMaxLength(10)
                    .HasColumnName("ComfirmID")
                    .HasComment("回簽帳號");

                entity.Property(e => e.ConfirmDate)
                    .HasColumnType("datetime")
                    .HasColumnName("confirm_date")
                    .HasComment("確認時間");

                entity.Property(e => e.Confirmfilename)
                    .HasMaxLength(200)
                    .HasComment("回簽檔案路徑");

                entity.Property(e => e.CusUploadNum)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("Cus_Upload_Num")
                    .HasComment("單號");

                entity.Property(e => e.Filename)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("filename")
                    .HasComment("簽核檔案路徑");

                entity.Property(e => e.Item)
                    .HasColumnName("item")
                    .HasComment("項目");

                entity.Property(e => e.ModIp)
                    .HasMaxLength(50)
                    .HasColumnName("ModIP")
                    .HasComment("異動IP");

                entity.Property(e => e.Modtime)
                    .HasColumnType("datetime")
                    .HasComment("異動時間");

                entity.Property(e => e.SalesId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("SalesID")
                    .HasComment("開單帳號");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
