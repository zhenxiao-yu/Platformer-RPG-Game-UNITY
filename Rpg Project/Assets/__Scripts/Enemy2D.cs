using UnityEngine;
using System.Collections;
　　
　　public class Enemy2D : MonoBehaviour {
		// get player script
　　	public Controller2D player;
		// access GameManager
　　	public GameManager gameManager;
		// get coin prefab
		public Rigidbody coin;

		// start and end positions of enemy
　　	float startPos;
　　	float endPos;
		// how much damage enemy takes on collision
　　	float takenDamage = 0.2f;
		// how far enemy moves
　　	public int unitsToMove = 5;
		// speed of enemy
　　	public int moveSpeed = 2;
		// how much damage enemy creates
		public int damageValue = 1;
		// determines which way enemy is facing
　　	bool moveRight = true;
		// health of enemy
　　	int enemyHealth = 1;
		// set up type of enemy
　　	public bool basicEnemy;
　　	public bool advancedEnemy;


　　
　　	void Awake(){
			// set up boundaries for horizontal movement
　　		startPos = transform.position.x;
　　		endPos = startPos + unitsToMove;
			// set up small enemy health
　　		if (basicEnemy) {
　　			enemyHealth = 2;		
　　		}
			// set up large enemy health
　　		if (advancedEnemy) {
　　			enemyHealth = 4;		
　　		}
　　	}
　　	
　　	void Update(){
					// move left and right within boundaries
　　		        if (moveRight) {
　　				GetComponent<Rigidbody>().position += Vector3.right * moveSpeed * Time.deltaTime;	
　　				}
　　				if (GetComponent<Rigidbody>().position.x >= endPos) {
　　						moveRight = false;
　　				}
　　				if (moveRight==false) {
　　						GetComponent<Rigidbody>().position -= Vector3.right * moveSpeed * Time.deltaTime;	
　　				}
　　				if (GetComponent<Rigidbody>().position.x <= startPos) {
　　						moveRight = true;
　　				}
　　		}
　　	
　　	
　　	void OnTriggerEnter(Collider col){
			// damage player if collides with enemy
　　		if (col.gameObject.tag == "Player") {
　　			gameManager.SendMessage("PlayerDamaged",damageValue,SendMessageOptions.DontRequireReceiver);
　　			gameManager.controller2D.SendMessage("TakenDamage",SendMessageOptions.DontRequireReceiver);
　　		}
			// destroy enemy if hit by mine
			if (col.gameObject.CompareTag("Mine")){
			GameManager.playersEXP += 90;
			DropCoin();
            Destroy(col.gameObject);
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
　　	
　　	void EnemyDamaged(int damage){
			// destroy enemy if enemyHealth is <=0
　　		if (enemyHealth > 0) {
　　			enemyHealth -= damage;		
　　		}
　　
　　		if (enemyHealth <= 0) {
　　			enemyHealth = 0;
				DropCoin();
　　			Destroy(gameObject);
				
　　		}
　　	}

		void DropCoin(){
			// drop coins around where enemy was destroyed
			Vector3 coinDrop = transform.position;
			Vector3 offsetH = new Vector3(0.5f,0f,0f);
			Vector3 offsetV = new Vector3(0f,0.5f,0f);
			// ensure coins are dropped on same plane as player
            coinDrop.z = 0f;
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
