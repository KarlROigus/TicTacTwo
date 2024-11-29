
using System.Security.Cryptography;
using System.Threading.Channels;
using Domain;
using GameBrain;
using Microsoft.EntityFrameworkCore;


namespace DAL;

public class GameRepositoryDb : IGameRepository
{
    
    private AppDbContext _database;

    public GameRepositoryDb(AppDbContext ctx)
    {
        _database = ctx;
    }
    
    public void SaveGame(string jsonStateString, string savedGameName, string userName)
    {

        var user = _database.Users.First(user => user.Username == userName);

        var gameId = _database.Games.FirstOrDefault(game => game.GameName == savedGameName);

        if (gameId == null)
        {
            _database.Games.Add(new Game()
            {
                GameName = savedGameName,
                PlayerXUserId = user.UserId
            });
            _database.SaveChanges();
        }
        
        var state = new State()
        {
            GameId = _database.Games.First(each => each.GameName == savedGameName).GameId,
            StateJson = jsonStateString
        };

        _database.States.Add(state);
        _database.SaveChanges();
        
    }

    public List<string> GetGamesThatCouldBeJoined(string myUsername)
    {
        
        var currentUser = _database.Users.First(user => user.Username == myUsername);
        var currentUserId = currentUser.UserId;

        var gamesWhereCanJoin = _database.Games.Where(game => game.PlayerOUserId == null)
            .Where(game => game.PlayerXUserId != currentUserId);

        var runner = new List<string>();

        foreach (var game in gamesWhereCanJoin)
        {
            runner.Add(game.GameName);
        }

        return runner;

    }

    public string GetFreeGameByIndex(int index, string userName)
    {
        var gamesThatCanBeJoined = GetGamesThatCouldBeJoined(userName);
        var correctGame = gamesThatCanBeJoined[index];

        return _database.Games.First(each => each.GameName == correctGame).GameName;

    }

    public void AddSecondPlayer(string userName, string gameName)
    {
        var selectedGame = _database.Games.First(each => each.GameName == gameName);
        var otherUserId = _database.Users.First(each => each.Username == userName).UserId;

        selectedGame.PlayerOUserId = otherUserId;
        _database.SaveChanges();
    }

    public List<string> GetGamesImPartOf(string userName)
    {
        var myId = _database.Users.First(each => each.Username == userName).UserId;

        var gameNames = _database.Games.Include(each => each.States)
            .Where(game => game.PlayerOUserId == myId || game.PlayerXUserId == myId)
            .Select(each => each.GameName)
            .ToList();

        return gameNames;
    }

    public string GetSavedGameLastStateByIndex(int index, string userName)
    {
        var gamesImPartOf = GetGamesImPartOf(userName);
        var correctGameName = gamesImPartOf[index];
        var chosenGame = _database.Games
            .Include(each => each.States)
            .First(each => each.GameName == correctGameName);

        
        var lastState = chosenGame.States!
            .OrderByDescending(state => state.StateId)
            .First();

        return lastState.StateJson;
    }

    public string? GetPlayerName(string nameOfTheGame, string playerSign)
    {
        if (playerSign == "X")
        {
            var correctGame = _database.Games.First(each => each.GameName == nameOfTheGame);

            var playerXId = correctGame.PlayerXUserId;

            var playerX = _database.Users.First(x => x.UserId == playerXId);

            return playerX.Username;
            
        } else 
        {
            var correctGame = _database.Games.First(each => each.GameName == nameOfTheGame);

            var playerOId = correctGame.PlayerOUserId;

            var playerO = _database.Users.FirstOrDefault(x => x.UserId == playerOId);

            return playerO?.Username;
        }

    }

    public string GetChosenGameNameByIndex(int index, string userName)
    {
        return GetGamesImPartOf(userName)[index];
    }

    public string GetGameByName(string gameName)
    {
        var correctGame = _database.Games
            .Include(each => each.States)
            .First(each => each.GameName == gameName);

        var lastState = correctGame.States!
            .OrderByDescending(state => state.StateId)
            .First();


        return lastState.StateJson;

    }
}