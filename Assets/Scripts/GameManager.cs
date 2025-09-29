using UnityEngine;


public class GameManager : MonoBehaviour
{

    public static GameManager instance { get; private set; } //Esto sirve que para acceder (get) sea público y que cuando quiera cambiarlo es privado (private set)
    private int _stars = 0;
    [SerializeField] private GameObject _pauseCanvas;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void AddStar()
    {
        _stars++;
        Debug.Log("Estrellas recogicas: " + _stars);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        _pauseCanvas.SetActive(true);
    }
}
