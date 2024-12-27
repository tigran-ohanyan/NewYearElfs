using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Cysharp.Threading.Tasks;

public class SceneLoader : ISceneLoader
{
    private readonly Animator _loadingAnimator;
    private readonly string _loadingAnimationKey = "Start";

    public SceneLoader(Animator loadingAnimator)
    {
        _loadingAnimator = loadingAnimator;
    }

    public async UniTask LoadSceneAsync(int sceneID)
    {
        if (_loadingAnimator != null)
        {
            _loadingAnimator.SetTrigger(_loadingAnimationKey);
            await UniTask.Delay(2000);
        }
        else
        {
            Debug.LogWarning("No animator attached to this scene");
        }
        
        var loadOperation = SceneManager.LoadSceneAsync(sceneID);
        while (!loadOperation.isDone)
        {
            await UniTask.Yield();
        }

        if (_loadingAnimator != null)
        {
            _loadingAnimator.SetTrigger("End");
        }
    }
}