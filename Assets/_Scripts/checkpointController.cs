using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointController : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log("Triggered!");
        // get player controller from player
        PlayerController player = collider.gameObject.GetComponent<PlayerController>();
        // set current checkpoint to this object
        player.currentCheckpoint = this.gameObject;
    }
}
