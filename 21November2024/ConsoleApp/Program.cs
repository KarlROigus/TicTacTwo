using ConsoleApp;
using DAL;
using Microsoft.EntityFrameworkCore;


var connectionString = "Data Source=/Users/karlrudolf/Desktop/21novStart/app.db";


var contextOptions = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlite(connectionString)
    .EnableDetailedErrors()
    .EnableSensitiveDataLogging()
    .Options;


using var context = new AppDbContext(contextOptions);


var userRepository = new UserRepository(context);
userRepository.CreateUserOrLogin();

var userName = userRepository.GetUserName();



var menuController = new MenuController(userName);

menuController.GetMainMenu().Run();



