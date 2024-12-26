using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Cysharp.Threading.Tasks;

public class SceneLoadButton : MonoBehaviour
{
    private int sceneID;
    [SerializeField] private Button button; // this.. continue

    private ISceneLoader _sceneLoader;

    [Inject]
    public void Construct(ISceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    private void Start()
    {
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    private async void OnButtonClick()
    {
        if (sceneID >= 0)
        {
            await _sceneLoader.LoadSceneAsync(sceneID);
        }
        else
        {
            Debug.LogError($"Invalid scene ID! {sceneID}");
        }
    }
}