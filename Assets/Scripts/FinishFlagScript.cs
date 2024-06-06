using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishFlagScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            LevelEvents.LevelFinish();
        }
    }
}
