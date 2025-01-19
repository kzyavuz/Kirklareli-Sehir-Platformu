using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaMerkezleri.Areas.Admin.Models
{
    public class bannerModel
    {
        public int id { get; set; }
        public string bannerUrl { get; set; }
        public IFormFile bannerImage { get; set; }
        public IFormFile NewImageFile { get; set; }
        public string ExistingImagePath { get; set; }
    }
}
