using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IGenericDal<T>
    {
        void Update(T t);
        void Insert(T t);
        void Delete(T t);
        List<T> GetList();
        T GetByID(int id);
        //List<T> GetListFiltre(Expression<Func<T, bool>> filter);
    }
}
