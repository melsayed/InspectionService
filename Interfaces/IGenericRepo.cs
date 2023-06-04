using System.Linq.Expressions;

namespace InspectionService.Interfaces
{
    public interface IGenericRepo<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
        T FindByCondition(Expression<Func<T,bool>> predicate);
        bool CheckIfExist(Expression<Func<T,bool>> predicate);
        bool SaveChanges();

    }
}