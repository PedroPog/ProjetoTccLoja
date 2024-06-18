using System.ComponentModel.DataAnnotations;

namespace LojaCamisa.Models
{
    public class FormaPagamento
    {
        public int IdFormaPagamento { get; set; }

        [Required(ErrorMessage = "O Nome Completo é obrigatório.")]
        [StringLength(50, ErrorMessage = "O Nome Completo pode ter no máximo 50 caracteres.")]
        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "O Número do Cartão é obrigatório.")]
        [CreditCard(ErrorMessage = "Número de cartão de crédito inválido.")]
        [Display(Name = "Número do Cartão")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "O CVV é obrigatório.")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "O CVV deve ter exatamente 3 caracteres.")]
        public string CVV { get; set; }

        [Required(ErrorMessage = "A Data de Vencimento é obrigatória.")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/[0-9]{2}$", ErrorMessage = "O Vencimento deve estar no formato MM/YY.")]
        public string Vencimento { get; set; }

        [Required(ErrorMessage = "O Id do Usuário é obrigatório.")]
        public int IdUsuario { get; set; }
    }
}