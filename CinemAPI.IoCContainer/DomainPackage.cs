using CinemAPI.Domain;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.NewProjection;
using CinemAPI.Domain.NewReservation;
using CinemAPI.Domain.NewTicket;
using CinemAPI.Domain.NewTicket.WithReservation;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace CinemAPI.IoCContainer
{
    public class DomainPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            //For new projection
            container.Register<INewProjection, NewProjectionCreation>();
            container.RegisterDecorator<INewProjection, NewProjectionMovieValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionUniqueValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionRoomValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionPreviousOverlapValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionNextOverlapValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionAvailableSeatsCountValidation>();

            //For new Reservation
            container.Register<INewReservation, NewReservationCreation>();
            container.RegisterDecorator<INewReservation, NewReservationInExpirationTimeValidation>();
            container.RegisterDecorator<INewReservation, NewReservationReservedSeatsValidation>();
            container.RegisterDecorator<INewReservation, NewReservationNotExistValidation>();
            container.RegisterDecorator<INewReservation, NewReservationFinishedProjectionValidation>();
            container.RegisterDecorator<INewReservation, NewReservationBoughtSeatValidation>();
            container.RegisterDecorator<INewReservation, NewReservationProjectionValidation>();

            //For new Ticket
            container.Register<INewTicket, NewTicketCreation>();           
            container.RegisterDecorator<INewTicket, NewTicketBoughtSeatValdation>();
            container.RegisterDecorator<INewTicket, NewTicketReservationSeatsValidation>();
            container.RegisterDecorator<INewTicket, NewTicketStartProjectionValidation>();
            container.RegisterDecorator<INewTicket, NewTicketFinishedProjectionReservation>();
            container.RegisterDecorator<INewTicket, NewTicketProjectionValidation>();

            //For new Ticket with reservation
            container.Register<INewTicketWithReservation, NewTicketReservationCreationcs>();
            container.RegisterDecorator<INewTicketWithReservation, NewTicketWithReservationInExpirationTimeValidation>();
            container.RegisterDecorator<INewTicketWithReservation, NewTicketCanceledReservationValidation>();
            container.RegisterDecorator<INewTicketWithReservation, NewTIcketReservationKeyValidation>();
            container.RegisterDecorator<INewTicketWithReservation, NewTicketWithReservationProjectionValidation>();
        }
    }
}