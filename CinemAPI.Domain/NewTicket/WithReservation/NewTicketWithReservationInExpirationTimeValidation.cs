using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Ticket;
using System;

namespace CinemAPI.Domain.NewTicket.WithReservation
{
    public class NewTicketWithReservationInExpirationTimeValidation : INewTicketWithReservation
    {
        private readonly INewTicketWithReservation newTicket;
        private readonly IProjectionRepository projectionRepo;

        public NewTicketWithReservationInExpirationTimeValidation(INewTicketWithReservation newTicket,
                                                        IProjectionRepository projectionRepository)
        {
            this.newTicket = newTicket;
            this.projectionRepo = projectionRepository;
        }

        public NewTicketSummary New(ITicketCreation ticket)
        {
            IProjection projection = this.projectionRepo.GetById(ticket.ProjectionId);
            DateTime currentTime = DateTime.Now;
            bool isStartingInLessThan10Min = projection.StartDate.AddMinutes(-10) <= currentTime;

            if (isStartingInLessThan10Min)
            {
                return new NewTicketSummary(false, $"Your reservation has expired");                
            }

            return newTicket.New(ticket);
        }
    }
}
