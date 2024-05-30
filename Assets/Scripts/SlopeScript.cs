//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SlopeScript : MonoBehaviour
//{
//    public GameObject player;
//    // Start is called before the first frame update
//    void Start()
//    {
//        player = GameObject.FindWithTag("Player");
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if(other.tag == "Player" )
//        {
//            player.transform.rotation = Quaternion.Euler(0f, 0f, f);
//        }
//    }
//    private void OnTriggerExit2D(Collider2D other)
//    {
//        if(other.tag == "Player")
//        {
//            player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
//        }
//    }
//}
