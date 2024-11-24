// See https://aka.ms/new-console-template for more information

using DAL;
using Domain;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

var connectionString = "Data Source=/Users/karlrudolf/Desktop/test/app.db";

Console.WriteLine(Path.GetFullPath(connectionString));

var contextOptions = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlite(connectionString)
    .EnableDetailedErrors()
    .EnableSensitiveDataLogging()
    .Options;


using var context = new AppDbContext(contextOptions);

var asd = context.Games.Where(x => x.GameId == 1)
    .Include(x => x.GameStates);

foreach (var ggg in asd)
{
    Console.WriteLine(ggg);
}

