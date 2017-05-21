using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

	private Scene activeScene;

	// Use this for initialization
	void Start () {
		activeScene = SceneManager.GetActiveScene ();

		//if (activeScene.name == "Play") {
		//	DisableMouse ();
		//} else {
		//	EnableMouse ();
		//}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (SceneManager.GetActiveScene ().name == "Play") {
				LoadMenu ();
			} else if (SceneManager.GetActiveScene ().name == "Menu") {
				QuitGame ();
			}
		}
	}

	public void LoadPlay() {
		SceneManager.LoadScene ("Intro");
	}

	public void LoadMenu() {
		SceneManager.LoadScene ("Menu");
	}

	public void QuitGame() {
		Application.Quit ();
	}

	private void EnableMouse() {
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = true;
	}

	private void DisableMouse() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
}
