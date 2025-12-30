namespace MisPostulacionesApp.Api.Services
{
    public interface INotificacionService
    {
        void EnviarMensaje(string numeroDestino,string mensaje);
    }
}
