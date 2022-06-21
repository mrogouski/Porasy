using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float interval;
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", interval, delay);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void SpawnEnemy()
    {
        //float xPosition =  Random.RandomRange()
        Vector3 position = Camera.main.ViewportToWorldPoint(new Vector3(1.1f, 0, 1.1f));
        position.y = 0;
        Instantiate(enemy, position, enemy.transform.rotation);
    }
}
