using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class SectorManager : ISectorService
    {
        private readonly ISektorDal _sektorDal;

        public SectorManager(ISektorDal sektorDal)
        {
            _sektorDal = sektorDal;
        }

        public void TAdd(Sektor t)
        {
            _sektorDal.Insert(t);
        }

        public void TDelete(Sektor t)
        {
            _sektorDal.Delete(t);
        }

        public Sektor TGetByID(int id)
        {
            return _sektorDal.GetByID(id);
        }

        public List<Sektor> TGetList()
        {
            return _sektorDal.GetList();
        }

        public List<string> TListSektorCategory()
        {
            return _sektorDal.ListSektorCategory();
        }

        public void TUpdate(Sektor t)
        {
            _sektorDal.Update(t);
        }
    }
}
