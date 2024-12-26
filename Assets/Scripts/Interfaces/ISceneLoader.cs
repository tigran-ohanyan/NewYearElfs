using Cysharp.Threading.Tasks;

public interface ISceneLoader
{
    UniTask LoadSceneAsync(int sceneID);
}