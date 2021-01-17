using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using EFCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Repo
{
    public class Repository : IRepository
    {
        readonly HeroiContext _context;
        public Repository(HeroiContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<Batalha>> GetAllBatalhas()
        {
            IQueryable<Batalha> query = _context.Batalhas
                .AsNoTracking()
                .OrderBy(x=> x.Id);

            return await query.ToArrayAsync();
        }

        public async Task<IEnumerable<Heroi>> GetAllHerois()
        {
            IQueryable<Heroi> query = _context.Herois
                .Include(x => x.Identidade)
                .Include(x => x.Armas)
                .AsNoTracking()
                .OrderBy(x=> x.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Batalha> GetBatalhaById(int id)
        {
            IQueryable<Batalha> query = _context.Batalhas
                .AsNoTracking()
                .OrderBy(x=> x.Id);

            return await query
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Heroi> GetHeroiById(int id)
        {
            IQueryable<Heroi> query = _context.Herois
                .Include(x => x.Identidade)
                .Include(x => x.Armas)
                .AsNoTracking()
                .OrderBy(x=> x.Id);

            return await query
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Heroi>> GetHeroisByName(string nome)
        {
            IQueryable<Heroi> query = _context.Herois
                .Include(x => x.Identidade)
                .Include(x => x.Armas)
                .AsNoTracking()
                .OrderBy(x=> x.Id);

            return await query
                .Where(h => h.Nome.Contains(nome))
                .ToListAsync();
        }

        public async Task<bool> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
    }
}