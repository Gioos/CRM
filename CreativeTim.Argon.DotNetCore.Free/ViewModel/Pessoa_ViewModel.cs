using JuntoSeguros.Utils;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace JuntoSeguros.ViewModel
{
    public class Pessoa_ViewModel
    {
        public int ID { get; set; }
        public string Id_Identity_User { get; set; }//Id da tabela AspNetUser(Configuracoes de usuario do dot net)
        public string TipoPessoa { get; set; }//Id da tabela AspNetUser(Configuracoes de usuario do dot net)

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        [MaxLength(150)]
        public string Nome { get; set; }

        [Display(Name = "Usuário")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        [RegularExpression("^[a-zA-Z0-9_.-]*$", ErrorMessage = "Cóntem caracteres não permitido no nome de usuário")]
        [MaxLength(30)]
        public string Login { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(200)]
        public string Email { get; set; }

        [MaxLength(25)]
        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Cpf")]
        public string CpfCnpj { get; set; }
        public int Id_Pessoa_Cadastro { get; set; }
        public void SendRegisterEmail(string senha)
        {
            Email _emailSender = new Email
            {
                EmailFrom = "borinha52@hotmail.com",
                EmailTo = Email,
                Subject = MontarSubjectEmail(),
                Body = MontarMensagemEmail(senha)
            };
            _emailSender.Enviar();
        }
        private string MontarMensagemEmail(string senha)
        {
            StringBuilder textoMensagem = new StringBuilder("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>");
            textoMensagem.Append($"<html><body><h1><b>Crm - Bora</b></h1>");
            textoMensagem.Append($"<font color='blue'><b>Acesso usuário.</b></font>");
            textoMensagem.Append("<br>");
            textoMensagem.Append("</br>");
            textoMensagem.Append($"<font color='blue'><b>Usuário:</b></font> {Email}");
            textoMensagem.Append("<br>");
            textoMensagem.Append($"<font color='blue'><b>Senha:</b></font> {senha}");
            textoMensagem.Append("<br>");
            textoMensagem.Append("</br>");
            textoMensagem.Append($"<p>Não responder este e-mail.");
            textoMensagem.Append($"<p><a href='https://www.google.com/'>Clique Aqui para Entrar no Sistema</a></td></tr></table></html>");
            textoMensagem.Append("<br>");
            return textoMensagem.ToString();
        }
        private string MontarSubjectEmail()
        {
            string subject = "";
            subject = "CRM BORA - Novo acesso";
            return subject;
        }


        public void SendReseteSenha(string emailResete,string code)
        {
            Email _emailSender = new Email
            {
                EmailFrom = "borinha52@hotmail.com",
                EmailTo = emailResete,
                Subject = "CRM BORA - Redefinir senha",
                Body = code
            };
            _emailSender.Enviar();
        }
        public void SendConfirmarEmail(string emailConfirmar, string code)
        {
            Email _emailSender = new Email
            {
                EmailFrom = "borinha52@hotmail.com",
                EmailTo = emailConfirmar,
                Subject = "CRM BORA - Confirmar email",
                Body = code
            };
            _emailSender.Enviar();
        }
        public void SendAutenticacaoDoisFator(string emailDoisFatores, string code)
        {
            Email _emailSender = new Email
            {
                EmailFrom = "borinha52@hotmail.com",
                EmailTo = emailDoisFatores,
                Subject = "CRM BORA - Autenticação dois fatores",
                Body = code
            };
            _emailSender.Enviar();
        }
        public string GeraSenhaAleatoria()
        {
            string chars_LOWER = "abcdefghjkmnpqrstuvwxyz";
            string chars_UPER = "ABCDEFGHJKMNPQRSTUVWXYZ";
            string naoAlfanumerico = "@#*!";
            string numeros = "0123456789";
            string pass = "";
            Random random = new Random();
            for (int f = 0; f < 2; f++)
            {
                pass = pass + chars_LOWER.Substring(random.Next(0, chars_LOWER.Length - 1), 1);
            }
            for (int f = 0; f < 2; f++)
            {
                pass = pass + chars_UPER.Substring(random.Next(0, chars_UPER.Length - 1), 1);
            }
            for (int f = 0; f < 2; f++)
            {
                pass = pass + naoAlfanumerico.Substring(random.Next(0, naoAlfanumerico.Length - 1), 1);
            }
            for (int f = 0; f < 3; f++)
            {
                pass = pass + numeros.Substring(random.Next(0, numeros.Length - 1), 1);
            }
            return pass;
        }
    }
}
