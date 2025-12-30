namespace MisPostulacionesApp.Api.Models
{
    public class SP_REGISTRAR_POSTULACION
    {
        public SP_REGISTRAR_POSTULACION(
            Guid id,
            string titulo,
            string empresa,
            string rol,
            string descripcion,
            decimal salario,
            string tecnologia,
            string estado,
            string modalidad,
            string plataforma,
            string notas)
        {
            Id = id;
            Titulo = titulo;
            Empresa = empresa;
            Rol = rol;
            Descripcion = descripcion;
            Salario = salario;
            Tecnologia = tecnologia;
            Estado = estado;
            Modalidad = modalidad;
            Plataforma = plataforma;
            Notas = notas;
        }
        public Guid Id { get; set; }
        public string Titulo {get; set; }
        public string Empresa { get; set; }
        public string Rol { get; set; }
        public string Descripcion { get; set; }
        public decimal Salario { get; set; }
        public string Tecnologia { get; set; }
        public string Estado { get; set; }
        public string Modalidad { get; set; }
        public string Plataforma { get; set; }
        public string Notas { get; set; }
    }
    public enum Estado
    {
        Visto,
        Postulado,
        Gestion,
        Finalizado
    }
}
