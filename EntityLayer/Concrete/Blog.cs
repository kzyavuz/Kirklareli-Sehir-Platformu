using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string SubTitle { get; set; }
        [Required]
        public string Content { get; set; }
        public string ImagePath { get; set; }

        public string CategoryName { get; set; }
        public DateTime Created { get; set; }

        [ForeignKey("AppUser")]
        public int AppUserID { get; set; }
        public AppUser AppUser { get; set; }
    }
}
