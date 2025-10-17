using UnityEngine;

public class ParalaxBackground : MonoBehaviour
{
    private float startPosition, length;
    [SerializeField]private GameObject cam;
    [SerializeField]private float parallaxEffect;

    void Start()
    {
        startPosition = transform.position.x;
    }

    void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxEffect;
        float movement = cam.transform.position.x * (1 - parallaxEffect);


        transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);

        if (movement > startPosition + length)
        {
            startPosition += length;
        }
        else if (movement < startPosition - length)
        {
            startPosition -= length; 
        }
        
    }
}
