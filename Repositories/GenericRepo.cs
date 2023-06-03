using System.Linq.Expressions;
using InspectionService.Data;
using InspectionService.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InspectionService.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _entities;

        public GenericRepo(AppDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public void Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _entities.Add(entity);
        }

        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _entities.Remove(entity);
        }

        public T FindByCondition(Expression<Func<T, bool>> predicate) => _entities.FirstOrDefault(predicate);

        public IEnumerable<T> GetAll() => _entities.ToList();

        public bool SaveChanges() => _context.SaveChanges() > 0;

    }
}