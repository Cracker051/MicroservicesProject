using Application.Department.Commands.UpdateDepartment;
using FluentValidation;
using MediatR;

namespace Application.Department.Commands.UpdateDepartment
{
    public class UpdateDepartmentValidator: AbstractValidator<UpdateDepartmentCommand>
    {
        UpdateDepartmentValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(25)
                .NotEmpty();

        }
    }
}
