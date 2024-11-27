using Domain;
using GameBrain;

namespace DAL;

public interface IGameRepository
{
    public void SaveGame(string jsonStateString, string gameConfigName, string userName);
    
    public List<string> GetGamesThatCouldBeJoined(string username);

    public string GetFreeGameByIndex(int index, string userName);

    public void AddSecondPlayer(string userName, string gameName);

    public List<string> GetGamesImPartOf(string userName);

    public string GetSavedGameLastStateByIndex(int index, string userName);

    public string? GetPlayerName(string nameOfTheGame, string playerSign);

    public string GetChosenGameNameByIndex(int index, string userName);

}