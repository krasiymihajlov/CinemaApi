using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewReservation
{
    public class NewReservationReservedSeatsValidation : INewReservation
    {
        private readonly INewReservation newReservation;
        private readonly IReservationRepository reservationRepo;

        public NewReservationReservedSeatsValidation(INewReservation newReservation,
                                                     IReservationRepository reservationRepository)
        {
            this.reservationRepo = reservationRepository;
            this.newReservation = newReservation;
        }

        public NewReservationSummary New(IReservationCreation reservation)
        {
            IReservationCreation currentReservation = reservationRepo.Get(reservation.ProjectionId, reservation.Row, reservation.Column);

            if (currentReservation != null)
            {
                return new NewReservationSummary(false, "This seat is already reserved");
            }

            return newReservation.New(reservation);
        }
    }
}
