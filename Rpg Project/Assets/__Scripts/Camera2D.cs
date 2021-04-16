using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camera2D : MonoBehaviour {
        // player object transform
　　	public Transform player;
        // float to lag camera movement
　　	public float smoothRate = 0.5f;
        // get camera's transform
　　	private Transform thisTransform;
        // 2D vector to handle movement of camera
　　	private Vector2 velocity;
　　
　　	// set up player and velocity
　　	void Start () {
　　		thisTransform = transform;
　　		velocity = new Vector2 (0.5f, 0.5f);
　　	}
　　	
　　	
　　	void Update () {
　　		Vector2 newPos2D = Vector2.zero;
　　		// smoothen camera movement
　　		newPos2D.x = Mathf.SmoothDamp (thisTransform.position.x, player.position.x, ref velocity.x, smoothRate);
　　		newPos2D.y = Mathf.SmoothDamp (thisTransform.position.y, player.position.y, ref velocity.y, smoothRate);
　　	
　　		Vector3 newPos = new Vector3 (newPos2D.x, newPos2D.y, transform.position.z);
　　		
　　		transform.position = Vector3.Slerp (transform.position, newPos, Time.time);
            // Restart if player falls out of world
            if (player.position.y < -5){
                StartCoroutine(Restart());
            }

　　	}
        // Restart sequence
        public IEnumerator Restart(){
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("Scene1");
        }

　　}

