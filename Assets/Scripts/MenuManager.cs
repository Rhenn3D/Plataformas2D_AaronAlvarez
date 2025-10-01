using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void SceneChanger(string sceneName)
    {
        SceneLoader.Instance.ChangeScene(sceneName);
    }
}
