using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour {

	[HeaderAttribute("Music")]
	public AudioMixerSnapshot menu;
	public AudioMixerSnapshot play;
	public AudioMixerSnapshot gameOver;


	public AudioSource menuSource;
	public AudioSource playSource;
	public AudioSource gameOverSource;

	[HeaderAttribute("Sound Effects")]
	public AudioSource[] effectsSources;
	public AudioClip sunExplosionClip;
	public AudioClip playerDeathClip;
	public AudioClip[] smallEnemyDeathClips;
	public AudioClip[] largeEnemyDeathClips;

	private float transitionIn;
	private float transitionOut;
	private string activeScene;
	private string previousScene;


	// Pseudo-singleton
	private static MusicController instance = null;
	public static MusicController Instance 
	{
		get
		{
			return instance;
		}
	}

	void Awake ()
	{
		// Pseudo-singleton
		if (instance == null) {
			DontDestroyOnLoad (gameObject);
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

		SceneManager.activeSceneChanged += UpdateActiveScene;

		if (previousScene == null)
		{
			activeScene = SceneManager.GetActiveScene().name;
			previousScene = SceneManager.GetActiveScene().name;
		}
	}

	void UpdateActiveScene (Scene oldScene, Scene newScene)
	{
		previousScene = activeScene;
		activeScene = SceneManager.GetActiveScene().name;

		if (activeScene == "Menu" && previousScene != "Logo") {
			menuSource.Play();
			menu.TransitionTo(0.5f);
		} 
		else if (activeScene == "Play")
		{
			playSource.Play();
			play.TransitionTo(0.5f);
		} 
		else if (activeScene == "Credits")
		{
			gameOverSource.Play();
			gameOver.TransitionTo(0.5f);
		}
	}

	private void PlaySoundEffect(AudioClip clip)
	{
		foreach (AudioSource source in effectsSources)
		{
			if (source.isPlaying != true)
			{
				source.clip = clip;
				source.Play();
				return;
			}
		}
	}
	public void PlaySunExplosionSound()
	{
		PlaySoundEffect(sunExplosionClip);
	}

	public void PlayPlayerDeathSound()
	{
		PlaySoundEffect(playerDeathClip);
	}

	public void PlayEnemyDeathSound(bool isLarge)
	{
		AudioClip clip;
		if (isLarge)
		{
			clip = largeEnemyDeathClips[Random.Range(0,largeEnemyDeathClips.Length)];
		}
		else
		{
			clip = smallEnemyDeathClips[Random.Range(0,smallEnemyDeathClips.Length)];
		}
		PlaySoundEffect(clip);
	}



}
