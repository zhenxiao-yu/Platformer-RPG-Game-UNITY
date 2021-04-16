using System.Collections;
using System.Collections.Generic;
using UnityEngine;
　　
　　
　　
　　public class Bullet : MonoBehaviour {
        // Bullet Singleton
        private static Bullet _instance;
        // Bullet property
        public static Bullet Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<Bullet>();
                }

                return _instance;
            }
        }
        // set value of damage
　　	int damageValue = 1;
　　
　　	void OnTriggerEnter(Collider other){
        // damage enemy on collision
　　		if (other.gameObject.tag == "Enemy") {
                GameManager.playersEXP += 30;
　　			Destroy(gameObject);
　　			other.gameObject.SendMessage("EnemyDamaged",damageValue,SendMessageOptions.DontRequireReceiver);
                other.gameObject.SendMessage("TakenDamage",SendMessageOptions.DontRequireReceiver);
                
　　		}
            // damage chest on collision 
            if (other.gameObject.tag == "Chest") {
                GameManager.playersEXP += 50;
　　			Destroy(gameObject);
　　			other.gameObject.SendMessage("ChestDamaged",damageValue,SendMessageOptions.DontRequireReceiver);
                other.gameObject.SendMessage("TakenDamage",SendMessageOptions.DontRequireReceiver);
                
　　		} 

            if (other.gameObject.tag == "Flying Enmey") {
                GameManager.playersEXP += 20;
　　			Destroy(gameObject);
                Destroy(other.gameObject);
　　		} 

            // handle collisions with walls
            if (other.gameObject.tag == "Walls") {
            Destroy(gameObject);
            }
　　	}
        // Destroy Bullet once out of screen
　　	void FixedUpdate(){
　　		Destroy (gameObject, 1.25f);
　　	}
　　}