using JuntoSeguros.ViewModel;
using System.Collections.Generic;
using System.Linq;
using WebApp.Identity;
using JuntoSeguros.Models;
using System;
using Microsoft.EntityFrameworkCore;

namespace JuntoSeguros.Business
{
    public class PessoaBusiness
    {
        private readonly Context _context;
        public PessoaBusiness(Context context)
        {
            _context = context;
        }

        public List<Pessoa_ViewModel> ListaPessoas()
        {
            var TodosUSuario = _context.Tpessoa.ToList();
            var user = new Pessoa_ViewModel();
            var Lista = new List<Pessoa_ViewModel>();
            foreach (var item in TodosUSuario)
            {
                user = new Pessoa_ViewModel()
                {
                    ID = item.ID_PESSOA,
                    Nome = item?.NO_PESSOA,
                };
                Lista.Add(user);
            }
            return Lista.OrderBy(x=>x.Nome).ToList();
        }

        public Usuario_ViewModel CadastrarUsuario(Usuario_ViewModel Usuario)
        {
            using (var objTrans = _context.Database.BeginTransaction())
            {
                try
                {
                    //TPESSOA
                    var Pessoa = new Tpessoa()
                    {
                        NO_PESSOA = Usuario.Nome,
                        FK_PESSOA_CADASTRO = Usuario.Id_Pessoa_Cadastro,
                        DT_CADASTRO = DateTime.Now,
                    };
                    _context.Tpessoa.Add(Pessoa);
                    _context.SaveChanges();

                    //TUSUARIO
                    var UsarioNew = new Tusuario()
                    {
                        FK_PESSOA = Pessoa.ID_PESSOA,
                        DS_LOGIN = Usuario.Login.ToLower(),
                        DT_CADASTRO = DateTime.Now,
                        ID_IDENTITY_USER = Usuario.Id_Identity_User
                    };
                    _context.Tusuario.Add(UsarioNew);
                    _context.SaveChanges();

                    objTrans.Commit();
                }
                catch (Exception)
                {
                    objTrans.Rollback();
                }
            }
            return Usuario;
        }
    }
}
