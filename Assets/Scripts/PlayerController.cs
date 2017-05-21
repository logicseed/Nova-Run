using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour {
    public Camera gameCamera;
    public float ForwardSpeed;
    public float BackwardSpeed;
    public float SteeringSpeed;

    public Vector2 speed = new Vector2(10.0f, 10.0f);
    private Vector2 movement;

    private Vector2 boundary;
    private bool shipAlive = true;

    public GameObject weapon;
    private Transform weaponPositionTransform;
    public float rateOfFire;
    private float nextFire;
    public float scoreMultiplier;
    private GameObject obstacleController;
    public int maxShotsFired;
    private int shotsFired = 0;
    public float weaponCooldown;

    public int health;
    private int maxHealth;
    public string healthChar;
    public Text healthBar;
    public float costPerShot;


    // Use this for initialization
    void Start () {
        // If we didn't specify a camera, grab the main camera
        if (gameCamera == null) {
            gameCamera = Camera.main;
        }

        // Find the screen boundary in world space
        var screenSize = new Vector2 (Screen.width, Screen.height);
        var shipSize = GetComponent<SpriteRenderer>().bounds.size;
        boundary = gameCamera.ScreenToWorldPoint (screenSize) - shipSize;

        // Setup rate of fire
        nextFire = Time.time;

        // Get weaponPosition object
        weaponPositionTransform = transform.Find("WeaponPosition");

        // Get ObstacleController object
        obstacleController = GameObject.Find ("ObstacleController");

        maxHealth = health;
    }
    
    // Update is called once per frame
    void Update () {
        
        var inputX = Input.GetAxis ("Horizontal");
        var inputY = Input.GetAxis ("Vertical");

        movement = new Vector2 (speed.x * inputX, speed.y * inputY);

        AddDistanceScore ();
    }

    void AddDistanceScore () {
        float currentVelocity = obstacleController.GetComponent<ObstacleController> ().CurrentVelocity;
        ScoreController.Instance.AddScore (Time.deltaTime * currentVelocity * scoreMultiplier);
    }

    // FixedUpdate is called once per physics tick
    void FixedUpdate () {
        if (shipAlive) {
            // Move ship
            SetShipVelocity(movement);

            // Fire weapon
            if ((Input.GetKey(KeyCode.Space)
                || Input.GetKey(KeyCode.JoystickButton0)
                || Input.GetMouseButton(0))
                && Time.time >= nextFire) {
                
                //if (shotsFired < maxShotsFired)
                //{
                    FireWeapon ();
                    shotsFired++;
                //}
                //else if (shotsFired == maxShotsFired)
                //{
                    //WeaponCooldown();
                //}
            }
        }

        // Keep ship within boundary
        var targetPosition = transform.position;
        targetPosition.x = Mathf.Clamp (transform.position.x, -boundary.x, boundary.x);
        targetPosition.y = Mathf.Clamp (transform.position.y, -boundary.y, boundary.y);
        transform.position = targetPosition;
    }

    public IEnumerator WeaponCooldown ()
    {
        yield return new WaitForSeconds(weaponCooldown);
        shotsFired = 0;
    }

    void OnTriggerEnter2D (Collider2D collider) {
        var colliderLayer = collider.transform.gameObject.layer;

        
        if (colliderLayer == 13)
        {
            health--;
            Destroy (collider.gameObject);
            // update healthBar
            var newHealthBar = new string(healthChar[0], health);
            healthBar.text = newHealthBar;

            if (health < (maxHealth / 2))
            { healthBar.color = Color.red; }
            else { healthBar.color = Color.cyan; }

            FlashDamage(gameObject.GetComponent<SpriteRenderer>().material);
            //var renderer = gameObject.GetComponent<SpriteRenderer>();
            //for (var n = 0; n < 5; n++)
            //{
            //    renderer.material.color = Color.red;
            //    yield WaitForSeconds(.1f);
            //    renderer.material.color = Color.white;
            //    yield WaitForSeconds(.1f);
            //}

        }

        if (colliderLayer == 15 || colliderLayer == 12)
        {
            // Stop ship movement
            SetShipVelocity (Vector2.zero);

            healthBar.text = "";

            // Kill ship
            shipAlive = false;

            // Stop obstacle movement
            obstacleController.GetComponent<ObstacleController> ().StopObstacles ();

            health = 0;
        }

        if (health <= 0)
        {
            // Spawn player explosion
            GameObject go = (GameObject)Instantiate (Resources.Load("PlayerShipDeath"));
            go.transform.position = new Vector2 (transform.position.x, transform.position.y);
            
            // Play sound effect
            MusicController.Instance.PlayPlayerDeathSound();
            
            Destroy (transform.gameObject);
        }
    }

    public IEnumerator FlashDamage(Material material)
    {
        Debug.Log("Flashing damage.");
        //var renderer = gameObject.GetComponent<SpriteRenderer>();

        //renderer.color = Color.red;
        //yield return new WaitForSeconds(0.5f);
        //renderer.color = Color.white;

        for (var n = 0; n < 5; n++)
        {
            material.color = Color.red;
            yield return new WaitForSeconds(.1f);
            material.color = Color.white;
            yield return new WaitForSeconds(.1f);
        }
    }

    void SetShipVelocity (Vector2 velocity) {
        GetComponent<Rigidbody2D> ().velocity = velocity;
    }

    void FireWeapon() {
        // Limit rate of fire
        nextFire = Time.time + rateOfFire;

        // Create weapons fire
        var weaponPosition = weaponPositionTransform.position;
        var aimPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimPosition.z = 0.0f;
        Debug.Log("aimPosition: (" + aimPosition.x + "," + aimPosition.y + "," + aimPosition.z + ")");
        var angleOfFire = Vector3.Angle(weaponPosition, aimPosition);
        Debug.Log("angleOfFire: " + angleOfFire);

        //var weaponRotation = new Quaternion(0.0f, 0.0f, angleOfFire, 0.0f);
        var weaponRotation = Quaternion.FromToRotation(Vector3.right, aimPosition - weaponPosition);

        Instantiate (weapon, weaponPosition, weaponRotation);
        ScoreController.Instance.AddScore(-costPerShot);
    }
}
