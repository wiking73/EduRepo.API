using MediatR;
using EduRepo.Domain;
using EduRepo.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EduRepo.Application.Uczestnictwa
{
    public class List
    {
        public class Query : IRequest<List<Uczestnictwo>> { public string UserId { get; set; } }
        
        public class Handler : IRequestHandler<Query, List<Uczestnictwo>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Uczestnictwo>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Uczestnictwa
                    .Where(u => u.WlascicielId == request.UserId) 
                    .Include(u => u.Kurs)
                    .ToListAsync(cancellationToken);
            }
        }

    }

}
