using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduRepo.Domain;
using EduRepo.Infrastructure;
using MediatR;

namespace EduRepo.Application.Powiadomienia
{



    public class Details
    {
        public class Query : IRequest<PowiadomienieBrakOdpowiedzi>
        {
            public int Id { get; set; }
        }


        public class Handler : IRequestHandler<Query, PowiadomienieBrakOdpowiedzi>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<PowiadomienieBrakOdpowiedzi> Handle(Query request, CancellationToken cancellationToken)
            {


                return await _context.Powiadomienia.FindAsync(request.Id);
            }
        }
    }
}