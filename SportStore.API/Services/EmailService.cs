using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using SportStore.API.Interfaces;
using SportStore.Application.Models;

namespace SportStore.Infrastructure.Services
{
    public class EmailService : IEmailService
    {


        public EmailSettings _emailSettings {get;}

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task<bool> SendEmail(Email email)
        {
            var client = new SendGridClient(_emailSettings.ApiKey);
            var subject = email.Subject;
            var emailBody = email.Body;
            var from = new EmailAddress{
                Email = _emailSettings.FromAddress,
                Name = _emailSettings.FormName
            };
            var to = new EmailAddress(email.To);

            var sendGridMessage = MailHelper.CreateSingleEmail(from,to,subject,emailBody,emailBody);
            var response =  await client.SendEmailAsync(sendGridMessage);

            if(response.StatusCode == System.Net.HttpStatusCode.Accepted ||
            response.StatusCode == System.Net.HttpStatusCode.OK){
                return true;
            }
            return false;
        }
    }
}