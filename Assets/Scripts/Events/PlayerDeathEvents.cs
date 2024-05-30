using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 This Class is intended for storing References to different Events relating to the Death of the player
 */
public class PlayerDeathEvents : MonoBehaviour
{
    
    public static event Action TouchingWorldBorder;

    public static void OnWorldBorderTouch()
    {
        TouchingWorldBorder?.Invoke();
    }
}
