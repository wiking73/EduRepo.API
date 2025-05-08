using System.Threading;
using System.Threading.Tasks;
using EduRepo.Domain;
using EduRepo.Infrastructure;
using MediatR;

namespace EduRepo.Application.Uczestnictwa
{
    public class Details
    {
        public class Query : IRequest<Uczestnictwo>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Uczestnictwo>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Uczestnictwo> Handle(Query request, CancellationToken cancellationToken)
            {
              
                var uczestnictwo = await _context.Uczestnictwa.FindAsync(request.Id, cancellationToken);

                
                if (uczestnictwo == null)
                {
                    throw new InvalidOperationException($"Uczestnictwo o ID {request.Id} nie zostało znalezione.");
                }

                return uczestnictwo;
            }
        }
    }
}
