using Library2._2.Interfaces.TaskInterfaces;
using MediatR;

namespace Library2._2.Commands.TestTaskCommands
{
    public class DeleteTaskCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
        {
            private readonly ITaskService _taskService;

            public DeleteTaskCommandHandler(ITaskService taskService)
            {
                _taskService = taskService;
            }

            public async Task<Unit> Handle(DeleteTaskCommand command, CancellationToken cancellationToken)
            {
                _taskService.Delete(command.Id);
                return Unit.Value;  
            }
        }
    }
}
