using System.ComponentModel;

namespace GerenciadorTarefas.Enums
{
    public enum StatusTarefasEnum
    {
        [Description("Pendente")]
        Pendente,
        [Description("Em Progresso")]
        EmProgresso,
        [Description("Concluído")]
        Concluido
    }
}
