using FluentValidation;
using MisPostulacionesApp.Api.DTOs.Requests;

namespace MisPostulacionesApp.Api.Validations
{
    public class RegistrarPostulacionRequestValidation
        : AbstractValidator<RegistrarPostulacionRequest>
    {
        public RegistrarPostulacionRequestValidation()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("El título es obligatorio")
                .MaximumLength(150);

            RuleFor(x => x.Empresa)
                .NotEmpty().WithMessage("La empresa es obligatoria")
                .MaximumLength(150);

            RuleFor(x => x.Rol)
                .NotEmpty().WithMessage("El rol es obligatorio")
                .MaximumLength(150);

            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("La descripción es obligatoria")
                .MaximumLength(300);

            RuleFor(x => x.Tecnologias)
                .NotEmpty().WithMessage("Las tecnologías son obligatorias")
                .MaximumLength(300);

            RuleFor(x => x.Salario)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El salario no puede ser negativo");

            RuleFor(x => x.Modalidad)
                .NotEmpty().WithMessage("La modalidad es obligatoria")
                .MaximumLength(50);

            RuleFor(x => x.Plataforma)
                .NotEmpty().WithMessage("La plataforma es obligatoria")
                .MaximumLength(100);

            RuleFor(x => x.Notas)
                .MaximumLength(500);
        }
    }
}
