using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class LancamentosRepository : ILancamentosRepository
    {
        public Context _dataContext { get; }
        public LancamentosRepository(Context context)
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

        public async Task<Lancamentos[]> GetLancamentosById(int AlunoId, int DisciplinaId)
        {
            IQueryable<Lancamentos> query = _dataContext.Lancamentos;
            query = query.AsNoTracking()
                .Where(p => p.AlunoId == AlunoId)
                .Where(p => p.DisciplinaId == DisciplinaId);
            return await query.ToArrayAsync();
        }

        public async Task<Lancamentos[]> GetLancamentosByIdeTipo(int AlunoId, int DisciplinaId, string Tipo)
        {
            IQueryable<Lancamentos> query = _dataContext.Lancamentos;
            query = query.AsNoTracking()
                .Where(p => p.AlunoId == AlunoId)
                .Where(p => p.DisciplinaId == DisciplinaId)
                .Where(p => p.Tipo == Tipo);
            return await query.ToArrayAsync();
        }

        public async Task<Lancamentos[]> getLancamentosByDisciplinaId(int DisciplinaId)
        {
            IQueryable<Lancamentos> query = _dataContext.Lancamentos;
            query = query.AsNoTracking()
                .Where(p => p.DisciplinaId == DisciplinaId);
            return await query.ToArrayAsync();
        }

        public async Task<Lancamentos> GetLancamentosById(int Id)
        {
            IQueryable<Lancamentos> query = _dataContext.Lancamentos;
            query = query.AsNoTracking()
                .Where(p => p.Id == Id);
            return await query.FirstOrDefaultAsync();
        }
    }
}
