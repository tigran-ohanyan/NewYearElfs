using System;

[Serializable]
public class PlayerData
{
    public int PlayerLevel { get; set; }
    public int Health { get; set; }
    public int Score { get; set; }
    
    public PlayerData(int playerLevel = 1, int health = 100, int score = 0)
    {
        PlayerLevel = playerLevel;
        Health = health;
        Score = score;
    }
}