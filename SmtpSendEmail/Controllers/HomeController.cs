using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmtpSendEmail.Models;
using SmtpSendEmail.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SmtpSendEmail.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailService _emailService;

        public HomeController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task<ViewResult> Index()
        {
            UserEmailOptions options = new UserEmailOptions
            {
                EnviarParaEmail = new List<string>() { "baatata_@hotmail.com" },
                PlaceHolders = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("{{UserName}}", "Cliente")
                }
            };

            await _emailService.SenTestEmail(options);
            return View();
        }  
    }
}
