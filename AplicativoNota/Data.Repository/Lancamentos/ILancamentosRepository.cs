using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface ILancamentosRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        Task<Lancamentos> GetLancamentosById(int Id);
        Task<Lancamentos[]> GetLancamentosById(int AlunoId,int DisciplinaId);
        Task<Lancamentos[]> GetLancamentosByIdeTipo(int AlunoId, int DisciplinaId,string Tipo);
        Task<Lancamentos[]> getLancamentosByDisciplinaId(int DisciplinaId);
    }
}
