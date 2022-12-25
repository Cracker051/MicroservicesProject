using FluentValidation;

namespace Application.Drugstore.Commands.CreateDrugstore
{
    public class CreateDrugstoreValidatior:AbstractValidator<CreateDrugstoreCommand>
    {
        CreateDrugstoreValidatior()
        {
            RuleFor(x => x.Name)
                .MaximumLength(20)
                .NotEmpty();

            RuleFor(x => x.Address)
                .MaximumLength(30)
                .NotEmpty();

            RuleFor(x => x.DepartmentId)
                .NotNull();

        }
    }
}
