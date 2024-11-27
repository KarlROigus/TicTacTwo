using System.Text.Json;
using System.Text.Json.Nodes;
using Domain;

namespace DAL;

public class GameRepositoryJson : IGameRepository
{
    public void SaveGame(string jsonStateString, string savedGameName, string userName)
    {
        var fileName = ConstantlyUsed.BasePath + savedGameName + ConstantlyUsed.GameExtension;

        var gameinfo = new GameInfoDto()
        {
            GameName = savedGameName,
            GameStateJson = jsonStateString,
            PlayerX = userName,
        };

        var serializedGameinfo = JsonSerializer.Serialize(gameinfo);
        
        File.WriteAllText(fileName, serializedGameinfo);
        
    }

    public List<string> GetGamesThatCouldBeJoined(string username)
    {
        var answer = new List<string>();
        
        foreach (var fullFileName in Directory.GetFiles(ConstantlyUsed.BasePath, "*" + ConstantlyUsed.GameExtension).ToList())
        {
            var fileData = File.ReadAllText(fullFileName);

            var gameInfoDto = JsonSerializer.Deserialize<GameInfoDto>(fileData)!;

            if (gameInfoDto.PlayerO == null && gameInfoDto.PlayerX != username)
            {
                answer.Add(gameInfoDto.GameName);
            }
        }
        
        return answer;

    }

    public string GetFreeGameByIndex(int index, string userName)
    {
        return GetGamesThatCouldBeJoined(userName)[index];
    }

    public void AddSecondPlayer(string userName, string gameName)
    {
        foreach (var fullFileName in Directory.GetFiles(ConstantlyUsed.BasePath, "*" + ConstantlyUsed.GameExtension)
                     .ToList())
        {
            var twoParts = Path.GetFileNameWithoutExtension(fullFileName);
            var mainNamePart = Path.GetFileNameWithoutExtension(twoParts);
            if (mainNamePart == gameName)
            {
                var fileData = File.ReadAllText(fullFileName);
                var gameInfoDto = JsonSerializer.Deserialize<GameInfoDto>(fileData)!;
                gameInfoDto.PlayerO = userName;
                
                var reSerialized = JsonSerializer.Serialize(gameInfoDto);
                File.WriteAllText(fullFileName, reSerialized);
                
            }
        }
    }

    public List<Game> GetGamesImPartOf(string userName)
    {
        throw new NotImplementedException();
    }

    public Game GetSavedGameByIndex(int index, string userName)
    {
        throw new NotImplementedException();
    }

    public string? GetPlayerName(string nameOfTheGame, string playerSign)
    {
        throw new NotImplementedException();
    }
}