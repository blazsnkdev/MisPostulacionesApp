namespace MisPostulacionesApp.Api.Models
{
    public class SP_OBTENER_POSTULACION_POR_ID
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Empresa { get; set; }
        public string Rol { get; set; }
        public string Descripcion { get; set; }
        public string Tecnologias { get; set; }
        public decimal Salario { get; set; }
        public string Estado { get; set; }
        public string Modalidad { get; set; }
        public string Plataforma { get; set; }
        public DateTime FechaPostulacion { get; set; }
        public string? Notas { get; set; }
        public DateTime FechaCreacion { get; set; }

    }
}
