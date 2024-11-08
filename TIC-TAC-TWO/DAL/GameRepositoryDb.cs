using System.Text.Json;
using Domain;
using GameBrain;
using Microsoft.EntityFrameworkCore;
using GameState = Domain.GameState;

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
    
    
    public void SaveGame(string jsonStateString, string savedGameName, GameConfiguration config)
    {
        var correctConfig = _context.Configs.FirstOrDefault(x => x.Name == config.Name);
        
        _context.GameStates.Add(new GameState()
        {
            Name = savedGameName,
            GameStateJsonString = jsonStateString,
            Config = correctConfig
        });

        _context.SaveChanges();
    }

    public List<string> GetSavedGameNames()
    {
        return _context.GameStates.Select(savedGame => savedGame.Name).ToList();
    }

    public GameBrain.GameState GetGameStateByIndex(int index)
    {
        var allSavedGameNames = GetSavedGameNames();
        var correctSavedGameName = allSavedGameNames[index];
        
        var gameStateJsonString = _context.GameStates
            .Where(x => x.Name == correctSavedGameName)
            .Select(x => x.GameStateJsonString)
            .FirstOrDefault()!;

        return JsonSerializer.Deserialize<GameBrain.GameState>(gameStateJsonString)!;
    }

    public void DeleteSavedGame(int index)
    {
        List<String> savedGameNames = GetSavedGameNames();
        var correctGameName = savedGameNames[index];
        var gameToBeDeleted = _context.GameStates.First(x => x.Name == correctGameName);

        _context.GameStates.Remove(gameToBeDeleted);
        _context.SaveChanges();
    }
}