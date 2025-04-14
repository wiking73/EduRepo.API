using System;
using System.Threading;
using System.Threading.Tasks;
using EduRepo.Infrastructure;
using MediatR;

namespace EduRepo.Application.Kursy
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
            var kurs = await _context.Kursy.FindAsync(request.Id);

            if (kurs == null)
            {
                throw new KeyNotFoundException($"Kurs with ID {request.Id} not found.");
            }

            _context.Kursy.Remove(kurs);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
