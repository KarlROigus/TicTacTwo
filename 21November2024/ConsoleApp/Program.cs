using ConsoleApp;
using DAL;
using Microsoft.EntityFrameworkCore;

var connectionString = $"Data Source={ConstantlyUsed.BasePath}app.db";
var contextOptions = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlite(connectionString)
    .EnableDetailedErrors()
    .EnableSensitiveDataLogging()
    .Options;
using var context = new AppDbContext(contextOptions);

var userRepository = new UserRepositoryDb(context);
// var userRepository = new UserRepositoryJson();

userRepository.CreateUserOrLogin();
var userName = userRepository.GetUserName();

var menuController = new MenuController(userName);
menuController.GetMainMenu().Run();

//JSON saving does not keep the games in loaded games list but throws back to gamesThatCouldBeJoined



