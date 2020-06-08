using Data.Models;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IAlunoRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<Aluno[]> GetAllAlunos();
        Task<Aluno> GetAlunosById(int id);
        Task<Aluno[]> GetAlunosByCurso(int Id);
        Task<bool> SaveChangesAsync();
    }
}
