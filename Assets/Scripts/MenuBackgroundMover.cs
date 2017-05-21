using UnityEngine;
using System.Collections;

public class MenuBackgroundMover : MonoBehaviour {

	public float speed;
	private bool instantiatedNext = false;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = true;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector2(transform.position.x - (speed * Time.deltaTime), 0.0f);

		if (!instantiatedNext && transform.position.x <= -15.0f) {
			instantiatedNext = true;
			GameObject go = (GameObject)Instantiate (Resources.Load("MenuBackground"));
			go.transform.position = new Vector2(transform.position.x + GetComponent<Renderer> ().bounds.size.x, 0.0f);
		}

		if (transform.position.x <= -33.0f) {
			Destroy (transform.gameObject);
		}
	}
}
