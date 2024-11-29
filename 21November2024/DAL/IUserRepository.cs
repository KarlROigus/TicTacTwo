namespace DAL;

public interface IUserRepository
{
    
    public string? FindUserByName(string name);

    public void CreateNewUser(string userName);

}