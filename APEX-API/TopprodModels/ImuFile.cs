using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class ImuFile
    {
        public string Imu01 { get; set; }
        public string Imu02 { get; set; }
        public short? Imu11 { get; set; }
        public string Imu12 { get; set; }
        public short? Imu21 { get; set; }
        public string Imu22 { get; set; }
        public short? Imu31 { get; set; }
        public string Imu32 { get; set; }
        public short? Imu41 { get; set; }
        public string Imu42 { get; set; }
        public short? Imu51 { get; set; }
        public string Imu52 { get; set; }
        public short? Imu61 { get; set; }
        public string Imu62 { get; set; }
        public string Imuacti { get; set; }
        public string Imuuser { get; set; }
        public string Imugrup { get; set; }
        public string Imumodu { get; set; }
        public DateTime? Imudate { get; set; }
        public string Imuoriu { get; set; }
        public string Imuorig { get; set; }
    }
}
