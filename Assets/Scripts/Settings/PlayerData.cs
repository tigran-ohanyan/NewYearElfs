using System;

[Serializable]
public class PlayerData
{
    public int PlayerLevel { get; set; }
    public int Health { get; set; }
    public int Score { get; set; }
    public int SceneID { get; set; } = 1;

}