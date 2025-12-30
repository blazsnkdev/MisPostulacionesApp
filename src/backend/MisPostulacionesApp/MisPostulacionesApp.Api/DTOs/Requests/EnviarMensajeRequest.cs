namespace MisPostulacionesApp.Api.DTOs.Requests
{
    public sealed record EnviarMensajeRequest
    (
        string NumeroDestino,
        Guid Id
    );
}
