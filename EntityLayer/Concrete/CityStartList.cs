using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class CityStartList
    {
        [Key]
        public int ID { get; set; }
        public string CategoryName { get; set; }
    }
}
