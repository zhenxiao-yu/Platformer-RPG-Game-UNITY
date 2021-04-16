using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // set up how much damage shield creates
     int damageValue = 3;
    
    void OnTriggerEnter(Collider other){
            // damage enemy on collision
　　		if (other.gameObject.tag == "Enemy") {
                GameManager.playersEXP += 20;
　　			other.gameObject.SendMessage("EnemyDamaged",damageValue,SendMessageOptions.DontRequireReceiver);
                other.gameObject.SendMessage("TakenDamage",SendMessageOptions.DontRequireReceiver);
                
　　		}
            // damage chest on collision
            if (other.gameObject.tag == "Chest") {
                GameManager.playersEXP += 50;
　　			other.gameObject.SendMessage("ChestDamaged",damageValue,SendMessageOptions.DontRequireReceiver);
                other.gameObject.SendMessage("TakenDamage",SendMessageOptions.DontRequireReceiver);
                
　　		} 
            // handle collisions with bullets
            if (other.gameObject.tag == "Enemy Bullet") {
            Destroy(other.gameObject);
            }
            // handle collisions with bullets
            if (other.gameObject.tag == "Freeze Bullet") {
            Destroy(other.gameObject);
            }
            // handle collisions with trap
            if (other.gameObject.tag == "Trap") {
            Destroy(other.gameObject);
            }
　　	}
　　	
        

}
