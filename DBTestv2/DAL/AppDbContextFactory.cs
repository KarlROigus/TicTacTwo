using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DAL;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var connectionstring = "Data Source=/Users/karlrudolf/Desktop/test/app.db";
        
        var contextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(connectionstring)
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .Options;

        return new AppDbContext(contextOptions);
    }
}