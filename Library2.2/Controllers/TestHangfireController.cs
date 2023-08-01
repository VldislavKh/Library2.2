using Hangfire;
using Library2._2.Commands.TestTaskCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library2._2.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TestHangfireController : Controller
    {
        private readonly IMediator _mediator;

        public TestHangfireController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddTask([FromBody] AddTaskCommand command, CancellationToken cancellationToken)
        {
            //Fire-and-Forget
            
            RecurringJob.AddOrUpdate<AddTaskCommand>(job => job.Add(5), Cron.Minutely);
            //return await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteTask(int id, CancellationToken cancellation)
        {
           await _mediator.Send(new DeleteTaskCommand { Id = id }, cancellation);
           return NoContent();
        }
    }
}
