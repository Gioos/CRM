using JuntoSeguros.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuntoSeguros.Mapping
{
    public class TpessoaMapping
    {
        public TpessoaMapping(EntityTypeBuilder<Tpessoa> entity)
        {
            entity.HasKey(e => e.ID_PESSOA);

            entity.ToTable("TPESSOA");

            entity.Property(e => e.ID_PESSOA)
                .HasColumnName("ID_PESSOA");

            entity.Property(e => e.DT_ATUALIZACAO)
                .HasColumnType("timestamp")
                .HasColumnName("DT_ATUALIZACAO");

            entity.Property(e => e.DT_CADASTRO)
                .HasColumnType("timestamp")
                .HasColumnName("DT_CADASTRO");

            entity.Property(e => e.FK_PESSOA_ATUALIZACAO)
                .HasColumnName("FK_PESSOA_ATUALIZACAO");

            entity.Property(e => e.FK_PESSOA_CADASTRO)
                .HasColumnName("FK_PESSOA_CADASTRO");

            entity.Property(e => e.FL_ATIVO)
                .HasColumnName("FL_ATIVO")
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.NO_PESSOA)
                .HasMaxLength(255)
                .HasColumnName("NO_PESSOA");
           
            entity.Property(e => e.TP_PESSOA)
                .HasMaxLength(1)
                .HasColumnName("TP_PESSOA")
                .IsFixedLength();
        }
    }
}
