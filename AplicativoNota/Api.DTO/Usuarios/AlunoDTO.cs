using System;
using System.Collections.Generic;
using System.Text;

namespace Api.DTO
{
    public class AlunoRequest
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int CursoId { get; set; }
        public int MatriculaId { get; set; }
    }
}
