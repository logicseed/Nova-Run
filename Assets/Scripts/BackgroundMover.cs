using UnityEngine;
using System.Collections;

public class BackgroundMover : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x > -15.0f) {
			transform.position = new Vector3 (transform.position.x - (speed * Time.deltaTime), 0.0f);
		}
	}
}
