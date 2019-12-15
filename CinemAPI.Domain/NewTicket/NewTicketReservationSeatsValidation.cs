using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Reservation;
using CinemAPI.Models.Contracts.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewTicket
{
    public class NewTicketReservationSeatsValidation : INewTicket
    {
        private readonly IReservationRepository reservationRepo;
        private readonly INewTicket newTicket;

        public NewTicketReservationSeatsValidation(IReservationRepository reservationRepository,
                                       INewTicket newTicket)
        {
            this.reservationRepo = reservationRepository;
            this.newTicket = newTicket;
        }

        public NewTicketSummary New(ITicketCreation ticket)
        {
            IReservationCreation currentReservation = this.reservationRepo.Get(ticket.ProjectionId, ticket.Row, ticket.Column);

            if (currentReservation != null)
            {
                return new NewTicketSummary(false, "This seat is already reserved");
            }

            return newTicket.New(ticket);
        }
    }
}
