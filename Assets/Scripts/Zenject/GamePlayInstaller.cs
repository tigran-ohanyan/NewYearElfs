using UnityEngine;
using Zenject;
using System.Collections.Generic;

public class GamePlayInstaller : MonoInstaller
{
    [SerializeField] private Players[] _playersPrefabs;
    [SerializeField] private Transform[] _playersSpawnPoint;

    private List<Players> _players = new List<Players>();

    public override void InstallBindings()
    {
        DeviceInput();
        Container.Bind<UnityEngine.AI.NavMeshAgent>().FromComponentInHierarchy().AsSingle();
        Container.Bind<MovementHandler>().AsSingle().NonLazy();
        //InstantiatePlayers();

    }

    private void DeviceInput()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            Container.Bind<IInput>().To<MobileInput>().AsSingle().NonLazy();
        }
        else
        {
            Container.Bind<IInput>().To<DesktopInput>().AsSingle().NonLazy();
        }
    }

    public void Start()
    {
        InstantiatePlayers();
    }

    private void InstantiatePlayers()
    {
        for (int i = 0; i < _playersPrefabs.Length; i++)
        {
            Debug.Log($"Instantiating player {i}");
            var player = Container.InstantiatePrefabForComponent<Players>(
                _playersPrefabs[i],
                _playersSpawnPoint[i].position,
                Quaternion.identity,
                null
            );
            var navMeshAgent = player.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (navMeshAgent == null)
            {
                Debug.LogError($"Player {i} does not have a NavMeshAgent component!");
            }
            else
            {
                Container.Inject(navMeshAgent); // Инжект NavMeshAgent в контейнер
            }
            _players.Add(player); // Сохраняем игрока в список
        }
        BindPlayers();
    }

    private void BindPlayers()
    {
        for (int i = 0; i < _players.Count; i++)
        {
            var player = _players[i];
            /*var navMeshAgent = player.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (navMeshAgent != null)
            {
                Container.Bind<UnityEngine.AI.NavMeshAgent>().WithId(i).FromInstance(navMeshAgent).AsCached();
            }
            else
            {
                Debug.LogWarning($"Player {i} does not have a NavMeshAgent component!");
            }*/
            Container.Bind<IMovable>().WithId(i).FromInstance(player).AsCached();
            Container.Bind<Players>().WithId(i).FromInstance(player).AsCached();
        }
    }
}