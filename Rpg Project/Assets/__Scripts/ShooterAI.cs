using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterAI : MonoBehaviour
{   
    // get player script
    public Controller2D player1;
    // access GameManager
　　public GameManager gameManager;
    // get shooter speed
    public float speed;
    // set up movement boundaries for drone shooter
    public float stoppingDistance;
    public float retreatDistance;
    // set up time between drone shots
    private float timeBtwShots;
    public float startTimeBtwShots;
    // get coin prefab
    public Rigidbody coin;
    // get drone bullet prefab
    public GameObject projectile;
    // get player
    public Transform player;

    void Start()
    {
        // find player
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
    }

    
    void Update()
    {
        // chase and shoot at player if in range
        if(Vector2.Distance(transform.position, player.position) > stoppingDistance){

            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        } 
        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance){
            
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance){

            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }

        if(timeBtwShots <= 0){

            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        } 
        else {

            timeBtwShots -= Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider col){
			// damage player if collides with enemy
　　		if (col.gameObject.tag == "Player") {
　　			gameManager.controller2D.SendMessage("TakenDamage",SendMessageOptions.DontRequireReceiver);
　　		}
            // destroy drone shooter if it hits wall
            if (col.gameObject.tag == "Walls") {
　　			Destroy(gameObject);
　　		}
            // destroy drone shooter if it hits bullet
             if (col.gameObject.tag == "Bullet") {
　　			Destroy(gameObject);
                Destroy(col.gameObject);
                DropCoin();
　　		}
            // destroy drone shooter if it hits shield
            if (col.gameObject.tag == "Shield") {
　　			Destroy(gameObject);
                DropCoin();
　　		}
　　	}
      

        void DropCoin(){
            // drop coins at current position
			Vector3 coinDrop = transform.position;
			Vector3 offsetH = new Vector3(0.5f,0f,0f);
			Vector3 offsetV = new Vector3(0f,0.5f,0f);
            // ensure coins are in same plane as player
            coinDrop.z = 0f;
            // instantiate coins, some with offsets
            Instantiate (coin, coinDrop, Quaternion.identity);
			Instantiate (coin, coinDrop + offsetH + offsetV, Quaternion.identity);
			Instantiate (coin, coinDrop - offsetH + offsetV, Quaternion.identity);
			Instantiate (coin, coinDrop + offsetV, Quaternion.identity);
			Instantiate (coin, coinDrop + offsetV * 2, Quaternion.identity);
		}
}
