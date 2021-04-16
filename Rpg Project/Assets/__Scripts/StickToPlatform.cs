using UnityEngine;
using System.Collections;
　　
　　public class StickToPlatform : MonoBehaviour {

        //make sure that the player sticks to platform when it lands on it 
　　	void OnTriggerStay(Collider other){
　　		if (other.tag == "Platform") {
　　			this.transform.parent = other.transform;
　　		}
　　	}
        //make sure that the platform and player have seperate tags
　　	void OnTriggerExit(Collider other){
　　		if (other.tag == "Platform") {
　　			this.transform.parent = null;
　　		}
　　	}
　　}