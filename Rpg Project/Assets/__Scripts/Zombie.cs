using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{   
    // hold zombie speed
    public float speed; 
    // hold distance zombie goes 
    public float range;
    // set up target to chase
    private Transform target;
    // enemy's prefab
    public Rigidbody flyer;
    
    void Start()
    {
        // make player into target
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    
    void Update()
    {
        // chase player with enemy if player is within range
        if(Vector2.Distance(transform.position, target.position) < range){
            Chase();
        }

        
    }

    void Chase(){
        // move enmy towards player
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider col){
            // destroy if hit by mine and drop enemy　　		
            if (col.gameObject.tag == "Mine") {
　　			Destroy(gameObject);
				DropFlyer();
　　		}
            // destroy if hit by shield and drop enemy
			if (col.gameObject.tag == "Shield") {
　　			Destroy(gameObject);
				DropFlyer();
　　		}
　　	}

        void DropFlyer(){
            // drop enemy in position of current GameObject
			Vector3 flyerDrop = transform.position;
            Vector3 offsetH = new Vector3(4f,0f,0f);
            // ensure enemy is in plane of player
            flyerDrop.z = 0f;
            Instantiate (flyer, flyerDrop + offsetH, Quaternion.identity);
            Instantiate (flyer, flyerDrop - offsetH, Quaternion.identity);
		}

}
