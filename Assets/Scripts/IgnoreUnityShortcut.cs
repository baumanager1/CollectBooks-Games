using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreUnityShortcut : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isPlaying)
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Space))
            {
                // Handle the Shift + Space combination here
                Debug.Log("Shift + Space pressed in play mode");
                // Add your custom logic here

                // Optionally consume the event to prevent Unity from handling it
                Event currentEvent = Event.current;
                if (currentEvent != null)
                {
                    currentEvent.Use();
                }
            }
        }
    }
}
