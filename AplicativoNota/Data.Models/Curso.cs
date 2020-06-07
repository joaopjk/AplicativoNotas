using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Curso
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string DescDisciplina { get; set; }
        public List<Aluno> alunos { get; }
        public List<Disciplina> disciplinas { get; }
    }
}
