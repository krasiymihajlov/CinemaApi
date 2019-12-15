using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Reservation;
using CinemAPI.Models.Contracts.Ticket;

namespace CinemAPI.Domain.NewReservation
{
    public class NewReservationBoughtSeatValidation : INewReservation
    {
        private readonly INewReservation newReservation;
        private readonly ITicketRepository ticketRepository;

        public NewReservationBoughtSeatValidation(INewReservation newReservation,
                                            ITicketRepository ticketRepository)
        {
            this.newReservation = newReservation;
            this.ticketRepository = ticketRepository;
        }

        public NewReservationSummary New(IReservationCreation reservation)
        {
            ITicketCreation currentTicket = this.ticketRepository.Get(reservation.ProjectionId, reservation.Row, reservation.Column);

            if (currentTicket != null)
            {
                return new NewReservationSummary(false, $"This seat is already bought");
            }

            return this.newReservation.New(reservation);
        }
    }
}
