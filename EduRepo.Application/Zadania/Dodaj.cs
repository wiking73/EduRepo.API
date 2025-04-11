using EduRepo.Domain;
using EduRepo.Infrastructure;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using EduRepo.Infrastructure;

namespace EduRepo.Application.Zadania
{
    public class Command : IRequest<Unit>
    {
        public Kurs Kurs { get; set; }

        public string Nazwa { get; set; }
        public string Tresc { get; set; }
        public DateTime TerminOddania { get; set; }
        public string PlikPomocniczy { get; set; }
        public bool CzyObowiazkowe { get; set; }
    }
     
      
    

    public class Handler : IRequestHandler<Command,  Unit> 
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var zadanie = new Zadanie
            {
                Kurs = request.Kurs,
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