using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Reservation;
using CinemAPI.Models.Contracts.Ticket;

namespace CinemAPI.Domain.NewTicket.WithReservation
{
    public class NewTicketCanceledReservationValidation : INewTicketWithReservation
    {
        private readonly INewTicketWithReservation newTicket;
        private readonly IReservationRepository reservationRepository;

        public NewTicketCanceledReservationValidation(INewTicketWithReservation newTicket,
                                                      IReservationRepository reservationRepository)
        {
            this.newTicket = newTicket;
            this.reservationRepository = reservationRepository;
        }
        public NewTicketSummary New(ITicketCreation ticket)
        {
            IReservationCreation reservation = this.reservationRepository.Get(ticket.ProjectionId, ticket.Row, ticket.Column);

            if (reservation.IsCanceled)
            {
                return new NewTicketSummary(false, $"Your reservation has been canceled");
            }

            return newTicket.New(ticket);
        }
    }
}
