using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.Models
{
    public partial class MotorNet
    {
        public int MotoId { get; set; }
        public string Brand { get; set; }
        public string Specification { get; set; }
        public float? Power { get; set; }
        public float? T1b { get; set; }
        public float? T1n { get; set; }
        public float? N1b { get; set; }
        public float? N1n { get; set; }
        public float? Inertia { get; set; }
        public float? S { get; set; }
        public float? Lr { get; set; }
        public float? Lb { get; set; }
        public float? Le { get; set; }
        public float? Lt { get; set; }
        public float? La { get; set; }
        public float? Lz { get; set; }
        public float? Lc { get; set; }
        public double? FlangeWidth { get; set; }
        public float? Dinertia { get; set; }
        public float? ScrewDia { get; set; }
        public bool SaveChk { get; set; }
        public string Display { get; set; }
        public string FixPlate { get; set; }
        public string FixScrew { get; set; }
        public string SaveChk2 { get; set; }
    }
}
