using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class AboutMe
    {
        [Key]
        public int AboutID { get; set; }
        public string AboutName { get; set; }
        public string AboutTelefon { get; set; }
        public string TaxiLink { get; set; }
        public string instagram { get; set; }
        public string AboutImg { get; set; }
        public string AboutBlogImg { get; set; }
        public string AboutSektorImg { get; set; }
        public string AboutIlanImg { get; set; }
        public string AboutCityStartImg { get; set; }
        public string LogoImg { get; set; }
    }
}
