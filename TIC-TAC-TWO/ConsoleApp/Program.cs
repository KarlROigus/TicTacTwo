using System.Text.Json;
using ConsoleApp;
using GameBrain;

var game = new Game();
game.Run();

//     string json = @"{
//             ""Name"": ""Default TIC-TAC-TWO"",
//             ""BoardWidth"": 5,
//             ""BoardHeight"": 5,
//             ""GridHeight"": 3,
//             ""GridWidth"": 3,
//             ""WinCondition"": 3,
//             ""HowManyMovesTillAdvancedGameMoves"": 2,
//             ""Grid"": {
//                 ""MiddlePointX"": 2,
//                 ""MiddlePointY"": 2,
//                 ""BigBoardSize"": 5,
//                 ""GridLength"": 3
//             }
//         }";
//     
//     string json2 = @"{
//     ""Name"": ""Classical TIC-TAC-TOE"",
//     ""BoardWidth"": 3,
//     ""BoardHeight"": 3,
//     ""GridHeight"": 3,
//     ""GridWidth"": 3,
//     ""WinCondition"": 3,
//     ""HowManyMovesTillAdvancedGameMoves"": -1,
//     ""Grid"": {
//         ""MiddlePointX"": 1,
//         ""MiddlePointY"": 1,
//         ""BigBoardSize"": 3,
//         ""GridLength"": 3
//     }
// }";
//
//
//     GameConfiguration gameConfig = JsonSerializer.Deserialize<GameConfiguration>(json);
//     Console.WriteLine(gameConfig);
//     Console.WriteLine(gameConfig.Grid);
//     
//     GameConfiguration gameConfig2 = JsonSerializer.Deserialize<GameConfiguration>(json2);
//     Console.WriteLine(gameConfig2);
//     Console.WriteLine(gameConfig2.Grid); 

