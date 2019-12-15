using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Movie;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Ticket;
using System;

namespace CinemAPI.Domain.NewTicket
{
    public class NewTicketStartProjectionValidation : INewTicket
    {
        private readonly INewTicket newTicket;
        private readonly IProjectionRepository projectionRepo;

        public NewTicketStartProjectionValidation(INewTicket newTicket, 
                                                  IProjectionRepository projectionRepo)
        {
            this.newTicket = newTicket;
            this.projectionRepo = projectionRepo;
        }

        public NewTicketSummary New(ITicketCreation ticket)
        {
            IProjection projection = this.projectionRepo.GetById(ticket.ProjectionId);
            DateTime currentTime = DateTime.Now;
            bool isStarting = projection.StartDate < currentTime;

            if (isStarting)
            {
                return new NewTicketSummary(false, $"You cannot buy ticket because projection is started");
            }

            return this.newTicket.New(ticket);
        }
    }
}
