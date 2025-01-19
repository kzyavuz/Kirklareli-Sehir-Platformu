using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Uyelikler
    {
        [Key]
        public int UyelikID { get; set; }
        public string UyelikTipi { get; set; }
        public decimal YillikUyelik { get; set; }
        public string Ozellikler { get; set; }
    }
}
