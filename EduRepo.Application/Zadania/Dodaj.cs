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
        public string PlikPomocniczy { get; set; }
        public bool CzyObowiazkowe { get; set; }
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

            var zadanie = new Zadanie
            {
                IdKursu = request.IdKursu,
                Nazwa = request.Nazwa,
                Tresc = request.Tresc,
                TerminOddania = request.TerminOddania,
                PlikPomocniczy = request.PlikPomocniczy,
                CzyObowiazkowe = request.CzyObowiazkowe,
            };

            _context.Zadania.Add(zadanie);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            return Unit.Value;
        }
    }
}
