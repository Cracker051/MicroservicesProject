using FluentValidation;

namespace Application.Drugstore.Commands.UpdateDrugstore
{
    public class UpdateDrugstoreValidator : AbstractValidator<UpdateDrugstoreCommand>
    {
        UpdateDrugstoreValidator()
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
