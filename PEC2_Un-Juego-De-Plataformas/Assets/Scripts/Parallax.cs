using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    private float length, startPosition;
    public GameObject cameraObject;
    public float parallaxEffect;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float currentPosition = cameraObject.transform.position.x * (1 - parallaxEffect);
        float dist = cameraObject.transform.position.x * parallaxEffect;
        transform.position = new Vector3(startPosition + dist, transform.position.y, transform.position.z);

        if(currentPosition > startPosition + length)
        {
            startPosition += length;
        }
    }
}
