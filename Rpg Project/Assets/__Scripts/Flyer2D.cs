using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer2D : MonoBehaviour
{
		// get coin prefab
		public Rigidbody coin;

　　	// set up boundaries of movement
　　	float startPos;
　　	float endPos;
　　	public int unitsToMove = 5;
　　	// set speed
　　	public int moveSpeed = 2;
　　	// set up how much damage flyer creates
		public int damageValue = 1;
		// determines is enemy moving up or down
　　	bool moveUp = true;
		// set health of enemy
　　	public int enemyHealth = 1;
　　	


　　
　　	void Awake(){
			// set up boundaries for horizontal movement
　　		startPos = transform.position.y;
　　		endPos = startPos + unitsToMove;
		
　　	}
　　	
　　	void Update(){
					// move up and down within boundaries
　　		        if (moveUp) {
　　				GetComponent<Rigidbody>().position += Vector3.up * moveSpeed * Time.deltaTime;	
　　				}
　　				if (GetComponent<Rigidbody>().position.y >= endPos) {
　　						moveUp = false;
　　				}
　　				if (moveUp==false) {
　　						GetComponent<Rigidbody>().position -= Vector3.up * moveSpeed * Time.deltaTime;	
　　				}
　　				if (GetComponent<Rigidbody>().position.y <= startPos) {
　　						moveUp = true;
　　				}
　　		}
　　	
　　	
　　
        void OnTriggerEnter(Collider col){
			
　　		// destroy if hit by mine
            if (col.gameObject.tag == "Mine") {
　　			Destroy(gameObject);
				DropCoin();
　　		}
			// destroy if hit by shield
			if (col.gameObject.tag == "Shield") {
　　			Destroy(gameObject);
				DropCoin();
　　		}
			// destroy if hit by bullet
             if (col.gameObject.tag == "Bullet") {
　　			Destroy(gameObject);
				Destroy(col.gameObject);
                DropCoin();
　　		}
　　	}

		void DropCoin(){
			// drop coins at object's position
			Vector3 coinDrop = transform.position;
			Vector3 offsetH = new Vector3(0.5f,0f,0f);
			// ensure coins are dropped in same plane as player
            coinDrop.z = 0f;
            Instantiate (coin, coinDrop, Quaternion.identity);
			Instantiate (coin, coinDrop + offsetH, Quaternion.identity);
			Instantiate (coin, coinDrop - offsetH, Quaternion.identity);
		}
}
