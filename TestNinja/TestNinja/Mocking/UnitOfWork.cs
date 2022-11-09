using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking
{
    //public interface IUnitOfWork
    //{
    //    IEnumerable<T> Query<T>();
    //}

    public /*abstract*/ class UnitOfWork/* : IUnitOfWork*/
    {
        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();
        }
    }
}
