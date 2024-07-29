using System.Collections.Generic;

namespace GerenciadorTarefas.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }

        public virtual List<User> Users { get; set; } = new List<User>();
    }
}