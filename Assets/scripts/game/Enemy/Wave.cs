using System.Collections;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject _zombiePrefab; // Prefab of the zombie to spawn
    [SerializeField] private Transform _player; // Reference to the player's transform
    [SerializeField] private float _spawnRadius = 5f; // Radius around the player to spawn zombies
    [SerializeField] private float _initialSpawnInterval = 10f; // Time between spawns (in seconds)
    [SerializeField] private int _initialZombieCount = 10; // Initial number of zombies to spawn
    [SerializeField] private int _maxZombieCount = 100; // Maximum number of zombies to spawn

    private int _currentZombieCount; // Current number of zombies to spawn

    private void Start()
    {
        // Check for missing references
        if (_zombiePrefab == null)
        {
            Debug.LogError("Zombie prefab is not assigned!", this);
            enabled = false; // Disable the script if the prefab is missing
            return;
        }

        if (_player == null)
        {
            Debug.LogError("Player reference is not assigned!", this);
            enabled = false; // Disable the script if the player is missing
            return;
        }

        // Initialize the zombie count
        _currentZombieCount = _initialZombieCount;

        // Start the spawning coroutine
        StartCoroutine(SpawnZombies());
    }

    private IEnumerator SpawnZombies()
    {
        // Initial delay before spawning starts
        yield return new WaitForSeconds(5f);

        while (true) // Infinite loop for continuous spawning
        {
            // Spawn zombies around the player
            for (int i = 0; i < _currentZombieCount; i++)
            {
                SpawnZombie();
            }

            // Double the number of zombies for the next spawn
            _currentZombieCount *= 2;

            // Cap the number of zombies at the maximum limit
            if (_currentZombieCount > _maxZombieCount)
            {
                _currentZombieCount = _maxZombieCount;
            }

            // Wait for the next spawn interval
            yield return new WaitForSeconds(_initialSpawnInterval);
        }
    }

    private void SpawnZombie()
    {
        // Calculate a random position around the player within the spawn radius
        Vector2 randomDirection = Random.insideUnitCircle.normalized * _spawnRadius;
        Vector3 spawnPosition = _player.position + new Vector3(randomDirection.x, randomDirection.y, 0);

        // Instantiate the zombie at the calculated position
        Instantiate(_zombiePrefab, spawnPosition, Quaternion.identity);
    }
}