using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Reservation;
using CinemAPI.Models.Contracts.Ticket;

namespace CinemAPI.Domain.NewTicket.WithReservation
{
    public class NewTIcketReservationKeyValidation : INewTicketWithReservation
    {
        private readonly INewTicketWithReservation newTicket;
        private readonly IReservationRepository reservationRepository;

        public NewTIcketReservationKeyValidation(INewTicketWithReservation newTicket,
                                                      IReservationRepository reservationRepository)
        {
            this.newTicket = newTicket;
            this.reservationRepository = reservationRepository;
        }
        public NewTicketSummary New(ITicketCreation ticket)
        {
            IReservationCreation reservation = this.reservationRepository.Get(ticket.ProjectionId, ticket.Row, ticket.Column);

            if (reservation.IsReservationUsed)
            {
                return new NewTicketSummary(false, $"You are already used this reservation key");
            }

            return newTicket.New(ticket);
        }
    }
}
