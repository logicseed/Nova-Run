using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoShrinker : MonoBehaviour {

    public float zoomFactor;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (transform.localScale.x > 0.8)
        {
            var newScale = new Vector3(transform.localScale.x, transform.localScale.y);
            newScale = newScale * zoomFactor;
            transform.localScale = newScale;
        }
	}
}
