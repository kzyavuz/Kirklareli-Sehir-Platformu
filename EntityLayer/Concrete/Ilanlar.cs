using System;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class Ilanlar
    {
        [Key]
        public int IlanID { get; set; }
        public string IlanCesidi { get; set; }
        public string IlanYeri { get; set; }
        public string İlanAcikklamasi { get; set; }
        public string İlanKonumu { get; set; }
        public bool StandOutStatus { get; set; }
        public string Status { get; set; }
        public string ImagePath { get; set; }
        public string İlanTelefonNo { get; set; }
        public DateTime ilanDate { get; set; }
        public DateTime ApprovalDateTime { get; set; }
        public DateTime RejectionDateTime { get; set; }
        public DateTime StandOutDateTime { get; set; }
        public string IlanBasligi { get; set; }
        public string IlanNumarasi { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}
