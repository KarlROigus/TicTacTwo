namespace DAL;

public class UserRepositoryJson
{

    private string _userName = default!;
    
    public string GetUserName()
    {
        return _userName;
    }

    public void CreateUserOrLogin()
    {
        if (!Directory.Exists(ConstantlyUsed.UserFolderPath))
        {
            Directory.CreateDirectory(ConstantlyUsed.UserFolderPath);
        }
        
        string userChoice;
        
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
            bool successfulAccountAdded = false;

            while (!successfulAccountAdded)
            {
                successfulAccountAdded = CreateNewAccount();
            }
        }
        else 
        {
            LogInToExistingAccount();
        }
    }

    private void LogInToExistingAccount()
    {
        bool validUser = false;

        while (!validUser)
        {
            Console.Write("Please insert your username: ");
            var userName = Console.ReadLine();
            userName = userName?.ToLower();
                
            var folders = Directory.GetDirectories(ConstantlyUsed.UserFolderPath);
            foreach (var folder in folders)
            {
                var folderName = Path.GetFileName(folder);
                if (folderName == userName)
                {
                    Console.WriteLine("User found! Press any key to log in!");
                    Console.ReadLine();
                    validUser = true;
                    _userName = userName;
                }
            }
            Console.WriteLine("User not found. Please try again.");
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

        var usernameAlreadyExists = UsernameAlreadyExists(userName);

        switch (usernameAlreadyExists)
        {
            case true:
                Console.WriteLine("Cannot add user with this name as it exists already!");
                return false;
            default:
                AddNewUser(userName);
                _userName = userName;
                return true;
        }
        
    }

    private bool UsernameAlreadyExists(string username)
    {
        var folders = Directory.GetDirectories(ConstantlyUsed.UserFolderPath);
        return folders.Select(folder => Path.GetFileName(folder)).Any(folderName => folderName == username);
    }

    private void AddNewUser(string username)
    {
        Directory.CreateDirectory(ConstantlyUsed.UserFolderPath + username);
    }
}