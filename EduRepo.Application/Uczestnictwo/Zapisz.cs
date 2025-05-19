using EduRepo.Domain;
using EduRepo.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EduRepo.Application.Uczestnictwa
{
    public class ZapiszCommand : IRequest<Unit>
    {
        public int KursId { get; set; }
        public string UserId { get; set; }
        public StatusUczestnika Status { get; set; }
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
                .AnyAsync(u => u.KursId == request.KursId && u.WlascicielId == request.UserId, cancellationToken);

            if (istnieje)
                throw new InvalidOperationException("U¿ytkownik ju¿ zapisany na ten kurs.");

            var kurs = await _context.Kursy.FirstOrDefaultAsync(k => k.IdKursu == request.KursId, cancellationToken);
            if (kurs == null)
                throw new InvalidOperationException("Nie znaleziono kursu.");

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user == null)
                throw new InvalidOperationException("Nie znaleziono u¿ytkownika.");

            // Tutaj dodaj logi
            Console.WriteLine($"DEBUG: KursId: {request.KursId}");
            Console.WriteLine($"DEBUG: UserId: {request.UserId}");
            Console.WriteLine($"DEBUG: Kurs istnieje? {kurs != null}");
            Console.WriteLine($"DEBUG: U¿ytkownik istnieje? {user != null}");

            var uczestnik = new Uczestnictwo
            {
                KursId = request.KursId,
                Status = StatusUczestnika.Oczekuje,
                WlascicielId = request.UserId,
                UserName = user.UserName
            };

            _context.Uczestnictwa.Add(uczestnik);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

    }
}
