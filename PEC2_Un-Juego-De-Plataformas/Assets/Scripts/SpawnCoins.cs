using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoins : MonoBehaviour
{

    public GameObject coinPrefab;
    public int minSpawnRange = 3;
    public int maxSpawnRange = 5;
    public float minSpreadRange = 1.0f;
    public float maxSpreadRange = 1.5f;
    public bool open = false;

    private void Update()
    {
        if (open)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        open = false;
        gameObject.GetComponent<Animator>().SetTrigger("OpenChest");
        for (int i = 0; i < Random.Range(minSpawnRange, maxSpawnRange); i++)
        {
            GameObject coin = Instantiate(coinPrefab, transform);
            var xForce = Random.Range(5f, 7f);
            var yForce = Random.Range(5f, 7f);
            Vector2 force = new Vector2(xForce, yForce);
            coin.GetComponent<Rigidbody2D>().AddRelativeForce(force);
        }
    }

}
