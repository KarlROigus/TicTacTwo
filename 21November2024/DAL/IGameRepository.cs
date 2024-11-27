using Domain;
using GameBrain;

namespace DAL;

public interface IGameRepository
{
    public void SaveGame(string jsonStateString, string gameConfigName, string userName);
    
    public List<string> GetGamesThatCouldBeJoined(string username);

    public string GetFreeGameByIndex(int index, string userName);

    public void AddSecondPlayer(string userName, string gameName);

    public List<Game> GetGamesImPartOf(string userName);

    public Game GetSavedGameByIndex(int index, string userName);

    public string? GetPlayerName(string nameOfTheGame, string playerSign);

}