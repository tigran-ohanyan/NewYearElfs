using System.Threading.Tasks;
public interface ISave
{
    PlayerData PlayerData { get; }
    void SavePlayerData();
    Task LoadPlayerDataAsync();
}