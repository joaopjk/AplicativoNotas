using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class MatriculaRepository : IMatriculaRepository
    {
        public Context _dataContext { get; }
        public MatriculaRepository(Context context)
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

        public async Task<Matricula[]> GetAllMatricula()
        {
            IQueryable<Matricula> query = _dataContext.Matricula;
            return await query.ToArrayAsync();
        }

        public async Task<Matricula> GetMatriculaById(int Id)
        {
            IQueryable<Matricula> query = _dataContext.Matricula;
            query = query.AsNoTracking()
                .Where(p => p.Id == Id);
            return await query.FirstOrDefaultAsync();
        }
    }
}
