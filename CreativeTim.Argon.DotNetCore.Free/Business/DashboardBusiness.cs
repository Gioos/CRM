using JuntoSeguros.Utils;
using JuntoSeguros.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebApp.Identity;

namespace JuntoSeguros.Business
{

    public class DashboardBusiness
    {
        private readonly Context _context;
        public DashboardBusiness(Context context)
        {
            _context = context;
        }
        public List<Pessoas> ExemploConsultaProcedure(int IdPessoa,string Nome,int? Classifificacao)
        {
            #region CONSULTA 
            var Lista = new List<Pessoas>();
            using (SqlConnection connection = new SqlConnection(StringCon.ConString()))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SP_BUSCA @DS_BUSCA,@FK_TIPO_CLASSIFICACAO_EMPRESA,@ID_PESSOA";
                command.Parameters.Add(new SqlParameter { ParameterName = "@DS_BUSCA", Value = Nome });
                command.Parameters.Add(new SqlParameter { ParameterName = "@FK_TIPO_CLASSIFICACAO_EMPRESA", Value =  Classifificacao==null?DBNull.Value:Classifificacao});
                command.Parameters.Add(new SqlParameter { ParameterName = "@ID_PESSOA", Value = IdPessoa });

                command.CommandTimeout = 1000000;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Pessoas obj = new Pessoas()
                        {
                            IdPessoa = Convert.ToInt32(reader["ID_PESSOA"].ToString()),
                            Codigo = reader["ID_PESSOA"].ToString(),
                            Tipo = reader["TP_PESSOA"].ToString()== "F" ? "FÍSICA" : "JURÍDICA",
                            Cpf_Cnpj = reader["DC_CPFCNPJ"].ToString(),
                            Nome = reader["NO_PESSOA"].ToString(),
                            RazaoSocial = reader["DS_RAZAO_SOCIAL"].ToString(),
                            Telefone = reader["NM_DDD"].ToString() +" "+ reader["NM_FONE"].ToString(),
                            Protocolo = reader["ID_PESSOA_PROTOCOLO"].ToString(),
                          
                        };
                        Lista.Add(obj);
                    }
                }
            }
            #endregion
            return Lista;
        }
    }
}
