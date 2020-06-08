using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class CursoRepository : ICursoRepository
    {
        public Context _dataContext { get; }
        public CursoRepository(Context context)
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
            return await (_dataContext.SaveChangesAsync()) > 0;
        }

        public void Update<T>(T entity) where T : class
        {
            _dataContext.Update(entity);
        }

        public async Task<Curso[]> GetAllCurso()
        {
            IQueryable<Curso> query = _dataContext.Curso;
            return await query.ToArrayAsync();
        }

        public async Task<Curso> GetCursoById(int id)
        {
            IQueryable<Curso> query = _dataContext.Curso;
            query = query.AsNoTracking()
                .Where(p => p.Id == id);
            return await query.FirstOrDefaultAsync();
        }
    }
}
