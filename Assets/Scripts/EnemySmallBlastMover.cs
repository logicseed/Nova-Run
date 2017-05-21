using UnityEngine;
using System.Collections;

public class EnemySmallBlastMover : MonoBehaviour {

	public float percentOfObstacleSpeed;
	private ObstacleController obstacleController;

	// Use this for initialization
	void Start () {
		obstacleController = GameObject.Find("ObstacleController").GetComponent<ObstacleController>();

		var velocity = obstacleController.CurrentVelocity * -percentOfObstacleSpeed;
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (velocity, 0.0f);
	}

}
