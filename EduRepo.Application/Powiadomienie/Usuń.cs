using System;
using System.Threading;
using System.Threading.Tasks;
using EduRepo.Infrastructure;
using MediatR;

namespace EduRepo.Application.Powiadomienia
{
    public class DeleteCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }

    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, Unit>
    {
        private readonly DataContext _context;

        public DeleteCommandHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var powiadomienia = await _context.Powiadomienia.FindAsync(request.Id);

            if (powiadomienia == null)
            {
                throw new KeyNotFoundException($"Kurs with ID {request.Id} not found.");
            }

            _context.Powiadomienia.Remove(powiadomienia);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
