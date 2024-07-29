using GerenciadorTarefas.Enums;
using GerenciadorTarefas.Extensions;

namespace GerenciadorTarefas.ViewModels
{
    public class RetornoTarefaViewModel
    {
        public Guid Id { get; set; } 
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        public DateTime DataVencimento { get; set; }

        public StatusTarefasEnum StatusTarefa { get; set; }

        public string DescricaoStatusTarefa => StatusTarefa.GetDescription();

        public string Responsavel { get; set; }
        public Guid UserId { get; set; }
    }
}
