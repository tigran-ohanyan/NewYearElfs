using UnityEngine;
using Zenject;
public class GameCacheManager : ICache
{
    public int Level { get; set; }
    public int Health { get; set; }
    public int Score { get; set; }
    public void Cache()
    {
        Level = 2;
    }
    [Inject] 
    public void Construct()
    {
        Level = 1;
        Health = 100;
        Score = 20;
        //Debug.Log($"Player Level = " + Level);
    }
}
