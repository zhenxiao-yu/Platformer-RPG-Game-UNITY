using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Vector3 localScale;
    private float dirX;
    private bool facingRight = true;
    

    void Start()
    {
       localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal") * 2f;
    }

    private void LateUpdate(){
        if(dirX > 0){
            facingRight = true;
        }
        else if (dirX < 0){
            facingRight = false;
        }

        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0))){
            localScale.x *= -1;
        }

        transform.localScale = localScale;
    }
}
