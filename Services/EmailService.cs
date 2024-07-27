using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Mailjet;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;

namespace RudyHealthCare.Services
{
    public class EmailService
    {
        private readonly string? _apiKey;
        private readonly string? _apiSecret;

        public EmailService(IConfiguration configuration)
        {
            _apiKey = configuration["Mailjet:ApiKey"];
            _apiSecret = configuration["Mailjet:ApiSecret"];
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            MailjetClient client = new MailjetClient(_apiKey, _apiSecret);

            var request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
            .Property(Send.FromEmail, "billymobile19@gmail.com")
            .Property(Send.FromName, "Rudy Gunawan Health Care")
            .Property(Send.Subject, subject)
            .Property(Send.TextPart, message)
            .Property(Send.Recipients, new JArray
            {
            new JObject
            {
                { "Email", toEmail }
            }
            });

            MailjetResponse response = await client.PostAsync(request);
            
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Total: {response.GetTotal()}");
                Console.WriteLine($"Count: {response.GetCount()}");

                Console.WriteLine(response.GetData());
            }
            else
            {
                Console.WriteLine($"StatusCode: {response.StatusCode}");
                Console.WriteLine($"ErrorInfo: {response.GetErrorInfo()}");

                Console.WriteLine(response.GetData());

                Console.WriteLine($"ErrorMessage: {response.GetErrorMessage()}");
            }
        }
    }
}