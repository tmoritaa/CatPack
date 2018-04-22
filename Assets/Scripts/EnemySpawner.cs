using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField]
    private int minSpawnRange = 500;

    [SerializeField]
    private int maxSpawnRange = 1000;

    [SerializeField]
    private float spawnRate = 1f;

    [SerializeField]
    private List<int> scoreThresholdPerLevel;

    [SerializeField]
    private List<Enemy> enemyPrefabsForLevel1;

    [SerializeField]
    private List<int> enemySpawnPercentageForLevel1;

    [SerializeField]
    private Transform enemyRoot;

    List<List<Enemy>> enemyPrefabsPerLevel = new List<List<Enemy>>();
    List<List<int>> enemySpawnPercentagePerLevel = new List<List<int>>();

    private int curLevel = 0;

    private float timeSinceLastSpawn = 999;

    void Awake() {
        enemyPrefabsPerLevel.Add(enemyPrefabsForLevel1);

        enemySpawnPercentagePerLevel.Add(enemySpawnPercentageForLevel1);
    }

    void Update () {
        if (GameManager.Instance.IsGameOver) {
            return;
        }

		if (timeSinceLastSpawn > spawnRate) {
            spawnEnemy();
            timeSinceLastSpawn = 0;
        }

        if (ScoreHandler.Instance.Score > scoreThresholdPerLevel[curLevel]) {
            curLevel = Mathf.Min(curLevel + 1, scoreThresholdPerLevel.Count - 1);
        }

        timeSinceLastSpawn += Time.deltaTime;
	}

    private void spawnEnemy() {
        List<Enemy> enemyPrefabsForLevel = enemyPrefabsPerLevel[curLevel];
        List<int> enemySpawnRateForLevel = enemySpawnPercentagePerLevel[curLevel];

        Debug.Assert(enemyPrefabsForLevel.Count == enemySpawnRateForLevel.Count, "Enemy prefabs for level count does not match enemy spawn rate for level count for level " + curLevel + ". Should never happen");

        int randPerc = GlobalRandom.GetRandomNumber(0, 101);

        int spawnIdx = 0;
        int curRate = 0;
        foreach (int rate in enemySpawnRateForLevel) {
            curRate += rate;

            if (randPerc < curRate) {
                break;
            }

            spawnIdx += 1;
        }

        Enemy enemyPrefab = enemyPrefabsForLevel[spawnIdx];

        int range = GlobalRandom.GetRandomNumber(minSpawnRange, maxSpawnRange);
        int angle = GlobalRandom.GetRandomNumber(0, 360);

        Vector2 spawnDir = new Vector2(0, 1).Rotate(angle).normalized * range;

        Vector3 newPos = ObjectRefHolder.Instance.PlayerRef.transform.position + (Vector3)spawnDir;

        Enemy enemyInstance = Instantiate(enemyPrefab, enemyRoot, false);
        enemyInstance.transform.position = newPos;
    }
}
