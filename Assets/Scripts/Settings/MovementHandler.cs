using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class MovementHandler : IInitializable, IDisposable, ITickable
{
    private IInput _input;
    private List<IMovable> _movables;
	private Camera _mainCamera;
	private UnityEngine.AI.NavMeshAgent _navMeshAgent;
	private UnityEngine.AI.NavMeshAgent _selectedNavMeshAgent; // Выбранный игрок
	private GameObject _selectedPlayer;
	private float _cameraEdgeThreshold = 20f; // Расстояние от края экрана для перемещения камеры
	private float _cameraSpeed = 10f; // Скорость перемещения камеры
    [Inject]
    public void Construct(IInput input, List<IMovable> movable, UnityEngine.AI.NavMeshAgent navMeshAgent)
    {
        _input = input;
        _movables = movable;
        _navMeshAgent = navMeshAgent;
        Debug.Log($"input = {_input.GetType()}");
        Debug.Log($"movable = {_movables.GetType()}");
        
        
    }
	
	private void Start(){
		_mainCamera = Camera.main;
	}

	public void Initialize()
	{
		_input.ClickDown += OnClickDown;
	}

	public void Dispose()
	{
		_input.ClickDown -= OnClickDown;
	}

	public void Tick()
	{
		UpdateCamera();
	}

	private void OnClickDown(Vector3 position)
	{
		Ray ray = _mainCamera.ScreenPointToRay(position);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit))
		{
			Debug.Log($"Raycast hit: {hit.collider.name}"); // Отладка
			if (hit.collider.CompareTag("Player")) // Проверка тега
			{
				_selectedPlayer = hit.collider.gameObject;
				_selectedNavMeshAgent = _selectedPlayer.GetComponent<UnityEngine.AI.NavMeshAgent>();

				if (_selectedNavMeshAgent != null)
				{
					Debug.Log($"Selected player: {_selectedPlayer.name}");
					ShowActionUI(true); // Включить UI действий
				}
				else
				{
					Debug.LogError("Selected object does not have a NavMeshAgent!");
				}
			}
			else
			{
				Debug.Log("Raycast hit object is not a player.");
			}
		}
		else
		{
			Debug.Log("Raycast did not hit any object.");
		}
	}
	private void SelectPlayer(GameObject player)
	{
		_selectedPlayer = player;
		_selectedNavMeshAgent = player.GetComponent<UnityEngine.AI.NavMeshAgent>();

		if (_selectedNavMeshAgent == null)
		{
			Debug.LogWarning("Selected player does not have a NavMeshAgent component.");
			return;
		}

		ShowActionUI(true);
		Debug.Log($"Player selected: {_selectedPlayer.name}");
	}

	private void MoveSelectedPlayer(Vector3 destination)
	{
		_selectedNavMeshAgent.SetDestination(destination);
		Debug.Log($"Moving {_selectedPlayer.name} to {destination}");
	}

	private void UpdateCamera()
	{
		Vector3 direction = Vector3.zero;

		if (Input.mousePosition.x < _cameraEdgeThreshold)
		{
			direction += Vector3.left;
		}
		else if (Input.mousePosition.x > Screen.width - _cameraEdgeThreshold)
		{
			direction += Vector3.right;
		}

		if (Input.mousePosition.y < _cameraEdgeThreshold)
		{
			direction += Vector3.back;
		}
		else if (Input.mousePosition.y > Screen.height - _cameraEdgeThreshold)
		{
			direction += Vector3.forward;
		}

		if (direction != Vector3.zero)
		{
			_mainCamera.transform.Translate(direction.normalized * _cameraSpeed * Time.deltaTime, Space.World);
		}
	}

	private void ShowActionUI(bool show)
	{
		Debug.Log("UI ON!");
		//_uiActionPanel.gameObject.SetActive(show);
	}
}
