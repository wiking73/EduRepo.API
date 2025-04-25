using EduRepo.Domain;
using EduRepo.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EduRepo.Application.Odpowiedzi
{
    public class CreateCommand : IRequest<Unit>
    {
        public int IdZadania { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public DateTime DataOddania { get; set; }
        public string KomentarzNauczyciela { get; set; }
        public string NazwaPliku { get; set; }
        public string Ocena { get; set; }
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
            
            if (string.IsNullOrEmpty(request.KomentarzNauczyciela))
            {
                request.KomentarzNauczyciela = "brak"; 
            }

            if (string.IsNullOrEmpty(request.Ocena))
            {
                request.Ocena = "brak"; 
            }

            var odpowiedz = new Odpowiedz
            {
                IdZadania = request.IdZadania,
                DataOddania = request.DataOddania,
                KomentarzNauczyciela = request.KomentarzNauczyciela,
                NazwaPliku = request.NazwaPliku,
                Ocena = request.Ocena,
                WlascicielId = request.UserId,
                UserName = request.Name
            };

            
            _context.Odpowiedzi.Add(odpowiedz);

          
            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (result)
            {
                return Unit.Value; 
            }

         
            throw new Exception("Nie udało się zapisać odpowiedzi w bazie.");
        }
    }
}
