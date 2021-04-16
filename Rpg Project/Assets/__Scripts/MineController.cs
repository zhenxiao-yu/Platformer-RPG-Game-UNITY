using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour
{
    // destroy after ~5 seconds of instantiation
    public Controller2D controller2D;
    void FixedUpdate(){
　　		Destroy (gameObject, 5.25f);
　　	}
}
