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

public class Delete
    {
        public class Command : IRequest<Unit>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var zadanie = await _context.Zadania.FindAsync(request.Id);



                _context.Zadania.Remove(zadanie);

                var result = await _context.SaveChangesAsync(cancellationToken) > 0;

               

                return Unit.Value;
            }
        }
    }
}
