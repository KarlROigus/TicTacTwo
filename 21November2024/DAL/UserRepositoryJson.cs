namespace DAL;

public class UserRepositoryJson : IUserRepository
{
    

    public string? FindUserByName(string name)
    {
        var folders = Directory.GetDirectories(ConstantlyUsed.UserFolderPath);
        foreach (var folder in folders)
        {
            var folderName = Path.GetFileName(folder);
            if (folderName == name)
            {
                return name;
            }
        }

        return null;
    }

    public void CreateNewUser(string userName)
    {
        Directory.CreateDirectory(ConstantlyUsed.UserFolderPath + userName);
    }
    
}