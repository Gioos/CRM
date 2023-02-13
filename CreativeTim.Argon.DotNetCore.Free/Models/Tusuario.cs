using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JuntoSeguros.Models
{
    [Table("Tusuario")]
    public partial class Tusuario
    {
        public Tusuario()
        {
        }

        [Key]
        public int ID_USUARIO { get; set; }
        public int FK_PESSOA { get; set; }
        public string ID_IDENTITY_USER { get; set; }
        public string DS_LOGIN { get; set; }
        public DateTime DT_CADASTRO { get; set; }
        public DateTime? DT_ATUALIZACAO { get; set; }
        public DateTime? DT_ULTIMO_ACESSO { get; set; }
        public byte FL_ATIVO { get; set; }
        public virtual Tpessoa Tpessoa { get; set; }

    }
}
