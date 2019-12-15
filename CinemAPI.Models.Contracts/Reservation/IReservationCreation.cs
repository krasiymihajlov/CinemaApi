namespace CinemAPI.Models.Contracts.Reservation
{
    public interface IReservationCreation
    {
        int Id { get; }

        int ProjectionId { get; }

        int Row { get; }

        int Column { get; }

        bool IsCanceled { get; }

        bool IsReservationUsed { get; }

        string UniqueId { get; }
    }
}
