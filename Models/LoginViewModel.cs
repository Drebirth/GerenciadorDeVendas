using System.ComponentModel.DataAnnotations;

namespace Gerenciador_De_Vendas.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A Senha é obrigatória")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Lembrar-me")]
        public bool RememberMe { get; set; }
    }
}
