using Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using Library2._2.Interfaces.AuthorInterfaces;
using Library2._2.Validators;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Library2._2.Commands.AuthorCommands
{
    public class AddAuthorCommand : IRequest<int>
    {
        //Параметры автора
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public DateOnly? DeathDate { get; set; }

        public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, int>
        {
            private readonly IAddDeleteAuthor _addAuthor;
            private readonly AddAuthorCommandValidator _validator;

            public AddAuthorCommandHandler(IAddDeleteAuthor addAuthor)
            {
                _addAuthor = addAuthor;
                _validator = new AddAuthorCommandValidator();
            }

            public async Task<int> Handle(AddAuthorCommand command, CancellationToken cancellationToken)
            {
                await _validator.ValidateAndThrowAsync(command);

                return _addAuthor.Add(command.Name, command.BirthDate, command.DeathDate);
            }
        }
    }
}
