using UnityEngine;
using System.Collections;

public class BoundaryDestroyer : MonoBehaviour {

	void OnTriggerExit2D (Collider2D collider) {
		var colliderLayer = collider.gameObject.layer;

		if (colliderLayer == 9 || 
			colliderLayer == 12 || 
			colliderLayer == 13) {
			Destroy (collider.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
