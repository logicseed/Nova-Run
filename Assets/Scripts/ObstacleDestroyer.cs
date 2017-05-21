using UnityEngine;
using System.Collections;

public class ObstacleDestroyer : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.layer == 15) {
			Destroy (collider.gameObject);
		}
	}

}
