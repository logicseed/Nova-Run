using UnityEngine;
using System.Collections;

public class StationaryObstacleMover : MonoBehaviour {

	private ObstacleController obstacleController;

	// Use this for initialization
	void Start () {
		obstacleController = GameObject.Find("ObstacleController").GetComponent<ObstacleController>();
	}
	
	// Update is called once per frame
	void Update () {
        //float movement = transform.position.x - (obstacleController.CurrentVelocity * Time.fixedDeltaTime);
        //transform.position = new Vector3 (movement, 0);
        transform.Translate(-obstacleController.CurrentVelocity * Time.deltaTime, 0.0f, 0.0f, Space.World);
	}

}
