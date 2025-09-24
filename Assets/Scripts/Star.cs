using UnityEngine;

public class Star : MonoBehaviour
{

    GameManager _gameManager;


    void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void Interaction()
    {
        _gameManager.AddStar();
        Destroy(gameObject, 0.5f);
    }
}
