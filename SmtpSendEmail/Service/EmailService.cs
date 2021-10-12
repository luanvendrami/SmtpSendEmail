using Microsoft.Extensions.Options;
using SmtpSendEmail.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SmtpSendEmail.Service
{
    public class EmailService : IEmailService
    {
        private readonly string templatePath = @"TemplateEmail/{0}.html";
        private readonly SmtpConfigModel _smtpConfig;


        public async Task SenTestEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.TituloEmail = UpdatePlaceHolders("Olá {{UserName}}, essa mensagem é para você!", userEmailOptions.PlaceHolders);
            userEmailOptions.CorpoEmail = UpdatePlaceHolders(GetEmailBody("EmailTemplate"), userEmailOptions.PlaceHolders) ;

            await EnviarEmail(userEmailOptions);
        }
        public EmailService(IOptions<SmtpConfigModel> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
        }
        private async Task EnviarEmail(UserEmailOptions userEmailOptions)
        {
            MailMessage mail = new MailMessage
            {
                Subject = userEmailOptions.TituloEmail,
                Body = userEmailOptions.CorpoEmail,
                From = new MailAddress(_smtpConfig.EmailDoEnvio, _smtpConfig.NomeEmailEnvio),
                IsBodyHtml = _smtpConfig.IsBodyHTML
            };


            foreach (var paraEmail in userEmailOptions.EnviarParaEmail)
            {
                mail.To.Add(paraEmail);
            }

            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);

            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential
            };

            mail.BodyEncoding = Encoding.Default;

            await smtpClient.SendMailAsync(mail);
        }
        private string GetEmailBody(string TemplateName)
        {
            var body = File.ReadAllText(string.Format(templatePath, TemplateName));
            return body;
        }

        private string UpdatePlaceHolders(string text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if(!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {
                foreach (var placeHolder in keyValuePairs)
                {
                    if (text.Contains(placeHolder.Key))
                    {
                        text = text.Replace(placeHolder.Key, placeHolder.Value);
                    };
                }
            }
            return text;
        }
    }
}
