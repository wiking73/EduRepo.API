using EduRepo.Domain;
using EduRepo.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EduRepo.Application.Zadania
{
    public class CreateCommand : IRequest<Unit>
    {
        public int IdKursu { get; set; }
        public string Nazwa { get; set; }
        public string Tresc { get; set; }
        public DateTime TerminOddania { get; set; }
        public string? PlikPomocniczy { get; set; }
        public bool CzyObowiazkowe { get; set; }

        public string UserId { get; set; }
        public string Name { get; set; }
    }

    public class Handler : IRequestHandler<CreateCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var kurs = await _context.Kursy.FindAsync(request.IdKursu);
            if (kurs == null)
            {
                throw new KeyNotFoundException($"Kurs o ID {request.IdKursu} nie istnieje.");
            }

            if (kurs.WlascicielId != request.UserId)
            {
                throw new UnauthorizedAccessException("Tylko właściciel kursu może dodać zadanie.");
            }

            if (request.TerminOddania < DateTime.Now)
            {
                throw new ArgumentException("Termin oddania nie może być wcześniejszy niż aktualna data.");
            }

            var zadanie = new Zadanie
            {
                IdKursu = request.IdKursu,
                Nazwa = request.Nazwa,
                Tresc = request.Tresc,
                TerminOddania = request.TerminOddania,
                PlikPomocniczy = request.PlikPomocniczy,
                CzyObowiazkowe = request.CzyObowiazkowe,
                WlascicielId = request.UserId,
                UserName = request.Name,
            };

            _context.Zadania.Add(zadanie);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            return Unit.Value;
        }
    }
}
