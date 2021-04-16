using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinTrap : MonoBehaviour
{
   public float rotationSpeed;
   private int damageValue = 2;
   public GameManager gameManager;
   public bool clockwise;

   void Update(){
       //rotate along Z axis
       if(clockwise == true){
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
       } else {
        transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);   
       }
   }

   void OnTriggerEnter(Collider col){
			// damage player if collides with enemy
　　		if (col.gameObject.tag == "Player") {
　　			gameManager.SendMessage("PlayerDamaged",damageValue,SendMessageOptions.DontRequireReceiver);
　　			gameManager.controller2D.SendMessage("TakenDamage",SendMessageOptions.DontRequireReceiver);
　　		}
   }
}
