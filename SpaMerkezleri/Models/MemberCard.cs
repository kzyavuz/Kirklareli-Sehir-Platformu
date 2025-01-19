using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaMerkezleri.Models
{
    public class MemberCard
    {
        public int UyelikID { get; set; }
        public string UyelikCesidi { get; set; }
        public string UyelikTipi { get; set; }
        public string baslık { get; set; }
        public decimal AylikUyelik { get; set; }
        public decimal YillikUyelik { get; set; }
        public string Ozellikler { get; set; }
    }
}
