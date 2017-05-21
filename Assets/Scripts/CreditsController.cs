using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsController : MonoBehaviour {

	private ScoreController scoreController;
	public GameObject creditsInputField;
	public GameObject anyKeyToContinueText;
	private bool canContinue = false;
	public float lifetime;
	private float age = 0.0f;
	private string originalAnyKeyText;
	public GameObject loadBackground;

	// Use this for initialization
	void Start () {
		scoreController = ScoreController.Instance;


		if (!scoreController.HasHighScore ()) {
			Destroy (creditsInputField);
		} else {
			creditsInputField.GetComponent<InputField> ().ActivateInputField ();
			Destroy (anyKeyToContinueText);
		}
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene ("Menu");
		}

		if (!scoreController.HasHighScore ()) {
			if (Input.anyKeyDown && canContinue) {

				// Destroy background
				var backgrounds = GameObject.FindGameObjectsWithTag("Background");
				foreach (var background in backgrounds)
				{
					loadBackground.transform.position = background.transform.position;
					Destroy(background);
				}

				RandomSeeder.Instance.ResetSeeds();
				SceneManager.LoadScene ("Play");
				
			} else {
				age += Time.deltaTime;
				if (age > lifetime)
					canContinue = true;
			}
		}

		
	}

	public void AddHighScore () {
		string name = creditsInputField.GetComponent<InputField> ().text;
		scoreController.AddHighScore (name);

		SceneManager.LoadScene ("Menu");
	}
}