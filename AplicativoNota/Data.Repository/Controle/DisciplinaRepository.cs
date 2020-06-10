using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class DisciplinaRepository : IDisciplinaRepository
    {
        public Context _dataContext { get; }
        public DisciplinaRepository(Context context)
        {
            this._dataContext = context;
            _dataContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public void Add<T>(T entity) where T : class
        {
            _dataContext.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _dataContext.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await(_dataContext.SaveChangesAsync()) > 0;
        }

        public void Update<T>(T entity) where T : class
        {
            _dataContext.Update(entity);
        }

        public async Task<Disciplina[]> GetAllDisciplina()
        {
            IQueryable<Disciplina> query = _dataContext.Disciplina;
            return await query.ToArrayAsync();
        }

        public async Task<Disciplina> GetDisciplinaById(int id)
        {
            IQueryable<Disciplina> query = _dataContext.Disciplina;
            query = query.AsNoTracking()
                .Where(p => p.Id == id);
            return await query.FirstOrDefaultAsync();
        }
    }
}
