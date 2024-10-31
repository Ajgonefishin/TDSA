using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public float minCameraY;
    public float maxCameraY;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        // get correct y-value (we don't want to go below 0; if we fall past the camera, we have the player die)
        float cameraY = player.transform.position.y;
        if (cameraY < minCameraY) cameraY = minCameraY;
        if (cameraY > maxCameraY) cameraY = maxCameraY;
        // update camera position
        gameObject.transform.position = new Vector3(
            player.transform.position.x, 
            cameraY, 
            gameObject.transform.position.z
        );
    }

    // FixedUpdate is called a fixed amount of time every second
}
