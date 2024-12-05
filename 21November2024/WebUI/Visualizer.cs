using GameBrain;

namespace WebUI;

public class Visualizer
{
    public static string DrawBoard(TicTacTwoBrain gameInstance)
    {
        var html = "";
        
        for (int y = 0; y < gameInstance.DimY; y++)
        {
            html += "<tr>";
            for (int x = 0; x < gameInstance.DimX; x++)
            {
                html += "<td>";
                html += gameInstance.GameBoard[y][x].GetSpotValue();
                html += "</td>";
            }

            html += "</tr>";
        }

        return html;
    }
}