using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Cysharp.Threading.Tasks;

public class SceneLoadButton : MonoBehaviour
{
    private int sceneID;
    [SerializeField] private Button button;

    private ISceneLoader _sceneLoader;

    [Inject]
    public void Construct(ISceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }
    private ISave _save;

    [Inject]
    public void Construct(ISave save)
    {
        _save = save;
    }

    private void Start()
    {
        
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnButtonClick);
        }
    }

    private async void OnButtonClick()
    {
        Debug.LogError($"SceneID = {_save.PlayerData.SceneID}");
        sceneID = _save.PlayerData.SceneID;
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