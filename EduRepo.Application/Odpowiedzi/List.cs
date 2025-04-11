using MediatR;
using EduRepo.Domain;
using EduRepo.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EduRepo.Application.Odpowiedzi
{
    public class List {
        public class Query : IRequest<List<Odpowiedz>> { }

        public class Handler : IRequestHandler<Query, List<Odpowiedz>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Odpowiedz>> Handle(Query request, CancellationToken cancellationToken)
            {

                return await _context.Odpowiedzi.ToListAsync(cancellationToken);
            }
        }
    }
  
}
