using System;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class Isletmeler
    {
        [Key]
        public int IsletmeID { get; set; }
        public string IsletmeUyeAdi { get; set; }
        public string IsletmeUyeSoyAdi { get; set; }
        public string IsletmeUyeMail { get; set; }
        public string IsletmeTelefonNumarasi { get; set; }
        public string ISletmeAdi { get; set; }
        public string ISletmeTipi { get; set; }
        public string IsletmeLinki { get; set; }
        public string IsletmeResimi { get; set; }
        public string IsletmeResimi1 { get; set; }
        public string IsletmeResimi2 { get; set; }
        public string IsletmeResimi3 { get; set; }
        public string IsletmeResimi4 { get; set; }
        public string IsletmeResimi5 { get; set; }
        public string IsletmeResimi6 { get; set; }
        public string IsletmeLogo { get; set; }
        public string Status { get; set; }
        public bool StandOutStatus { get; set; }
        public string IsletmeAcikKonum { get; set; }
        public string ISletmeNot { get; set; }
        public string IsletmeInstagram { get; set; }
        public DateTime MessageDate { get; set; }
        public DateTime ApprovalDateTime { get; set; }
        public DateTime RejectionDateTime { get; set; }
        public DateTime StandOutDateTime { get; set; }


        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}