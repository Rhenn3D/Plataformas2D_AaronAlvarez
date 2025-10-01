using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    [SerializeField] private GameObject _loadingCanvas;
    [SerializeField] private Image _loadingBar;
    private Animator _animator;


    void Awake()
    {
        _animator = GetComponent<Animator>();
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    public void ChangeScene(string sceneName)
    {
        StartCoroutine(LoadNewScene(sceneName));
    }

    IEnumerator LoadNewScene(string sceneName)
    {
        yield return null;

        _loadingCanvas.SetActive(true);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        asyncLoad.allowSceneActivation = false;

        float fakeLoadPercentage = 0f;


        while (!asyncLoad.isDone)
        {
            //_loadingBar.fillAmount = asyncLoad.progress;
            _animator.SetBool("IsLoading", true);

            fakeLoadPercentage += 0.001f;
            _loadingBar.fillAmount = fakeLoadPercentage;

            if (asyncLoad.progress >= 0.9f && fakeLoadPercentage >= 0.99f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }

        _loadingCanvas.SetActive(false);
    }
}
