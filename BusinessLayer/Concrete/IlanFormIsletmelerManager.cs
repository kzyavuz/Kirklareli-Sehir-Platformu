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
    public class IlanFormIsletmelerManager : IIlanFormIsletmelerService
    {
        private readonly IIlanFormIsletmelerDal _ilanFormIsletmelerDal;

        public IlanFormIsletmelerManager(IIlanFormIsletmelerDal ilanFormIsletmelerDal)
        {
            _ilanFormIsletmelerDal = ilanFormIsletmelerDal;
        }

        public void TAdd(IlanFormIsletmeler t)
        {
            _ilanFormIsletmelerDal.Insert(t);
        }

        public void TDelete(IlanFormIsletmeler t)
        {
            _ilanFormIsletmelerDal.Delete(t);
        }

        public IlanFormIsletmeler TGetByID(int id)
        {
            return _ilanFormIsletmelerDal.GetByID(id);
        }

        public List<IlanFormIsletmeler> TGetList()
        {
            return _ilanFormIsletmelerDal.GetList();
        }

        public void TUpdate(IlanFormIsletmeler t)
        {
            _ilanFormIsletmelerDal.Update(t);
        }
    }
}
