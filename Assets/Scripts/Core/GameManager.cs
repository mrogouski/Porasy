using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private List<GameObject> _spawnedEnemies;

    public GameObject gameOverUI;
    public GameObject player;
    //public Button button;

    public Transform[] spawnPoints;
    public List<GameObject> enemyPrefabs;
    public int maxEnemiesSpawned;

    // Start is called before the first frame update
    void Start()
    {
        gameOverUI.SetActive(false);

        _spawnedEnemies = new List<GameObject>();
        InvokeRepeating("SpawnEnemies", 10, 15);

        //button.onClick.AddListener(RestartScene);
    }

    // Update is called once per frame
    void Update()
    {
        var playerStats = player.GetComponent<Damageable>();
        if (playerStats.currentHealth <= 0)
        {
            gameOverUI.SetActive(true);
        }
    }

    private void SpawnEnemies()
    {
        if (_spawnedEnemies.Count < maxEnemiesSpawned)
        {
            var points = new List<Transform>(spawnPoints);
            foreach (var point in points)
            {
                var enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], point);
                _spawnedEnemies.Add(enemy);
            }
        }
    }

    private void RestartScene()
    {
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
    }
}
