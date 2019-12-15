using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewTicket
{
    public class NewTicketCreation : INewTicket
    {
        private readonly ITicketRepository ticketRepository;

        public NewTicketCreation(ITicketRepository ticketRepository)
        {
            this.ticketRepository = ticketRepository;
        }

        public NewTicketSummary New(ITicketCreation ticket)
        {
            this.ticketRepository.Insert(new Ticket(ticket.ProjectionId, ticket.Row, ticket.Column));

            return new NewTicketSummary(true);
        }
    }
}
