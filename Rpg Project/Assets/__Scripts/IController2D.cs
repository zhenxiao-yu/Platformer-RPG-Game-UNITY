using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController2D
{  
    // method to customize player
    void Custom();
    // method to undo customization of player
    void UndoCustom();
    // method to pause game
    void Pause();
    // method to resume game
    void Resume();
    // method to change scene
    void ChangeScene();
    // method to decrease coin as shield is used
    void ShieldDecreaseCoin();
    // method to attack with mine
    void MineAttack();
    
} 

