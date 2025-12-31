namespace MisPostulacionesApp.Api.DTOs.Responses
{
    public sealed record RegistrarPostulacionResponse
    (
        string Titulo,
        string Empresa,
        string Rol,
        string Descripcion,
        string Tecnologias,
        decimal Salario,
        string Modalidad,
        string Plataforma,
        string Notas
    );
}
