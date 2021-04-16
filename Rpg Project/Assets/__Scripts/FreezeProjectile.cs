using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeProjectile : MonoBehaviour
{
    // set speed of bullet
    public float speed;
    // set lifetime of bullet
    private float lifeTime = 4.25f;
    // get player
    private Transform player;
    // create a target position to shoot at
    private Vector2 target;

    void Start(){
        // find player
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // make player position into target
        target = new Vector2(player.position.x, player.position.y);
    }

    void Update(){
        // make bullet move towards player
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        // destroy once bullet reaches destination
        if(transform.position.x == target.x && transform.position.y == target.y){
            DestroyProjectile();
        }
    }



        // Destroy Bullet once lifetime is exceeded
　　void FixedUpdate(){
　　		Destroy (gameObject, lifeTime);
　　	}


    void DestroyProjectile(){
        // destroy the freeze bullet
        Destroy(gameObject);
    }
}
