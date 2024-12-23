using UnityEngine;

public interface IAudio
{
    void EnableGroup(string groupParameter);
    void DisableGroup(string groupParameter);
    bool GetGroupStatus(string groupParameter);
}
