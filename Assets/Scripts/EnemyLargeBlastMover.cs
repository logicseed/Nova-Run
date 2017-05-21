using UnityEngine;
using System.Collections;

public class EnemyLargeBlastMover : MonoBehaviour {

	public float percentOfObstacleSpeed;
	public bool isUpperBlast;
	private ObstacleController obstacleController;

	// Use this for initialization
	void Start () {
		obstacleController = GameObject.Find("ObstacleController").GetComponent<ObstacleController>();

		var velocityX = obstacleController.CurrentVelocity * -percentOfObstacleSpeed;
		float velocityY;
		if (isUpperBlast)
		{
			transform.rotation = new Quaternion(0.0f, 0.0f, -15.0f, 0.0f);
			velocityY = -velocityX * Mathf.Tan(Mathf.PI/12);
		}
		else
		{
			transform.rotation = new Quaternion(0.0f, 0.0f, 15.0f, 0.0f);
			velocityY = velocityX * Mathf.Tan(Mathf.PI/12);
		}

		GetComponent<Rigidbody2D> ().velocity = new Vector2 (velocityX, velocityY);
	}
}
