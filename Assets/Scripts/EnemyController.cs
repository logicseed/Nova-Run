using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	[HeaderAttribute("Attack Variables")]
	[TooltipAttribute("How many shot will be fired per attack.")]
	public int numberOfBlasts;
	[TooltipAttribute("How much time will pass between the individual shots of an attack.")]
	public float timeBetweenBlasts;
	[TooltipAttribute("How much time will pass between attacks.")]
	public float timeBetweenFiring;
	[TooltipAttribute("Attacks like a big enemy.")]
	public bool isBig;
	[TooltipAttribute("How many shots from the player this enemy can withstand.")]
	public int health;
	public GameObject weaponBlast1;
	public GameObject weaponBlast2;
	[HeaderAttribute("Scoring")]
	public float scoreAward;


	private float lastAttack;
	private float lastShot;
	private bool isAttacking;
	private int blastCount;
	private Transform weapon;

	// Use this for initialization
	void Start () {
		lastAttack = 0.0f;

		weapon = transform.Find("EnemyWeapon").transform;

	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - lastAttack >= timeBetweenFiring)
		{
			Attack();
		}
		if(isAttacking && Time.time - lastShot >= timeBetweenBlasts)
		{
			NextShot();
		}
	}

	void Attack()
	{
		lastAttack = Time.time;
		isAttacking = true;
		blastCount = 0;
		NextShot();
	}

	void NextShot()
	{
		if (blastCount >= numberOfBlasts)
		{
			isAttacking = false;
			return;
		}

		lastShot = Time.time;
		blastCount++;


		if (isBig)
		{
			LargeBlast();
		}
		else
		{
			SmallBlast();
		}
	}

	void LargeBlast()
	{
		var weaponPosition = weapon.position;
		Instantiate (weaponBlast1, weaponPosition, Quaternion.identity);
		Instantiate (weaponBlast2, weaponPosition, Quaternion.identity);
	}

	void SmallBlast()
	{
		var weaponPosition = weapon.position;
		Instantiate (weaponBlast1, weaponPosition, Quaternion.identity);
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		if (collider.gameObject.layer == 9)
		{
			health--;
			Destroy(collider.gameObject);
		}

		if (health <= 0)
		{
			ScoreController.Instance.AddScore(scoreAward);
			// Spawn enemy explosion
			GameObject go; //= (GameObject)Instantiate (Resources.Load("PlayerShipDeath"));
			if (isBig)
			{
				go = (GameObject)Instantiate (Resources.Load("EnemyLargeShipDeath"));
			}
			else
			{
				go = (GameObject)Instantiate (Resources.Load("EnemySmallShipDeath"));
			}
			go.transform.position = new Vector2 (transform.position.x, transform.position.y);

			// Play enemy death sound
			MusicController.Instance.PlayEnemyDeathSound(isBig);

			Destroy(gameObject);
		}
	}
}
