using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform spawnPoint;
    //public TMP_Text label;
    private List<GameObject> _spawnedEnemies;
    public CinemachineVirtualCamera virtualCamera;
    private List<GameObject> spawnedMaps = new List<GameObject>();

    public GameObject gameOverUI;
    public GameObject player;
    public GameObject healthBar;
    public GameObject[] maps;
    public List<GameObject> enemyPrefabs;
    public int maxEnemiesSpawned;

    // Start is called before the first frame update
    void Start()
    {
        gameOverUI.SetActive(false);

        _spawnedEnemies = new List<GameObject>();

        SpawnMaps();
        SpawnPlayer();
        SpawnEnemies();
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

    private void SpawnMaps()
    {
        for (int i = 0; i < maps.Length; i++)
        {
            var instance = Instantiate(maps[i], maps[i].transform);

            spawnedMaps.Add(instance);
        }
    }

    private void SpawnPlayer()
    {
        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacterIndex");
        GameObject prefab = characterPrefabs[selectedCharacter];

        var playerInstance = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        virtualCamera.Follow = playerInstance.transform;
        playerInstance.GetComponent<Damageable>().healthBar = healthBar;
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < maxEnemiesSpawned; i++)
        {
            var randomPoint = Random.insideUnitSphere;
            var spawnPoint = new Vector3(randomPoint.x, 0, randomPoint.z) * 50;
            var prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
            var enemy = Instantiate(prefab, spawnPoint, transform.rotation);
            _spawnedEnemies.Add(enemy);
        }
    }

    private void RestartScene()
    {
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
    }
}
