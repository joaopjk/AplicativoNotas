using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class AlunoRepository : IAlunoRepository
    {
        public Context _dataContext { get; }
        public AlunoRepository(Context context)
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

        public async Task<Aluno[]> GetAllAlunos()
        {
            IQueryable<Aluno> query = _dataContext.Aluno;
            return await query.ToArrayAsync();
        }

        public async Task<Aluno> GetAlunosById(int id)
        {
            IQueryable<Aluno> query = _dataContext.Aluno;
            query = query.AsNoTracking()
                .Where(p => p.Id == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Aluno[]> GetAlunosByCurso(int Id)
        {
            IQueryable<Aluno> query = _dataContext.Aluno;
            query = query.AsNoTracking()
                .Where(p => p.CursoId == Id);
            return await query.ToArrayAsync();
        }

        public async Task<Aluno[]> GetAlunosByDisiciplna(int CursoId, int DisciplinaId)
        {
            var query = from a in _dataContext.Aluno
                        join t in _dataContext.Turma on a.Id equals t.AlunoId
                        where t.DisciplinaId == DisciplinaId
                        select a;
            return await query.ToArrayAsync();
        }
    }
}
