using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpaMerkezleri.Models
{
    public class FormValidator
    {
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

        public string IsletmeLinki { get; set; }

        public string IsletmeResimi { get; set; }

        public string IsletmeAcikKonum { get; set; }

        public string ISletmeTuru { get; set; }

        public string ISletmeNot { get; set; }
    }
}
