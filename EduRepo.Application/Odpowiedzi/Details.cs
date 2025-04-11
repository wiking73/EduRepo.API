/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduRepo.Domain;
using EduRepo.Infrastructure;
using MediatR;

namespace EduRepo.Application.Odpowiedzi
{



    public class Details
    {
        public class Query : IRequest<Odpowiedz>
        {
            public Guid Id { get; set; }
        }


        public class Handler : IRequestHandler<Query, Odpowiedz>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Odpowiedz> Handle(Query request, CancellationToken cancellationToken)
            {


                return await _context.Odpowiedzi.FindAsync(request.Id);
            }
        }
    }
}
*/