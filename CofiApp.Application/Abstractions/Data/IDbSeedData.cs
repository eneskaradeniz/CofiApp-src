using Microsoft.EntityFrameworkCore;

namespace CofiApp.Application.Abstractions.Data
{
    public interface IDbSeedData
    {
        Task RunAsync(ModelBuilder builder);
    }
}
