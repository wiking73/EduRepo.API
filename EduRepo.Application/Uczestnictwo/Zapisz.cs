using EduRepo.Domain;
using EduRepo.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace EduRepo.Application.Uczestnictwa
{
    public class ZapiszCommand : IRequest<Unit>
    {


        public int IdKursu { get; set; }

        public string UserId { get; set; }
        public StatusUczestnika Status { get; set; }

        public string Name { get; set; }
    }


    public class Handler : IRequestHandler<ZapiszCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ZapiszCommand request, CancellationToken cancellationToken)
        {

            var istnieje = await _context.Uczestnictwa
    .AnyAsync(u => u.IdKursu == request.IdKursu && u.WlascicielId == request.UserId, cancellationToken);

            if (istnieje)
                throw new InvalidOperationException("U¿ytkownik ju¿ zapisany na ten kurs.");


            var uczestnik = new Uczestnictwo
            {
                IdKursu = request.IdKursu,
                Status = StatusUczestnika.Oczekuje,
                WlascicielId = request.UserId,
                UserName = request.Name,
            };


            _context.Uczestnictwa.Add(uczestnik);


            var result = await _context.SaveChangesAsync(cancellationToken) > 0;
            return Unit.Value;
        }
    }

}
