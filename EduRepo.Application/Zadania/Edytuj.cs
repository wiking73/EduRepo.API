using System;
using System.Threading;
using System.Threading.Tasks;
using EduRepo.Domain;
using EduRepo.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduRepo.Application.Zadania
{
   
        public class EditCommand : IRequest<Unit>
        {
            public Guid Id { get; set; }
            public string Nazwa { get; set; }
            public string Tresc { get; set; }
            public DateTime TerminOddania { get; set; }
            public string PlikPomocniczy { get; set; }
            public bool CzyObowiazkowe { get; set; }
        }

        public class EditHandler : IRequestHandler<EditCommand, Unit>
        {
            private readonly DataContext _context;

            public EditHandler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(EditCommand request, CancellationToken cancellationToken)
            {
                var zadanie = await _context.Zadania.FindAsync(new object[] { request.Id }, cancellationToken);

             

                zadanie.Nazwa = request.Nazwa ?? zadanie.Nazwa;
                zadanie.Tresc = request.Tresc ?? zadanie.Tresc;
                zadanie.TerminOddania = request.TerminOddania;
                zadanie.PlikPomocniczy = request.PlikPomocniczy ?? zadanie.PlikPomocniczy;
                zadanie.CzyObowiazkowe = request.CzyObowiazkowe;

                await _context.SaveChangesAsync(cancellationToken);

                
                return Unit.Value;
            }
        }
    
}
