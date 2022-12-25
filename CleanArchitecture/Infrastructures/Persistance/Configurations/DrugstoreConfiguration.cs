using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistance.Configurations
{
    public class DrugstoreConfiguration:IEntityTypeConfiguration<Drugstores>
    {
        public void Configure(EntityTypeBuilder<Drugstores> builder)
        {
            builder.HasOne(d => d.Department);
        }
    }

}
