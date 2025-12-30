namespace MisPostulacionesApp.Api.DTOs.Responses
{
    public sealed record ObtenerPostulacionResponse
    (
        Guid Id,
        string Titulo,
        string Empresa,
        string Descripcion,
        string Tecnologias,
        decimal Salario,
        string Estado,
        string Modalidad,
        string Plataforma,
        DateTime FechaPostulacion,
        string Notas,
        DateTime FechaCreacion
    );
}
