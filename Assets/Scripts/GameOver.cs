using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		var lifetime = gameObject.GetComponent<LifetimeController> ().lifetime;

		ScoreController.Instance.GameOver (lifetime);
	}
}
