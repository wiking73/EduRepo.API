using MediatR;
using EduRepo.Domain;
using EduRepo.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Linq;

namespace EduRepo.Application.Kursy
{
    public class List
    {
        public class Query : IRequest<List<Kurs>> { }

        public class Handler : IRequestHandler<Query, List<Kurs>>
        {
            private readonly DataContext _context;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(DataContext context, IHttpContextAccessor httpContextAccessor)
            {
                _context = context;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<List<Kurs>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = _httpContextAccessor.HttpContext?.User;
                var userId = user?.FindFirstValue(ClaimTypes.NameIdentifier);
                var role = user?.FindFirstValue(ClaimTypes.Role);

                var query = _context.Kursy.AsQueryable();
                if (role == "Teacher")
                {
                    query = query.Where(k => k.WlascicielId == userId);
                }



                return await query.ToListAsync(cancellationToken);
            }
        }
    }
}
