/*using System;
using System.Threading;
using System.Threading.Tasks;
using EduRepo.Domain;
using EduRepo.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduRepo.Application.Kursy
{
   
        public class EditCommand : IRequest<Unit>
        {
        public int IdKursu { get; set; }
        public string Nazwa { get; set; }
        public string OpisKursu { get; set; }
        public string RokAkademicki { get; set; }
        public string Klasa { get; set; }
        public bool CzyZarchiwizowany { get; set; }
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
                var kurs = await _context.Kursy.FindAsync(new object[] { request.IdKursu }, cancellationToken);


            //////////////////!!!!!!!!!!!!!!!!!!!!!!!
            kurs.Nazwa = request.Nazwa;
            kurs.OpisKursu = request.OpisKursu;
            kurs.RokAkademicki = request.RokAkademicki;
            kurs.Klasa = request.Klasa;
            kurs.CzyZarchiwizowany = request.CzyZarchiwizowany;

                await _context.SaveChangesAsync(cancellationToken);

                
                return Unit.Value;
            }
        }
    
}
*/