using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IUyelikService : IGenericService<Uyelikler>
    {
        public IEnumerable<Uyelikler> SearchUyelikler(string query);
    }
}
