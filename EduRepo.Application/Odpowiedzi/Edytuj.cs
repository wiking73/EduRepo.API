using System;
using System.Threading;
using System.Threading.Tasks;
using EduRepo.Domain;
using EduRepo.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduRepo.Application.Odpowiedzi
{
   
        public class EditCommand : IRequest<Unit>
        {
       public int IdOdpowiedzi { get; set; }
        //public int IdZadania { get; set; }
        public Zadanie Zadanie { get; set; }

        //public int IdUzytkownika { get; set; }
        public Uzytkownik Uzytkownik { get; set; }

        public DateTime DataOddania { get; set; }
        public string KomentarzNauczyciela { get; set; }
        public string NazwaPliku { get; set; }
        public string Ocena { get; set; }
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
                var odpowiedz = await _context.Odpowiedzi.FindAsync(new object[] { request.IdOdpowiedzi }, cancellationToken);


            //////////////////!!!!!!!!!!!!!!!!!!!!!!!
            odpowiedz.DataOddania = request.DataOddania;
            odpowiedz.KomentarzNauczyciela = request.KomentarzNauczyciela;
            odpowiedz.NazwaPliku = request.NazwaPliku;
            odpowiedz.Ocena = request.Ocena;

                await _context.SaveChangesAsync(cancellationToken);

                
                return Unit.Value;
            }
        }
    
}
