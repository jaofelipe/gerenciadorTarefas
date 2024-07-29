using GerenciadorTarefas.Enums;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorTarefas.ViewModels
{
    public class TarefaViewModel
    {
        [Required(ErrorMessage = "O campo Título é obrigatório")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Esse campo deve conter entre 3 e 40 caracteres")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "O campo descrição é obrigatório")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo Data de Vencimento é obrigatório")]
        public DateTime DataVencimento { get; set; }

        [Required(ErrorMessage = "O campo Status da tarefa é obrigatório")]
        public StatusTarefasEnum StatusTarefa { get; set; }

        [Required(ErrorMessage = "O campo Responsável é obrigatório")]
        public string Responsavel { get; set; }
    }
}
