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
    public class IsletmeManager : IIsletmelerService
    {
        private readonly IIsletmelerDal _isletmelerDal;

        public IsletmeManager(IIsletmelerDal isletmelerDal)
        {
            _isletmelerDal = isletmelerDal;
        }

        public void ConvertStandOutFalse(int id)
        {
            _isletmelerDal.ConvertStandOutFalse(id);
        }

        public void ConvertStandOutTrue(int id)
        {
            _isletmelerDal.ConvertStandOutTrue(id);
        }

        public void ConvertToFalseIsletme(int id)
        {
            _isletmelerDal.ConvertToFalseIsletme(id);
        }

        public void ConvertToTrueIsletme(int id)
        {
            _isletmelerDal.ConvertToTrueIsletme(id);
        }

        public void TAdd(Isletmeler t)
        {
            _isletmelerDal.Insert(t);
        }

        public void TDelete(Isletmeler t)
        {
            _isletmelerDal.Delete(t);
        }

        public Isletmeler TGetByID(int id)
        {
            return _isletmelerDal.GetByID(id);
        }

        public List<Isletmeler> TGetList()
        {
            return _isletmelerDal.GetList();
        }

        public void TUpdate(Isletmeler t)
        {
            _isletmelerDal.Update(t);
        }
    }
}