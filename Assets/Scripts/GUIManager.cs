using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public GameObject _pauseCanvas;
    public static GUIManager Instance;

    [SerializeField] private Image _healthBar;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void ChangeCanvasStatus(GameObject canvas, bool status)
    {
        canvas.SetActive(status);
    }

    public void Resume()
    {
        GameManager.instance.Pause();
    }

    public void UpdateHealthBar(float _currentHealth, float vidaMax)
    {
        _healthBar.fillAmount = _currentHealth / vidaMax;
    }
    
    public void SceneChanger(string sceneName)
    {
        SceneLoader.Instance.ChangeScene(sceneName);
    }
}
