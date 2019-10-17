using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject enemy;
    public float spawnRate = 2f;
    float randY;
    Vector2 whereToSpawn;
    float nextSpawn = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            randY = Random.Range(-7.0f, 7.0f);
            whereToSpawn = new Vector2(transform.position.x, randY);
            Instantiate(enemy, whereToSpawn, Quaternion.identity);
        }
    }
}
