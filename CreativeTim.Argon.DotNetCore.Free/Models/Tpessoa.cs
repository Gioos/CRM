using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JuntoSeguros.Models
{
    [Table("Tpessoa")]
    public partial class Tpessoa
    {
        public Tpessoa()
        {
            Tusuarios = new HashSet<Tusuario>();
        }
        [Key]
        public int ID_PESSOA { get; set; }
        public string NO_PESSOA { get; set; }
        public string TP_PESSOA { get; set; }
        public int? FK_PESSOA_CADASTRO { get; set; }
        public int? FK_PESSOA_ATUALIZACAO { get; set; }
        public DateTime DT_CADASTRO { get; set; }
        public DateTime? DT_ATUALIZACAO { get; set; }
        public byte FL_ATIVO { get; set; }
        public virtual ICollection<Tusuario> Tusuarios { get; set; }
    }
}
