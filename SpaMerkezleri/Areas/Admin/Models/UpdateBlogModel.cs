using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpaMerkezleri.Areas.Admin.Models
{
    public class UpdateBlogModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlığın doldurulması gerekir.")]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "İçerik doldurulması gerekir.")]
        public string DetailDescription { get; set; }

        public string BlogCategory { get; set; }

        public string SektorCategory { get; set; }

        public string ImageUrl { get; set; }

        public IFormFile NewImageFile { get; set; }
    }
}
