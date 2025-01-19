using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaMerkezleri.Areas.Member.Models
{
    public class IlanModel
    {
        public int IlanID { get; set; }
        public int AppUserId { get; set; }
        public DateTime ilanDate { get; set; }
        public string IlanCesidi { get; set; }
        public string IlanYeri { get; set; }
        public string İlanAcikklamasi { get; set; }
        public string İlanKonumu { get; set; }
        public string İlanTelefonNo { get; set; }
        public string IlanBasligi { get; set; }
        public string IlanNumarasi { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ImagePath { get; set; }
        public IFormFile Image { get; set; }
        public DateTime ApprovalDateTime { get; set; }
    }
}
