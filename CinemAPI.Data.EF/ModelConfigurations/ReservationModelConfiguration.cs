using CinemAPI.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace CinemAPI.Data.EF.ModelConfigurations
{
    internal sealed class ReservationModelConfiguration : IModelConfiguration
    {
        public void Configure(DbModelBuilder modelBuilder)
        {
            EntityTypeConfiguration<Reservation> projectionModel = modelBuilder.Entity<Reservation>();
            projectionModel.HasKey(model => model.Id);
            projectionModel.Property(model => model.Row).IsRequired();
            projectionModel.Property(model => model.Column).IsRequired();
            projectionModel.Property(model => model.ProjectionId).IsRequired();
            projectionModel.Property(model => model.IsCanceled).IsRequired();
            projectionModel.Property(model => model.UniqueId).IsRequired();
            projectionModel.Property(model => model.IsReservationUsed).IsRequired();
        }
    }
}
