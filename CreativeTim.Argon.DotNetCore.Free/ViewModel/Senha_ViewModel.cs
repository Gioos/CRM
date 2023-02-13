using System.ComponentModel.DataAnnotations;

namespace JuntoSeguros.ViewModel
{
    public class Senha_ViewModel
    {
        [Required]
        [Display(Name = "Senha atual")]
        [DataType(DataType.Password)]
        public string SenhaAtual { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} e no maximo {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        public string NovaSenha { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("NovaSenha", ErrorMessage = "A nova senha e a senha de confirmação não coincidem!")]
        public string ConfirmarSenha { get; set; }

    }
}
