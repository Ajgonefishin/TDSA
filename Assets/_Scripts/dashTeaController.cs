using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashTeaController : MonoBehaviour {

    AudioSource audioSource;

    // sound effects
    public AudioClip collect;


    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        // unlock dash for the player who collides with this
        playerController.dashUnlocked = true;
        audioSource.PlayOneShot(collect);
        Destroy(this.gameObject);
    }
}
