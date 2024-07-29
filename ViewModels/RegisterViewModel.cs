using System.ComponentModel.DataAnnotations;

namespace GerenciadorTarefas.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="O Nome é obrigatório")]
        public string Name { get; set; }
        
        [Required(ErrorMessage ="O E-mail é obrigatório")]
        [EmailAddress(ErrorMessage ="O E-mail é inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatório")]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

    }
}
