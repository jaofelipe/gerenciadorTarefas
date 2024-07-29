using GerenciadorTarefas.Enums;

namespace GerenciadorTarefas.Models
{
    public class Tarefa
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataVencimento { get; set; }
        public StatusTarefasEnum StatusTarefa { get; set; }
        public string Responsavel { get; set; }
        public Guid UserId { get; set; } 
        public User User { get; set; } 
    }
}