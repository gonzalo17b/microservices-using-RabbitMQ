using RabbitsBurrow.Domain.Bus;
using RabbitsBurrow.Infrastructure;
using RabbitsBurrow.Rabbit.Domain.Services.Interfaces;
using System.Threading.Tasks;

namespace RabbitsBurrow.Rabbit.Application.Rabbit.AddRabbit.Events
{
    public class AddRabbitEventHandler : IEventHandler<AddRabbitEvent>
    {
        private readonly IRabbitService rabbitService;
        public AddRabbitEventHandler(IRabbitService rabbitService)
        {
            Ensure.ThatIsNotNull(rabbitService);
            this.rabbitService = rabbitService;
        }

        public Task Handle(AddRabbitEvent @event)
        {
            var rabbit = Domain.Model.Rabbit.New();
            rabbit.SetXcoordinate(@event.Xcoordinate);
            rabbit.SetYcoordinate((@event.Ycoordinate));

            this.rabbitService.Add(rabbit);

            return Task.CompletedTask;
        }
    }
}
