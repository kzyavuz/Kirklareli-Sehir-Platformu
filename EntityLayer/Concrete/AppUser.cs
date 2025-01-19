using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class AppUser: IdentityUser<int>
    {
        public string name { get; set; }

        public string UyeTipi { get; set; }

        public string Status { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime CreateDateTime { get; set; }

        public ICollection<Isletmeler> Isletmelers { get; set; }

        public ICollection<Blog> Blogs { get; set; }

        public ICollection<Ilanlar> Ilanlars { get; set; }

    }
}
