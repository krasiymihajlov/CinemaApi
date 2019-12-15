using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Reservation;

namespace CinemAPI.Domain.NewReservation
{
    public class NewReservationCreation : INewReservation
    {
        private readonly IReservationRepository reservationRepo;

        public NewReservationCreation(IReservationRepository reservationRepo)
        {
            this.reservationRepo = reservationRepo;
        }

        public NewReservationSummary New(IReservationCreation reservation)
        {
            reservationRepo.Insert(new Reservation(reservation.Row, reservation.Column, reservation.ProjectionId));

            return new NewReservationSummary(true);
        }
    }
}
