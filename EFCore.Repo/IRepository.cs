using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EFCore.Domain;

namespace EFCore.Repo
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangeAsync();

        Task<IEnumerable<Heroi>> GetAllHerois();
        Task<Heroi> GetHeroiById(int id);
        Task<IEnumerable<Heroi>> GetHeroisByName(string nome);
               
        Task<IEnumerable<Batalha>> GetAllBatalhas();
        Task<Batalha> GetBatalhaById(int id);
    }
}