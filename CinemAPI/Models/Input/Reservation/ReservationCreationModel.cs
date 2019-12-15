using CinemAPI.Models.Contracts.Reservation;

namespace CinemAPI.Models.Input.Reservation
{
    public class ReservationCreationModel : IReservationCreation
    {
        public int Id { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public bool IsCanceled { get; set; }

        public bool IsReservationUsed { get; set; }

        public string UniqueId { get; set; }

        public int ProjectionId { get; set; }
    }
}