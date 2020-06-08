using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IProfessorRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<Professor[]> GetAllProfessores();
        Task<Professor> GetProfessorById(int id);
        Task<bool> SaveChangesAsync();
    }
}
