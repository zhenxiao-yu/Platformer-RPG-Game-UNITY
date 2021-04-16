using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shooter : MonoBehaviour
{   
    // get player script
    public Controller2D player;
    // access GameManager
    public GameManager gameManager;
    // get coin prefab
    public Rigidbody coin;
    // get health prefab
    public Rigidbody health;
    // cooldown period between bullet attacks
    float coolDown;
    // set up amount of damage colliding with shooter does to player
    public int damageValue = 1;
    // set up shooter health
    public int shooterHealth = 3;
    // set up amount of time damage is taken
    float takenDamage = 0.2f;
    // determines which way shooter is facing
    public bool lookRight = true;
    // set up rate of shots
　　public float attackRate = 1.5f;
    // get bullet prefab
    public Rigidbody shooterBulletPrefab;

    
    // Update is called once per frame
    void Update(){
        // BulletAttack() when F key is pressed
　　		if (Time.time >= coolDown) 
                {
　　				shooterAttack ();	
　　			}
　　	}

    void OnTriggerEnter(Collider col){
			// damage player if collides with enemy
　　		if (col.gameObject.CompareTag("Player")){
　　			gameManager.SendMessage("PlayerDamaged",damageValue,SendMessageOptions.DontRequireReceiver);
　　			gameManager.controller2D.SendMessage("TakenDamage",SendMessageOptions.DontRequireReceiver);
　　		}
            // drop coins and destroy shooter if hit by mine
			if (col.gameObject.CompareTag("Mine")){
			DropCoin();
            Destroy(col.gameObject);
            Destroy(gameObject);
        	}
　　	}

    void EnemyDamaged(int damage){
			// destroy enemy if shooterHealth is <=0
　　		if (shooterHealth > 0) {
　　			shooterHealth -= damage;		
　　		}
　　
　　		if (shooterHealth <= 0) {
　　			shooterHealth = 0;
                DropCoin();
                DropHealth();
　　			Destroy(gameObject);
　　		}
　　	}

    public IEnumerator TakenDamage(){
			// flash enemy if it takes damage
		    GetComponent<Renderer>().enabled = false;
		    yield return new WaitForSeconds(takenDamage);
		    GetComponent<Renderer>().enabled = true;
		    yield return new WaitForSeconds(takenDamage);
		    GetComponent<Renderer>().enabled = false;
		    yield return new WaitForSeconds(takenDamage);
		    GetComponent<Renderer>().enabled = true;
		    yield return new WaitForSeconds(takenDamage);
		    GetComponent<Renderer>().enabled = false;
		    yield return new WaitForSeconds(takenDamage);
		    GetComponent<Renderer>().enabled = true;
		    yield return new WaitForSeconds(takenDamage);
				
	    } 

    void shooterAttack(){
　　		if (lookRight) {
　　			// shoot right if facing right
　　			Rigidbody bPrefab = Instantiate (shooterBulletPrefab, transform.position, Quaternion.identity)as Rigidbody;
　　			bPrefab.GetComponent<Rigidbody>().AddForce (Vector3.right * 500);
　　			coolDown = Time.time + attackRate;
　　				}
　　		else {
　　			// shoot left if facing left
　　			Rigidbody bPrefab = Instantiate (shooterBulletPrefab, transform.position, Quaternion.identity)as Rigidbody;
　　			bPrefab.GetComponent<Rigidbody>().AddForce (-Vector3.right * 500);
　　			coolDown = Time.time + attackRate;
　　		}
　　	}

    void DropHealth(){
            // drop health power up at enemy position
            Vector3 healthDrop = transform.position;
            Vector3 offsetH = new Vector3 (0,1.7f,0);
            // ensure power up is in plane with player
            healthDrop.z = 0f;
            // randomize a number 
            int chance = Random.Range(1,3);
            // drop health power up if random number is 1
            if (chance == 1){
            Instantiate(health, healthDrop + offsetH, Quaternion.identity);
            }
        }

    void DropCoin(){
            // drop coin at transform location
			Vector3 coinDrop = transform.position;
			Vector3 offsetH = new Vector3(0.5f,0f,0f);
			Vector3 offsetV = new Vector3(0f,0.5f,0f);
            // ensure coin location is in plane with player
            coinDrop.z = 0f;
            // drop coins with offsets
            Instantiate (coin, coinDrop, Quaternion.identity);
			Instantiate (coin, coinDrop + offsetH, Quaternion.identity);
			Instantiate (coin, coinDrop - offsetH, Quaternion.identity);
			Instantiate (coin, coinDrop + offsetH + offsetV, Quaternion.identity);
			Instantiate (coin, coinDrop - offsetH + offsetV, Quaternion.identity);
			Instantiate (coin, coinDrop + offsetV, Quaternion.identity);
			Instantiate (coin, coinDrop + offsetV * 2, Quaternion.identity);
			Instantiate (coin, coinDrop + offsetV * 2 + offsetH, Quaternion.identity);
			Instantiate (coin, coinDrop + offsetV * 2 - offsetH, Quaternion.identity);
		}
    
}
