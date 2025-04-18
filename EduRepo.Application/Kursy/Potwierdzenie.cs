/*using System;
using System.Threading;
using System.Threading.Tasks;
using EduRepo.Application;
using EduRepo.Domain;
using EduRepo.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bikes.Application.Reservations
{

    public class ConfirmCommand : IRequest<Result<Kurs>>
    {
        public int KursId { get; set; }
    }

    public class ConfirmHandler : IRequestHandler<ConfirmCommand, Result<Kurs>>
    {
        private readonly DataContext _context;

        public ConfirmHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<Kurs>> Handle(ConfirmCommand request, CancellationToken cancellationToken)
        {

            var dolaczenie = await _context.Kursy
                .FirstOrDefaultAsync(r => r.ReservationId == request.ReservationId, cancellationToken);

            if (dolaczenie == null)
            {
                return Result<Reservation>.Failure("Rezerwacja nie została znaleziona.");
            }

            reservation.Status = ReservationStatus.Confirmed;


            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result)
            {
                return Result<Reservation>.Failure("Nie udało się potwierdzić rezerwacji.");
            }

            return Result<Reservation>.Success(reservation);
        }
    }
}*/