using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour
{
    public float moveSpeed = 0.8f;
    private readonly float boundPosition = -19.52f;



    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x - (moveSpeed * Time.deltaTime), transform.position.y, transform.position.z);
        if(transform.position.x <= boundPosition)
        {
            transform.position = new Vector3(-boundPosition, transform.position.y, transform.position.z);
        }
    }
}
