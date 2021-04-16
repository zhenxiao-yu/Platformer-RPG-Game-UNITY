using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
     // hold zombie speed
    public float speed; 
    // hold distance zombie goes 
    // set up target to chase
    private Transform target;
    bool doChase = false;
    
     void Start()
    {
        // make player into target
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate (0,0,50*Time.deltaTime); //rotates 50 degrees per second around z 

        if (doChase){
            Chase();
        }
    }

    void Chase(){
        // move enmy towards player
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

     void OnTriggerEnter(Collider col){
            // destroy if hit by mine and drop enemy　　		
            if (col.gameObject.tag == "Magnet") {
　　			doChase = true;
　　		}
     }
}
