using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortal_ManagementSystem.Models
{
    public class UploadedFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }


        public byte[] ImageData { get; set; }
    }
}