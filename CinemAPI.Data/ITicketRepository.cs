using CinemAPI.Models.Contracts.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Data
{
    public  interface ITicketRepository
    {
        ITicketCreation Get(int projId, int row, int col);

        void Insert(ITicketCreation reservation);
    }
}
