namespace GerenciadorTarefas.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Image { get; set; }
        public string Slug { get; set; }
        public string Bio { get; set; }

        public virtual ICollection<Tarefa> Tarefas { get; set; }

        public virtual List<Role> Roles { get; set; } = new List<Role>();
    }
}