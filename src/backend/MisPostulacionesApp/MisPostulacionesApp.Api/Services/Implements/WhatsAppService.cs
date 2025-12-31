using MisPostulacionesApp.Api.Utils;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace MisPostulacionesApp.Api.Services.Implements
{
    public class WhatsAppService : INotificacionService
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _fromNumber;

        public WhatsAppService(IConfiguration cfg)
        {
            _accountSid = cfg["Twilio:AccountSid"];
            _authToken = cfg["Twilio:AuthToken"];
            _fromNumber = cfg["Twilio:FromNumber"];
            TwilioClient.Init(_accountSid, _authToken);
        }

        public Result<string> EnviarMensaje(string numeroDestino, string mensaje)
        {
            try
            {
                var message = MessageResource.Create(
                from: new PhoneNumber(_fromNumber),
                to: new PhoneNumber($"whatsapp:{numeroDestino}"),
                body: mensaje
                );
                //Console.WriteLine($"Mensaje enviado {messaje.Sid}");
                Console.WriteLine($"Data:{message.Sid}, {message.Body}");
                return Result<string>.Success(message.Sid);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Result<string>.Failure(Error.Unknown());
            }
            
        }
    }
}
