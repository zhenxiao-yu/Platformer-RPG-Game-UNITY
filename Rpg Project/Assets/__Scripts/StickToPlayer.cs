using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToPlayer : MonoBehaviour
{
    private Transform player;
    Vector3 magnetPosition = new Vector3(0f, 1f,0f);

    void Start()
    {
        // find player
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {   
        
        //follow player
        transform.position = player.position + magnetPosition;
    }
}
