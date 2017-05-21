using UnityEngine;
using System.Collections;

public class StationaryObstacleSpriteGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject obstacleCollider;

		RandomSeeder.Instance.obstacleCount++;

		int caveNumber = 0;
		if (RandomSeeder.Instance.HasStartedObstacles)
		{
            caveNumber = RandomSeeder.Instance.ObstacleRandom.Next(1,31);
			obstacleCollider = Resources.Load("Caves/caves" + caveNumber.ToString("D2")) as GameObject;
		}
		else
		{
			if (RandomSeeder.Instance.obstacleCount <= 3)
			{
				switch (RandomSeeder.Instance.obstacleCount)
                {
                    case 1: caveNumber = 3; break;
                    case 2: caveNumber = 1; break;
                    case 3: caveNumber = 2; break;
                }
				obstacleCollider = Resources.Load("Caves/asteroid" + caveNumber.ToString("D2")) as GameObject;
			}
			else
			{
				RandomSeeder.Instance.HasStartedObstacles = true;
				obstacleCollider = Resources.Load("Caves/caves" + caveNumber.ToString("D2")) as GameObject;
			}
		}

		

		Instantiate (obstacleCollider, transform, false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
