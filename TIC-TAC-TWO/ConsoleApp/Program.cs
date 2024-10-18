using System.Text.Json;
using ConsoleApp;
using GameBrain;

var menuController = new MenuController();
menuController.GetMainMenu().Run();


//TODO: implement reset gmae functionality
//TODO: winner announce when playing itself to a losing position
//TODO: saving game everywhere..? or only during thinking pause?
//TODO: quiting game everywhere??
