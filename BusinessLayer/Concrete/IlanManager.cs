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
    public class IlanManager : IIlanService
    {
        private readonly IIlanDal _ilanDal;

        public IlanManager(IIlanDal ilanDal)
        {
            _ilanDal = ilanDal;
        }

        public void ConvertStandOutFalse(int id)
        {
            _ilanDal.ConvertStandOutFalse(id);
        }

        public void ConvertStandOutTrue(int id)
        {
            _ilanDal.ConvertStandOutTrue(id);
        }

        public void ConvertToFalseIlan(int id)
        {
            _ilanDal.ConvertToFalseIlan(id);
        }

        public void ConvertToTrueIlan(int id)
        {
            _ilanDal.ConvertToTrueIlan(id);
        }

        public void TAdd(Ilanlar t)
        {
            _ilanDal.Insert(t);
        }

        public void TDelete(Ilanlar t)
        {
            _ilanDal.Delete(t);
        }

        public Ilanlar TGetByID(int id)
        {
            return _ilanDal.GetByID(id);
        }

        public List<Ilanlar> TGetList()
        {
            return _ilanDal.GetList();
        }

        public void TUpdate(Ilanlar t)
        {
            _ilanDal.Update(t);
        }
    }
}
