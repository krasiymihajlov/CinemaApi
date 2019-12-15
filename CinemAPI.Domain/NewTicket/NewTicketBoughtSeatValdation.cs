using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewTicket
{
    public class NewTicketBoughtSeatValdation : INewTicket
    {
        private readonly INewTicket newTicket;
        private readonly ITicketRepository ticketRepository;

        public NewTicketBoughtSeatValdation(INewTicket newTicket,
                                            ITicketRepository ticketRepository)
        {
            this.newTicket = newTicket;
            this.ticketRepository = ticketRepository;
        }

        public NewTicketSummary New(ITicketCreation ticket)
        {
            ITicketCreation currentTicket = ticketRepository.Get(ticket.ProjectionId, ticket.Row, ticket.Column);

            if (currentTicket != null)
            {
                return new NewTicketSummary(false, $"This seat is already bought");
            }

            return newTicket.New(ticket);
        }
    }
}
