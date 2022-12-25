using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructures.Persistance.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructures.Persistance
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        { }
        public DbSet<Departments> Department { get; set; }
        public DbSet<Drugstores> Drugstore { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new DrugstoreConfiguration());
        }
    }
}
