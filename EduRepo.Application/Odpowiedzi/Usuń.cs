using System;
using System.Threading;
using System.Threading.Tasks;
using EduRepo.Infrastructure;
using MediatR;

namespace EduRepo.Application.Odpowiedzi
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
            var odpowiedz = await _context.Odpowiedzi.FindAsync(request.Id);

            if (odpowiedz == null)
            {
                throw new KeyNotFoundException($"Odpowiedz with ID {request.Id} not found.");
            }

            _context.Odpowiedzi.Remove(odpowiedz);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
