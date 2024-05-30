using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookCollectionScript : MonoBehaviour
{
    private AudioSource bookCollectionSFX;
    public GameObject player;
    public CharacterControllerCollegeStudent playerScript;
    void Start()
    {
        bookCollectionSFX = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<CharacterControllerCollegeStudent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered");
        if(other.tag == "Player")
        {
           playerScript.PlaySFX(bookCollectionSFX.clip); // play the book collection sound effect on the player
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {

    }
}
