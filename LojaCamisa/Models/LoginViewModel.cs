using System.ComponentModel.DataAnnotations;

namespace LojaCamisa.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "O e-mail não é válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(6, ErrorMessage = "A senha deve ter 6 caracteres", MinimumLength = 6)]
        public string Senha { get; set; }
    }
}
