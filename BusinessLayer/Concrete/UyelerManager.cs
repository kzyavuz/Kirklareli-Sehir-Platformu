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
    public class UyelerManager : IUyelikService
    {
        private readonly IUyeliklerDal _uyeliklerDal;

        public UyelerManager(IUyeliklerDal uyeliklerDal)
        {
            _uyeliklerDal = uyeliklerDal;
        }

        public IEnumerable<Uyelikler> SearchUyelikler(string query)
        {
            return _uyeliklerDal.SearchUyelikler(query);
        }

        public void TAdd(Uyelikler t)
        {
            _uyeliklerDal.Insert(t);
        }

        public void TDelete(Uyelikler t)
        {
            _uyeliklerDal.Delete(t);
        }

        public Uyelikler TGetByID(int id)
        {
            return _uyeliklerDal.GetByID(id);
        }

        public List<Uyelikler> TGetList()
        {
            return _uyeliklerDal.GetList();
        }

        public void TUpdate(Uyelikler t)
        {
            _uyeliklerDal.Update(t);
        }
    }
}
