using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IIsletmelerService : IGenericService<Isletmeler>
    {
        public void ConvertToTrueIsletme(int id);
        public void ConvertToFalseIsletme(int id);

        public void ConvertStandOutTrue(int id);
        public void ConvertStandOutFalse(int id);
    }
}
