using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LogoTimer : MonoBehaviour {

	public float displayTime;
	private float logoLoadTime;

	// Use this for initialization
	void Start () {
		logoLoadTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= logoLoadTime + displayTime) {
			SceneManager.LoadScene ("Menu");
		}
	}
}
