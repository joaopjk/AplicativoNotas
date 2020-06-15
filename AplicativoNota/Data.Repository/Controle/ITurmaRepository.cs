using Data.Models;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface ITurmaRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        Task<Turma[]> GetAllTurmas();
        Task<Turma[]> GetTurmaById(int id);
    }
}
