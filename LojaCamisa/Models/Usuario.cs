using LojaCamisa.Repository.Utils;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LojaCamisa.Models
{
    public class Usuario
    {
        [Display(Name = "Id Usuario", Description = "Id Usuario.")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "O nome é obrigatorio.")]
        public string Nome { get; set; }

        [Display(Name = "E-Mail")]
        [EmailAddress(ErrorMessage = "O email não é valido")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email valido...")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "A senha é obrigatorio.")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 10 carcacteres")]
        public string Senha { get; set;}
        [DefaultValue(0)]
        public TipoEnum Tipo { get; set; }
        public SituacaoEnum Status {  get; set; } 
    }
}
