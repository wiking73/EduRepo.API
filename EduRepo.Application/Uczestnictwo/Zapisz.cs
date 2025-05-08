using System;
using System.Threading;
using System.Threading.Tasks;
using EduRepo.Domain;
using EduRepo.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduRepo.Application.Uczestnictwa
{
    public class ZapiszCommand : IRequest<Uczestnictwo>
    {
        public int IdKursu { get; set; }
        public string WlascicielId { get; set; }
        public string UserName { get; set; }  
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
           
            var istnieje = await _context.Uczestnictwa
                .AnyAsync(u => u.IdKursu == request.IdKursu && u.WlascicielId == request.WlascicielId);

            if (istnieje)
            {
                throw new InvalidOperationException("U¿ytkownik jest ju¿ zapisany do tego kursu.");
            }

           
            var kurs = await _context.Kursy.FindAsync(request.IdKursu);
            if (kurs == null)
            {
                throw new InvalidOperationException("Kurs o podanym ID nie istnieje.");
            }

           
            var zapis = new Uczestnictwo
            {
                IdKursu = request.IdKursu,
                WlascicielId = request.WlascicielId,
                UserName = request.UserName,  
                Status = StatusUczestnika.Oczekuje 
            };

            
            _context.Uczestnictwa.Add(zapis);

           
            await _context.SaveChangesAsync(cancellationToken);

            return zapis; 
        }
    }
}
