using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Exceptions;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace RudyHealthCare.Services
{
    public class TwilioService
    {
        private readonly string? _accountSid;
        private readonly string? _authToken;
        private readonly string? _whatsAppFrom;

        public TwilioService(IConfiguration configuration)
        {
            _accountSid = configuration["Twilio:AccountSid"];
            _authToken = configuration["Twilio:AuthToken"];
            _whatsAppFrom = configuration["Twilio:WhatsAppFrom"];

            TwilioClient.Init(_accountSid, _authToken);
        }

        public async Task SendWhatsAppMessage(string to, string message)
        {
            try
            {
                var messageOptions = new CreateMessageOptions(new PhoneNumber("whatsapp:" + to))
                {
                    From = new PhoneNumber(_whatsAppFrom),
                    Body = message
                };

                var msg = await MessageResource.CreateAsync(messageOptions);
            }
            catch (ApiException ex)
            {
                throw new Exception("Failed to send WhatsApp message", ex);
            }
        }
    }
}