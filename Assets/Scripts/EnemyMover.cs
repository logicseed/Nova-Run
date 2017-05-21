using UnityEngine;
using System.Collections;

public class EnemyMover : MonoBehaviour
{

    private ObstacleController obstacleController;
	public float percentOfObstacleSpeed;

    // Use this for initialization
    void Start()
    {
		obstacleController = GameObject.Find("ObstacleController").GetComponent<ObstacleController>();
    }

    // Update is called once per frame
    void Update()
    {
		float movement = transform.position.x - (
			obstacleController.CurrentVelocity * Time.deltaTime * percentOfObstacleSpeed);
		transform.position = new Vector3 (movement, transform.position.y);
    }
}
