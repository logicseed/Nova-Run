using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {

	public GameObject ObstacleCreator;
	public GameObject ObstacleDestroyer;
	public float StartingSpeed;
	public float SpeedIncrement;
	public float CurrentVelocity;
	public float ObstacleStartDelay;

	private bool hasStartedObstacles = false;
	private float startTime;

	// Use this for initialization
	void Start () {
		CurrentVelocity = StartingSpeed;
		startTime = Time.time + ObstacleStartDelay;
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasStartedObstacles)
		{
			if (Time.time >= startTime)
			{
				hasStartedObstacles = true;
				GameObject go = (GameObject)Instantiate (Resources.Load("StationaryObstacle"));
				go.transform.position = new Vector3 (17, 0, 0);
			}
		}

		CurrentVelocity += SpeedIncrement * Time.deltaTime;
	}

	public void StopObstacles() {
		SpeedIncrement = 0;
		CurrentVelocity = 0;
	}
}
