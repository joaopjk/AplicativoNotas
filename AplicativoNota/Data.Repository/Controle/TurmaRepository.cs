using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class TurmaRepository : ITurmaRepository
    {
        public Context _dataContext { get; }
        public TurmaRepository(Context context)
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

        public async Task<Turma[]> GetAllTurmas()
        {
            IQueryable<Turma> query = _dataContext.Turma;
            return await query.ToArrayAsync();
        }

        public async Task<Turma[]> GetTurmaById(int id)
        {
            IQueryable<Turma> query = _dataContext.Turma;
            query = query.AsNoTracking()
                .Where(p => p.DisciplinaId == id)
                .GroupBy( p => new { p.CursoId,p.DisciplinaId })
                .Select(g => g.FirstOrDefault());
            return await query.ToArrayAsync();
        }
    }
}
