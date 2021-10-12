using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmtpSendEmail.Models
{
    public class UserEmailOptions
    {
        public List<string> EnviarParaEmail { get; set; }
        public string TituloEmail { get; set; }
        public string CorpoEmail { get; set; }
        public List<KeyValuePair<string, string>> PlaceHolders { get; set; }
    }
}
