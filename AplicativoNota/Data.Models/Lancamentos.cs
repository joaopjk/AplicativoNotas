using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Lancamentos
    {
        public int Id { get; set; }
        public string DescLancamento { get; set; }
        public string Nota { get; set; }
        public string NotaTotal { get; set; }
        public int AlunoId { get; set; }
        public int DisciplinaId { get; set; }
        public string Tipo { get; set; }
        public DateTime DataLancamento { get; set; }
    }
}
