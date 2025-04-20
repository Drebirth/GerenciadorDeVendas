using System.ComponentModel.DataAnnotations;

namespace Gerenciador_De_Vendas.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatório.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmação da Senha")]
        [Compare("Password", ErrorMessage = "A senha e a confirmação não são iguais.")]
        public string? ConfirmPassword { get; set; }
    }
}
