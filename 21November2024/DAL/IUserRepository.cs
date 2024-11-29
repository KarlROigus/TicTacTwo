namespace DAL;

public interface IUserRepository
{
    public void CreateUserOrLogin();

    public string GetUserName();
}