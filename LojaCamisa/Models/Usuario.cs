using System.ComponentModel.DataAnnotations;

namespace LojaCamisa.Models
{
    public class Usuario
    {
        public int idUsuario { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string nome { get; set; }
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "O e-mail não é válido")]
        public string email { get; set; }
        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(6, ErrorMessage = "A senha deve ter 6 caracteres", MinimumLength = 6)]
        public string senha { get; set; }
        public int tipo { get; set; }
        public int std { get; set; }
    }
}
