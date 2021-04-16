using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class LevelControl : MonoBehaviour
{
    // handles automatic level change of portal
    public string levelName; 

    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Player"){
            SceneManager.LoadScene(levelName);
        }
    }
}
