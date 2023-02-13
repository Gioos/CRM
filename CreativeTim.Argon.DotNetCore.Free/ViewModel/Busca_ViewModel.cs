using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JuntoSeguros.ViewModel
{
    public class Busca_ViewModel
    {
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Nome { get; set; }
    }
    public class Pessoas
    {
        public int IdPessoa { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string DsTipo { get; set; }
        public string Cpf_Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string Telefone { get; set; }
        public string Protocolo { get; set; }
        public string Codigo { get; set; }
    }
    public class BuscaRapida
    {
        public string ValorBusca { get; set; }
    }
}
