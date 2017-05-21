using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	[HeaderAttribute("Spawn locations")]
	public Transform Spawner1;
	public Transform Spawner2;
	public Transform Spawner3;
	public Transform Spawner4;

	[HeaderAttribute("Enemy Objects")]
	public GameObject EnemySmallRed;
	public GameObject EnemySmallPurple;
	public GameObject EnemyLargeRed;
	public GameObject EnemyLargePurple;

	[HeaderAttribute("Spawning Variables")]
	public float timeBetweenSpawns;
	public float chanceOfLargeEnemy;
	public float chanceOfPurpleEnemy;
	public float timeUntilAllLarge;
	public float timeUntilAllPurple;

	private float lastSpawnTime;
	private float playStartTime;
	private float timePlayed;
    public float enemyOffset;

	// Use this for initialization
	void Start () {
		playStartTime = Time.time;
		// First wave in half the time
		lastSpawnTime = (Time.time - playStartTime) - (timeBetweenSpawns / 2);
	}
	
	// Update is called once per frame
	void Update () 
	{
		timePlayed += Time.deltaTime;
		if (timePlayed - lastSpawnTime >= timeBetweenSpawns)
		{
			SpawnWave();
			lastSpawnTime = timePlayed;
		}
	}

	void SpawnWave ()
	{
		// Calculate spawn chances
		float currentChanceOfLargeEnemy = Mathf.Min(
			1.0f,
			chanceOfLargeEnemy + ((1.0f - chanceOfLargeEnemy) * (timePlayed/timeUntilAllLarge))
			);
		float currentChanceOfPurpleEnemy = Mathf.Min(
			1.0f,
			chanceOfPurpleEnemy + ((1.0f - chanceOfPurpleEnemy) * (timePlayed/timeUntilAllPurple))
			);
		
		// Determine if large
		bool isLarge = RandomSeeder.Instance.EnemyRandom.NextDouble() <= currentChanceOfLargeEnemy;
		// Determine if purple
		bool isPurple = RandomSeeder.Instance.EnemyRandom.NextDouble() <= currentChanceOfPurpleEnemy;

		var spawnLocation = RandomSeeder.Instance.EnemyRandom.Next(1,5);
		if (isLarge)
		{
			if (isPurple)
			{
				SpawnEnemy(EnemyLargePurple, spawnLocation);
			}
			else
			{
				SpawnEnemy(EnemyLargeRed, spawnLocation);
			}
		}
		else
		{
			// Small enemies spawn in waves of two
			int secondSpawnLocation;
			do
			{
				secondSpawnLocation = RandomSeeder.Instance.EnemyRandom.Next(1,5);
			} while (secondSpawnLocation == spawnLocation);

			if (isPurple)
			{
				SpawnEnemy(EnemySmallPurple, spawnLocation);
				SpawnEnemy(EnemySmallPurple, secondSpawnLocation, true);
			}
			else
			{
				SpawnEnemy(EnemySmallRed, spawnLocation);
				SpawnEnemy(EnemySmallRed, secondSpawnLocation, true);
			}
		}

		
		
	}

    void SpawnEnemy(GameObject enemy, int location, bool isSecond = false)
	{
		Transform spawnLocation;

		switch (location)
		{
			case 1:
				spawnLocation = Spawner1;
				break;
			case 2:
				spawnLocation = Spawner2;
				break;
			case 3:
				spawnLocation = Spawner3;
				break;
			case 4:
				spawnLocation = Spawner4;
				break;
			default:
				spawnLocation = Spawner2;
				break;
		}

        Vector3 position;
        if (isSecond)
        {
            position = new Vector3(spawnLocation.position.x - enemyOffset, spawnLocation.position.y);
        }
        else
        {
            position = spawnLocation.position;
        }
		Instantiate (enemy, position, Quaternion.identity);
	}
}
