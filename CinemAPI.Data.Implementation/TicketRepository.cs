using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Ticket;

namespace CinemAPI.Data.Implementation
{
    public class TicketRepository : ITicketRepository
    {
        private readonly CinemaDbContext db;

        public TicketRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public ITicketCreation Get(int projId, int row, int col)
        {
            return db.Tickets.Where(x => x.ProjectionId == projId &&
                                              x.Row == row &&
                                              x.Column == col).SingleOrDefault();
        }
        
        public void Insert(ITicketCreation ticket)
        {
            Ticket newTicket = new Ticket(ticket.ProjectionId, ticket.Row, ticket.Column);

            db.Tickets.Add(newTicket);
            db.SaveChanges();
        }
    }
}
