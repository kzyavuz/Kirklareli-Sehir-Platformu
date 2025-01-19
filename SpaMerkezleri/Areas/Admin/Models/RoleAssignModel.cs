using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaMerkezleri.Areas.Admin.Models
{
    public class RoleAssignModel
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public bool RoleExist { get; set; }
    }
}
