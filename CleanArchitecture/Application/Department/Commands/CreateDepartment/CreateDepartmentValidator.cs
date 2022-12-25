using Application.Department.Commands.UpdateDepartment;
using FluentValidation;
using MediatR;

namespace Application.Department.Commands.CreateDepartment
{
    public class CreateDepartmentValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        CreateDepartmentValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(25)
                .NotEmpty();

        }
    }
}