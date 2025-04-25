/*using System;
using System.Threading;
using System.Threading.Tasks;
using EduRepo.Domain;
using EduRepo.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduRepo.Application.Uczestnicwo
{

    public class ConfirmCommand : IRequest<Result<Uczestnictwo>>
    {
        public int Id { get; set; }
    }

    public class ConfirmReservationHandler : IRequestHandler<ConfirmCommand, Result<Uczestnictwo>>
    {
        private readonly DataContext _context;

        public ConfirmReservationHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<Uczestnictwo>> Handle(ConfirmCommand request, CancellationToken cancellationToken)
        {

            var reservation = await _context.Uczestnictwa
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            if (reservation == null)
            {
                return Result<Uczestnictwo>.Failure("Rezerwacja nie została znaleziona.");
            }

            reservation.Status = Status.Confirmed;


            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result)
            {
                return Result<Uczestnictwo>.Failure("Nie udało się potwierdzić rezerwacji.");
            }

            return Result<Uczestnictwo>.Success(reservation);
        }
    }
}*/