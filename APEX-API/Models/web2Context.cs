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
        public virtual DbSet<AdapClass1> AdapClass1s { get; set; }
        public virtual DbSet<AdapClassEx2> AdapClassEx2s { get; set; }
        public virtual DbSet<AdapClassEx3> AdapClassEx3s { get; set; }
        public virtual DbSet<AdapClassEx4> AdapClassEx4s { get; set; }
        public virtual DbSet<AdapDir1Class1> AdapDir1Class1s { get; set; }
        public virtual DbSet<AdapType1Class0> AdapType1Class0s { get; set; }
        public virtual DbSet<AdapType1Class1> AdapType1Class1s { get; set; }
        public virtual DbSet<AdapType1ClassEx2> AdapType1ClassEx2s { get; set; }
        public virtual DbSet<AdapType1ClassEx3> AdapType1ClassEx3s { get; set; }
        public virtual DbSet<AdapType1ClassEx4> AdapType1ClassEx4s { get; set; }
        public virtual DbSet<AgentsMember> AgentsMembers { get; set; }
        public virtual DbSet<ComplaintsApexF> ComplaintsApexFs { get; set; }
        public virtual DbSet<ComplaintsApexH> ComplaintsApexHs { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Cust> Custs { get; set; }
        public virtual DbSet<InputMotor> InputMotors { get; set; }
        public virtual DbSet<MotorNet> MotorNets { get; set; }
        public virtual DbSet<MotorNet1> MotorNets1 { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderBom> OrderBoms { get; set; }
        public virtual DbSet<OrderBom2> OrderBom2s { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderDetail2> OrderDetail2s { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Paymentdetail> Paymentdetails { get; set; }
        public virtual DbSet<ReSg> ReSgs { get; set; }
        public virtual DbSet<Reducer1> Reducer1s { get; set; }
        public virtual DbSet<Reducer2> Reducer2s { get; set; }
        public virtual DbSet<Reducer3> Reducer3s { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }

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

            modelBuilder.Entity<AdapClass1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Adap_class1");

                entity.Property(e => e.AdpType).HasColumnName("adp_type");

                entity.Property(e => e.AdpterReally)
                    .HasMaxLength(50)
                    .HasColumnName("adpter_really");

                entity.Property(e => e.C1).HasColumnName("c1");

                entity.Property(e => e.C10).HasColumnName("c10");

                entity.Property(e => e.C11).HasColumnName("c11");

                entity.Property(e => e.C21).HasColumnName("c21");

                entity.Property(e => e.C22).HasColumnName("c22");

                entity.Property(e => e.C23).HasColumnName("c23");

                entity.Property(e => e.C4).HasColumnName("c4");

                entity.Property(e => e.C4min).HasColumnName("c4min");

                entity.Property(e => e.C5).HasColumnName("c5");

                entity.Property(e => e.C6).HasColumnName("c6");

                entity.Property(e => e.C61).HasColumnName("c61");

                entity.Property(e => e.C7).HasColumnName("c7");

                entity.Property(e => e.C8).HasColumnName("c8");

                entity.Property(e => e.Class).HasColumnName("class");

                entity.Property(e => e.Conjunction)
                    .HasMaxLength(50)
                    .HasColumnName("conjunction");

                entity.Property(e => e.EffectiveDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Effective Date");

                entity.Property(e => e.NewPartNo)
                    .HasMaxLength(50)
                    .HasColumnName("newPartNo");

                entity.Property(e => e.PartNo).HasMaxLength(50);

                entity.Property(e => e.Rwidth).HasColumnName("RWidth");

                entity.Property(e => e.Specification).HasMaxLength(50);

                entity.Property(e => e.StockCheck).HasMaxLength(50);
            });

            modelBuilder.Entity<AdapClassEx2>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Adap_classEx2");

                entity.Property(e => e.AdpType).HasColumnName("adp_type");

                entity.Property(e => e.AdpterReally)
                    .HasMaxLength(50)
                    .HasColumnName("adpter_really");

                entity.Property(e => e.C1).HasColumnName("c1");

                entity.Property(e => e.C10).HasColumnName("c10");

                entity.Property(e => e.C11).HasColumnName("c11");

                entity.Property(e => e.C21).HasColumnName("c21");

                entity.Property(e => e.C22).HasColumnName("c22");

                entity.Property(e => e.C23).HasColumnName("c23");

                entity.Property(e => e.C4).HasColumnName("c4");

                entity.Property(e => e.C4min).HasColumnName("c4min");

                entity.Property(e => e.C5).HasColumnName("c5");

                entity.Property(e => e.C6).HasColumnName("c6");

                entity.Property(e => e.C61).HasColumnName("c61");

                entity.Property(e => e.C7).HasColumnName("c7");

                entity.Property(e => e.C8).HasColumnName("c8");

                entity.Property(e => e.Class).HasColumnName("class");

                entity.Property(e => e.Conjunction)
                    .HasMaxLength(50)
                    .HasColumnName("conjunction");

                entity.Property(e => e.EffectiveDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Effective Date");

                entity.Property(e => e.NewPartNo)
                    .HasMaxLength(50)
                    .HasColumnName("newPartNo");

                entity.Property(e => e.PartNo).HasMaxLength(50);

                entity.Property(e => e.Rwidth).HasColumnName("RWidth");

                entity.Property(e => e.Specification).HasMaxLength(50);

                entity.Property(e => e.StockCheck).HasMaxLength(50);
            });

            modelBuilder.Entity<AdapClassEx3>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Adap_classEx3");

                entity.Property(e => e.AdpType).HasColumnName("adp_type");

                entity.Property(e => e.AdpterReally)
                    .HasMaxLength(50)
                    .HasColumnName("adpter_really");

                entity.Property(e => e.C1).HasColumnName("c1");

                entity.Property(e => e.C10).HasColumnName("c10");

                entity.Property(e => e.C11).HasColumnName("c11");

                entity.Property(e => e.C21).HasColumnName("c21");

                entity.Property(e => e.C22).HasColumnName("c22");

                entity.Property(e => e.C23).HasColumnName("c23");

                entity.Property(e => e.C4).HasColumnName("c4");

                entity.Property(e => e.C4min).HasColumnName("c4min");

                entity.Property(e => e.C5).HasColumnName("c5");

                entity.Property(e => e.C6).HasColumnName("c6");

                entity.Property(e => e.C61).HasColumnName("c61");

                entity.Property(e => e.C7).HasColumnName("c7");

                entity.Property(e => e.C8).HasColumnName("c8");

                entity.Property(e => e.Class).HasColumnName("class");

                entity.Property(e => e.Conjunction)
                    .HasMaxLength(50)
                    .HasColumnName("conjunction");

                entity.Property(e => e.EffectiveDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Effective Date");

                entity.Property(e => e.NewPartNo)
                    .HasMaxLength(50)
                    .HasColumnName("newPartNo");

                entity.Property(e => e.PartNo).HasMaxLength(50);

                entity.Property(e => e.Rwidth).HasColumnName("RWidth");

                entity.Property(e => e.Specification).HasMaxLength(50);

                entity.Property(e => e.StockCheck).HasMaxLength(50);
            });

            modelBuilder.Entity<AdapClassEx4>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Adap_classEx4");

                entity.Property(e => e.AdpType).HasColumnName("adp_type");

                entity.Property(e => e.AdpterReally)
                    .HasMaxLength(50)
                    .HasColumnName("adpter_really");

                entity.Property(e => e.C1).HasColumnName("c1");

                entity.Property(e => e.C10).HasColumnName("c10");

                entity.Property(e => e.C11).HasColumnName("c11");

                entity.Property(e => e.C21).HasColumnName("c21");

                entity.Property(e => e.C22).HasColumnName("c22");

                entity.Property(e => e.C23).HasColumnName("c23");

                entity.Property(e => e.C4).HasColumnName("c4");

                entity.Property(e => e.C4min).HasColumnName("c4min");

                entity.Property(e => e.C5).HasColumnName("c5");

                entity.Property(e => e.C6).HasColumnName("c6");

                entity.Property(e => e.C61).HasColumnName("c61");

                entity.Property(e => e.C7).HasColumnName("c7");

                entity.Property(e => e.C8).HasColumnName("c8");

                entity.Property(e => e.Class).HasColumnName("class");

                entity.Property(e => e.Conjunction)
                    .HasMaxLength(50)
                    .HasColumnName("conjunction");

                entity.Property(e => e.EffectiveDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Effective Date");

                entity.Property(e => e.NewPartNo)
                    .HasMaxLength(50)
                    .HasColumnName("newPartNo");

                entity.Property(e => e.PartNo).HasMaxLength(50);

                entity.Property(e => e.Rwidth).HasColumnName("RWidth");

                entity.Property(e => e.Specification).HasMaxLength(50);

                entity.Property(e => e.StockCheck).HasMaxLength(50);
            });

            modelBuilder.Entity<AdapDir1Class1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Adap_dir1_class1");

                entity.Property(e => e.AdpType).HasColumnName("adp_type");

                entity.Property(e => e.AdpterReally)
                    .HasMaxLength(50)
                    .HasColumnName("adpter_really");

                entity.Property(e => e.C1).HasColumnName("c1");

                entity.Property(e => e.C10).HasColumnName("c10");

                entity.Property(e => e.C11).HasColumnName("c11");

                entity.Property(e => e.C21).HasColumnName("c21");

                entity.Property(e => e.C22).HasColumnName("c22");

                entity.Property(e => e.C23).HasColumnName("c23");

                entity.Property(e => e.C4).HasColumnName("c4");

                entity.Property(e => e.C4min).HasColumnName("c4min");

                entity.Property(e => e.C5).HasColumnName("c5");

                entity.Property(e => e.C6).HasColumnName("c6");

                entity.Property(e => e.C61).HasColumnName("c61");

                entity.Property(e => e.C7).HasColumnName("c7");

                entity.Property(e => e.C8).HasColumnName("c8");

                entity.Property(e => e.Class).HasColumnName("class");

                entity.Property(e => e.Conjunction)
                    .HasMaxLength(50)
                    .HasColumnName("conjunction");

                entity.Property(e => e.EffectiveDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Effective Date");

                entity.Property(e => e.Expr1).HasMaxLength(50);

                entity.Property(e => e.NewPartNo)
                    .HasMaxLength(50)
                    .HasColumnName("newPartNo");

                entity.Property(e => e.PartNo).HasMaxLength(50);

                entity.Property(e => e.Rwidth).HasColumnName("RWidth");

                entity.Property(e => e.Specification).HasMaxLength(50);

                entity.Property(e => e.StockCheck).HasMaxLength(50);
            });

            modelBuilder.Entity<AdapType1Class0>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Adap_type1_class0");

                entity.Property(e => e.AdpType).HasColumnName("adp_type");

                entity.Property(e => e.AdpterReally)
                    .HasMaxLength(50)
                    .HasColumnName("adpter_really");

                entity.Property(e => e.C1).HasColumnName("c1");

                entity.Property(e => e.C10).HasColumnName("c10");

                entity.Property(e => e.C11).HasColumnName("c11");

                entity.Property(e => e.C21).HasColumnName("c21");

                entity.Property(e => e.C22).HasColumnName("c22");

                entity.Property(e => e.C23).HasColumnName("c23");

                entity.Property(e => e.C4).HasColumnName("c4");

                entity.Property(e => e.C4min).HasColumnName("c4min");

                entity.Property(e => e.C5).HasColumnName("c5");

                entity.Property(e => e.C6).HasColumnName("c6");

                entity.Property(e => e.C61).HasColumnName("c61");

                entity.Property(e => e.C7).HasColumnName("c7");

                entity.Property(e => e.C8).HasColumnName("c8");

                entity.Property(e => e.Class).HasColumnName("class");

                entity.Property(e => e.Conjunction)
                    .HasMaxLength(50)
                    .HasColumnName("conjunction");

                entity.Property(e => e.EffectiveDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Effective Date");

                entity.Property(e => e.NewPartNo)
                    .HasMaxLength(50)
                    .HasColumnName("newPartNo");

                entity.Property(e => e.PartNo).HasMaxLength(50);

                entity.Property(e => e.Rwidth).HasColumnName("RWidth");

                entity.Property(e => e.Specification).HasMaxLength(50);

                entity.Property(e => e.StockCheck).HasMaxLength(50);
            });

            modelBuilder.Entity<AdapType1Class1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Adap_type1_class1");

                entity.Property(e => e.AdpType).HasColumnName("adp_type");

                entity.Property(e => e.AdpterReally)
                    .HasMaxLength(50)
                    .HasColumnName("adpter_really");

                entity.Property(e => e.C1).HasColumnName("c1");

                entity.Property(e => e.C10).HasColumnName("c10");

                entity.Property(e => e.C11).HasColumnName("c11");

                entity.Property(e => e.C21).HasColumnName("c21");

                entity.Property(e => e.C22).HasColumnName("c22");

                entity.Property(e => e.C23).HasColumnName("c23");

                entity.Property(e => e.C4).HasColumnName("c4");

                entity.Property(e => e.C4min).HasColumnName("c4min");

                entity.Property(e => e.C5).HasColumnName("c5");

                entity.Property(e => e.C6).HasColumnName("c6");

                entity.Property(e => e.C61).HasColumnName("c61");

                entity.Property(e => e.C7).HasColumnName("c7");

                entity.Property(e => e.C8).HasColumnName("c8");

                entity.Property(e => e.Class).HasColumnName("class");

                entity.Property(e => e.Conjunction)
                    .HasMaxLength(50)
                    .HasColumnName("conjunction");

                entity.Property(e => e.EffectiveDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Effective Date");

                entity.Property(e => e.NewPartNo)
                    .HasMaxLength(50)
                    .HasColumnName("newPartNo");

                entity.Property(e => e.PartNo).HasMaxLength(50);

                entity.Property(e => e.Rwidth).HasColumnName("RWidth");

                entity.Property(e => e.Specification).HasMaxLength(50);

                entity.Property(e => e.StockCheck).HasMaxLength(50);
            });

            modelBuilder.Entity<AdapType1ClassEx2>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Adap_type1_classEx2");

                entity.Property(e => e.AdpType).HasColumnName("adp_type");

                entity.Property(e => e.AdpterReally)
                    .HasMaxLength(50)
                    .HasColumnName("adpter_really");

                entity.Property(e => e.C1).HasColumnName("c1");

                entity.Property(e => e.C10).HasColumnName("c10");

                entity.Property(e => e.C11).HasColumnName("c11");

                entity.Property(e => e.C21).HasColumnName("c21");

                entity.Property(e => e.C22).HasColumnName("c22");

                entity.Property(e => e.C23).HasColumnName("c23");

                entity.Property(e => e.C4).HasColumnName("c4");

                entity.Property(e => e.C4min).HasColumnName("c4min");

                entity.Property(e => e.C5).HasColumnName("c5");

                entity.Property(e => e.C6).HasColumnName("c6");

                entity.Property(e => e.C61).HasColumnName("c61");

                entity.Property(e => e.C7).HasColumnName("c7");

                entity.Property(e => e.C8).HasColumnName("c8");

                entity.Property(e => e.Class).HasColumnName("class");

                entity.Property(e => e.Conjunction)
                    .HasMaxLength(50)
                    .HasColumnName("conjunction");

                entity.Property(e => e.EffectiveDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Effective Date");

                entity.Property(e => e.NewPartNo)
                    .HasMaxLength(50)
                    .HasColumnName("newPartNo");

                entity.Property(e => e.PartNo).HasMaxLength(50);

                entity.Property(e => e.Rwidth).HasColumnName("RWidth");

                entity.Property(e => e.Specification).HasMaxLength(50);

                entity.Property(e => e.StockCheck).HasMaxLength(50);
            });

            modelBuilder.Entity<AdapType1ClassEx3>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Adap_type1_classEx3");

                entity.Property(e => e.AdpType).HasColumnName("adp_type");

                entity.Property(e => e.AdpterReally)
                    .HasMaxLength(50)
                    .HasColumnName("adpter_really");

                entity.Property(e => e.C1).HasColumnName("c1");

                entity.Property(e => e.C10).HasColumnName("c10");

                entity.Property(e => e.C11).HasColumnName("c11");

                entity.Property(e => e.C21).HasColumnName("c21");

                entity.Property(e => e.C22).HasColumnName("c22");

                entity.Property(e => e.C23).HasColumnName("c23");

                entity.Property(e => e.C4).HasColumnName("c4");

                entity.Property(e => e.C4min).HasColumnName("c4min");

                entity.Property(e => e.C5).HasColumnName("c5");

                entity.Property(e => e.C6).HasColumnName("c6");

                entity.Property(e => e.C61).HasColumnName("c61");

                entity.Property(e => e.C7).HasColumnName("c7");

                entity.Property(e => e.C8).HasColumnName("c8");

                entity.Property(e => e.Class).HasColumnName("class");

                entity.Property(e => e.Conjunction)
                    .HasMaxLength(50)
                    .HasColumnName("conjunction");

                entity.Property(e => e.EffectiveDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Effective Date");

                entity.Property(e => e.NewPartNo)
                    .HasMaxLength(50)
                    .HasColumnName("newPartNo");

                entity.Property(e => e.PartNo).HasMaxLength(50);

                entity.Property(e => e.Rwidth).HasColumnName("RWidth");

                entity.Property(e => e.Specification).HasMaxLength(50);

                entity.Property(e => e.StockCheck).HasMaxLength(50);
            });

            modelBuilder.Entity<AdapType1ClassEx4>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Adap_type1_classEx4");

                entity.Property(e => e.AdpType).HasColumnName("adp_type");

                entity.Property(e => e.AdpterReally)
                    .HasMaxLength(50)
                    .HasColumnName("adpter_really");

                entity.Property(e => e.C1).HasColumnName("c1");

                entity.Property(e => e.C10).HasColumnName("c10");

                entity.Property(e => e.C11).HasColumnName("c11");

                entity.Property(e => e.C21).HasColumnName("c21");

                entity.Property(e => e.C22).HasColumnName("c22");

                entity.Property(e => e.C23).HasColumnName("c23");

                entity.Property(e => e.C4).HasColumnName("c4");

                entity.Property(e => e.C4min).HasColumnName("c4min");

                entity.Property(e => e.C5).HasColumnName("c5");

                entity.Property(e => e.C6).HasColumnName("c6");

                entity.Property(e => e.C61).HasColumnName("c61");

                entity.Property(e => e.C7).HasColumnName("c7");

                entity.Property(e => e.C8).HasColumnName("c8");

                entity.Property(e => e.Class).HasColumnName("class");

                entity.Property(e => e.Conjunction)
                    .HasMaxLength(50)
                    .HasColumnName("conjunction");

                entity.Property(e => e.EffectiveDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Effective Date");

                entity.Property(e => e.NewPartNo)
                    .HasMaxLength(50)
                    .HasColumnName("newPartNo");

                entity.Property(e => e.PartNo).HasMaxLength(50);

                entity.Property(e => e.Rwidth).HasColumnName("RWidth");

                entity.Property(e => e.Specification).HasMaxLength(50);

                entity.Property(e => e.StockCheck).HasMaxLength(50);
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

            modelBuilder.Entity<ComplaintsApexF>(entity =>
            {
                entity.HasKey(e => new { e.ComplaintsNum, e.Item });

                entity.ToTable("complaints_apex_f");

                entity.Property(e => e.ComplaintsNum)
                    .HasMaxLength(16)
                    .HasColumnName("complaints_num");

                entity.Property(e => e.Item).HasColumnName("item");

                entity.Property(e => e.Filename)
                    .HasMaxLength(200)
                    .HasColumnName("filename");
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

                entity.Property(e => e.ComfirmDate)
                    .HasColumnType("datetime")
                    .HasColumnName("comfirm_date");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("create_date");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.DriveType)
                    .HasMaxLength(100)
                    .HasColumnName("Drive_Type");

                entity.Property(e => e.F2aB).HasMaxLength(100);

                entity.Property(e => e.F2rB).HasMaxLength(100);

                entity.Property(e => e.Machinery)
                    .HasMaxLength(100)
                    .HasColumnName("machinery");

                entity.Property(e => e.ModeNo)
                    .HasMaxLength(200)
                    .HasColumnName("mode_no");

                entity.Property(e => e.OperationalModeS1)
                    .HasMaxLength(1)
                    .HasColumnName("Operational_Mode_S1");

                entity.Property(e => e.OperationalModeS5)
                    .HasMaxLength(1)
                    .HasColumnName("Operational_Mode_S5");

                entity.Property(e => e.Ptype)
                    .HasMaxLength(1)
                    .HasColumnName("ptype");

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

                entity.Property(e => e.ClaimLevel).HasColumnName("Claim_Level");

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

            modelBuilder.Entity<MotorNet>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("MotorNet");

                entity.Property(e => e.Brand).HasMaxLength(50);

                entity.Property(e => e.Dinertia).HasColumnName("DInertia");

                entity.Property(e => e.Display).HasMaxLength(50);

                entity.Property(e => e.FixPlate).HasMaxLength(12);

                entity.Property(e => e.FixScrew).HasMaxLength(12);

                entity.Property(e => e.La).HasColumnName("LA");

                entity.Property(e => e.Lb).HasColumnName("LB");

                entity.Property(e => e.Lc).HasColumnName("LC");

                entity.Property(e => e.Le).HasColumnName("LE");

                entity.Property(e => e.Lr).HasColumnName("LR");

                entity.Property(e => e.Lt).HasColumnName("LT");

                entity.Property(e => e.Lz).HasColumnName("LZ");

                entity.Property(e => e.MotoId).HasColumnName("MotoID");

                entity.Property(e => e.N1b).HasColumnName("N1B");

                entity.Property(e => e.N1n).HasColumnName("N1N");

                entity.Property(e => e.SaveChk2).HasMaxLength(10);

                entity.Property(e => e.Specification).HasMaxLength(50);

                entity.Property(e => e.T1b).HasColumnName("T1B");

                entity.Property(e => e.T1n).HasColumnName("T1N");
            });

            modelBuilder.Entity<MotorNet1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("MotorNet_");

                entity.Property(e => e.Brand).HasMaxLength(50);

                entity.Property(e => e.Dinertia).HasColumnName("DInertia");

                entity.Property(e => e.Display).HasMaxLength(50);

                entity.Property(e => e.La).HasColumnName("LA");

                entity.Property(e => e.La2).HasColumnName("LA2");

                entity.Property(e => e.Lb).HasColumnName("LB");

                entity.Property(e => e.Lc).HasColumnName("LC");

                entity.Property(e => e.Ld).HasColumnName("LD");

                entity.Property(e => e.Ld2).HasColumnName("LD2");

                entity.Property(e => e.Le).HasColumnName("LE");

                entity.Property(e => e.Ln).HasColumnName("LN");

                entity.Property(e => e.Ln2).HasColumnName("LN2");

                entity.Property(e => e.Lr).HasColumnName("LR");

                entity.Property(e => e.Lt).HasColumnName("LT");

                entity.Property(e => e.Lz).HasColumnName("LZ");

                entity.Property(e => e.Lz2).HasColumnName("LZ2");

                entity.Property(e => e.MotoId).HasColumnName("MotoID");

                entity.Property(e => e.N1b).HasColumnName("N1B");

                entity.Property(e => e.N1n).HasColumnName("N1N");

                entity.Property(e => e.SaveChk2).HasMaxLength(10);

                entity.Property(e => e.Specification).HasMaxLength(50);

                entity.Property(e => e.T1b).HasColumnName("T1B");

                entity.Property(e => e.T1n).HasColumnName("T1N");
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
                    .HasMaxLength(10)
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
                    .HasMaxLength(500)
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
                    .HasColumnName("adapter_cus");

                entity.Property(e => e.AdapterCus2)
                    .HasMaxLength(1)
                    .HasColumnName("adapter_cus2");

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

                entity.Property(e => e.DesginTool)
                    .HasMaxLength(1)
                    .HasColumnName("Desgin_Tool");

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

                entity.Property(e => e.Spec).HasMaxLength(150);

                entity.Property(e => e.Surcharge)
                    .HasColumnName("surcharge")
                    .HasDefaultValueSql("((0))");

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

                entity.Property(e => e.Surcharge)
                    .HasColumnName("surcharge")
                    .HasDefaultValueSql("((0))");

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

            modelBuilder.Entity<ReSg>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("RE-SG");

                entity.Property(e => e.Awidth1).HasColumnName("AWidth1");

                entity.Property(e => e.Awidth2).HasColumnName("AWidth2");

                entity.Property(e => e.Display).HasMaxLength(10);

                entity.Property(e => e.Lcmin).HasColumnName("LCmin");

                entity.Property(e => e.M3maxWeb).HasColumnName("M3max_Web");

                entity.Property(e => e.N1b).HasColumnName("N1B");

                entity.Property(e => e.N1n).HasColumnName("N1N");

                entity.Property(e => e.NewPartNo)
                    .HasMaxLength(50)
                    .HasColumnName("newPartNo");

                entity.Property(e => e.POuterDia).HasColumnName("P_OuterDia");

                entity.Property(e => e.PartNo).HasMaxLength(50);

                entity.Property(e => e.Primal)
                    .HasMaxLength(50)
                    .HasColumnName("primal");

                entity.Property(e => e.ReducerType)
                    .HasMaxLength(50)
                    .HasColumnName("Reducer_type");

                entity.Property(e => e.ReducerType1)
                    .HasMaxLength(50)
                    .HasColumnName("ReducerType");

                entity.Property(e => e.SgTeeth).HasColumnName("SG_Teeth");

                entity.Property(e => e.Stage).HasColumnName("stage");

                entity.Property(e => e.Suitadapter)
                    .HasMaxLength(50)
                    .HasColumnName("suitadapter");

                entity.Property(e => e.T1a).HasColumnName("T1A");

                entity.Property(e => e.T1b).HasColumnName("T1B");

                entity.Property(e => e.T1m).HasColumnName("T1M");

                entity.Property(e => e.T1n).HasColumnName("T1N");

                entity.Property(e => e.T2a).HasColumnName("T2A");

                entity.Property(e => e.T2b).HasColumnName("T2B");

                entity.Property(e => e.T2n).HasColumnName("T2N");

                entity.Property(e => e.品號).HasMaxLength(255);

                entity.Property(e => e.規格).HasMaxLength(255);
            });

            modelBuilder.Entity<Reducer1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Reducer1");

                entity.Property(e => e.Awidth1).HasColumnName("AWidth1");

                entity.Property(e => e.Awidth2).HasColumnName("AWidth2");

                entity.Property(e => e.Display).HasMaxLength(10);

                entity.Property(e => e.Lcmin).HasColumnName("LCmin");

                entity.Property(e => e.M3maxWeb).HasColumnName("M3max_Web");

                entity.Property(e => e.N1b).HasColumnName("N1B");

                entity.Property(e => e.N1n).HasColumnName("N1N");

                entity.Property(e => e.NewPartNo)
                    .HasMaxLength(50)
                    .HasColumnName("newPartNo");

                entity.Property(e => e.POuterDia).HasColumnName("P_OuterDia");

                entity.Property(e => e.PartNo).HasMaxLength(50);

                entity.Property(e => e.Primal)
                    .HasMaxLength(50)
                    .HasColumnName("primal");

                entity.Property(e => e.ReducerType).HasMaxLength(50);

                entity.Property(e => e.SgTeeth).HasColumnName("SG_Teeth");

                entity.Property(e => e.Stage).HasColumnName("stage");

                entity.Property(e => e.Suitadapter)
                    .HasMaxLength(50)
                    .HasColumnName("suitadapter");

                entity.Property(e => e.T1a).HasColumnName("T1A");

                entity.Property(e => e.T1b).HasColumnName("T1B");

                entity.Property(e => e.T1m).HasColumnName("T1M");

                entity.Property(e => e.T1n).HasColumnName("T1N");

                entity.Property(e => e.T2a).HasColumnName("T2A");

                entity.Property(e => e.T2b).HasColumnName("T2B");

                entity.Property(e => e.T2n).HasColumnName("T2N");
            });

            modelBuilder.Entity<Reducer2>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Reducer2");

                entity.Property(e => e.Awidth1).HasColumnName("AWidth1");

                entity.Property(e => e.Awidth2).HasColumnName("AWidth2");

                entity.Property(e => e.Display).HasMaxLength(10);

                entity.Property(e => e.Lcmin).HasColumnName("LCmin");

                entity.Property(e => e.M3maxWeb).HasColumnName("M3max_Web");

                entity.Property(e => e.N1b).HasColumnName("N1B");

                entity.Property(e => e.N1n).HasColumnName("N1N");

                entity.Property(e => e.NewPartNo)
                    .HasMaxLength(50)
                    .HasColumnName("newPartNo");

                entity.Property(e => e.POuterDia).HasColumnName("P_OuterDia");

                entity.Property(e => e.PartNo).HasMaxLength(50);

                entity.Property(e => e.Primal)
                    .HasMaxLength(50)
                    .HasColumnName("primal");

                entity.Property(e => e.Re)
                    .HasMaxLength(81)
                    .HasColumnName("re");

                entity.Property(e => e.ReducerType).HasMaxLength(50);

                entity.Property(e => e.SgTeeth).HasColumnName("SG_Teeth");

                entity.Property(e => e.Stage).HasColumnName("stage");

                entity.Property(e => e.Suitadapter)
                    .HasMaxLength(50)
                    .HasColumnName("suitadapter");

                entity.Property(e => e.T1a).HasColumnName("T1A");

                entity.Property(e => e.T1b).HasColumnName("T1B");

                entity.Property(e => e.T1m).HasColumnName("T1M");

                entity.Property(e => e.T1n).HasColumnName("T1N");

                entity.Property(e => e.T2a).HasColumnName("T2A");

                entity.Property(e => e.T2b).HasColumnName("T2B");

                entity.Property(e => e.T2n).HasColumnName("T2N");
            });

            modelBuilder.Entity<Reducer3>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Reducer3");

                entity.Property(e => e.Awidth1).HasColumnName("AWidth1");

                entity.Property(e => e.Awidth2).HasColumnName("AWidth2");

                entity.Property(e => e.Display).HasMaxLength(10);

                entity.Property(e => e.Lcmin).HasColumnName("LCmin");

                entity.Property(e => e.M3maxWeb).HasColumnName("M3max_Web");

                entity.Property(e => e.N1b).HasColumnName("N1B");

                entity.Property(e => e.N1n).HasColumnName("N1N");

                entity.Property(e => e.NewPartNo)
                    .HasMaxLength(50)
                    .HasColumnName("newPartNo");

                entity.Property(e => e.POuterDia).HasColumnName("P_OuterDia");

                entity.Property(e => e.PartNo).HasMaxLength(50);

                entity.Property(e => e.Primal)
                    .HasMaxLength(50)
                    .HasColumnName("primal");

                entity.Property(e => e.ReducerType).HasMaxLength(50);

                entity.Property(e => e.SgTeeth).HasColumnName("SG_Teeth");

                entity.Property(e => e.Stage).HasColumnName("stage");

                entity.Property(e => e.Suitadapter)
                    .HasMaxLength(50)
                    .HasColumnName("suitadapter");

                entity.Property(e => e.T1a).HasColumnName("T1A");

                entity.Property(e => e.T1b).HasColumnName("T1B");

                entity.Property(e => e.T1m).HasColumnName("T1M");

                entity.Property(e => e.T1n).HasColumnName("T1N");

                entity.Property(e => e.T2a).HasColumnName("T2A");

                entity.Property(e => e.T2b).HasColumnName("T2B");

                entity.Property(e => e.T2n).HasColumnName("T2N");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.SalesId);

                entity.Property(e => e.SalesId)
                    .HasMaxLength(50)
                    .HasColumnName("SalesID");

                entity.Property(e => e.ClaimLevel).HasColumnName("Claim_Level");

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.EmployeeCnName)
                    .HasMaxLength(50)
                    .HasColumnName("Employee_Cn_Name");

                entity.Property(e => e.ExceptionLoginTp)
                    .HasMaxLength(50)
                    .HasColumnName("Exception_LoginTP");

                entity.Property(e => e.Pwd)
                    .HasMaxLength(50)
                    .HasColumnName("PWD");

                entity.Property(e => e.Receiver).HasMaxLength(1);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
