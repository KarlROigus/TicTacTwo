
using ConsoleApp;
using DAL;


// MenuController.GetMainMenu().Run();


//TODO: implement reset game functionality
//TODO: winner announce when playing itself to a losing position
//TODO: saving game everywhere..? or only during thinking pause?
//TODO: quiting game everywhere??
//TODO: Drawing the game logic

//TODO: making a new configuration returns what..?? string or can it be void?

//TODO: error check for loading a saved game


//TODO: ASK ANDRES. BLUE TERMINAL BACKGROUND


var configRepoDB = new ConfigRepositoryDB();

Console.WriteLine(configRepoDB._context.Configs.Count());

configRepoDB.GetConfigurationNames();

foreach (var config in configRepoDB._context.Configs)
{
    Console.WriteLine(config.ConfigJsonString);
}





