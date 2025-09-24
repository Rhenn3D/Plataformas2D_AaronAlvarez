using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    private int _stars = 0;

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
    }

    public void AddStar()
    {
        _stars++;
        Debug.Log("Estrellas recogicas: " + _stars);
    }
}
