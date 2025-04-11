﻿/*using EduRepo.Domain;
using EduRepo.Infrastructure;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EduRepo.Application.Kursy
{
    public class CreateCommand : IRequest<Unit>
    {
        public string Nazwa { get; set; }
        public string OpisKursu { get; set; }
        public string RokAkademicki { get; set; }
        public string Klasa { get; set; }
        public bool CzyZarchiwizowany { get; set; }

        public int IdWlasciciela { get; set; }
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
            
            if (request.IdWlasciciela == 0)
            {
                
                throw new ArgumentException("IdWlasciciela jest wymagane.");
            }

            
            var kurs = new Kurs
            {
                Nazwa = request.Nazwa,
                OpisKursu = request.OpisKursu,
                RokAkademicki = request.RokAkademicki,
                Klasa = request.Klasa,
                CzyZarchiwizowany = request.CzyZarchiwizowany,
                IdWlasciciela = request.IdWlasciciela,  
            };

           
            _context.Kursy.Add(kurs);

           
            var result = await _context.SaveChangesAsync(cancellationToken) > 0;
            return Unit.Value;
        }
    }
}
*/