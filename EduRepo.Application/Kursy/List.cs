using MediatR;
using EduRepo.Domain;
using EduRepo.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EduRepo.Application.Kursy
{
    public class List
    {
        public class Query : IRequest<List<Kurs>> { }

        public class Handler : IRequestHandler<Query, List<Kurs>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Kurs>> Handle(Query request, CancellationToken cancellationToken)
            {

                return await _context.Kursy.ToListAsync(cancellationToken);
            }
        }
    }

}
