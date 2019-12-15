using CinemAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Data.EF.ModelConfigurations
{
    public class TicketModelConfiguraton : IModelConfiguration
    {
        public void Configure(DbModelBuilder modelBuilder)
        {
            EntityTypeConfiguration<Ticket> projectionModel = modelBuilder.Entity<Ticket>();
            projectionModel.HasKey(model => model.Id);
            projectionModel.Property(model => model.Row).IsRequired();
            projectionModel.Property(model => model.Column).IsRequired();
            projectionModel.Property(model => model.ProjectionId).IsRequired();
            projectionModel.Property(model => model.UniqueId).IsRequired();
        }
    }
}
