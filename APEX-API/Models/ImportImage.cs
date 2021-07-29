using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.Models
{
    public partial class ImportImage
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string FolderName { get; set; }
        public byte[] Photo { get; set; }
    }
}
