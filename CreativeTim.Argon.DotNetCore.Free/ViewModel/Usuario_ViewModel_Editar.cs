using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JuntoSeguros.ViewModel
{
    public class Usuario_ViewModel_Editar
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public int Tipo_Usuario { get; set; }
        public int? Fila_Usuario { get; set; }
        public string[] Filas_Usuario { get; set; }
        public List<SelectListItem> Lista_FilaUsuario { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(200)]
        public string Email { get; set; }
    }
}
