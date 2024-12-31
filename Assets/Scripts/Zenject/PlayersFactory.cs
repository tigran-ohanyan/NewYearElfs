using UnityEngine;
using Zenject;

public class PlayersFactory
{
    private readonly DiContainer _container;

    public PlayersFactory(DiContainer container)
    {
        _container = container;
    }

    public Players Create(Players prefab, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        var player = _container.InstantiatePrefabForComponent<Players>(prefab, position, rotation, parent);

        // Получение NavMeshAgent
        var navMeshAgent = player.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent not found on instantiated Player prefab!");
        }
        else
        {
            // Инжект NavMeshAgent если требуется
            _container.Inject(navMeshAgent);
        }

        return player;
    }
}