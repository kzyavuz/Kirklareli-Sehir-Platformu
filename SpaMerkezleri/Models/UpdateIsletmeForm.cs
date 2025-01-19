using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpaMerkezleri.Models
{
    public class UpdateIsletmeForm
    {
        public int IsletmeID { get; set; }
        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        public string IsletmeUyeAdi { get; set; }

        [Required(ErrorMessage = "Soy ad alanı zorunludur.")]
        public string IsletmeUyeSoyAdi { get; set; }

        public string IsletmeTipi { get; set; }

        [Required(ErrorMessage = "E-posta alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string IsletmeUyeMail { get; set; }

        [Required(ErrorMessage = "Telefon alanı zorunludur.")]
        public string IsletmeTelefonNumarasi { get; set; }

        [Required(ErrorMessage = "İşletme ad alanı zorunludur.")]
        public string ISletmeAdi { get; set; }
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
        public DateTime MessageDate { get; set; }

        public string LogoYolu { get; set; }
        public string IsletmeInstagram { get; set; }
        public string ExistingImagePath1 { get; set; }
        public string ExistingImagePath2 { get; set; }
        public string ExistingImagePath3 { get; set; }
        public string ExistingImagePath4 { get; set; }
        public string ExistingImagePath5 { get; set; }
        public string ExistingImagePath6 { get; set; }
        public string ExistingImagePath { get; set; }
    }
}
