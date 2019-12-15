using CinemAPI.Models.Contracts.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Models
{
    public class Ticket : ITicketCreation
    {
        public Ticket()
        {
        }

        public Ticket(int projectionId, int row, int colum)
        {
            this.ProjectionId = projectionId;
            this.Row = row;
            this.Column = colum;
            this.UniqueId = Guid.NewGuid().ToString();
        }

        public int Id { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public string UniqueId { get; set; }

        public int ProjectionId { get; set; }

        public virtual Projection Projection { get; set; }
    }
}
