using Domain.Entities;
using FluentValidation;
using Library2._2.Commands.AuthorCommands;

namespace Library2._2.Validators
{
    public class AddAuthorCommandValidator : AbstractValidator<AddAuthorCommand>
    {
        public AddAuthorCommandValidator()
        {
            RuleFor(command => command.Name).NotNull()
                .WithMessage("Name is null!");

            RuleFor(command => command.Name).NotEmpty()
                .WithMessage("Name is empty!");

            RuleFor(command => command.Name).Must(command => command.All(Char.IsLetter))
                .WithMessage("Incorrect name!");

            RuleFor(command => command.BirthDate).NotNull().
                WithMessage("BirthDate is null!");

            RuleFor(command => command.BirthDate).LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("BirthDate must be a day from past!");

            RuleFor(command => command.DeathDate).GreaterThan(command => command.BirthDate)
                .WithMessage("DeathDate must be later than BirthDate!");
        }
    }
}
