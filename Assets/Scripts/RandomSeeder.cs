using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class RandomSeeder : MonoBehaviour
{

    // Pseudo-singleton
    public static RandomSeeder Instance;

    public string seed;
    public bool useRandomSeed;
    private System.Random obstacleRandom;
    private System.Random enemyRandom;

    private bool hasStartedObstacles = false;
    public int obstacleCount = 0;
    public bool randomizingSeed = false;

    void Awake()
    {
        // Pseudo-singleton
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        ResetSeeds();

    }

    public void ResetSeeds()
    {
        obstacleRandom = new System.Random(seed.GetHashCode());
        enemyRandom = new System.Random(seed.GetHashCode());

        hasStartedObstacles = false;
        obstacleCount = 0;
    }
    public void ResetSeeds(InputField seedField)
    {
        if (!randomizingSeed)
        {
            seed = seedField.text;
            ResetSeeds();
            ScoreController.Instance.hasLoaded = false;
            ScoreController.Instance.seedChanged = true;
            ScoreController.Instance.LoadScores();
        }
    }

    public void OneTimeRandomSeed(InputField seedField)
    {
        GenerateRandomSeed();
        seedField.text = seed;
        ResetSeeds();
        randomizingSeed = true;
        ScoreController.Instance.hasLoaded = false;
        ScoreController.Instance.seedChanged = true;
        ScoreController.Instance.LoadScores();
    }

    private void GenerateRandomSeed()
    {
        string newSeed = "";
        for (int i = 0; i < 20; i++)
        {
            newSeed += (char)UnityEngine.Random.Range('A', 'Z'+1);
        }
        seed = newSeed;
    }

    public System.Random ObstacleRandom
    {
        get
        {
            return obstacleRandom;
        }
    }

    public System.Random EnemyRandom
    {
        get
        {
            return enemyRandom;
        }
    }

    public bool HasStartedObstacles 
    {
        get
        {
            return hasStartedObstacles;
        }
        set
        {
            hasStartedObstacles = value;
        }
    }
}
