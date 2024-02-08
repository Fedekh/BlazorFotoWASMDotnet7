using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using BlazorFotoWASMDotnet7.Shared.Models;

namespace BlazorFotoWASMDotnet7.Server.Controllers
{
    [Route("api/mail/[action]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> SendMail([FromBody] Mail mailRequest)
        {
            try
            {
                // Verifica se i dati necessari sono stati forniti
                if (mailRequest == null || string.IsNullOrEmpty(mailRequest.To))
                {
                    return BadRequest("I dati dell'email non sono stati forniti correttamente.");
                }

                using (MailMessage newMail = new MailMessage())
                {
                    newMail.From = new MailAddress("mail mittente");
                    newMail.To.Add(mailRequest.To);
                    newMail.Subject = mailRequest.Subject;
                    newMail.Body = mailRequest.Body;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("mail mittente", "yourpassword");
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(newMail);

                        return Ok("Email inviata correttamente");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Errore durante l'invio dell'email: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }
    }

}
