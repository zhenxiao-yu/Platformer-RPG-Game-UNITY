using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

	// access GameManager
　　public GameManager gameManager;
	// get coin prefab
	public Rigidbody coin;
	// get health prefab
    public Rigidbody health;
	// get diamond prefab
    public Rigidbody diamond;
	// amount of damage taken on bullet collision
    float takenDamage = 0.2f;
	// number of health points for chest
    public int chestHitPoint = 2;

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

        void ChestDamaged(int damage){
			// decrement chest health points on bullet collision
　　		if (chestHitPoint > 0) {
　　			chestHitPoint -= damage;		
　　		}
			// destroy chest and drop loot
　　		if (chestHitPoint <= 0) {
　　			chestHitPoint = 0;
				DropLoot();
　　			Destroy(gameObject);
				
　　		}
　　	} 

        

        void DropLoot(){
			// drop coin prefabs around where chest was destroyed
			Vector3 coinDrop = transform.position;
			Vector3 offsetH = new Vector3(1f,0f,0f);
			Vector3 offsetV = new Vector3(0f,1f,0f);
			// ensure coins are dropped in same plane as player
            coinDrop.z = 0f;
            Instantiate (health, coinDrop, Quaternion.identity);
			Instantiate (diamond, coinDrop + offsetH, Quaternion.identity);
			Instantiate (diamond, coinDrop - offsetH, Quaternion.identity);
			Instantiate (coin, coinDrop + offsetH + offsetV, Quaternion.identity);
			Instantiate (coin, coinDrop - offsetH + offsetV, Quaternion.identity);
			Instantiate (coin, coinDrop + offsetV, Quaternion.identity);
			Instantiate (diamond, coinDrop + offsetV * 2, Quaternion.identity);
			Instantiate (coin, coinDrop + offsetV * 2 + offsetH, Quaternion.identity);
			Instantiate (coin, coinDrop + offsetV * 2 - offsetH, Quaternion.identity);
		}


}
