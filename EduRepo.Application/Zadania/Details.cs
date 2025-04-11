using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduRepo.Domain;
using EduRepo.Infrastructure;
using MediatR;

namespace EduRepo.Application.Zadania
{



    public class Details
    {
        public class Query : IRequest<Zadanie>
        {
            public int Id { get; set; }
        }


        public class Handler : IRequestHandler<Query, Zadanie>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Zadanie> Handle(Query request, CancellationToken cancellationToken)
            {


                return await _context.Zadania.FindAsync(request.Id);
            }
        }
    }
}
