
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpaMerkezleri.Models
{
    public class IsletmeForm
    {
        public int IsletmeID { get; set; }

        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        public string IsletmeUyeAdi { get; set; }

        [Required(ErrorMessage = "Soy ad alanı zorunludur.")]
        public string IsletmeUyeSoyAdi { get; set; }

        [Required(ErrorMessage = "E-posta alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string IsletmeUyeMail { get; set; }

        [Required(ErrorMessage = "Telefon alanı zorunludur.")]
        public string IsletmeTelefonNumarasi { get; set; }

        [Required(ErrorMessage = "İşletme ad alanı zorunludur.")]
        public string ISletmeAdi { get; set; }
        public string IsletmeTipi { get; set; }
        public string IsletmeLinki { get; set; }

        public IFormFile IsletmeResimi { get; set; }
        public IFormFile IsletmeResimi1 { get; set; }
        public IFormFile IsletmeResimi2 { get; set; }
        public IFormFile IsletmeResimi3 { get; set; }
        public IFormFile IsletmeResimi4 { get; set; }
        public IFormFile IsletmeResimi5 { get; set; }
        public IFormFile IsletmeResimi6 { get; set; }

        public IFormFile IsletmeLogo { get; set; }

        public string IsletmeAcikKonum { get; set; }
        public string ISletmeNot { get; set; }

        public string ResimYolu { get; set; }
        public string LogoYolu { get; set; }

        public DateTime MessageDate { get; set; }
        public string IsletmeInstagram { get; set; }

        public int UyelikID { get; set; }
        public string UyelikCesidi { get; set; }
        public string UyelikTipi { get; set; }
        public string baslık { get; set; }
        public decimal YillikUyelik { get; set; }
        public string Ozellikler { get; set; }
    }
}
