using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    PlayerHealth playerhealth;

    [Header("Settings")]
    public GameObject EnemyPrefab;
    public Transform[] SpawnPoints;

    [Header("Wave Info")]
    public int CurrentWave = 1;
    public int EnemiesPerWave = 3;
    private int _enemiesRemaining;

    [Header("UI References")]
    public GameObject GameOverPanel;
    public GameObject VictoryPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
           
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartNextWave();
    }

    public void StartNextWave()
    {
        int countToSpawn = EnemiesPerWave + (CurrentWave - 1) * 2;
        _enemiesRemaining = countToSpawn;

        Debug.Log($"Start Wave {CurrentWave} with {countToSpawn} enemies");

        for(int i = 0; i< countToSpawn; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, SpawnPoints.Length);
        Transform spawnPoint = SpawnPoints[randomIndex];
        Instantiate(EnemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public void OnEnemyKilled()
    {
        _enemiesRemaining--;
        Debug.Log($"{_enemiesRemaining} enemies remaining");

        if( _enemiesRemaining <= 0)
        {
            CurrentWave++;
            Invoke(nameof(StartNextWave), 4f);
        }

        if( CurrentWave % 5 == 0)
        {
            playerhealth.Heal(10);
        }
    }

    public void GameOver()
    {
        Debug.Log("Game over");
        if(GameOverPanel != null)
        {
            GameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
