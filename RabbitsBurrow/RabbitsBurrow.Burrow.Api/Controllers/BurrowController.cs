using MediatR;
using Microsoft.AspNetCore.Mvc;
using RabbitsBurrow.Burrow.Application.Burrow.AddMember;
using RabbitsBurrow.Burrow.Application.Model.Add;
using RabbitsBurrow.Burrow.Domain.Services.Interfaces;
using RabbitsBurrow.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RabbitsBurrow.Burrow.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BurrowController : ControllerBase
    {
        private readonly IBurrowService burrowService;
        private readonly IMediator mediator;
        public BurrowController(IBurrowService burrowService, IMediator mediator)
        {
            Ensure.ThatIsNotNull(burrowService);
            this.burrowService = burrowService;

            Ensure.ThatIsNotNull(mediator);
            this.mediator = mediator;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Domain.Model.Burrow>> GetBurrow()
        {
            var burrows = burrowService.All();
            return Ok(burrows);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddNewRabbit rabbit)
        {
            var command = new AddMemberCommand(rabbit.Xcoordinate, rabbit.Ycoordinate);
            await this.mediator.Send(command);
            return Created(nameof(GetBurrow), rabbit);
        }
    }
}
