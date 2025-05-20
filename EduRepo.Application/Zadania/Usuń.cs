using System;
using System.Threading;
using System.Threading.Tasks;
using EduRepo.Infrastructure;
using MediatR;

namespace EduRepo.Application.Zadania
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
            var zadanie = await _context.Zadania.FindAsync(request.Id);

            if (zadanie == null)
            {
                throw new KeyNotFoundException($"Zadanie z ID {request.Id} nie zostało znalezione.");
            }

            _context.Zadania.Remove(zadanie);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
