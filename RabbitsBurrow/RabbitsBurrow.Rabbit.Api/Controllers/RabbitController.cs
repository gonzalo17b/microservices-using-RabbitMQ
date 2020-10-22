using Microsoft.AspNetCore.Mvc;
using RabbitsBurrow.Infrastructure;
using RabbitsBurrow.Rabbit.Domain.Services.Interfaces;
using System.Collections.Generic;

namespace RabbitsBurrow.Rabbit.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RabbitController : ControllerBase
    {

        private readonly IRabbitService rabbitService;
        public RabbitController(IRabbitService rabbitService)
        {
            Ensure.ThatIsNotNull(rabbitService);
            this.rabbitService = rabbitService;
        }


        [HttpGet("All")]
        public ActionResult<IEnumerable<Domain.Model.Rabbit>> GetAll()
        {
            var rabbits = this.rabbitService.All();
            return Ok(rabbits);
        }
    }
}
