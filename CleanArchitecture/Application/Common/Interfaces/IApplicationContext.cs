using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationContext
    {
        public DbSet<Departments> Department { get; set; }
        public DbSet<Drugstores> Drugstore { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}