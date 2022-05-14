using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{

    public GameObject helpGameObject;
    public int nextLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            helpGameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            helpGameObject.SetActive(false);
        }
    }

    public void EndLevel()
    {
        GameManager.Instance.LevelCompleted(nextLevel);
    }
}
