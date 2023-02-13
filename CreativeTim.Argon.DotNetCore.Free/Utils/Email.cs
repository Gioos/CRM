using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace JuntoSeguros.Utils
{
    public class Email
    {
        public string Body
        {
            get;
            set;
        }

        public string EmailFrom
        {
            get;
            set;
        }

        public string EmailTo
        {
            get;
            set;
        }

        public string Subject
        {
            get;
            set;
        }
        public List<string> ListaEmails { get; set; } //Se for passada informar separada por ;
        public string ListaAnexos { get; set; } //Lista de anexos enviada nos emails ;
        public List<string> EmailsCopia { get; set; }

        public void Enviar()
        {
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(EmailFrom),
            };

            if (ListaAnexos != null)
            {
                string[] anexos = ListaAnexos.Split(';');
                if (!String.IsNullOrEmpty(anexos[0]))
                {

                    foreach (var item in anexos)
                    {
                        if (!String.IsNullOrEmpty(item))
                        {
                            try
                            {
                                mailMessage.Attachments.Add(new Attachment(item));
                            }
                            catch (Exception ex)
                            { }
                        }
                    }
                }
            }
            if (ListaEmails != null)
            {
                foreach (var item in ListaEmails)
                {
                    if (!String.IsNullOrEmpty(item))
                    {
                        if (IsValidEmail(item))
                        {
                            mailMessage.To.Add(item);
                        }
                    }
                }
            }
            else
            {
                if (IsValidEmail(EmailTo))
                {
                    mailMessage.To.Add(EmailTo);
                }
            }
            if (EmailsCopia != null)
            {
                foreach (var item in EmailsCopia)
                {
                    if (!String.IsNullOrEmpty(item))
                    {
                        if (IsValidEmail(item))
                        {
                            mailMessage.CC.Add(item);
                        }
                    }
                }
            }
            mailMessage.Subject = Subject;
            mailMessage.Body = Body;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.High;         

            SmtpClient _smtpClient = new SmtpClient("smtp.office365.com", Convert.ToInt32("587"));

            //CONFIGURAÇÃO SEM PORTA

            //Credencial para envio por SMTP Seguro(Quando o servidor exige autenticação)
            _smtpClient.UseDefaultCredentials = false;
            _smtpClient.Credentials = new NetworkCredential("borinha52@hotmail.com", "Aa@222abcdegrb36");
            _smtpClient.EnableSsl = true;
            _smtpClient.Send(mailMessage);
        }
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
