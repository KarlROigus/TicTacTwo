using System.Text.Json;
using System.Text.Json.Nodes;
using Domain;
using GameBrain;

namespace DAL;

public class GameRepositoryJson : IGameRepository
{
    public void SaveGame(string jsonStateString, string savedGameName, string userName)
    {
        
        var fileName = ConstantlyUsed.BasePath + savedGameName + ConstantlyUsed.GameExtension;
        if (File.Exists(fileName))
        {
            var fullFileName = ConstantlyUsed.BasePath + savedGameName + ConstantlyUsed.GameExtension;
            var fileData = File.ReadAllText(fullFileName);
            var gameInfoDto = JsonSerializer.Deserialize<GameInfoDto>(fileData)!;
            
            if (gameInfoDto.PlayerO == null && userName != gameInfoDto.PlayerX)
            {
                gameInfoDto.PlayerO = userName;
            }

            var updatedGameInfoDto = new GameInfoDto()
            {
                GameName = savedGameName,
                GameStateJson = jsonStateString,
                PlayerX = gameInfoDto.PlayerX,
                PlayerO = gameInfoDto.PlayerO
            };
            
            File.WriteAllText(fileName, JsonSerializer.Serialize(updatedGameInfoDto));
        }
        else
        {

            var gameinfo = new GameInfoDto()
            {
                GameName = savedGameName,
                GameStateJson = jsonStateString,
                PlayerX = userName,
            };
            var serializedGameinfo = JsonSerializer.Serialize(gameinfo);
            File.WriteAllText(fileName, serializedGameinfo);
        }
        
       
        
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
        
        var fullFileName = ConstantlyUsed.BasePath + gameName + ConstantlyUsed.GameExtension;
        var fileData = File.ReadAllText(fullFileName);
        var gameInfoDto = JsonSerializer.Deserialize<GameInfoDto>(fileData)!;
        gameInfoDto.PlayerO = userName;
                
        var reSerialized = JsonSerializer.Serialize(gameInfoDto);
        File.WriteAllText(fullFileName, reSerialized);
        
    }

    public List<string> GetGamesImPartOf(string userName)
    {
        var runner = new List<string>();
        
        foreach (var fullFileName in Directory.GetFiles(ConstantlyUsed.BasePath, "*" + ConstantlyUsed.GameExtension)
                     .ToList())
        {
            var fileData = File.ReadAllText(fullFileName);
            var gameInfoDto = JsonSerializer.Deserialize<GameInfoDto>(fileData)!;
            
            if (gameInfoDto.PlayerX == userName || gameInfoDto.PlayerO == userName)
            {
                runner.Add(gameInfoDto.GameName);
            }
            
        }
        
        return runner;
    }

    public string GetSavedGameLastStateByIndex(int index, string userName)
    {
        var gamesImPartOf = GetGamesImPartOf(userName);
        var correctGameName = gamesImPartOf[index];
        
        foreach (var fullFileName in Directory.GetFiles(ConstantlyUsed.BasePath, "*" + ConstantlyUsed.GameExtension)
                     .ToList())
        {
            var twoParts = Path.GetFileNameWithoutExtension(fullFileName);
            var mainNamePart = Path.GetFileNameWithoutExtension(twoParts);
            if (mainNamePart == correctGameName)
            {
                var fileData = File.ReadAllText(fullFileName);
                var gameInfoDto = JsonSerializer.Deserialize<GameInfoDto>(fileData)!;
                return gameInfoDto.GameStateJson;

            }
        }

        return "-1"; //Should never happen

    }

    public string? GetPlayerName(string nameOfTheGame, string playerSign)
    {
        foreach (var fullFileName in Directory.GetFiles(ConstantlyUsed.BasePath, "*" + ConstantlyUsed.GameExtension)
                     .ToList())
        {
            var twoParts = Path.GetFileNameWithoutExtension(fullFileName);
            var mainNamePart = Path.GetFileNameWithoutExtension(twoParts);
            if (mainNamePart == nameOfTheGame)
            {
                var fileData = File.ReadAllText(fullFileName);
                var gameInfoDto = JsonSerializer.Deserialize<GameInfoDto>(fileData)!;

                return playerSign == "X" ? gameInfoDto.PlayerX : gameInfoDto.PlayerO;
            }
        }

        return null; // Should never happen

    }

    public string GetChosenGameNameByIndex(int index, string userName)
    {
        return GetGamesImPartOf(userName)[index];
    }
}