using SmtpSendEmail.Models;
using System.Threading.Tasks;

namespace SmtpSendEmail.Service
{
    public interface IEmailService
    {
        Task SenTestEmail(UserEmailOptions userEmailOptions);
    }
}