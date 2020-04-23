using System.Collections.Generic;

namespace Objects.Scripts
{
    public class Settings
    {
        static public string ComputerName = "COMPUTER"; // white pieces
        static public string PlayerName = "PLAYER"; // black pieces
        
        static public int PlayerScore = 2;
        static public int ComputerScore = 2;

        static public int turnOrder = 0; // defaults to player going first
        static public int maxDepth = 1; // defaults to level 1
        static public string currentPlayer;
    }
}