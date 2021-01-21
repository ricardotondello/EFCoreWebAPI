using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EFCore.Domain;

namespace EFCore.Repo
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly HeroiContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(HeroiContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToArrayAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.SingleOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }     

    }
}