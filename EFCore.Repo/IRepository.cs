using System.Collections.Generic;
using System.Threading.Tasks;
using EFCore.Domain;

namespace EFCore.Repo
{
    public interface IRepository<T> where T : BaseEntity 
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
    }
}