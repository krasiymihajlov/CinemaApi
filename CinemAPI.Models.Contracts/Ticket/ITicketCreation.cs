using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Models.Contracts.Ticket
{
    public interface ITicketCreation
    {
        int Id { get; }

        int ProjectionId { get; }

        int Row { get; }

        int Column { get; }

        string UniqueId { get; }
    }
}
