using UnityEngine;
using System.Collections;

public class LifetimeController : MonoBehaviour {

	public float lifetime;
	public float maxSize;
	public float minSize;
	private float age;
	private Vector3 originalScale;

	// Use this for initialization
	void Start () {
		originalScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		age += Time.deltaTime;
		if (age >= lifetime) {
			Destroy (transform.gameObject);
		}

		SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		if (Random.value < 0.5)
			spriteRenderer.flipX = !spriteRenderer.flipX;
		if (Random.value < 0.5)
			spriteRenderer.flipY = !spriteRenderer.flipY;

		var size = minSize + ((maxSize - minSize) * (age / lifetime));
		transform.localScale = new Vector3 (originalScale.x * size, originalScale.y * size);
	}
}
