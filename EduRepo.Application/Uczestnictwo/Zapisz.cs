/*using System;
using System.Threading;
using System.Threading.Tasks;

using EduRepo.Domain;
using EduRepo.Infrastructure; 
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bikes.Application.Reservations
{
    public class ZapiszCommand : IRequest<Uczestnictwo>
    {
        public int IdKursu { get; set; }
        public string WlascicielId { get; set; }

    }

    public class ZapiszHandler : IRequestHandler<ZapiszCommand, Uczestnictwo>
    {
        private readonly DataContext _context;

        public ZapiszHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<Uczestnictwo> Handle(ZapiszCommand request, CancellationToken cancellationToken)
        {
           

            
            var zapis = new Uczestnictwo
            {
                IdKursu = request.IdKursu,
                WlascicielId = request.WlascicielId,       
                Status = StatusUczestnika.Oczekuje, 
            };

            
            _context.Uczestnictwa.Add(zapis);

            
            await _context.SaveChangesAsync(cancellationToken);


            return Unit.Value;

            
        }
    }
}*/