using UnityEngine;
using System.Collections;

public class NovaMover : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x > -8.2f) {
			transform.position = new Vector3 (transform.position.x - (speed * Time.deltaTime), 0.0f);
		}
	}
}
