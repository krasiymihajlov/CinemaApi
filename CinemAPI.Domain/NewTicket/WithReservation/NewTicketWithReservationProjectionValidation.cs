using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewTicket.WithReservation
{
    public class NewTicketWithReservationProjectionValidation : INewTicketWithReservation
    {
        private readonly INewTicketWithReservation newTicket;
        private readonly IProjectionRepository projectionRepo;

        public NewTicketWithReservationProjectionValidation(INewTicketWithReservation newTicket,
                                                        IProjectionRepository projectionRepository)
        {
            this.newTicket = newTicket;
            this.projectionRepo = projectionRepository;
        }

        public NewTicketSummary New(ITicketCreation ticket)
        {
            IProjection projection = this.projectionRepo.GetById(ticket.ProjectionId);
            if (projection == null)
            {
                return new NewTicketSummary(false, $"Projection not exist");
            }

            return newTicket.New(ticket);
        }
    }
}
