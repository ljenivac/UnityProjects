using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject enemy;
    public float spawnRate = 2f;
    float randY;
    Vector2 whereToSpawn;
    public float initialSpawn = 0.0f;
    public float nextSpawn = 0.0f;
    // Start is called before the first frame update
    /*void Start()
    {
        StartCoroutine(Example());
    }

    IEnumerator Example()
    {
        print(Time.time);
        yield return new WaitForSeconds(8);
        //SceneManager.LoadScene("Menu");
    }*/
    void Start()
    {
        if (initialSpawn == 0)
            nextSpawn = 0;
        else
            nextSpawn = Time.time + initialSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            randY = Random.Range(-10.0f, 10.0f);
            whereToSpawn = new Vector2(transform.position.x, randY);
            Instantiate(enemy, whereToSpawn, Quaternion.identity);
        }
    }
}
