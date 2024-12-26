using System.Threading.Tasks;
public interface ISave
{
    PlayerData PlayerData { get; set; }
    Task SavePlayerData();
    Task LoadPlayerDataAsync();
}