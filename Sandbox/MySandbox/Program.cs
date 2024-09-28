// See https://aka.ms/new-console-template for more information


using MenuSystem;


var deepMenu = new Menu(EMenuLevel.Deep, "TIC-TAC-TOE DEEP", new Dictionary<string, MenuItem>
{
    {"Y", new MenuItem()
    {
        Title = "YYYYYY",
        Shortcut = "Y",
        MenuItemAction = null
    }},
});


var optionsMenu = new Menu(EMenuLevel.Secondary, "TIC-TAC-TOE OPTIONS", new Dictionary<string, MenuItem>
{
    {"X", new MenuItem()
    {
        Title = "X Starts",
        Shortcut = "X",
        MenuItemAction = deepMenu.Run
    }},
    {"O", new MenuItem()
    {
        Title = "O Starts",
        Shortcut = "O",
        MenuItemAction = null
    }}
});



var mainMenu = new Menu(EMenuLevel.Main, "TIC-TAC-TOE", new Dictionary<string, MenuItem>
{
    {"O", new MenuItem()
    {
        Title = "Options",
        Shortcut = "O",
        MenuItemAction = optionsMenu.Run
    }},
    {"N", new MenuItem()
    {
        Title = "New game",
        Shortcut = "N",
        MenuItemAction = null
    }}
});

mainMenu.Run();

return;


// ==================================

