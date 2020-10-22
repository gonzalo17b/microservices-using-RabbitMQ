using MediatR;
using RabbitsBurrow.Burrow.Application.Burrow.AddMember.Events;
using RabbitsBurrow.Burrow.Domain.Services.Interfaces;
using RabbitsBurrow.Domain.Bus;
using RabbitsBurrow.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitsBurrow.Burrow.Application.Burrow.AddMember
{
    public class AddMemberCommandHandler : IRequestHandler<AddMemberCommand, bool>
    {
        private readonly IEventBus bus;
        private readonly IBurrowService burrowService;

        public AddMemberCommandHandler(IEventBus bus, IBurrowService burrowService)
        {
            Ensure.ThatIsNotNull(bus);
            this.bus = bus;

            Ensure.ThatIsNotNull(burrowService);
            this.burrowService = burrowService;
        }

        public Task<bool> Handle(AddMemberCommand request, CancellationToken cancellationToken)
        {
            var burrow = this.burrowService.First();
            Ensure.That<ArgumentException>(
                burrow.MaximumX > request.Xcoordinate && burrow.MaximumY > request.Ycoordinate,
                "Size exceded");

            burrowService.AddMember(burrow.Id);

            this.bus.Publish(new AddRabbitEvent(burrow.Id, request.Xcoordinate, request.Ycoordinate));
            return Task.FromResult(true);
        }
    }
}
