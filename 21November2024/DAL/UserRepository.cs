using Domain;

namespace DAL;

public class UserRepository
{

    private AppDbContext _database;

    private string _userName;
    
    public UserRepository(AppDbContext ctx)
    {
        _database = ctx;
    }

    public string GetUserName()
    {
        return _userName;
    }
    
    
    public void CreateUserOrLogin()
    {
        
        Console.WriteLine("Hello!");
        Console.WriteLine("1) LOG IN");
        Console.WriteLine("2) CREATE NEW ACCOUNT");
        Console.Write("Please insert your choice: ");
        var choice = Console.ReadLine();

        if (choice == "2")
        {
            bool successfulAccountAdded = false;

            while (!successfulAccountAdded)
            {
                successfulAccountAdded = CreateNewAccount();
            }
            
        }
        else
        {
            Console.Write("Please insert your username: ");
            var userName = Console.ReadLine();
            
            var user = _database.Users.FirstOrDefault(user => user.Username == userName);
            
            if (user != null)
            {
                _userName = user.Username;
            }
            else
            {
                Console.WriteLine("I dont have this type of user!");
                
            }
        }

    }

    private bool CreateNewAccount()
    {
        var userName = "";
        
        do
        {
            Console.Write("Please insert account name: ");
            userName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userName))
            {
                Console.WriteLine("Username cannot be empty or whitespace. Please try again.");
            }

        } while (string.IsNullOrWhiteSpace(userName));

        userName = userName.ToLower();

        var userNameExistsAlready = _database.Users.Any(user => user.Username == userName);

        if (userNameExistsAlready)
        {
            Console.WriteLine("Cannot add user with this name as it exists already!");
            return false;
        }
        
        _database.Users.Add(new User()
        {
            Username = userName
        });
        _database.SaveChanges();
        
        Console.Clear();
        Console.WriteLine("User saved successfully! Press Enter to continue!");
        Console.ReadLine();

        _userName = userName;
        return true;
    }
}