
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
　　
　　public class MovingPlatform : MonoBehaviour {
　　	// boundaries for platform movement
　　	float startPos;
　　	float endPos;
　　	public int unitsToMove = 5;
        // set up speed of platform movement
　　	public int moveSpeed = 2;
        // determine if platform is moving right or left
　　	bool moveRight = true;
　　	
        // set up limits for platform movement
　　	void Awake(){
　　		startPos = transform.position.x;
　　		endPos = startPos + unitsToMove;
　　	}
　　
　　	void Update(){
                    // make platform move left to right between limits
　　		        if (moveRight) {
　　				transform.position += Vector3.right * moveSpeed * Time.deltaTime;	
　　				}
　　				if (transform.position.x >= endPos) {
　　						moveRight = false;
　　				}
　　				if (moveRight==false) {
　　						transform.position -= Vector3.right * moveSpeed * Time.deltaTime;	
　　				}
　　				if (transform.position.x <= startPos) {
　　						moveRight = true;
　　				}
　　	}
　　}
