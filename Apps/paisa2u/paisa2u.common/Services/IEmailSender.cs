using Microsoft.AspNetCore.Mvc;
using paisa2u.common.Models;
namespace paisa2u.common.Services
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message);
    }
}
