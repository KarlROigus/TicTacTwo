using System.Text.Json;
using Domain;
using GameBrain;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class GameRepositoryDb : IGameRepository
{
    
    public AppDbContext _context;

    public GameRepositoryDb()
    {
        var connectionString = $"Data Source={ConstantlyUsed.BasePath}app.db";
        
        
        var contextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(connectionString)
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .Options;

        _context = new AppDbContext(contextOptions);
    }
    
    
    public void SaveGame(string jsonStateString, string gameConfigName, GameConfiguration config)
    {
        var correctConfig = _context.Configs.FirstOrDefault(x => x.Name == config.Name);
        
        _context.GameStateJsons.Add(new GameStateJson()
        {
            Name = gameConfigName,
            GameStateJsonString = jsonStateString,
            Config = correctConfig
        });

        _context.SaveChanges();
    }

    public List<string> GetSavedGameNames()
    {
        return _context.GameStateJsons.Select(savedGame => savedGame.Name).ToList();
    }

    public GameState GetGameStateByIndex(int index)
    {
        var allSavedGameNames = GetSavedGameNames();
        var correctSavedGameName = allSavedGameNames[index];
        
        var gameStateJsonString = _context.GameStateJsons
            .Where(x => x.Name == correctSavedGameName)
            .Select(x => x.GameStateJsonString)
            .FirstOrDefault();

        if (gameStateJsonString != null)
        {
            return JsonSerializer.Deserialize<GameState>(gameStateJsonString)!;
        }
        
        throw new Exception(); // Should never happen
    }

    public void DeleteSavedGame(int index)
    {
        throw new NotImplementedException();
    }
}