using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaCamisa.Models
{
    public class FormaPagamento
    {
        public int IdFormaPagamento { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "O Nome Completo pode ter no máximo 50 caracteres.")]
        public string NomeCompleto { get; set; }

        [Required]
        [Range(1000000000000, 9999999999999999999, ErrorMessage = "O Número deve ter entre 13 e 19 dígitos.")]
        public double Numero { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "O CVV deve ter exatamente 3 caracteres.")]
        public string CVV { get; set; }

        [Required]
        [RegularExpression(@"\d{2}/\d{2}", ErrorMessage = "O Vencimento deve estar no formato MM/AA.")]
        public string Vencimento { get; set; }
        
        public int IdUsuario { get; set; }
    }
}