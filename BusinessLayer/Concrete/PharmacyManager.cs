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
    public class PharmacyManager : IPharmacyService
    {
        private readonly IPharmacyDal _pharmacyDal;

        public PharmacyManager(IPharmacyDal pharmacyDal)
        {
            _pharmacyDal = pharmacyDal;
        }

        public void TAdd(Pharmacy t)
        {
            _pharmacyDal.Insert(t);
        }

        public void TDelete(Pharmacy t)
        {
            _pharmacyDal.Delete(t);
        }

        public Pharmacy TGetByID(int id)
        {
            return _pharmacyDal.GetByID(id);
        }

        public List<Pharmacy> TGetList()
        {
            return _pharmacyDal.GetList().OrderBy(x => x.PharmacyName).ToList();
        }

        public void TUpdate(Pharmacy t)
        {
            _pharmacyDal.Update(t);
        }
    }
}
