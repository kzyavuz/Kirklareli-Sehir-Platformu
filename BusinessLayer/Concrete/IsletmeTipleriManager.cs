using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class IsletmeTipleriManager : IIsletmeTipleriService
    {
        private readonly IIsletmeTipleriDal _isletmeTipleriDal;

        public IsletmeTipleriManager(IIsletmeTipleriDal isletmeTipleriDal)
        {
            _isletmeTipleriDal = isletmeTipleriDal;
        }

        public void TAdd(IsletmeTipleri t)
        {
            _isletmeTipleriDal.Insert(t);
        }

        public void TDelete(IsletmeTipleri t)
        {
            _isletmeTipleriDal.Delete(t);
        }

        public IsletmeTipleri TGetByID(int id)
        {
            return _isletmeTipleriDal.GetByID(id);
        }

        public List<IsletmeTipleri> TGetList()
        {
            return _isletmeTipleriDal.GetList();
        }

        public void TUpdate(IsletmeTipleri t)
        {
            _isletmeTipleriDal.Update(t);
        }
    }
}
