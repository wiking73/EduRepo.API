using EduRepo.Domain;
using EduRepo.Infrastructure;
using MediatR;


namespace EduRepo.Application.Odpowiedzi
{
    public class CreateCommand : IRequest<Unit>
    {
       // public int IdOdpowiedzi { get; set; }
        public int IdZadania { get; set; }
        //   public Zadanie Zadanie { get; set; }

        
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
            var odpowiedz = new Odpowiedz
            {

                IdZadania = request.IdZadania,
               
                DataOddania = request.DataOddania,
                KomentarzNauczyciela = request.KomentarzNauczyciela,
                NazwaPliku = request.NazwaPliku,
                Ocena = request.Ocena,
                WlascicielId = request.UserId,
                UserName = request.Name,

            };

            _context.Odpowiedzi.Add(odpowiedz);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;



            return Unit.Value;
        }
    }
}