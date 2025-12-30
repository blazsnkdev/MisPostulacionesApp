namespace MisPostulacionesApp.Api.DTOs.Requests
{
    public sealed record RegistrarPostulacionRequest
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
