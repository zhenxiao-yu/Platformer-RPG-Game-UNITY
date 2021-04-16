using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
       public float lifeTime = 2.25f;
        // handle collisions with walls
　　	void OnTriggerEnter(Collider other){
　　	    	
            // handle collisions with walls
            if (other.gameObject.tag == "Walls") {
            Destroy(gameObject);
            }
　　	}
        // Destroy Bullet once out of screen
　　	void FixedUpdate(){
　　		Destroy (gameObject, lifeTime);
　　	}
}
