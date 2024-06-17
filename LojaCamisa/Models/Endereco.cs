using System.ComponentModel.DataAnnotations;

namespace LojaCamisa.Models
{
    public class Endereco
    {
        public int IdEndereco { get; set; }
        public int IdUsuario { get; set; }

        [Required]
        [StringLength(50)]
        public string EnderecoCompleto { get; set; }

        [Required]
        [StringLength(30)]
        public string Complemento { get; set; }

        [Required]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "CEP inválido. Formato correto: 00000-000")]
        public string Cep { get; set; }

        [Required]
        [RegularExpression(@"^\([0-9]{2}\) [0-9]{5}-[0-9]{4}$", ErrorMessage = "Telefone inválido. Formato correto: (XX) 9XXXX-XXXX")]
        public string Telefone { get; set; }

        [Required]
        public bool Responsavel { get; set; }
    }
}