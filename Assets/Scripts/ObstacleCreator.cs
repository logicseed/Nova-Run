using UnityEngine;
using System.Collections;

public class ObstacleCreator : MonoBehaviour {

	void OnTriggerExit2D (Collider2D collider) {
			GameObject go = (GameObject)Instantiate (Resources.Load("StationaryObstacle"));

			var x = collider.transform.position.x + collider.transform.GetComponent<BoxCollider2D>().size.x;
			var y = collider.transform.position.y;

			go.transform.position = new Vector3 (x, y, 0);
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
