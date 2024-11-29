using ConsoleApp;
using DAL;
using Domain;
using Microsoft.EntityFrameworkCore;

var connectionString = $"Data Source={ConstantlyUsed.BasePath}app.db";
var contextOptions = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlite(connectionString)
    .EnableDetailedErrors()
    .EnableSensitiveDataLogging()
    .Options;
using var context = new AppDbContext(contextOptions);



var userRepository = new UserRepositoryDb(context);
var configRepository = new ConfigRepositoryDb(context);
var gameRepository = new GameRepositoryDb(context);

// var userRepository = new UserRepositoryJson();
// var configRepository = new ConfigRepositoryJson();
// var gameRepository = new GameRepositoryJson();




string userChoice;
var loginUserName = "";
        
do
{
    Console.WriteLine("Hello!");
    Console.WriteLine("1) LOG IN");
    Console.WriteLine("2) CREATE NEW ACCOUNT");
    Console.Write("Please insert your choice: ");
    userChoice = Console.ReadLine()!;

    if (userChoice != "1" && userChoice != "2")
    {
        Console.WriteLine("Invalid choice. Please select 1 or 2.");
    }
            
} while (userChoice != "1" && userChoice != "2");

if (userChoice == "2") // Create new account
{
    var userName = "";    
    bool isAccountCreated = false;

    while (!isAccountCreated)
    {
        do
        {
            Console.Write("Please insert account name: ");
            userName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userName))
            {
                Console.WriteLine("Username cannot be empty or whitespace. Please try again");
            }

        } while (string.IsNullOrWhiteSpace(userName));

        userName = userName.ToLower();
        var usernameAlreadyExists = userRepository.FindUserByName(userName);

        if (usernameAlreadyExists == null)
        {
            userRepository.CreateNewUser(userName);
            loginUserName = userName;
            Console.WriteLine("User added successfully! Press any key to log in!");
            Console.ReadLine();
            isAccountCreated = true;
        }
        else
        {
            Console.WriteLine("Cannot add user with this name as it exists already! Please try again.");
        }
    }
}
else // Login
{
    var userName = "";
    bool isLoggedIn = false;

    while (!isLoggedIn)
    {
        do
        {
            Console.Write("Please insert account name: ");
            userName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userName))
            {
                Console.WriteLine("Username cannot be empty or whitespace. Please try again.");
            }

        } while (string.IsNullOrWhiteSpace(userName));

        var user = userRepository.FindUserByName(userName);
        if (user != null)
        {
            loginUserName = userName;
            Console.WriteLine("User found! Press any key to log in!");
            Console.ReadLine();
            isLoggedIn = true;
        }
        else
        {
            Console.WriteLine("THIS USER DOES NOT EXIST! Please try again.");
        }
    }
}


var menu = new MenuController(loginUserName, configRepository, gameRepository);
menu.GetMainMenu().Run();
