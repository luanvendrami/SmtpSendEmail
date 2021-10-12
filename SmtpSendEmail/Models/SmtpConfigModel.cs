using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmtpSendEmail.Models
{
    public class SmtpConfigModel
    {
        public string EmailDoEnvio { get; set; }
        public string NomeEmailEnvio { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public bool IsBodyHTML { get; set; }
    }
}
