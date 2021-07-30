using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.Models
{
    public partial class Reducer2
    {
        public string NewPartNo { get; set; }
        public string PartNo { get; set; }
        public string ReducerType { get; set; }
        public float? Ratio { get; set; }
        public float? T1b { get; set; }
        public float? T1a { get; set; }
        public float? T1n { get; set; }
        public float? T2b { get; set; }
        public float? T2a { get; set; }
        public float? T2n { get; set; }
        public float? N1b { get; set; }
        public float? N1n { get; set; }
        public float? T1m { get; set; }
        public float? Inertia { get; set; }
        public float? Weight { get; set; }
        public float? M3max { get; set; }
        public float? M3maxWeb { get; set; }
        public float? POuterDia { get; set; }
        public float? Lcmin { get; set; }
        public float? Awidth1 { get; set; }
        public float? Awidth2 { get; set; }
        public short? Shaft { get; set; }
        public short? Backlash { get; set; }
        public short? Stage { get; set; }
        public float? Breakaway { get; set; }
        public float? Backdrive { get; set; }
        public string Suitadapter { get; set; }
        public string Display { get; set; }
        public string Primal { get; set; }
        public short? SgTeeth { get; set; }
        public string Re { get; set; }
    }
}
