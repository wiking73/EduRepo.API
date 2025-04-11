using MediatR;
using EduRepo.Domain;
using EduRepo.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EduRepo.Application.Zadania
{
    public class List {
        public class Query : IRequest<List<Zadanie>> { }

        public class Handler : IRequestHandler<Query, List<Zadanie>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Zadanie>> Handle(Query request, CancellationToken cancellationToken)
            {

                return await _context.Zadania.ToListAsync(cancellationToken);
            }
        }
    }
  
}
