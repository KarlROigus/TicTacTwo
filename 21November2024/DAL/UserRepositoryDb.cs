using Domain;

namespace DAL;

public class UserRepositoryDb : IUserRepository
{

    private readonly AppDbContext _database;
    
    public UserRepositoryDb(AppDbContext ctx)
    {
        _database = ctx;
    }

    public string? FindUserByName(string name)
    {
        var user = _database.Users.FirstOrDefault(each => each.Username == name);
        return user?.Username;
    }

    public void CreateNewUser(string userName)
    {
        _database.Users.Add(new User()
        {
            Username = userName
        });
        _database.SaveChanges();
    }
    
    
}