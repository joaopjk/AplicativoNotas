using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Turma
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public int CursoId { get; set; }
        public int ProfessorId { get; set; }
        public int DisciplinaId { get; set; }
        public List<Aluno> alunos { get; }
        public List<DiscTurma> discTurmas { get; }
    }
}
