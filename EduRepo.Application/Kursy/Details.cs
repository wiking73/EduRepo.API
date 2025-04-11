using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduRepo.Domain;
using EduRepo.Infrastructure;
using MediatR;

namespace EduRepo.Application.Kursy
{



    public class Details
    {
        public class Query : IRequest<Kurs>
        {
            public  int Id { get; set; }
        }


        public class Handler : IRequestHandler<Query, Kurs>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Kurs> Handle(Query request, CancellationToken cancellationToken)
            {


                return await _context.Kursy.FindAsync(request.Id);
            }
        }
    }
}
