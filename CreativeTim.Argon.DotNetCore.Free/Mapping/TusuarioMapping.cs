using JuntoSeguros.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuntoSeguros.Mapping
{
    public class TusuarioMapping
    {
        public TusuarioMapping(EntityTypeBuilder<Tusuario> entity)
        {
            entity.HasKey(e => e.ID_USUARIO);

            entity.ToTable("TUSUARIO");

            entity.Property(e => e.ID_USUARIO)
                .HasColumnName("ID_USUARIO");

            entity.Property(e => e.DS_LOGIN)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("DS_LOGIN");

            entity.Property(e => e.ID_IDENTITY_USER)
                 .HasMaxLength(46)
                 .HasColumnName("Id_Identity_User")
                 .IsFixedLength();

            entity.Property(e => e.DT_ATUALIZACAO)
                .HasColumnType("timestamp")
                .HasColumnName("DT_ATUALIZACAO");

            entity.Property(e => e.DT_CADASTRO)
                .HasColumnType("timestamp")
                .HasColumnName("DT_CADASTRO");

            entity.Property(e => e.DT_ULTIMO_ACESSO)
                .HasColumnType("timestamp")
                .HasColumnName("DT_ULTIMO_ACESSO");

            entity.Property(e => e.FK_PESSOA)
                .HasColumnName("FK_PESSOA");

            entity.HasOne(d => d.Tpessoa)
                .WithMany(p => p.Tusuarios)
                .HasForeignKey(d => d.FK_PESSOA)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TUSUARIO_TPESSOA");
        }
    }
}
