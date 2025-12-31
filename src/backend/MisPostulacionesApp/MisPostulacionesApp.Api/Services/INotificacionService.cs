using MisPostulacionesApp.Api.Utils;

namespace MisPostulacionesApp.Api.Services
{
    public interface INotificacionService
    {
        Result<string> EnviarMensaje(string numeroDestino,string mensaje);
    }
}
