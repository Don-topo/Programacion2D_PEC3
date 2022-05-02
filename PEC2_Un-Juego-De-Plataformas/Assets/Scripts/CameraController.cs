using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float xOffset = 5f;
    public float yOffset = 5f;
    public float cameraSpeed = 2f;

    // Update is called once per frame
    void Update()
    {
        var newPosition = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffset, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, cameraSpeed * Time.deltaTime);
    }
}
