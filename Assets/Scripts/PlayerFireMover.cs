using UnityEngine;
using System.Collections;

public class PlayerFireMover : MonoBehaviour {

	public float velocity;

	// Use this for initialization
	void Start () {

        //GetComponent<Rigidbody2D> ().velocity = new Vector2 (velocity, 0.0f);
        var velocityVector = new Vector2(velocity, 0.0f);
        GetComponent<Rigidbody2D>().velocity = transform.rotation * velocityVector;
    }
}
