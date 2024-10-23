using GameBrain;

namespace DAL;

public interface IGameRepository
{
    public void SaveGame(string jsonStateString, string gameConfigName);

    public List<string> GetSavedGameNames();

    GameState GetGameStateByIndex(int index);

    void DeleteSavedGame(int index);
}