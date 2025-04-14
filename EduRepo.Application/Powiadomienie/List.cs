using MediatR;
using EduRepo.Domain;
using EduRepo.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EduRepo.Application.Powiadomienia
{
    public class List
    {
        public class Query : IRequest<List<PowiadomienieBrakOdpowiedzi>> { }
    
        public class Handler : IRequestHandler<Query, List<PowiadomienieBrakOdpowiedzi>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<PowiadomienieBrakOdpowiedzi>> Handle(Query request, CancellationToken cancellationToken)
            {

                return await _context.Powiadomienia.ToListAsync(cancellationToken);
            }
        }
    }

}
