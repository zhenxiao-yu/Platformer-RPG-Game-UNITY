using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    // get drone prefab
    public Rigidbody spawnItem;
    // spawn drone if player hits trigger
    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Player") {
				Spawn();
        }
    }
    // get trigger's location, and instantiate drone above it
    void Spawn(){
			Vector3 triggerLocation = transform.position;
            Vector3 offSetV = new Vector3(0f, 3f, 0f);
            triggerLocation.z = 0f;
            Instantiate (spawnItem, triggerLocation + offSetV, Quaternion.identity);
    }
}
